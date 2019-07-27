using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DriverParser.Extensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DriverParser.Data
{
    public class DbService
    {
        private const string SqlExtension = ".sql";
        private const string SchemaVersionsTable = "SchemaVersions";

        private readonly ILogger<DbService> _logger;
        private readonly DriverParserContext _context;
        private readonly string _connectionString;

        public DbService(ILoggerFactory loggerFactory, DriverParserContext context)
        {
            _logger = loggerFactory.ThrowIfNull(nameof(loggerFactory)).CreateLogger<DbService>();
            _context = context.ThrowIfNull(nameof(context));
            _connectionString = context.ThrowIfNull(nameof(context)).Database.GetDbConnection().ConnectionString;

            _logger.LogDebug("DbService created");
        }

        public void PerformDbUpdate()
        {
            _logger.LogDebug("PerformDbUpdate: checking for update, running if necessary...");
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames()
                .Where(s => s.EndsWith(SqlExtension))
                .OrderBy(s => s)
                .ToList();

            _logger.LogDebug($"PerformDbUpdate: found [{resourceNames.Count}] scripts");

            var existingScripts = GetExistingScriptNumber();
            _logger.LogDebug($"PerformDbUpdate: found [{existingScripts.Count}] existing scripts");
            foreach (var resourceName in resourceNames)
            {
                if (existingScripts.Contains(resourceName))
                {
                    _logger.LogDebug($"PerformDbUpdate: script [{resourceName}] already exists, skipping");
                    continue;
                }
            
                _logger.LogDebug($"PerformDbUpdate: script [{resourceName}] doesn't exist, running");
                // ReSharper disable once RedundantAssignment
                var script = string.Empty;
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        script = reader.ReadToEnd();
                    }
                }

                if (!string.IsNullOrWhiteSpace(script))
                {
                    var result = ExecuteScript(script);
                    _logger.LogDebug($"PerformDbUpdate: Result [{result}] from executing script [{script}]");
                    result = AddScriptToSchemaVersions(resourceName);
                    _logger.LogDebug($"PerformDbUpdate: Result [{result}] from adding script [{script}] to {SchemaVersionsTable}");
                }
            }
        }

        private IList<string> GetExistingScriptNumber()
        {
            var scripts = new List<string>();
            var tableExists = false;
            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{SchemaVersionsTable}';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tableName = reader.GetString(0);
                            if (!string.IsNullOrWhiteSpace(tableName))
                            {
                                tableExists = true;
                            }
                        }
                    }

                    if (tableExists)
                    {
                        cmd.CommandText = $"SELECT ScriptName FROM {SchemaVersionsTable} ORDER BY Id;";
                        using (var reader = cmd.ExecuteReader())
                        {
                            {
                                while (reader.Read())
                                {
                                    var script = reader.GetString(0);
                                    scripts.Add(script);
                                }
                            }
                        }
                    }
                }

                conn.Close();
            }

            return scripts;
        }

        private int ExecuteScript(string script)
        {
            int result;
            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = script;
                        result = cmd.ExecuteNonQuery();
                    }

                    trans.Commit();
                }

                conn.Close();
            }

            return result;
        }

        private int AddScriptToSchemaVersions(string scriptName)
        {
            int result;
            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO {SchemaVersionsTable} (ScriptName, Added) VALUES ('{scriptName}', '{DateTime.UtcNow:u}');";
                    result = cmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            return result;
        }
    }
}

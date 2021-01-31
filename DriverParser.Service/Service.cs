using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using DriverParser.Extensions;
using DriverParser.Model;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

namespace DriverParser.Service
{
    /// <summary>
    /// Implementation of the <see cref="IService"/> object
    /// </summary>
    public class Service : IService
    {
        /// <summary>
        /// Output header
        /// </summary>
        private const string OutputHeader = "Position,Name,Race Number,Car Model,Lap Count,Best Lap,Total Time";

        /// <summary>
        /// Output line format
        /// </summary>
        private const string OutputLine = "{0},{1},{2},{3},{4},{5},{6}";

        /// <summary>
        /// Dictionary to map car model numbers to actual car models
        /// </summary>
        private readonly IDictionary<int, string> _cars = new Dictionary<int, string>
        {
            { 0, "Porsche 991 GT3" },
            { 1, "Mercedes AMG GT3" },
            { 2, "Ferrari 488 GT3" },
            { 3, "Audi R8 LMS" },
            { 4, "Lamborghini Huracan GT3" },
            { 5, "Mclaren 650s GT3" },
            { 6, "Nissan GT R Nismo GT3 2018" },
            { 7, "BMW M6 GT3" },
            { 8, "Bently Continental GT3 2018" },
            { 9, "Porsche 991.2 GT3 Cup" },
            { 10, "Nissan GT-R Nismo GT3 2017" },
            { 11, "Bently Continental GT3 2016" },
            { 12, "Aston Martin Vantage V12 GT" },
            { 13, "Lamborghini Gallardo R-EX" },
            { 14, "Jaguar G3" },
            { 15, "Lexus RC F GT3" },
            { 16, "/" },
            { 17, "Honda NSX GT3" },
            { 18, "Lamborghini Huracan SuperTr" },
        };

        private readonly ILogger<Service> _logger;
        private readonly string _inputPath;
        private readonly string _outputPath;
        private readonly IList<FinalResult> _finalResults;

        /// <summary>
        /// Result header object from the input file, deserialized JSON object
        /// </summary>
        private Result _result;

        /// <summary>
        /// Current status message
        /// </summary>
        public string StatusMessage { get; set; }

        public Service(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory
                .ThrowIfNull(nameof(loggerFactory))
                .CreateLogger<Service>()
                .ThrowIfNull(nameof(_logger));
            _result = null;
            _finalResults = new List<FinalResult>();
            StatusMessage = string.Empty;

            _logger.LogDebug("Service created");
        }

        /// <summary>
        /// Constructor - Initializes the <see cref="IService"/> object
        /// </summary>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> logger factory - creates a <see cref="ILogger"/> object</param>
        /// <param name="inputPath"><see cref="string"/> of the input file path</param>
        /// <param name="outputPath"><see cref="string"/> of the output file path</param>
        public Service(ILoggerFactory loggerFactory, string inputPath, string outputPath)
        {
            _logger = loggerFactory
                .ThrowIfNull(nameof(loggerFactory))
                .CreateLogger<Service>()
                .ThrowIfNull(nameof(_logger));
            _inputPath = inputPath.ThrowIfNullOrEmptyOrWhitespace(inputPath);
            _outputPath = outputPath.ThrowIfNullOrEmptyOrWhitespace(outputPath);
            _result = null;
            _finalResults = new List<FinalResult>();
            StatusMessage = string.Empty;

            _logger.LogDebug("Service with strings created");
        }

        /// <summary>
        /// Constructor - Initialized the <see cref="IService"/> object
        /// </summary>
        /// <param name="loggerFactory">Logger factory - creates a <see cref="ILogger"/> object</param>
        /// <param name="settings">Settings from the config file</param>
        public Service(ILoggerFactory loggerFactory, IOptions<DriverParserSettings> settings)
            : this(loggerFactory, settings.ThrowIfNull(nameof(settings)).Value.ThrowIfNull(nameof(settings.Value)).InputPath, settings.ThrowIfNull(nameof(settings)).Value.ThrowIfNull(nameof(settings.Value)).OutputPath)
        {
            _logger.LogDebug("Service with options created");
        }

        /// <summary>
        /// Parses the input file to the <see cref="Result"/> object
        ///
        /// Deserializes the JSON file
        /// </summary>
        public void ParseFile()
        {
            _logger.LogDebug("ParseFile: Parsing file");
            try
            {
                ParseFile(_inputPath);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ParseFile: Error parsing file [{ex}]", ex);
                StatusMessage = $"Error parsing file [{ex.Message}]";
            }
        }

        public void ParseFile(string fileName)
        {
            _logger.LogDebug($"ParseFile: Parsing file [{fileName}]");
            try
            {
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    _logger.LogError("ParseFile: File name is blank or empty.  Cannot continue.");
                    StatusMessage = "Filename is blank or empty.  Cannot continue.";
                    return;
                }

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                _logger.LogDebug($"ParseFile: Opening file [{filePath}], assuming ASCII encoding");
                var sb = new StringBuilder();
                using (var sr1 = new StreamReader(filePath, Encoding.Unicode, true))
                {
                    _logger.LogDebug($"ParseFile: Stream created, reading lines");
                    string line;
                    while ((line = sr1.ReadLine()) != null)
                    {
                        sb.AppendLine(line);
                    }

                    var file = sb.ToString();

                    _logger.LogDebug($"ParseFile: got all lines, deserializing file, [{sb.Length}] lines");
                    _result = JsonConvert.DeserializeObject<Result>(file);
                }

                if (_result != null)
                {
                    _logger.LogDebug(
                        "ParseFile: successfully deserialized file, attempting to convert to an entity so it can be saved");
                    StatusMessage = "Successfully parsed file.";
                }
                else
                {
                    _logger.LogWarning("ParseFile: unable to deserialize file");
                    StatusMessage = "Unable to parse file";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"ParseFile: Error parsing file [{ex}]", ex);
                StatusMessage = $"Error parsing file [{ex.Message}]";
            }
        }

        /// <summary>
        /// Computes the Results - orders by best lap time
        /// </summary>
        public void ComputeResults()
        {
            _logger.LogDebug("Computing results");

            if (_result == null)
            {
                _logger.LogWarning("ComputeResults: results object is null, cannot computer");
                StatusMessage = "Unable to compute results";
                return;
            }

            //var racers = _result.LeaderBoardLines.OrderBy(t => t.Timing.BestLap).ToList();
            var racers = _result.LeaderBoardLines;

            var racersLength = racers.Count;
            for (var i = 0; i < racersLength; i++)
            {
                var racer = racers[i];
                var finalResult = new FinalResult
                {
                    Position = i,
                    Name = $"{racer.CurrentDriver.FirstName.Trim()} {racer.CurrentDriver.LastName.Trim()}",
                    RaceNumber = racer.Car.RaceNumber,
                    CarModel = GetCarModel(racer.Car.CarModel),
                    LapCount = racer.Timing.LapCount,
                    BestLap = TimeSpan.FromMilliseconds(racer.Timing.BestLap).ToString(),
                    TotalTime = racer.Timing.TotalTime
                };

                _logger.LogDebug($"ComputeResults: finalResult[{finalResult}]");
                _finalResults.Add(finalResult);
            }

            StatusMessage = "Successfully ordered";
        }

        /// <summary>
        /// Outputs the results to the specified output file in settings
        /// </summary>
        public void WriteResults()
        {
            _logger.LogDebug($"WriteResults: outputting [{_finalResults.Count}] final results");

            if (File.Exists(_outputPath))
            {
                File.Delete(_outputPath);
            }

            using (var tw = new StreamWriter(_outputPath))
            {
                tw.WriteLine(OutputHeader);
                foreach (var result in _finalResults)
                {
                    var line = string.Format(OutputLine, result.Position, result.Name, result.RaceNumber,
                        result.CarModel, result.LapCount, result.BestLap, result.TotalTime);
                    tw.WriteLine(line);
                }
            }

            _logger.LogDebug("WriteResults: done outputting results!");
            StatusMessage = "Successfully written output";
        }

        /// <summary>
        /// Returns the results as a <see cref="string"/>
        /// </summary>
        /// <returns><see cref="string"/> of the results</returns>
        public string OutputResults()
        {
            _logger.LogDebug($"OutputResults: returning [{_finalResults.Count}] final results");

            var sb = new StringBuilder();
            foreach (var result in _finalResults)
            {
                var line = string.Format(OutputLine, result.Position, result.Name, result.RaceNumber,
                    result.CarModel, result.LapCount, result.BestLap, result.TotalTime);
                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets the Car Model - turns car model number to the actual car model
        /// </summary>
        /// <param name="carId">Car Model Number</param>
        /// <returns>Car Model Name</returns>
        private string GetCarModel(int carId)
        {
            _logger.LogDebug($"GetCarModel: getting car model for carId [{carId}]");
            var result = string.Empty;

            if (_cars.ContainsKey(carId))
            {
                result = _cars[carId];
            }

            _logger.LogDebug($"GetCarId: returning car model [{result}]");

            return result;
        }
    }
}

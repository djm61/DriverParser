using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverParser.Extensions;
using DriverParser.Model;
using DriverParser.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DriverParser.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService _service;

        public HomeController(ILoggerFactory loggerFactory, IService service)
        {
            _logger = loggerFactory
                .ThrowIfNull(nameof(loggerFactory))
                .CreateLogger<HomeController>()
                .ThrowIfNull(nameof(_logger));
            _service = service.ThrowIfNull(nameof(service));

            _logger.LogDebug("HomeController created");
        }

        public IActionResult Index()
        {
            _logger.LogDebug("Index");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            _logger.LogDebug($"UploadFile: file count[{files.Count}]");

            string filePath = string.Empty;
            try
            {
                var sb = new StringBuilder();
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        // full path to file in temp location
                        filePath = Path.GetTempFileName();

                        using (var fStream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(fStream);
                        }

                        _service.ParseFile(filePath);
                        _service.ComputeResults();
                        var results = _service.OutputResults();
                        sb.AppendLine(results);
                    }
                }

                var fileName = $"{DateTime.Now:u}_race_results.csv";
                return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error reading and computing file [{ex.GetExceptionMessage()}]", ex);
                return BadRequest(ex.Message);
            }
            finally
            {
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
            }
        }
    }
}
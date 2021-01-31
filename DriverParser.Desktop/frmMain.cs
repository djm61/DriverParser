using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using DriverParser.Extensions;
using DriverParser.Service;

using Microsoft.Extensions.Logging;

namespace DriverParser.Desktop
{
    public partial class frmMain : Form
    {
        private readonly ILogger<frmMain> _logger;
        private readonly IService _service;

        private BackgroundWorker _worker;
        private string _fileName;
        private frmProgress _frmProgress;

        public frmMain(ILogger<frmMain> logger, IService service)
        {
            _logger = logger.ThrowIfNull(nameof(logger));
            _service = service.ThrowIfNull(nameof(service));

            InitializeComponent();

            _fileName = string.Empty;
            _frmProgress = new frmProgress();

            _logger.LogDebug("frmMain created");
        }

        private void btnRaceResults_Click(object sender, EventArgs e)
        {
            _logger.LogDebug("btnRaceResults_Click()");
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Race Results";
                    //ofd.Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|All Files (*.*)|*.*";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        _fileName = ofd.FileName;
                        btnRun.Enabled = true;
                    }
                    else
                    {
                        btnRun.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"brnRaceResults_Click() | error[{ex}]", ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            _logger.LogDebug("btnRun_Click()");
            try
            {
                _worker = new BackgroundWorker();
                _worker.DoWork += _worker_DoWork;
                _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;
                _worker.RunWorkerAsync();
                _frmProgress.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.LogError($"btnRun_Click() | error [{ex}]", ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("The task has been cancelled");
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Error. Details: " + (e.Error as Exception).ToString());
            }
            else
            {
                MessageBox.Show("The task has been completed. Results: " + e.Result.ToString());
            }

            _frmProgress.Close();
            btnRun.Enabled = false;
            _fileName = string.Empty;
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _logger.LogDebug("_worker_DoWork()");
            var stopwatch = Stopwatch.StartNew();

            try
            {
                var fi = new FileInfo(_fileName);
                var filePath = fi.DirectoryName;
                string results = string.Empty;
                if (File.Exists(_fileName))
                {
                    _service.ParseFile(_fileName);
                    _service.ComputeResults();
                    results = _service.OutputResults();
                }

                var fileName = $"{DateTime.Now:u}_race_results.csv";
                fileName = fileName.Replace(":", "_");
                fileName = Path.Combine(filePath, fileName);

                using (var sr = new StreamWriter(fileName))
                {
                    sr.Write(results);
                }

                e.Result = $"Successfully wrote to file {fileName}";
                _logger.LogDebug($"_worker_DoWork() | successfully wrote to file [{fileName}]");
            }
            catch (Exception ex)
            {
                _logger.LogError($"_worker_DoWork error [{ex}]", ex);
                e.Result = ex.Message;
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation($"*** _worker_DoWork took [{stopwatch.Elapsed}] ***");
            }
        }
    }
}

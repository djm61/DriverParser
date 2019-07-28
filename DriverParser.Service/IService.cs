namespace DriverParser.Service
{
    /// <summary>
    /// Interface for the <see cref="Service"/> object
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Current status message
        /// </summary>
        string StatusMessage { get; set; }

        /// <summary>
        /// Parses the input file from the settings file
        /// </summary>
        void ParseFile();

        /// <summary>
        /// Computes the results from the input data
        /// Currently it's just sorted by the fastest lap
        /// </summary>
        void ComputeResults();

        /// <summary>
        /// Outputs the sorted/ordered results to the output file (specified in the settings file)
        /// </summary>
        void OutputResults();
    }
}

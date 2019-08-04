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
        /// Parses the input file from the provide file name
        /// </summary>
        /// <param name="fileName"><see cref="string"/> file name to parse</param>
        void ParseFile(string fileName);

        /// <summary>
        /// Computes the results from the input data
        /// Currently it's just sorted by the fastest lap
        /// </summary>
        void ComputeResults();

        /// <summary>
        /// Writes the sorted/ordered results to the output file (specified in the settings file)
        /// </summary>
        void WriteResults();

        /// <summary>
        /// Returns the sorted/ordered results as a <see cref="string"/> to display on the screen
        /// </summary>
        /// <returns><see cref="string"/> of the sorted/ordered results</returns>
        string OutputResults();
    }
}

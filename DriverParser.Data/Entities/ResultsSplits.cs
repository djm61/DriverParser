namespace DriverParser.Data.Entities
{
    public partial class ResultsSplits
    {
        public long ResultId { get; set; }
        public long SplitsId { get; set; }

        public virtual Result Result { get; set; }
        public virtual Splits Splits { get; set; }
    }
}

namespace DriverParser.Data.Entities
{
    public partial class SplitsTimings
    {
        public long SplitsId { get; set; }
        public long TimingId { get; set; }

        public virtual Splits Splits { get; set; }
        public virtual Timing Timing { get; set; }
    }
}

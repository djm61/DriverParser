using System.Collections.Generic;

namespace DriverParser.Data.Entities
{
    public partial class Timing : EntityBase
    {
        public Timing()
        {
            LastSplits = new HashSet<Splits>();
            BestSplits = new HashSet<Splits>();
        }

        public long LastLap { get; set; }
        public ICollection<Splits> LastSplits { get; set; }
        public long BestLap { get; set; }
        public ICollection<Splits> BestSplits { get; set; }
        public long TotalTime { get; set; }
        public long LapCount { get; set; }
        public long LastSplitId { get; set; }

        public override string ToString()
        {
            return
                $"LastLap[{LastLap}], BestLap[{BestLap}], TotalTime[{TotalTime}], LapCount[{LapCount}], LastSplitsCount[{LastSplits.Count}], BestSplitsCount[{BestSplits.Count}], {base.ToString()}";
        }
    }
}

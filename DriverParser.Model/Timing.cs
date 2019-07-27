using System.Collections.Generic;

namespace DriverParser.Model
{
    public class Timing
    {
        public Timing()
        {
            LastSplits = new List<long>();
            BestSplits = new List<long>();
        }

        public long LastLap { get; set; }
        public List<long> LastSplits { get; set; }
        public long BestLap { get; set; }
        public List<long> BestSplits { get; set; }
        public long TotalTime { get; set; }
        public long LapCount { get; set; }
        public long LastSplitId { get; set; }

        public override string ToString()
        {
            return $"LastLap[{LastLap}], LastSplitsCount[{LastSplits.Count}], BestLap[{BestLap}], BestSplitsCount[{BestSplits.Count}], TotalTime[{TotalTime}], LapCount[{LapCount}], LastSplitId[{LastSplitId}], {base.ToString()}";
        }
    }
}

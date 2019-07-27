using System.Collections.Generic;

namespace DriverParser.Data.Entities
{
    public partial class Timing : EntityBase
    {
        public Timing()
        {
            LeaderBoardLine = new HashSet<LeaderBoardLine>();
            SplitsTimings = new HashSet<SplitsTimings>();
        }

        public long LastLap { get; set; }
        public long BestLap { get; set; }
        public long TotalTime { get; set; }
        public long LapCount { get; set; }
        public long LastSplitId { get; set; }

        public virtual ICollection<LeaderBoardLine> LeaderBoardLine { get; set; }
        public virtual ICollection<SplitsTimings> SplitsTimings { get; set; }

        public override string ToString()
        {
            return
                $"LastLap[{LastLap}], BestLap[{BestLap}], TotalTime[{TotalTime}], LapCount[{LapCount}], {base.ToString()}";
        }
    }
}

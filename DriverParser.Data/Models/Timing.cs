using System;
using System.Collections.Generic;

namespace DriverParser.Data.Models
{
    public partial class Timing
    {
        public Timing()
        {
            LeaderBoardLine = new HashSet<LeaderBoardLine>();
        }

        public long Id { get; set; }
        public long LastLap { get; set; }
        public long LastSplitsId { get; set; }
        public long BestLap { get; set; }
        public long BestSplitsId { get; set; }
        public long TotalTime { get; set; }
        public long LapCount { get; set; }
        public long LastSplitId { get; set; }

        public virtual Splits BestSplits { get; set; }
        public virtual Splits LastSplits { get; set; }
        public virtual ICollection<LeaderBoardLine> LeaderBoardLine { get; set; }
    }
}

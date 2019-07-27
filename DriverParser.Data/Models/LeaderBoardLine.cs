using System;
using System.Collections.Generic;

namespace DriverParser.Data.Models
{
    public partial class LeaderBoardLine
    {
        public LeaderBoardLine()
        {
            ResultsLeaderBoardLines = new HashSet<ResultsLeaderBoardLines>();
        }

        public long Id { get; set; }
        public long CarId { get; set; }
        public long CurrentDriverId { get; set; }
        public long CurrentDriverIndex { get; set; }
        public long TimingId { get; set; }

        public virtual Car Car { get; set; }
        public virtual Driver CurrentDriver { get; set; }
        public virtual Timing Timing { get; set; }
        public virtual ICollection<ResultsLeaderBoardLines> ResultsLeaderBoardLines { get; set; }
    }
}

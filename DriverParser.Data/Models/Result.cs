using System;
using System.Collections.Generic;

namespace DriverParser.Data.Models
{
    public partial class Result
    {
        public Result()
        {
            ResultsLeaderBoardLines = new HashSet<ResultsLeaderBoardLines>();
        }

        public long Id { get; set; }
        public long BestLap { get; set; }
        public long BestSplitsId { get; set; }
        public string IsWetSession { get; set; }
        public long Type { get; set; }

        public virtual Splits BestSplits { get; set; }
        public virtual ICollection<ResultsLeaderBoardLines> ResultsLeaderBoardLines { get; set; }
    }
}

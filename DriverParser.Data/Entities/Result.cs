using System.Collections.Generic;

namespace DriverParser.Data.Entities
{
    public partial class Result : EntityBase
    {
        public Result()
        {
            ResultsLeaderBoardLines = new HashSet<ResultsLeaderBoardLines>();
            ResultsSplits = new HashSet<ResultsSplits>();
        }

        public long BestLap { get; set; }
        public long BestSplitsId { get; set; }
        public string IsWetSession { get; set; }
        public long Type { get; set; }

        public virtual Splits BestSplits { get; set; }
        public virtual ICollection<ResultsLeaderBoardLines> ResultsLeaderBoardLines { get; set; }
        public virtual ICollection<ResultsSplits> ResultsSplits { get; set; }

        public override string ToString()
        {
            return $"BestLap[{BestLap}], Wet[{IsWetSession}], Type[{Type}], ResultsLeaderBoardCount[{ResultsLeaderBoardLines.Count}], {base.ToString()}";
        }
    }
}

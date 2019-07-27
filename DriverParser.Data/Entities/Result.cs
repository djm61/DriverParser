using System.Collections.Generic;

namespace DriverParser.Data.Entities
{
    public partial class Result : EntityBase
    {
        public Result()
        {
            BestSplits = new HashSet<Splits>();
            LeaderBoardLines = new HashSet<LeaderBoardLine>();
        }

        public long BestLap { get; set; }
        public ICollection<Splits> BestSplits { get; set; }
        public bool IsWetSession { get; set; }
        public long Type { get; set; }
        public ICollection<LeaderBoardLine> LeaderBoardLines { get; set; }

        public override string ToString()
        {
            return $"BestLap[{BestLap}], Wet[{IsWetSession}], Type[{Type}], LeaderBoardCount[{LeaderBoardLines.Count}], {base.ToString()}";
        }
    }
}

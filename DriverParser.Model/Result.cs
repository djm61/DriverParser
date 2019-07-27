using System.Collections.Generic;

namespace DriverParser.Model
{
    public class Result
    {
        public Result()
        {
            BestSplits = new List<long>();
            LeaderBoardLines = new List<LeaderBoardLine>();
        }

        public long BestLap { get; set; }
        public List<long> BestSplits { get; set; }
        public bool IsWetSession { get; set; }
        public long Type { get; set; }
        public List<LeaderBoardLine> LeaderBoardLines { get; set; }

        public override string ToString()
        {
            return $"BestLap[{BestLap}], BestSplitsCount[{BestSplits.Count}], IsWetSession[{IsWetSession}], Type[{Type}], LeaderBoardLines[{LeaderBoardLines.Count}], {base.ToString()}";
        }
    }
}

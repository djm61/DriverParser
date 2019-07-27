using System.Collections.Generic;
using DriverParser.Data.Models;

namespace DriverParser.Data.Entities
{
    public partial class LeaderBoardLine : EntityBase
    {
        public LeaderBoardLine()
        {
            ResultsLeaderBoardLines = new HashSet<ResultsLeaderBoardLines>();
        }

        public long CarId { get; set; }
        public long CurrentDriverId { get; set; }
        public long CurrentDriverIndex { get; set; }
        public long TimingId { get; set; }

        public virtual Car Car { get; set; }
        public virtual Driver CurrentDriver { get; set; }
        public virtual Timing Timing { get; set; }
        public virtual ICollection<ResultsLeaderBoardLines> ResultsLeaderBoardLines { get; set; }

        public override string ToString()
        {
            return $"Car[{Car}], CurrentDriver[{CurrentDriver}], Timing[{Timing}], {base.ToString()}";
        }
    }
}

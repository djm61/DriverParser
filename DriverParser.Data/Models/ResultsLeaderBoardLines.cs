using System;
using System.Collections.Generic;

namespace DriverParser.Data.Models
{
    public partial class ResultsLeaderBoardLines
    {
        public long ResultId { get; set; }
        public long LeaderBoardLineId { get; set; }

        public virtual LeaderBoardLine LeaderBoardLine { get; set; }
        public virtual Result Result { get; set; }
    }
}

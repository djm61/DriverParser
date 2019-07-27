using System;
using System.Collections.Generic;

namespace DriverParser.Data.Models
{
    public partial class Car
    {
        public Car()
        {
            CarDriver = new HashSet<CarDriver>();
            LeaderBoardLine = new HashSet<LeaderBoardLine>();
        }

        public long Id { get; set; }
        public long CarId { get; set; }
        public long RaceNumber { get; set; }
        public long CarModel { get; set; }
        public long CupCategory { get; set; }
        public string TeamName { get; set; }
        public long CurrentDriverId { get; set; }

        public virtual Driver CurrentDriver { get; set; }
        public virtual ICollection<CarDriver> CarDriver { get; set; }
        public virtual ICollection<LeaderBoardLine> LeaderBoardLine { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace DriverParser.Data.Models
{
    public partial class Driver
    {
        public Driver()
        {
            Car = new HashSet<Car>();
            CarDriver = new HashSet<CarDriver>();
            LeaderBoardLine = new HashSet<LeaderBoardLine>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShortName { get; set; }
        public string PlayerId { get; set; }

        public virtual ICollection<Car> Car { get; set; }
        public virtual ICollection<CarDriver> CarDriver { get; set; }
        public virtual ICollection<LeaderBoardLine> LeaderBoardLine { get; set; }
    }
}

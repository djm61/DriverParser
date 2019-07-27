using System.Collections.Generic;

namespace DriverParser.Data.Entities
{
    public partial class Driver : EntityBase
    {
        public Driver()
        {
            Car = new HashSet<Car>();
            CarDriver = new HashSet<CarDriver>();
            LeaderBoardLine = new HashSet<LeaderBoardLine>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShortName { get; set; }
        public string PlayerId { get; set; }

        public virtual ICollection<Car> Car { get; set; }
        public virtual ICollection<CarDriver> CarDriver { get; set; }
        public virtual ICollection<LeaderBoardLine> LeaderBoardLine { get; set; }

        public override string ToString()
        {
            return $"Name[{FirstName} {LastName}], PlayerId[{PlayerId}], {base.ToString()}";
        }
    }
}

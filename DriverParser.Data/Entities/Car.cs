using System.Collections.Generic;

namespace DriverParser.Data.Entities
{
    public partial class Car : EntityBase
    {
        public Car()
        {
            CarDriver = new HashSet<CarDriver>();
            LeaderBoardLine = new HashSet<LeaderBoardLine>();
        }

        public long CarId { get; set; }
        public long RaceNumber { get; set; }
        public long CarModel { get; set; }
        public long CupCategory { get; set; }
        public string TeamName { get; set; }
        public long CurrentDriverId { get; set; }

        public virtual Driver CurrentDriver { get; set; }
        public virtual ICollection<CarDriver> CarDriver { get; set; }
        public virtual ICollection<LeaderBoardLine> LeaderBoardLine { get; set; }

        public override string ToString()
        {
            return
                $"CarId[{CarId}], Race[{RaceNumber}], CarModel[{CarModel}], CupCategory[{CupCategory}], TeamName[{TeamName}], {base.ToString()}";
        }
    }
}

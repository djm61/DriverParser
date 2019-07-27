using System.Collections.Generic;

namespace DriverParser.Model
{
    public class Car
    {
        public Car()
        {
            Drivers = new List<Driver>();
        }

        public long Id { get; set; }
        public long CarId { get; set; }
        public long RaceNumber { get; set; }
        public long CarModel { get; set; }
        public long CupCategory { get; set; }
        public string TeamName { get; set; }
        public List<Driver> Drivers { get; set; }

        public override string ToString()
        {
            return $"CarId[{CarId}], Race[{RaceNumber}], Model[{CarModel}], CupCategory[{CupCategory}], TeamName[{TeamName}], DriverCount[{Drivers.Count}], {base.ToString()}";
        }
    }
}

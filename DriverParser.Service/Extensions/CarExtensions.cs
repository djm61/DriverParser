namespace DriverParser.Service.Extensions
{
    public static class CarExtensions
    {
        public static Data.Entities.Car ConvertToEntity(this Model.Car model)
        {
            var car = new Data.Entities.Car();
            car.CarId = model.CarId;
            car.RaceNumber = model.RaceNumber;
            car.CarModel = model.CarModel;
            car.CupCategory = model.CupCategory;
            car.TeamName = model.TeamName;
            //car.Drivers = model.Drivers.ConvertToEntity();

            return car;
        }
    }
}

using Microsoft.EntityFrameworkCore.Internal;

namespace DriverParser.Service.Extensions
{
    public static class CarExtensions
    {
        public static Data.Entities.Car ConvertToEntity(this Model.Car model)
        {
            var car = new Data.Entities.Car
            {
                CarId = model.CarId,
                RaceNumber = model.RaceNumber,
                CarModel = model.CarModel,
                CupCategory = model.CupCategory,
                TeamName = model.TeamName
            };

            if (model.Drivers != null && model.Drivers.Any())
            {
                foreach (var modelDriver in model.Drivers)
                {
                    var driver = modelDriver.ConvertToEntity();
                    var carDriver = new Data.Entities.CarDriver
                    {
                        CarId = car.Id,
                        DriverId = driver.Id,
                        Driver = driver
                    };

                    car.CarDriver.Add(carDriver);
                }
            }

            return car;
        }
    }
}

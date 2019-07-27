using System.Collections.Generic;
using System.Linq;
using Remotion.Linq.Clauses.ResultOperators;

namespace DriverParser.Service.Extensions
{
    public static class LeaderBoardLineExtensions
    {
        public static Data.Entities.LeaderBoardLine ConvertToEntity(this Model.LeaderBoardLine model)
        {
            var car = model.Car.ConvertToEntity();
            var currentDriver = model.CurrentDriver.ConvertToEntity();
            var timing = model.Timing.ConvertToEntity();
            
            var line = new Data.Entities.LeaderBoardLine
            {
                CarId = car.Id,
                Car = car,
                CurrentDriverId = currentDriver.Id,
                CurrentDriver = currentDriver,
                CurrentDriverIndex = model.CurrentDriverIndex,
                Timing = model.Timing.ConvertToEntity()
            };

            return line;
        }

        public static IList<Data.Entities.LeaderBoardLine> ConvertToEntity(this IEnumerable<Model.LeaderBoardLine> models)
        {
            var lines = models.Select(l => l.ConvertToEntity()).ToList();
            return lines;
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace DriverParser.Service.Extensions
{
    public static class LeaderBoardLineExtensions
    {
        public static Data.Entities.LeaderBoardLine ConvertToEntity(this Model.LeaderBoardLine model)
        {
            var line = new Data.Entities.LeaderBoardLine
            {
                Car = model.Car.ConvertToEntity(),
                CurrentDriver = model.CurrentDriver.ConvertToEntity(),
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

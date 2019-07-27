using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DriverParser.Service.Extensions
{
    public static class DriverExtensions
    {
        public static Data.Entities.Driver ConvertToEntity(this Model.Driver model)
        {
            var driver = new Data.Entities.Driver
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ShortName = model.ShortName,
                PlayerId = model.PlayerId
            };

            return driver;
        }

        public static IList<Data.Entities.Driver> ConvertToEntity(this IList<Model.Driver> models)
        {
            var drivers = models.Select(m => m.ConvertToEntity()).ToList();
            return drivers;
        }
    }
}

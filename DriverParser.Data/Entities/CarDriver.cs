using System;
using System.Collections.Generic;

namespace DriverParser.Data.Entities
{
    public partial class CarDriver
    {
        public long CarId { get; set; }
        public long DriverId { get; set; }

        public virtual Car Car { get; set; }
        public virtual Driver Driver { get; set; }
    }
}

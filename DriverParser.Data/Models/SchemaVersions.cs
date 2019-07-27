using System;
using System.Collections.Generic;

namespace DriverParser.Data.Models
{
    public partial class SchemaVersions
    {
        public long Id { get; set; }
        public string ScriptName { get; set; }
        public string Added { get; set; }
    }
}

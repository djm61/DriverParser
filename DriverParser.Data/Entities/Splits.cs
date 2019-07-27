using System.Collections.Generic;

namespace DriverParser.Data.Entities
{
    public partial class Splits : EntityBase
    {
        public Splits()
        {
            ResultsSplits = new HashSet<ResultsSplits>();
            SplitsTimings = new HashSet<SplitsTimings>();
        }

        public long Value { get; set; }

        public virtual ICollection<ResultsSplits> ResultsSplits { get; set; }
        public virtual ICollection<SplitsTimings> SplitsTimings { get; set; }

        public override string ToString()
        {
            return $"Value[{Value}], {base.ToString()}";
        }
    }
}

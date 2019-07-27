using System.Collections.Generic;

namespace DriverParser.Data.Entities
{
    public partial class Splits : EntityBase
    {
        public Splits()
        {
            Result = new HashSet<Result>();
            TimingBestSplits = new HashSet<Timing>();
            TimingLastSplits = new HashSet<Timing>();
        }

        public long Value { get; set; }

        public virtual ICollection<Result> Result { get; set; }
        public virtual ICollection<Timing> TimingBestSplits { get; set; }
        public virtual ICollection<Timing> TimingLastSplits { get; set; }

        public override string ToString()
        {
            return $"Value[{Value}], {base.ToString()}";
        }
    }
}

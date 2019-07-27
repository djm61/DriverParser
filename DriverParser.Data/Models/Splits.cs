using System;
using System.Collections.Generic;

namespace DriverParser.Data.Models
{
    public partial class Splits
    {
        public Splits()
        {
            Result = new HashSet<Result>();
            TimingBestSplits = new HashSet<Timing>();
            TimingLastSplits = new HashSet<Timing>();
        }

        public long Id { get; set; }
        public long Value { get; set; }

        public virtual ICollection<Result> Result { get; set; }
        public virtual ICollection<Timing> TimingBestSplits { get; set; }
        public virtual ICollection<Timing> TimingLastSplits { get; set; }
    }
}

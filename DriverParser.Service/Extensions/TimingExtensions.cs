namespace DriverParser.Service.Extensions
{
    public static class TimingExtensions
    {
        public static Data.Entities.Timing ConvertToEntity(this Model.Timing model)
        {
            var timing = new Data.Entities.Timing
            {
                LastLap = model.LastLap,
                BestLap = model.BestLap,
                TotalTime = model.TotalTime,
                LapCount = model.LapCount,
                LastSplitId = model.LastSplitId
            };

            foreach (var last in model.LastSplits)
            {
                var split = new Data.Entities.Splits { Value = last };
                var splitsTimings = new Data.Entities.SplitsTimings
                {
                    TimingId = timing.Id
                    , SplitsId = split.Id
                    , Splits = split
                };

                timing.SplitsTimings.Add(splitsTimings);
            }

            foreach (var best in model.BestSplits)
            {
                var split = new Data.Entities.Splits { Value = best };
                var splitsTimings = new Data.Entities.SplitsTimings
                {
                    TimingId = timing.Id,
                    SplitsId = split.Id,
                    Splits = split
                };

                //todo what now?  which property?
            }

            return timing;
        }
    }
}

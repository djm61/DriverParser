namespace DriverParser.Service.Extensions
{
    public static class ResultExtensions
    {
        public static Data.Entities.Result ConvertToEntity(this Model.Result model)
        {
            var result = new Data.Entities.Result
            {
                BestLap = model.BestLap,
                IsWetSession = model.IsWetSession,
                Type = model.Type,
                LeaderBoardLines = model.LeaderBoardLines.ConvertToEntity()
            };

            foreach (var best in model.BestSplits)
            {
                var split = new Data.Entities.Splits { Value = best };
                result.BestSplits.Add(split);
            }
            
            return result;
        }
    }
}

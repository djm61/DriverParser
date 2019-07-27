namespace DriverParser.Service.Extensions
{
    public static class ResultExtensions
    {
        public static Data.Entities.Result ConvertToEntity(this Model.Result model)
        {
            var test = model.IsWetSession.ToString();
            var result = new Data.Entities.Result
            {
                BestLap = model.BestLap,
                IsWetSession = model.IsWetSession.ToString(),
                Type = model.Type,
            };

            foreach (var best in model.BestSplits)
            {
                var split = new Data.Entities.Splits { Value = best };
                var resultSplits = new Data.Entities.ResultsSplits
                {
                    ResultId = result.Id,
                    SplitsId = split.Id,
                    Splits = split
                };

                result.ResultsSplits.Add(resultSplits);
            }

            foreach (var leaderBoardLine in model.LeaderBoardLines)
            {
                var entityLeaderBoardLine = leaderBoardLine.ConvertToEntity();
                var resultLeaderBoardLine = new Data.Entities.ResultsLeaderBoardLines
                {
                    ResultId = result.Id,
                    LeaderBoardLineId = entityLeaderBoardLine.Id,
                    LeaderBoardLine = entityLeaderBoardLine
                };

                result.ResultsLeaderBoardLines.Add(resultLeaderBoardLine);
            }

            return result;
        }
    }
}

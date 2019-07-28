namespace DriverParser.Service.Extensions
{
    public static class LeaderBoardLineExtensions
    {
        public static Data.Entities.LeaderBoardLine ConvertToEntity(this Model.LeaderBoardLine model)
        {
            var car = model.Car.ConvertToEntity();
            var currentDriver = model.CurrentDriver.ConvertToEntity();
            var timing = model.Timing.ConvertToEntity();
            
            var line = new Data.Entities.LeaderBoardLine
            {
                CarId = car.Id,
                Car = car,
                CurrentDriverId = currentDriver.Id,
                CurrentDriver = currentDriver,
                CurrentDriverIndex = model.CurrentDriverIndex,
                TimingId = timing.Id,
                Timing = timing
            };

            return line;
        }
    }
}

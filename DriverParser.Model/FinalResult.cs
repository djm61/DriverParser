namespace DriverParser.Model
{
    public class FinalResult
    {
        public long Position { get; set; }
        public string Name { get; set; }
        public long RaceNumber { get; set; }
        public string CarModel { get; set; }
        public long LapCount { get; set; }
        public string BestLap { get; set; }
        public long TotalTime { get; set; }

        public override string ToString()
        {
            return $"Position[{Position}], Name[{Name}], Race[{RaceNumber}], Car[{CarModel}], Lap[{LapCount}], BestLap[{BestLap}], Total[{TotalTime}], {base.ToString()}";
        }
    }
}

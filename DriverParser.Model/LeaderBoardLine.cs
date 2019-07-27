namespace DriverParser.Model
{
    public class LeaderBoardLine
    {
        public LeaderBoardLine()
        {
            Car = new Car();
            CurrentDriver = new Driver();
            Timing = new Timing();
        }

        public Car Car { get; set; }
        public Driver CurrentDriver { get; set; }
        public long CurrentDriverIndex { get; set; }
        public Timing Timing { get; set; }

        public override string ToString()
        {
            return $"Car[{Car}], CurrentDriver[{CurrentDriver}], Index[{CurrentDriverIndex}], Timing[{Timing}], {base.ToString()}";
        }
    }
}

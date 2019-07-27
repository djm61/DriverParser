namespace DriverParser.Data.Entities
{
    public partial class LeaderBoardLine : EntityBase
    {
        public LeaderBoardLine()
        {
            Car = new Car();
            CurrentDriver = new Driver();
            Timing = new Timing();
        }

        public long CarId { get; set; }
        public long DriverId { get; set; }
        public long TimingId { get; set; }

        public Car Car { get; set; }
        public Driver CurrentDriver { get; set; }
        public long CurrentDriverIndex { get; set; }
        public Timing Timing { get; set; }

        public override string ToString()
        {
            return $"Car[{Car}], CurrentDriver[{CurrentDriver}], Timing[{Timing}], {base.ToString()}";
        }
    }
}

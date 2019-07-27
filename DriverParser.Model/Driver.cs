namespace DriverParser.Model
{
    public class Driver
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShortName { get; set; }
        public string PlayerId { get; set; }

        public override string ToString()
        {
            return $"Name[{FirstName} {LastName}], PlayerId[{PlayerId}, {base.ToString()}";
        }
    }
}

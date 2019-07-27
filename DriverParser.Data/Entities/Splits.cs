namespace DriverParser.Data.Entities
{
    public partial class Splits : EntityBase
    {
        public long Value { get; set; }

        public override string ToString()
        {
            return $"Value[{Value}], {base.ToString()}";
        }
    }
}

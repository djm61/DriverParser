namespace DriverParser.Data.Entities
{
    public partial class EntityBase
    {
        public long Id { get; set; }

        public override string ToString()
        {
            return $"Id[{Id}], {base.ToString()}";
        }
    }
}

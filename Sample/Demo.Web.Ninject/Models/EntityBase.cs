namespace Demo.Web.Ninject
{
    public abstract class EntityBase
    {
        public int Id { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
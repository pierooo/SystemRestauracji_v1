namespace SystemRestauracji.Models.EntitiesForView
{
    public class OrderForOrderDetailsView
    {
        public int Id { get;}
        public string Name { get;}

        public OrderForOrderDetailsView(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}

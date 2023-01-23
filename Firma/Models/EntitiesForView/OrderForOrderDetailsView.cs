using SystemRestauracji.Models.BusinessLogic;

namespace SystemRestauracji.Models.EntitiesForView
{
    public class OrderForOrderDetailsView
    {
        public int Id { get;}
        public string Name { get;}

        public Status Status { get;}

        public OrderForOrderDetailsView(int id, string name, Status status)
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
        }
    }
}

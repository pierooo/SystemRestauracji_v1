using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class AddDocumentViewModel : ItemViewModel<Documents>
    {
        public AddDocumentViewModel() : base("Wystaw rachunek")
        {
            base.Item = new Documents();
        }

        public override void Save()
        {
        }
    }
}

namespace Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models
{
    public class DeliveryMan : User
    {
        public string Cnpj { get; set; }
        public DateTime Birthday { get; set; }
        public Guid Document { get; set; }
        public Guid? RentalPlan { get; set; } 

        public DeliveryMan()
        {
        }

        public DeliveryMan(string name, string email, string password, Guid document, string cnpj, DateTime birthday) : base(name, email, password)
        {
            Document = document;
            Cnpj = cnpj;
            Birthday = birthday;
        }
    }
}

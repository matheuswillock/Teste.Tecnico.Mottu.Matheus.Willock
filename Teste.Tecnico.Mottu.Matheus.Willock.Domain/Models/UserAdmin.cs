namespace Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models
{
    public class UserAdmin : User
    {
        public UserAdmin()
        {
        }

        public UserAdmin(string name, string email, string password) : base(name, email, password)
        {
        }
    }
}

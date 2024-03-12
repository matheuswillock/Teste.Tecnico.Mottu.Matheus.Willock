namespace Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models
{
    public class Motorcycle
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Plate { get; set; }
        public string Year { get; set; }
        public string Model { get; set; }
        public bool IsRented { get; set; } = false;

        public Motorcycle()
        {

        }

        public Motorcycle(string plate, string year, string model)
        {
            Plate = plate;
            Year = year;
            Model = model;
        }
    }
}

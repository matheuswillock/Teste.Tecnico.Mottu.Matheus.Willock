namespace Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models
{
    public class DeliveryManDocument
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CnhNumber { get; set; }
        public CNH CnhType { get; set; }
        public string IdCnhPhoto { get; set; } 
        public string CnhPhotoUrl { get; set; }
        public string CnhPhotoImgxUrl { get; set; }

        public DeliveryManDocument()
        {

        }

        public DeliveryManDocument(string cnhNumber, CNH cnhType, string idCnhPhoto, string cnhPhotoUrl, string cnhPhotoImgxUrl)
        {
            CnhNumber = cnhNumber;
            CnhType = cnhType;
            IdCnhPhoto = idCnhPhoto;
            CnhPhotoUrl = cnhPhotoUrl;
            CnhPhotoImgxUrl = cnhPhotoImgxUrl;
        }
    }
}

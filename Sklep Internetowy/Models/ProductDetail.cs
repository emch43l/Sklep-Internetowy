using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep_Internetowy.Models
{
    public class ProductDetail
    {
        public int Id { get; set; } 

        public string Description { get; set; }

        public DateTime Creation_Date { get; set; }

        public ICollection<Image> Images { get; set; }


        [EditorBrowsable(EditorBrowsableState.Never)]
        public string AditionalInformations { get; set; }

        [NotMapped]
        public List<string> Information 
        { 
            get => (AditionalInformations == "") ? new List<string>() : AditionalInformations.Split(';').ToList(); 
            set => AditionalInformations = (value == null || value.Count() == 0) ? String.Empty : String.Join(';',value); 
        }
        public Product Product { get; set; }

        public int ProductId { get; set; }
    }
}

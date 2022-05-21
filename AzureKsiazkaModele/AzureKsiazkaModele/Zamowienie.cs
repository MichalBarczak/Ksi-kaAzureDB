using Cassandra.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace Modele
{
    public class Zamowienie:BaseModel
    {
     
        [Required]
        public Guid IdKlienta { get; set; }
        [Required]
        public double Cena { get; set; }
        [Required]
        public double CenaDostawy { get; set; }
        [Required]
        public double Upust { get; set; }
        [Required]
        public string SposobDostawy {get;set;}
        [Required]
        public string SposobPlatnosci { get; set; }
        [Required]
        public string StatusZamowienia { get; set; }
        [Required]
        public Guid IdAdresu { get; set; }
        [NotMapped]
        [Cassandra.Mapping.Attributes.Ignore]
        [JsonIgnore]
        public Klient Klient { get; set; }
        [NotMapped]
        [Cassandra.Mapping.Attributes.Ignore]
        [JsonIgnore]
        public Adres AdresDostawy { get; set; }
        [Cassandra.Mapping.Attributes.Ignore]
        [JsonIgnore]
        public List<ElementZamowienia> ElementyZamowienia { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}

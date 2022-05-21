using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Modele
{
    public class Produkt:BaseModel
    {
        [Required]
        public Guid IdTypu { get; set; }
        [Required]
        public Guid IdProducenta { get; set; }
        [Required]
        public string Nazwa { get; set; }
        [Required]
        public string Opis { get; set; }
        [Required]
        public double Cena { get; set; }
        public TypProduktu TypProduktu { get; set; }
        public Producent Producent { get; set; }
        [BsonIgnore]
        [JsonIgnore]
        public List<ElementZamowienia> ZamówioneProdukty { get; set; }
        [NotMapped]
        [BsonIgnore]
        [JsonIgnore]
        public Magazyn Magazyn { get; set; }
        [NotMapped]
        public string NazwaProducenta { get; set; }
        [NotMapped]        
        public string NazwaTypu { get; set; }
        public override string ToString()
        {
            return Nazwa;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Modele
{
    public class Magazyn:BaseModel
    {
   
        [Required]
        public Guid IdProduktu { get; set; }
        [Required]
        public string NazwaProduktu { get; set; }
        [Required]
        public int IlośćProduktu { get; set; }
        [NotMapped]
        [JsonIgnore]
        public Produkt Produkt {get;set;}
        public override string ToString()
        {
            return $"{Produkt} {IlośćProduktu}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Modele
{
    public class ElementZamowienia:BaseModel
    {
       
        [Required]
        public Guid IdZamowienia { get; set; }
        [Required]
        public Guid IdProduktu { get; set; }
        [Required]
        public int IlośćProduktu { get; set; }
        [NotMapped]
        [JsonIgnore]
        public Produkt Produkt { get; set; }

        public override string ToString()
        {
            return $"{IlośćProduktu} {Produkt}";
        }
    }
}

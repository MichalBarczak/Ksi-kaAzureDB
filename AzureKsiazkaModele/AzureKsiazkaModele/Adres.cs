using Newtonsoft.Json;
//using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using System.Threading.Tasks;

namespace Modele
{
    public class Adres:BaseModel
    {
        [Required]
        public Guid IdKlienta { get; set; }
        [Required]
        public string Ulica { get; set; }
        [Required]
        public string  NumerDomu { get; set; }
        public int NumerMieszkania { get; set; }
        [Required]
        public string Miasto { get; set; }
        [Required]
        public string KodPocztowy { get; set; }
        public string Państwo { get; set; }
        [NotMapped]
        [JsonIgnore]
        public Klient Klient { get; set; }
        public bool Glowny { get; set; }
        public override string ToString()
        {
            return $"{Ulica} {NumerDomu} {NumerMieszkania} {Miasto} {KodPocztowy} {Państwo}";
        }
    }
}

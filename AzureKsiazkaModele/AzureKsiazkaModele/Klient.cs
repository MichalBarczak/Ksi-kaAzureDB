using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Modele
{
    public class Klient : BaseModel
    {
        [Required]
        public string Imię { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        [Required]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{2,3}\.[0-9]{2,3}\.[0-9]{2,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{2,3})(\]?)$", ErrorMessage = "Zły format maila")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"(?<!\w)(\(?(\+|00)?48\)?)?[ -]?\d{3}[ -]?\d{3}[ -]?\d{3}(?!\w)", ErrorMessage = "Zły format danych")]
        public double NumerTelefonu { get; set; }
        [JsonIgnore]
        public List<Adres> Adresy { get; set; }
       // [NotMapped]
        //[JsonIgnore]
        //public List<Zamowienie> Zamowienia { get; set; }
        [NotMapped]
        [Required]
        [StringLength(100,MinimumLength =8, ErrorMessage = "Hasło musi mieć conajmniej 8-siem znaków")]
        public string Hasło { get; set; }
        public override string ToString()
        {
            return $"{Imię} {Nazwisko}";
        }
    }
}

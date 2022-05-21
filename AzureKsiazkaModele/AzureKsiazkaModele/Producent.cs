using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Modele
{
    public class Producent : BaseModel, IComparable<Producent>
    {
        [Required]
        public string Nazwa { get; set; }
        [JsonIgnore]
        public List<Produkt> Produkty { get; set; }

        public int CompareTo([AllowNull] Producent other)
        {
            return String.Compare(this.Nazwa, other.Nazwa);
        }

        public override string ToString()
        {
            return Nazwa;
        }
    }
}

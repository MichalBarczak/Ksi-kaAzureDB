using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Modele
{
    public class TypProduktu:BaseModel, IComparable<TypProduktu>
    {
       [Required]
        public string Nazwa { get; set; }
        [JsonIgnore]
        public List<Produkt> Produkty { get; set; }

        public int CompareTo([AllowNull] TypProduktu other)
        {
            return String.Compare(this.Nazwa, other.Nazwa);
        }

        public override string ToString()
        {
            return Nazwa;
        }
    }
}

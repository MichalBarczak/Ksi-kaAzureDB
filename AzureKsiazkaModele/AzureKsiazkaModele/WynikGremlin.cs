using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    public class WynikGremlin
    {
        public string id { get; set; }
        public string label { get; set; }
        public string type { get; set; }
        public GremlinWlasciwość properties { get; set; }
        public Produkt MapujNaProdukt()
        {
            return new Produkt
            {
                Id = Guid.Parse(id),
                Nazwa = properties.Nazwa[0].value,
                Cena = Convert.ToInt64(properties.Cena[0].value),
                Opis = properties.Opis[0].value,
                IdProducenta = Guid.Parse(properties.IdProducenta[0].value),
                IdTypu = Guid.Parse(properties.IdTypu[0].value)
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Modele
{
    public class ElementListy
    {
        public Guid Id { get; set; }
        public string Wartość { get; set; }
        public bool Zaznaczony { get; set; }
        public override string ToString()
        {
            return Wartość;
        }
    }
}

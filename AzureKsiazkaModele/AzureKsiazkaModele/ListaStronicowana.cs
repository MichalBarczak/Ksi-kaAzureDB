using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modele
{
    public class ListaStronicowana<T> : List<T>
    {
        public int IndeksStrony { get; private set; }
        public int IlośćStron { get; private set; }

        public ListaStronicowana(List<T> items, int count, int pageIndex, int pageSize)
        {
            IndeksStrony = pageIndex;
            IlośćStron = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool MaPoprzedniąStronę
        {
            get
            {
                return (IndeksStrony > 1);
            }
        }

        public bool MaNastępnąStronę
        {
            get
            {
                return (IndeksStrony < IlośćStron);
            }
        }

        public static ListaStronicowana<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var liczba = source.Count();
            var elementy = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new ListaStronicowana<T>(elementy, liczba, pageIndex, pageSize);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAPIConnectionApp.Interfaces
{
    public interface IHasStarships
    {
        IEnumerable<string> starships { get; }
        IEnumerable<string> starshipsNames { get; }
    }
}

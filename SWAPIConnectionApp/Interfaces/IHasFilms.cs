using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAPIConnectionApp.Interfaces
{
    public interface IHasFilms
    {
        IEnumerable<string> films { get; }
        IEnumerable<string> filmsTitles { get; }
    }
}

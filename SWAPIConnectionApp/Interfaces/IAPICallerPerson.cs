using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAPIConnectionApp.Interfaces
{
    public interface IAPICallerPerson
    {
        IPerson GetPerson(int personId);
        IEnumerable<string> GetFilmsTitles(IEnumerable<string> films);
        IEnumerable<string> GetStarshipsNames(IEnumerable<string> starships);
        IEnumerable<string> GetVehiclesNames(IEnumerable<string> vehicles);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAPIConnectionApp.Interfaces
{
    public interface IHasVehicles
    {
        IEnumerable<string> vehicles { get; }
        IEnumerable<string> vehiclesNames { get; }
    }
}

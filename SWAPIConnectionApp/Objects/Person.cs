using Newtonsoft.Json;
using SWAPIConnectionApp.API;
using SWAPIConnectionApp.FileAccess;
using SWAPIConnectionApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SWAPIConnectionApp.Objects
{
    public class Person : IPerson, IHasFilms, IHasStarships, IHasVehicles
    {
        #region Properties
        public string name { get; set; }

        public IEnumerable<string> films { get; set; }

        public IEnumerable<string> filmsTitles { get; set; } = new List<string>();

        public IEnumerable<string> starships { get; set; }

        public IEnumerable<string> starshipsNames { get; set; } = new List<string>();

        public IEnumerable<string> vehicles { get; set; }

        public IEnumerable<string> vehiclesNames { get; set; } = new List<string>();
        #endregion

        #region ShouldSerialize
        public bool ShouldSerializefilms(){ return false;}
        public bool ShouldSerializestarships() { return false; }
        public bool ShouldSerializevehicles() { return false; }
        #endregion

        #region Methods
        public static Person DeserializeJSONToObject (string json)
        {
            FileWriter.AddLog($"Deserializacja JSON do obiektu Person");

            return JsonConvert.DeserializeObject<Person>(json);
        }

        public string SerializeObjectToJSON()
        {
            FileWriter.AddLog($"Serializacja obiektu Person do JSON");

            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

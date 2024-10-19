using SWAPIConnectionApp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAPIConnectionAppTests
{
    internal static class ObjectHelper
    {
        #region Const
        public const string personJSON = "{\"name\":\"Luke Skywalker\"," +
                                            "\"films\":[\"https://swapi.py4e.com/api/films/1/\",\"https://swapi.py4e.com/api/films/2/\",\"https://swapi.py4e.com/api/films/3/\",\"https://swapi.py4e.com/api/films/6/\",\"https://swapi.py4e.com/api/films/7/\"]," +
                                            "\"vehicles\":[\"https://swapi.py4e.com/api/vehicles/14/\",\"https://swapi.py4e.com/api/vehicles/30/\"]," +
                                            "\"starships\":[\"https://swapi.py4e.com/api/starships/12/\",\"https://swapi.py4e.com/api/starships/22/\"]}";

        public const string singleFilmTitleUrl = "https://swapi.py4e.com/api/films/1";
        public const string filmTitleJSON = "{\"title\":\"A New Hope\"}";

        public const string singleStarshipNameUrl = "https://swapi.py4e.com/api/starships/12";
        public const string starshipNameJSON = "{\"name\":\"X-wing\"}";

        public const string singleVehicleNameUrl = "https://swapi.py4e.com/api/vehicles/14";
        public const string vehicleNameJSON = "{\"name\":\"Snowspeeder\"}";
        #endregion

        #region Methods
        public static Person GetBasicPerson()
        {
            var person = new Person();

            person.name = "Luke Skywalker";
            person.films = new List<string>() { "https://swapi.py4e.com/api/films/1/",
                                                "https://swapi.py4e.com/api/films/2/",
                                                "https://swapi.py4e.com/api/films/3/",
                                                "https://swapi.py4e.com/api/films/6/",
                                                "https://swapi.py4e.com/api/films/7/" };
            person.filmsTitles = new List<string>();

            person.starships = new List<string>() { "https://swapi.py4e.com/api/starships/12/",
                                                    "https://swapi.py4e.com/api/starships/22/" };
            person.starshipsNames = new List<string>();

            person.vehicles = new List<string>() { "https://swapi.py4e.com/api/vehicles/14/",
                                                   "https://swapi.py4e.com/api/vehicles/30/" };
            person.vehiclesNames = new List<string>();

            return person;
        }

        public static IEnumerable<string> GetFilmsTitles()
        {
            return new List<string>() { "A New Hope",
                                        "The Empire Strikes Back",
                                        "Return of the Jedi",
                                        "Revenge of the Sith",
                                        "The Force Awakens"};
        }

        public static IEnumerable<string> GetSingleFilmTitle()
        {
            return new List<string>() { "A New Hope"};
        }

        public static IEnumerable<string> GetStarshipsNames()
        {
            return new List<string>() { "X-wing",
                                        "Imperial shuttle"};
        }

        public static IEnumerable<string> GetSingleStarshipName()
        {
            return new List<string>() { "X-wing" };
        }

        public static IEnumerable<string> GetVehiclesNames()
        {
            return new List<string>() { "Snowspeeder",
                                        "Imperial Speeder Bike"};
        }

        public static IEnumerable<string> GetSingleVehicleName()
        {
            return new List<string>() { "Snowspeeder" };
        }

        public static Person GetExpectedPersonFinal()
        {
            var person = new Person();

            person.name = "Luke Skywalker";
            person.films = new List<string>() { "https://swapi.py4e.com/api/films/1/",
                                                "https://swapi.py4e.com/api/films/2/",
                                                "https://swapi.py4e.com/api/films/3/",
                                                "https://swapi.py4e.com/api/films/6/",
                                                "https://swapi.py4e.com/api/films/7/" };
            person.filmsTitles = new List<string>() { "A New Hope",
                                                      "The Empire Strikes Back",
                                                      "Return of the Jedi",
                                                      "Revenge of the Sith",
                                                      "The Force Awakens"};

            person.starships = new List<string>() { "https://swapi.py4e.com/api/starships/12/",
                                                    "https://swapi.py4e.com/api/starships/22/" };
            person.starshipsNames = new List<string>() { "X-wing",
                                                         "Imperial shuttle"};

            person.vehicles = new List<string>() { "https://swapi.py4e.com/api/vehicles/14/",
                                                   "https://swapi.py4e.com/api/vehicles/30/" };
            person.vehiclesNames = new List<string>() { "Snowspeeder",
                                                        "Imperial Speeder Bike"};

            return person;
        }
        #endregion
    }
}

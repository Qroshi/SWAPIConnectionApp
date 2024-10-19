using Newtonsoft.Json;
using SWAPIConnectionApp.FileAccess;
using SWAPIConnectionApp.Interfaces;
using SWAPIConnectionApp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAPIConnectionApp.API
{
    public class APICallerPerson : APICaller, IAPICallerPerson
    {
        public APICallerPerson(IHttpClientFactory httpClientFactory) : base(httpClientFactory) 
        {
            FileWriter.AddLog("Inicjalizacja instancji APICallerPerson");
        }

        public IPerson GetPerson(int personId)
        {
            FileWriter.AddLog("Wysłanie rządania danych osoby do API");

            string response = string.Empty;
            try
            {
                response = this.APIGetResponse(Const.APIUrl + Const.PeopleEndpoint + $"{personId}");
            }
            catch (AggregateException ex) { FileWriter.AddLog($"Wystąpienie błędu: {ex.Message}"); }

            return Person.DeserializeJSONToObject(response);
        }

        public IEnumerable<string> GetFilmsTitles(IEnumerable<string> films)
        {
            var definitionTitle = new { title = "" };
            var filmsTitles = new List<string>();

            if(films == null || films.Count() == 0)
                return filmsTitles;

            foreach (var film in films)
            {
                FileWriter.AddLog($"Wysłanie rządania tytułu filmu do API : {film}");

                var filmJson = string.Empty; 

                try
                {
                    filmJson = this.APIGetResponse(film);
                }
                catch (AggregateException ex) { FileWriter.AddLog($"Wystąpienie błędu: {ex.Message}"); }

                FileWriter.AddLog("Deserializacja odpowiedzi API i dodanie tytułu filmu");

                var title = string.Empty;

                try
                {
                    title = JsonConvert.DeserializeAnonymousType(filmJson, definitionTitle).title;
                }
                catch (NullReferenceException ex) { FileWriter.AddLog($"Wystąpienie błędu: {ex.Message}"); }

                if(title != string.Empty)
                    filmsTitles.Add(title);
            }

            return filmsTitles;
        }

        public IEnumerable<string> GetStarshipsNames(IEnumerable<string> starships)
        {
            var definitionName = new { name = "" };
            var starshipsNames = new List<string>();

            if (starships == null || starships.Count() == 0)
                return starshipsNames;

            foreach (var starship in starships)
            {
                FileWriter.AddLog($"Wysłanie rządania nazwy statku kosmicznego do API : {starship}");

                var starshipJson = string.Empty;

                try
                {
                    starshipJson = this.APIGetResponse(starship);
                }
                catch (AggregateException ex) { FileWriter.AddLog($"Wystąpienie błędu: {ex.Message}"); }

                FileWriter.AddLog("Deserializacja odpowiedzi API i nazwy statku kosmicznego");

                var name = string.Empty;

                try
                {
                    name = JsonConvert.DeserializeAnonymousType(starshipJson, definitionName).name;
                }
                catch (NullReferenceException ex) { FileWriter.AddLog($"Wystąpienie błędu: {ex.Message}"); }

                if (name != string.Empty)
                    starshipsNames.Add(name);
            }

            return starshipsNames;
        }

        public IEnumerable<string> GetVehiclesNames(IEnumerable<string> vehicles)
        {
            var definitionName = new { name = "" };
            var vehiclesNames = new List<string>();

            if (vehicles == null || vehicles.Count() == 0)
                return vehiclesNames;

            foreach (var vehicle in vehicles)
            {
                FileWriter.AddLog($"Wysłanie rządania nazwy pojazdu do API : {vehicle}");

                var vehicleJson = string.Empty;

                try
                {
                    vehicleJson = this.APIGetResponse(vehicle);
                }
                catch (AggregateException ex) { FileWriter.AddLog($"Wystąpienie błędu: {ex.Message}"); }

                FileWriter.AddLog("Deserializacja odpowiedzi API i nazwy pojazdu");

                var name = string.Empty;

                try
                {
                    name = JsonConvert.DeserializeAnonymousType(vehicleJson, definitionName).name;
                }
                catch (NullReferenceException ex) { FileWriter.AddLog($"Wystąpienie błędu: {ex.Message}"); }

                if (name != string.Empty)
                    vehiclesNames.Add(name);
            }

            return vehiclesNames;   
        }
    }
}

using Newtonsoft.Json;
using SWAPIConnectionApp.API;
using SWAPIConnectionApp.FileAccess;
using SWAPIConnectionApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAPIConnectionApp.Objects
{
    public class PersonFactory
    {
        IAPICallerPerson apiCaller;

        Person _person = new Person();

        public PersonFactory(IAPICallerPerson apiCaller)
        {
            FileWriter.AddLog("Inicjalizacja instancji PersonFactory");

            this.apiCaller = apiCaller;
        }

        private void Reset()
        {
            FileWriter.AddLog("Reset danych PersonFactory");

            _person = new Person();
        }

        public PersonFactory GetPersonDataFromAPI(int personId)
        {
            FileWriter.AddLog("Zlecenie utworzenia instancji klasy Person");

            this._person = (Person)apiCaller.GetPerson(personId) ?? new Person();

            return this;
        }

        public PersonFactory GetFilmsTitles()
        {
            FileWriter.AddLog("Zlecenie pobrania tytułów filmów");

            this._person.filmsTitles = apiCaller.GetFilmsTitles(this._person.films);

            return this;
        }

        public PersonFactory GetStarshipsNames()
        {
            FileWriter.AddLog("Zlecenie pobrania nazw statków kosmicznych");

            this._person.starshipsNames = apiCaller.GetStarshipsNames(this._person.starships);
            return this;
        }

        public PersonFactory GetVehiclesNames()
        {
            FileWriter.AddLog("Zlecenie pobrania nazw pojazdów");

            this._person.vehiclesNames = apiCaller.GetVehiclesNames(this._person.vehicles);
            return this;
        }

        public Person GetPerson() 
        {
            FileWriter.AddLog("Pobranie instancji klasy Person z PersonFactory");

            var result = _person;

            Reset();

            return result; 
        }
    }
}

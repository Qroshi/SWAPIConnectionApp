using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWAPIConnectionApp.API;
using SWAPIConnectionApp.FileAccess;
using SWAPIConnectionApp.Interfaces;
using SWAPIConnectionApp.Objects;

var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

var apiCallerPerson = new APICallerPerson(httpClientFactory);
var personFactory = new PersonFactory(apiCallerPerson);

var lukeId = 1;
var lukeObject = personFactory.GetPersonDataFromAPI(lukeId)
                        .GetFilmsTitles()
                        .GetStarshipsNames()
                        .GetVehiclesNames()
                        .GetPerson();

FileWriter.WriteOutputPersonToFile(lukeObject);
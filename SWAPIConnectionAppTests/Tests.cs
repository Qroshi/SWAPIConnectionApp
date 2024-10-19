using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using SWAPIConnectionApp.API;
using SWAPIConnectionApp.Interfaces;
using SWAPIConnectionApp.Objects;

namespace SWAPIConnectionAppTests
{
    public class Tests
    {
        [TestClass]
        public class UnitTests
        {
            [TestMethod]
            public void PersonFactory_Builds_ExpectedPersonObject()
            {
                var personId = 1;
                var personExpected = ObjectHelper.GetExpectedPersonFinal();

                var mockReturnPerson = ObjectHelper.GetBasicPerson();

                var apiCallerMock = new Mock<IAPICallerPerson>();
                apiCallerMock.Setup(_ => _.GetPerson(personId)).Returns(mockReturnPerson);
                apiCallerMock.Setup(_ => _.GetFilmsTitles(mockReturnPerson.films)).Returns(ObjectHelper.GetFilmsTitles());
                apiCallerMock.Setup(_ => _.GetStarshipsNames(mockReturnPerson.starships)).Returns(ObjectHelper.GetStarshipsNames());
                apiCallerMock.Setup(_ => _.GetVehiclesNames(mockReturnPerson.vehicles)).Returns(ObjectHelper.GetVehiclesNames());

                var personFactory = new PersonFactory(apiCallerMock.Object);

                var personActual = personFactory.GetPersonDataFromAPI(personId)
                                          .GetFilmsTitles()
                                          .GetStarshipsNames()
                                          .GetVehiclesNames()
                                          .GetPerson();

                Assert.AreEqual(personExpected.name, personActual.name);
                CollectionAssert.AreEqual(personExpected.films.ToArray(),personActual.films.ToArray());
                CollectionAssert.AreEqual(personExpected.filmsTitles.ToArray(), personActual.filmsTitles.ToArray());
                CollectionAssert.AreEqual(personExpected.starships.ToArray(), personActual.starships.ToArray());
                CollectionAssert.AreEqual(personExpected.starshipsNames.ToArray(), personActual.starshipsNames.ToArray());
                CollectionAssert.AreEqual(personExpected.vehicles.ToArray(), personActual.vehicles.ToArray());
                CollectionAssert.AreEqual(personExpected.vehiclesNames.ToArray(), personActual.vehiclesNames.ToArray());
            }

            [TestMethod]
            public void GetPerson_Server200Response()
            {
                var personId = 1;
                var personExpected = ObjectHelper.GetBasicPerson();

                var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

                HttpResponseMessage result = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.OK, Content = new StringContent(ObjectHelper.personJSON) };

                handlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                    )
                    .ReturnsAsync(result)
                    .Verifiable();

                var httpClient = new HttpClient(handlerMock.Object);

                var mockHttpClientFactory = new Mock<IHttpClientFactory>();

                mockHttpClientFactory.Setup(_ => _.CreateClient(string.Empty)).Returns(httpClient).Verifiable();

                var apiCaller = new APICallerPerson(mockHttpClientFactory.Object);

                var personActual = (Person)apiCaller.GetPerson(personId);

                Assert.AreEqual(personExpected.name, personActual.name);
                CollectionAssert.AreEqual(personExpected.films.ToArray(), personActual.films.ToArray());
                CollectionAssert.AreEqual(personExpected.filmsTitles.ToArray(), personActual.filmsTitles.ToArray());
                CollectionAssert.AreEqual(personExpected.starships.ToArray(), personActual.starships.ToArray());
                CollectionAssert.AreEqual(personExpected.starshipsNames.ToArray(), personActual.starshipsNames.ToArray());
                CollectionAssert.AreEqual(personExpected.vehicles.ToArray(), personActual.vehicles.ToArray());
                CollectionAssert.AreEqual(personExpected.vehiclesNames.ToArray(), personActual.vehiclesNames.ToArray());
            }

            [TestMethod]
            public void GetPerson_Server404Response()
            {
                var personId = 999999;
                Person personExpected = null;

                var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

                HttpResponseMessage result = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.NotFound};

                handlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                    )
                    .ReturnsAsync(result)
                    .Verifiable();

                var httpClient = new HttpClient(handlerMock.Object);

                var mockHttpClientFactory = new Mock<IHttpClientFactory>();

                mockHttpClientFactory.Setup(_ => _.CreateClient(string.Empty)).Returns(httpClient).Verifiable();

                var apiCaller = new APICallerPerson(mockHttpClientFactory.Object);

                var personActual = (Person)apiCaller.GetPerson(personId);

                Assert.AreEqual(personExpected, personActual);
            }

            [TestMethod]
            public void GetFilmsTitles_Server200Response()
            {
                var filmsTitlesExpected = ObjectHelper.GetSingleFilmTitle();

                var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

                HttpResponseMessage result = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.OK, Content = new StringContent(ObjectHelper.filmTitleJSON) };

                handlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                    )
                    .ReturnsAsync(result)
                    .Verifiable();

                var httpClient = new HttpClient(handlerMock.Object);

                var mockHttpClientFactory = new Mock<IHttpClientFactory>();

                mockHttpClientFactory.Setup(_ => _.CreateClient(string.Empty)).Returns(httpClient).Verifiable();

                var apiCaller = new APICallerPerson(mockHttpClientFactory.Object);

                var filmsTitlesActual = apiCaller.GetFilmsTitles(new List<string>() { ObjectHelper.singleFilmTitleUrl });

                CollectionAssert.AreEqual(filmsTitlesExpected.ToArray(), filmsTitlesActual.ToArray());
            }

            [TestMethod]
            public void GetFilmsTitles_Server404Response()
            {
                var filmsTitlesExpected = new List<string>();

                var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

                HttpResponseMessage result = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.NotFound };

                handlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                    )
                    .ReturnsAsync(result)
                    .Verifiable();

                var httpClient = new HttpClient(handlerMock.Object);

                var mockHttpClientFactory = new Mock<IHttpClientFactory>();

                mockHttpClientFactory.Setup(_ => _.CreateClient(string.Empty)).Returns(httpClient).Verifiable();

                var apiCaller = new APICallerPerson(mockHttpClientFactory.Object);

                var filmsTitlesActual = apiCaller.GetFilmsTitles(new List<string>() { ObjectHelper.singleFilmTitleUrl + "999999" });

                CollectionAssert.AreEqual(filmsTitlesExpected.ToArray(), filmsTitlesActual.ToArray());
            }

            [TestMethod]
            public void GetStarshipsNames_Server200Response()
            {
                var starshipsNamesExpected = ObjectHelper.GetSingleStarshipName();

                var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

                HttpResponseMessage result = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.OK, Content = new StringContent(ObjectHelper.starshipNameJSON) };

                handlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                    )
                    .ReturnsAsync(result)
                    .Verifiable();

                var httpClient = new HttpClient(handlerMock.Object);

                var mockHttpClientFactory = new Mock<IHttpClientFactory>();

                mockHttpClientFactory.Setup(_ => _.CreateClient(string.Empty)).Returns(httpClient).Verifiable();

                var apiCaller = new APICallerPerson(mockHttpClientFactory.Object);

                var starshipsNamesActual = apiCaller.GetStarshipsNames(new List<string>() { ObjectHelper.singleStarshipNameUrl });

                CollectionAssert.AreEqual(starshipsNamesExpected.ToArray(), starshipsNamesActual.ToArray());
            }

            [TestMethod]
            public void GetStarshipsNames_Server404Response()
            {
                var starshipsNamesExpected = new List<string>();

                var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

                HttpResponseMessage result = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.NotFound };

                handlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                    )
                    .ReturnsAsync(result)
                    .Verifiable();

                var httpClient = new HttpClient(handlerMock.Object);

                var mockHttpClientFactory = new Mock<IHttpClientFactory>();

                mockHttpClientFactory.Setup(_ => _.CreateClient(string.Empty)).Returns(httpClient).Verifiable();

                var apiCaller = new APICallerPerson(mockHttpClientFactory.Object);

                var starshipsNamesActual = apiCaller.GetStarshipsNames(new List<string>() { ObjectHelper.singleStarshipNameUrl + "999999" });

                CollectionAssert.AreEqual(starshipsNamesExpected.ToArray(), starshipsNamesActual.ToArray());
            }

            [TestMethod]
            public void GetVehiclesNames_Server200Response()
            {
                var vehiclesNamesExpected = ObjectHelper.GetSingleVehicleName();

                var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

                HttpResponseMessage result = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.OK, Content = new StringContent(ObjectHelper.vehicleNameJSON) };

                handlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                    )
                    .ReturnsAsync(result)
                    .Verifiable();

                var httpClient = new HttpClient(handlerMock.Object);

                var mockHttpClientFactory = new Mock<IHttpClientFactory>();

                mockHttpClientFactory.Setup(_ => _.CreateClient(string.Empty)).Returns(httpClient).Verifiable();

                var apiCaller = new APICallerPerson(mockHttpClientFactory.Object);

                var vehiclesNamesActual = apiCaller.GetVehiclesNames(new List<string>() { ObjectHelper.singleVehicleNameUrl });

                CollectionAssert.AreEqual(vehiclesNamesExpected.ToArray(), vehiclesNamesActual.ToArray());
            }

            [TestMethod]
            public void GetVehiclesNames_Server404Response()
            {
                var vehiclesNamesExpected = new List<string>();

                var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

                HttpResponseMessage result = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.NotFound };

                handlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                    )
                    .ReturnsAsync(result)
                    .Verifiable();

                var httpClient = new HttpClient(handlerMock.Object);

                var mockHttpClientFactory = new Mock<IHttpClientFactory>();

                mockHttpClientFactory.Setup(_ => _.CreateClient(string.Empty)).Returns(httpClient).Verifiable();

                var apiCaller = new APICallerPerson(mockHttpClientFactory.Object);

                var vehiclesNamesActual = apiCaller.GetVehiclesNames(new List<string>() { ObjectHelper.singleVehicleNameUrl + "999999" });

                CollectionAssert.AreEqual(vehiclesNamesExpected.ToArray(), vehiclesNamesActual.ToArray());
            }

            [TestMethod]
            [ExpectedException(typeof(AggregateException))]
            public void APIGetResponse_Server404Response()
            {
                var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

                HttpResponseMessage result = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.NotFound };

                handlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                    )
                    .ReturnsAsync(result)
                    .Verifiable();

                var httpClient = new HttpClient(handlerMock.Object);

                var mockHttpClientFactory = new Mock<IHttpClientFactory>();

                mockHttpClientFactory.Setup(_ => _.CreateClient(string.Empty)).Returns(httpClient).Verifiable();

                var apiCaller = new APICallerPerson(mockHttpClientFactory.Object);

                var vehiclesNamesActual = apiCaller.APIGetResponse(ObjectHelper.singleVehicleNameUrl + "999999" );

                //CollectionAssert.AreEqual(vehiclesNamesExpected.ToArray(), vehiclesNamesActual.ToArray());
            }
        }
    }
}

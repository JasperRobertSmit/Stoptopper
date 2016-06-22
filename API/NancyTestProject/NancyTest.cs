using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Testing;
using Nancy;
using NancyAspNetHost.Modules;
using Newtonsoft.Json;
using NancyAspNetHost.Entities;

namespace NancyTestProject
{
    [TestClass]
    public class NancyTest
    {
        /* UTILS */
        public string Login()
        {
            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // When
            var response = browser.Post("/Login", (with) =>
            {
                with.HttpRequest();
                with.FormValue("username", "testuser");
                with.FormValue("password", "testpassword");
            });

            // Then
            Result body = JsonConvert.DeserializeObject<Result>(response.Body.AsString());
            return body.Data;
        }

        #region LOGIN
        [TestMethod]
        public void TestRegister()
        {
            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // When
            var response = browser.Post("/Register", (with) =>
            {
                with.HttpRequest();
                with.FormValue("username", "testuser");
                with.FormValue("password", "testpassword");
                with.FormValue("email", "mathijs__lagendijk@msn.com");
            });

            // Then
            string body = response.Body.AsString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void TestLogin()
        {
            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // When
            var response = browser.Post("/Login", (with) =>
            {
                with.HttpRequest();
                with.FormValue("username", "testuser");
                with.FormValue("password", "testpassword");
            });

            // Then
            string body = response.Body.AsString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void TestValidateUsername()
        {
            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // When
            var response = browser.Post("/ValidateUsername", (with) =>
            {
                with.HttpRequest();
                with.FormValue("username", "testuser");
            });

            // Then
            string body = response.Body.AsString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region CRUD
        [TestMethod]
        public void TestTime()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // When
            var response = browser.Post("/SaveTijd", (with) =>
            {
                with.HttpRequest();
                with.FormValue("time", "633896886277130000");
                with.FormValue("ploegid", "1");
                with.FormValue("blokid", "1");
                with.FormValue("token", token);
            });

            // Then
            string body = response.Body.AsString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void CRUDVereniging()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Save
            Result saveResponse = JsonConvert.DeserializeObject<Result>(browser.Post("/vereniging", (with) =>
            {
                with.HttpRequest();
                with.FormValue("afkorting", "TST");
                with.FormValue("naam", "TestVereniging");
                with.FormValue("token", token);
            }).Body.AsString());

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/vereniging/" + saveResponse.Data + "/" + token).Body.AsString());

            // Delete
            Result deleteResponse = JsonConvert.DeserializeObject<Result>(browser.Delete("/vereniging/" + saveResponse.Data + "/" + token).Body.AsString());

            Assert.IsNotNull(saveResponse);
            Assert.IsNotNull(getResponse);
            Assert.IsNotNull(deleteResponse);

            Assert.IsTrue(saveResponse.Success);
            Assert.IsTrue(getResponse.Success);
            Assert.IsTrue(deleteResponse.Success);
        }

        [TestMethod]
        public void CRUDVeld()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Save
            Result saveResponse = JsonConvert.DeserializeObject<Result>(browser.Post("/veld", (with) =>
            {
                with.HttpRequest();
                with.FormValue("afkorting", "TST");
                with.FormValue("naam", "TestVeld");
                with.FormValue("blokid", "1");
                with.FormValue("token", token);
            }).Body.AsString());

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/veld/" + saveResponse.Data + "/" + token).Body.AsString());

            // Delete
            Result deleteResponse = JsonConvert.DeserializeObject<Result>(browser.Delete("/veld/" + saveResponse.Data + "/" + token).Body.AsString());

            Assert.IsNotNull(saveResponse);
            Assert.IsNotNull(getResponse);
            Assert.IsNotNull(deleteResponse);

            Assert.IsTrue(saveResponse.Success);
            Assert.IsTrue(getResponse.Success);
            Assert.IsTrue(deleteResponse.Success);
        }

        [TestMethod]
        public void CRUDPloeg()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Save
            Result saveResponse = JsonConvert.DeserializeObject<Result>(browser.Post("/SavePloeg", (with) =>
            {
                with.HttpRequest();
                with.FormValue("bootnaam", "TST");
                with.FormValue("naam", "TestVeld");
                with.FormValue("rugnummer", "1");
                with.FormValue("veldid", "1");
                with.FormValue("verenigingsid", "1");
                with.FormValue("token", token);
            }).Body.AsString());

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/GetPloeg/" + saveResponse.Data + "/" + token).Body.AsString());

            // Delete
            Result deleteResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/DeletePloeg/" + saveResponse.Data + "/" + token).Body.AsString());

            Assert.IsNotNull(saveResponse);
            Assert.IsNotNull(getResponse);
            Assert.IsNotNull(deleteResponse);

            Assert.IsTrue(saveResponse.Success);
            Assert.IsTrue(getResponse.Success);
            Assert.IsTrue(deleteResponse.Success);
        }

        [TestMethod]
        public void CRUDPersoon()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Save
            Result saveResponse = JsonConvert.DeserializeObject<Result>(browser.Post("/persoon", (with) =>
            {
                with.HttpRequest();
                with.FormValue("geboortedatum", "633896886277130000");
                with.FormValue("knrbid", "1");
                with.FormValue("verenigingsid", "1");
                with.FormValue("naam", "Test");
                with.FormValue("token", token);
            }).Body.AsString());

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/persoon/" + saveResponse.Data + "/" + token).Body.AsString());

            // Delete
            Result deleteResponse = JsonConvert.DeserializeObject<Result>(browser.Delete("/persoon/" + saveResponse.Data + "/" + token).Body.AsString());

            Assert.IsNotNull(saveResponse);
            Assert.IsNotNull(getResponse);
            Assert.IsNotNull(deleteResponse);

            Assert.IsTrue(saveResponse.Success);
            Assert.IsTrue(getResponse.Success);
            Assert.IsTrue(deleteResponse.Success);
        }

        [TestMethod]
        public void CRUDEvent()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Save
            Result saveResponse = JsonConvert.DeserializeObject<Result>(browser.Post("/event", (with) =>
            {
                with.HttpRequest();
                with.FormValue("afkorting", "TST");
                with.FormValue("einddatum", "633896886277130000");
                with.FormValue("startdatum", "633896886277130000");
                with.FormValue("guid", "1234");
                with.FormValue("afstand", "1");
                with.FormValue("naam", "Test");
                with.FormValue("plaats", "Test");
                with.FormValue("organisatorid", "1");
                with.FormValue("token", token);
            }).Body.AsString());

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/event/" + saveResponse.Data + "/" + token).Body.AsString());

            // Delete
            Result deleteResponse = JsonConvert.DeserializeObject<Result>(browser.Delete("/event/" + saveResponse.Data + "/" + token).Body.AsString());

            Assert.IsNotNull(saveResponse);
            Assert.IsNotNull(getResponse);
            Assert.IsNotNull(deleteResponse);

            Assert.IsTrue(saveResponse.Success);
            Assert.IsTrue(getResponse.Success);
            Assert.IsTrue(deleteResponse.Success);
        }

        [TestMethod]
        public void CRUDDeelnemer()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Save
            Result saveResponse = JsonConvert.DeserializeObject<Result>(browser.Post("/deelnemer", (with) =>
            {
                with.HttpRequest();
                with.FormValue("persoonid", "1");
                with.FormValue("ploegid", "1");
                with.FormValue("nummer", "1");
                with.FormValue("token", token);
            }).Body.AsString());

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/deelnemer/" + saveResponse.Data + "/" + token).Body.AsString());

            // Delete
            Result deleteResponse = JsonConvert.DeserializeObject<Result>(browser.Delete("/deelnemer/" + saveResponse.Data + "/" + token).Body.AsString());

            Assert.IsNotNull(saveResponse);
            Assert.IsNotNull(getResponse);
            Assert.IsNotNull(deleteResponse);

            Assert.IsTrue(saveResponse.Success);
            Assert.IsTrue(getResponse.Success);
            Assert.IsTrue(deleteResponse.Success);
        }

        [TestMethod]
        public void CRUDBlok()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Save
            Result saveResponse = JsonConvert.DeserializeObject<Result>(browser.Post("/blok", (with) =>
            {
                with.HttpRequest();
                with.FormValue("starttijd", "633896886277130000");
                with.FormValue("eindtijd", "633896886277130000");
                with.FormValue("evenementid", "1");
                with.FormValue("nummer", "1");
                with.FormValue("token", token);
            }).Body.AsString());

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/blok/" + saveResponse.Data + "/" + token).Body.AsString());

            // Delete
            Result deleteResponse = JsonConvert.DeserializeObject<Result>(browser.Delete("/blok/" + saveResponse.Data + "/" + token).Body.AsString());

            Assert.IsNotNull(saveResponse);
            Assert.IsNotNull(getResponse);
            Assert.IsNotNull(deleteResponse);

            Assert.IsTrue(saveResponse.Success);
            Assert.IsTrue(getResponse.Success);
            Assert.IsTrue(deleteResponse.Success);
        }
        #endregion

        #region GETALL
        [TestMethod]
        public void GetEvents()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/event/all/user/" + token).Body.AsString());

            Assert.IsNotNull(getResponse);
            Assert.IsTrue(getResponse.Success);
        }

        [TestMethod]
        public void GetBlokken()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/blok/all/event/1/" + token).Body.AsString());

            Assert.IsNotNull(getResponse);
            Assert.IsTrue(getResponse.Success);
        }

        [TestMethod]
        public void GetVelden()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/veld/all/blok/1/" + token).Body.AsString());

            Assert.IsNotNull(getResponse);
            Assert.IsTrue(getResponse.Success);
        }

        [TestMethod]
        public void GetPloegenByVeld()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/ploeg/all/veld/1/" + token).Body.AsString());

            Assert.IsNotNull(getResponse);
            Assert.IsTrue(getResponse.Success);
        }

        [TestMethod]
        public void GetPloegenByVereniging()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/ploeg/all/vereniging/1/" + token).Body.AsString());

            Assert.IsNotNull(getResponse);
            Assert.IsTrue(getResponse.Success);
        }

        [TestMethod]
        public void GetDeelnemersByPloeg()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/deelnemer/all/ploeg/1/" + token).Body.AsString());

            Assert.IsNotNull(getResponse);
            Assert.IsTrue(getResponse.Success);
        }

        [TestMethod]
        public void GetPersoonByDeelnemer()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/persoon/all/deelnemer/1/" + token).Body.AsString());

            Assert.IsNotNull(getResponse);
            Assert.IsTrue(getResponse.Success);
        }

        [TestMethod]
        public void GetPersoonByVereniging()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/persoon/all/vereniging/1/" + token).Body.AsString());

            Assert.IsNotNull(getResponse);
            Assert.IsTrue(getResponse.Success);
        }

        [TestMethod]
        public void GetTijdByPloeg()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/tijd/all/ploeg/1/" + token).Body.AsString());

            Assert.IsNotNull(getResponse);
            Assert.IsTrue(getResponse.Success);
        }

        [TestMethod]
        public void GetTijdByBlok()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/tijd/all/blok/1/" + token).Body.AsString());

            Assert.IsNotNull(getResponse);
            Assert.IsTrue(getResponse.Success);
        }

        [TestMethod]
        public void GetTijdByPloegBlok()
        {
            string token = Login();

            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // Get
            Result getResponse = JsonConvert.DeserializeObject<Result>(browser.Get("/tijd/all/ploeg/blok/1/1/" + token).Body.AsString());

            Assert.IsNotNull(getResponse);
            Assert.IsTrue(getResponse.Success);
        }
        #endregion
    }
}

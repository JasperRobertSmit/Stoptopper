using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Testing;
using Nancy;
using NancyAspNetHost.Modules;

namespace NancyTestProject
{
    [TestClass]
    public class NancyTest
    {
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
            var response = browser.Post("/Register", (with) => {
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
            var response = browser.Post("/Login", (with) => {
                with.HttpRequest();
                with.FormValue("username", "testuser");
                with.FormValue("password", "testpassword");
            });

            // Then
            string body = response.Body.AsString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void TestTime()
        {
            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // When
            var response = browser.Post("/SaveTime", (with) => {
                with.HttpRequest();
                with.FormValue("time", DateTime.Now.Ticks.ToString());
                with.FormValue("team", "testteam");
            });

            // Then
            string body = response.Body.AsString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetTeam()
        {
            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // When
            var response = browser.Get("/GetTeams");

            // Then
            string body = response.Body.AsString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void SaveTeam()
        {
            // Given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                var module = new TimeModule();
                with.Module(module);
            });
            Browser browser = new Browser(bootstrapper);

            // When
            var response = browser.Post("/SaveTeam", (with) => {
                with.HttpRequest();
                with.FormValue("teamname","testteam");
                with.FormValue("teamnumber", "1");
            });

            // Then
            string body = response.Body.AsString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

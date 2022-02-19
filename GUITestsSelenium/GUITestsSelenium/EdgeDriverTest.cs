using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Edge;
using System;

/*
Ni ska skriva f�ljande tre GUI-tester med Selenium:

    1.  L�gg till en anteckning och bekr�fta att den visas p� sidan.

    2.  L�gg till en anteckning och bekr�fta att sidan visar "1 item left". 
        Kryssa sedan i anteckningen och bekr�fta att sidan visar "0 items left".

    3.  L�gg till 3 anteckningar, kryssa i en av dem och bekr�fta att sidan visar "2 items left".
*/
namespace GUITestsSelenium
{
    [TestClass]
    public class EdgeDriverTest
    {
        private const string edgeDriverDirectory = @"C:\Program Files\H�mtade Program\edgedriver";
        private const string startUrl = "http://localhost:54503/index.html";
        private EdgeDriver browser;

        // This is run before each test.
        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            browser = new EdgeDriver(edgeDriverDirectory);
            // We want to go to the same URL for all tests.
            browser.Url = startUrl;
        }

        [TestMethod]
        public void CheckPageTitle()
        {
            // Check the page title.
            Assert.AreEqual("TicketForecast", browser.Title);
        }

        [TestMethod]
        public void TicketInListAfterBuying()
        {
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Click "Buy Ticket" for the 4th screening.
            var buttons = browser.FindElementsByCssSelector(".buy-ticket-button");
            var moneyballButton = buttons[3];
            moneyballButton.Click();

            // Find the title of the movie we clicked, so that we can use in the assertion later.
            var movieTitleNodes = browser.FindElementsByCssSelector("#screenings .movie-title");
            string selectedMovieTitle = movieTitleNodes[3].Text;

            // Check that the same title is now also in the ticket list.
            var ticketTitleNode = browser.FindElementByCssSelector("#tickets .movie-title");
            Assert.AreEqual(selectedMovieTitle, ticketTitleNode.Text);
        }

        // This is run after each test.
        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            browser.Quit();
        }
    }
}

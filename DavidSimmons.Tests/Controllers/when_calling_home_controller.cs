using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using DavidSimmons.Controllers;
using DavidSimmons.Client;
using DavidSimmons.Contracts;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DavidSimmons.Tests.Controllers
{
    [TestClass]
    public class when_calling_home_controller
    {
        //[TestMethod] TODO: Get this Test running agaIn
        public void index_should_list_front_page_entries()
        {
            IBlogClient mockedClient = MockRepository.GenerateMock<IBlogClient>();

            var controllerToTest = new HomeController(mockedClient);

            var entries = new List<BlogEntry>(new BlogEntry[] { new BlogEntry() { Title = "Dave 1" }, new BlogEntry() { Title = "Dave 2" } });

            mockedClient.Expect(c => c.GetFrontPageEntries()).Return(entries);

            var result = controllerToTest.Index() as ViewResult;

            mockedClient.VerifyAllExpectations();
            Assert.AreEqual(entries, result.ViewData.Model as List<BlogEntry>);
        }
    }
}

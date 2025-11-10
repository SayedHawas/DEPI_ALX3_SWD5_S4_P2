using Microsoft.AspNetCore.Mvc;
using MVCDemoLab.Controllers;
namespace UnitTestDemo
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestDiv()
        {
            //Arrage
            TestDemoController demo = new TestDemoController();
            //Act 
            double result = demo.div(110, 2);
            //Assert 
            Assert.AreEqual(55, result);
        }

        [TestMethod]
        public void TestIndex()
        {
            //Arrage
            TestDemoController demo = new TestDemoController();
            //Act 
            ViewResult result = demo.Index() as ViewResult;
            //Assert 
            Assert.AreEqual("Welcome From MVC ", result.ViewData["data"]);
        }

    }
}

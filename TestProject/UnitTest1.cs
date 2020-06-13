using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuLogic;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }
        [TestMethod]
        public void TestMethod1()
        {
            var x = new SudokuLogic.Class1();
            var y = x.HelloTest();
            TestContext.WriteLine($"the value is {y}");
            Assert.Fail(y);


        }

        [TestMethod]
        public async Task URLTest()
        {
            var targetURL = @"https://ironmaster.queue-it.net/?c=ironmaster&e=june11restock&t=https%3A%2F%2Fwww.ironmaster.com%2F%3Fmc_cid%3Da1a20e0f04%26mc_eid%3Dc6fe4dabb3&cid=en-US&l=Custom%20Layout";
            var y = new HttpClient();
            var result = await y.GetAsync(targetURL);
            TestContext.WriteLine(result.Content.ToString());
        }
    }
}

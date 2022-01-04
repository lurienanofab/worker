using Microsoft.VisualStudio.TestTools.UnitTesting;
using Worker.Client;

namespace Worker.Tests
{
    [TestClass]
    public class WorkerClientTests
    {
        [TestMethod]
        public void CanExecute()
        {
            var client = new WorkerClient();
            string response = client.Execute("UpdateToolDataClean", new[] { "2018-09-01", "0" });
            Assert.IsTrue(response.StartsWith("Enqueued command UpdateToolDataClean at "));
        }
    }
}

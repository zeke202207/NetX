using NetX.App;

namespace NetX.UnitTests._01_App
{
    public class AppTest
    {
        private void CreateHost()
        {
            ServerHost.Start(RunOption.Default);
        }

        public AppTest()
        {
            Task.Run(() => CreateHost());
            Thread.Sleep(5000);
        }

        [Fact]
        public void Test()
        {

        }
    }
}

namespace Okr.Service.App.Repository
{
    public class AppService:IAppService
    {
        public void Service1()
        {
            Thread.Sleep(10000);
        }

        public void Service2()
        {
            Thread.Sleep(20000);
        }

        public void Service3()
        {
            Thread.Sleep(30000);
        }

    }
}

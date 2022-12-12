using Okr.Service.Bus.Model;

namespace Okr.Service.Bus.Repository
{
    public interface IOkrServiceBus
    {
        string SendMessage(UserBusModel userBusModel);
        List<UserBusModel> Consumer(string queueName);
    }
}

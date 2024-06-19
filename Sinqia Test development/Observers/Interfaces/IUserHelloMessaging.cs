namespace Sinqia_Test_development.Observers.Interfaces
{
    public interface IUserHelloMessaging
    {
        List<string> NotifyAll(string message);
        void SubscribeUserObserver(IUserObserver userObserver);
        void UnSubscribeUserObserver(IUserObserver userObserver);
        string Notify(IUserObserver userObserver, string message);
    }
}

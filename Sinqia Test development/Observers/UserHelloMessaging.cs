using Sinqia_Test_development.Observers.Interfaces;

namespace Sinqia_Test_development.Observers
{
    public class UserHelloMessaging : IUserHelloMessaging
    {
        private readonly List<IUserObserver> observers = new List<IUserObserver>();

        public List<string> NotifyAll(string message)
        {
            var logObserver = new List<string>();
            foreach (var user in observers)
            {
                logObserver.Add(user.Update(message));
            }
            return logObserver;
        }

        public string Notify(IUserObserver userObservers,string message)
        {
            return userObservers.Update(message);
        }

        public void SubscribeUserObserver(IUserObserver userObserver)
        {
            observers.Add(userObserver);
        }

        public void UnSubscribeUserObserver(IUserObserver userObserver)
        {
            observers.Remove(userObserver);
        }
        public List<IUserObserver> GetObservers()
        {
            return observers;
        }
    }
}

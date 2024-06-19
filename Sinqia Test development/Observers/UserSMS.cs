using Sinqia_Test_development.Observers.Interfaces;

namespace Sinqia_Test_development.Observers
{
    public class UserSMS:IUserObserver
    {
        private readonly string _user;
        public UserSMS(string user)
        {
            _user = user;
        }
        public string Update(string messsage)
        { 
            return "Hello "+ _user + " ,  here's your SMS:" + messsage;
        }


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (UserSMS)obj;
            return _user == other._user;
        }

       
    }
}

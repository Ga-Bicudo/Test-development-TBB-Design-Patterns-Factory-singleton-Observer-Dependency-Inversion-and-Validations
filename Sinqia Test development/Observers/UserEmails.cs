using Sinqia_Test_development.Models;
using Sinqia_Test_development.Observers.Interfaces;
using System;

namespace Sinqia_Test_development.Observers
{
    public class UserEmails : IUserObserver
    {
        private readonly string _user;
        public UserEmails(string user)
        {
            _user = user;
        }
        public string Update(string messsage)
        {
            return "Hello " + _user + " ,  here's your Email:" + messsage;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (UserEmails)obj;
            return _user == other._user;
        }

       
    }
}

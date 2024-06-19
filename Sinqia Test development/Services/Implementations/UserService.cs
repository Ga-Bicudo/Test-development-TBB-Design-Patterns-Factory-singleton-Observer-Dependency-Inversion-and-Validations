using Sinqia_Test_development.Models;
using Sinqia_Test_development.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Sinqia_Test_development.Services.Implementations
{
    public class UserService : IUserService
    {
        public string GreetUser(string name)
        {
            return "Hello, " + name+"!";
        }
    }
}

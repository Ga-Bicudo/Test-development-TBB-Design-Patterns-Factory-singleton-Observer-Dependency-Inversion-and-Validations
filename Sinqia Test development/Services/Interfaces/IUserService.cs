using Sinqia_Test_development.Models;
using System.ComponentModel.DataAnnotations;

namespace Sinqia_Test_development.Services.Interfaces
{
    public interface IUserService
    {
        string GreetUser(string name);
    }
}

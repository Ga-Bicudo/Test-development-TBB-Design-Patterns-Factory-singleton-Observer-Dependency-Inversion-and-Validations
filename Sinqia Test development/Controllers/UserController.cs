using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sinqia_Test_development.Factories.implementation;
using Sinqia_Test_development.Models;
using Sinqia_Test_development.Observers;
using Sinqia_Test_development.Observers.Interfaces;
using Sinqia_Test_development.Services.Interfaces;
using Sinqia_Test_development.Singleton;
using System.ComponentModel.DataAnnotations;

namespace Sinqia_Test_development.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IUserHelloMessaging _userHelloMessaging;

        public UserController(IUserService userService, IUserHelloMessaging userHelloMessaging)
        {
            _userService = userService;
            _userHelloMessaging = userHelloMessaging;
        }

        [HttpPost]
        public IActionResult ValidateUser([FromBody] User user)
        {
            if (Validate(user, out var results))
            {
                return Ok("User is valid");
            }
            return BadRequest(results);
        }
        public static bool Validate(User user, out List<ValidationResult> results)
        {
            var context = new ValidationContext(user,  null,  null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(user, context, results, true);
        }

        [HttpGet("[action]/{userName}")]
        public IActionResult GreetUser(string userName)
        {
            return Ok(_userService.GreetUser(userName));
        }

        [HttpGet("Factory/[action]")]
        public IActionResult GetUserProductTypeAandB(string userProductType)
        {
            var type = new ProductFactory().CreateProduct(userProductType);
            return Ok(_userService.GreetUser(type.Hello()));
        }

        [HttpPost("Observer/[action]")]
        public IActionResult SubscribeUser([FromBody] string user)
        {
            _userHelloMessaging.SubscribeUserObserver(new UserSMS(user));
            _userHelloMessaging.SubscribeUserObserver(new UserEmails(user));
            return Ok("User Subscribed");

        }

        [HttpPost("Observer/[action]")]
        public IActionResult UnSubscribeUser([FromBody] string user)
        {
            _userHelloMessaging.UnSubscribeUserObserver(new UserSMS(user));
            _userHelloMessaging.UnSubscribeUserObserver(new UserEmails(user));
            return Ok("User Unsubscribed");

        }

        [HttpGet("Observer/[action]")]
        public IActionResult NotifyUsersSomeMessage(string NotifyUsersSomeMessage)
        {
            return Ok(_userHelloMessaging.NotifyAll(NotifyUsersSomeMessage));
        }

        [HttpPost("Singleton/[action]")]
        public IActionResult AcumularDadosSingleton(string AcumularDadosSingleton)
        {
            SingletonDataBase.Instance.Data += AcumularDadosSingleton;
            return Ok(SingletonDataBase.Instance);
        }


    }
}

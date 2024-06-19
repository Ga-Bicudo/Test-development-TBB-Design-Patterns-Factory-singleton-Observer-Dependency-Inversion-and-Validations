using Microsoft.AspNetCore.Mvc;
using Sinqia_Test_development.Controllers;
using Sinqia_Test_development.Models;
using Sinqia_Test_development.Observers.Interfaces;
using Sinqia_Test_development.Observers;
using Sinqia_Test_development.Services.Implementations;
using Sinqia_Test_development.Services.Interfaces;
using Sinqia_Test_development.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinquia_Test_project
{
    public class CombinedTests
    {
   
        private readonly UserController _userController;
        private readonly UserService _userService;
        private readonly UserHelloMessaging _userHelloMessaging;

        public CombinedTests()
        {

            _userService = new UserService();
            _userHelloMessaging = new UserHelloMessaging();
            _userController = new UserController(_userService, _userHelloMessaging);
        }

        [Fact]
        public void GreetUserService_ShouldReturnGreetingMessage()
        {
            var userName = "Alice";
            var result = _userService.GreetUser(userName);
            Assert.Equal("Hello, Alice!", result);
        }

        [Fact]
        public void SingletonInstance_ShouldReturnSameInstance()
        {
            
            var instance1 = SingletonDataBase.Instance;
            var instance2 = SingletonDataBase.Instance;

            
            Assert.Same(instance1, instance2);
        }

        [Fact]
        public void SingletonInstance_ShouldAccumulateData()
        {
            
            var singleton = SingletonDataBase.Instance;
            singleton.Data = "Initial data. ";

            
            singleton.Data += "New data.";

            
            Assert.Equal("Initial data. New data.", singleton.Data);
        }

        [Fact]
        public void ValidateUser_ShouldReturnOkIfValid()
        {
            
            var user = new User { Email = "test@example.com", Password = "password123" };

            
            var result = _userController.ValidateUser(user) as OkObjectResult;

            
            Assert.NotNull(result);
            Assert.Equal("User is valid", result.Value);
        }

        [Fact]
        public void ValidateUser_ShouldReturnBadRequestIfInvalid()
        {
            
            var user = new User { Email = "invalid-email", Password = "short" };

            
            var result = _userController.ValidateUser(user) as BadRequestObjectResult;

            
            Assert.NotNull(result);
            Assert.IsType<List<ValidationResult>>(result.Value);
        }

        [Fact]
        public void GreetUser_ShouldReturnGreeting()
        {
            
            var userName = "Alice";

            
            var result = _userController.GreetUser(userName) as OkObjectResult;

            
            Assert.NotNull(result);
            Assert.Equal("Hello, Alice!", result.Value);
        }

        [Fact]
        public void SubscribeUserObserver_ShouldAddObserver()
        {
            
            var userObserver = new UserSMS("Alice");

            
            _userHelloMessaging.SubscribeUserObserver(userObserver);

            
            Assert.Contains(userObserver, _userHelloMessaging.GetObservers());
        }

        [Fact]
        public void UnSubscribeUserObserver_ShouldRemoveObserver()
        {
            
            var userObserver = new UserSMS("Alice");
            _userHelloMessaging.SubscribeUserObserver(userObserver);

            
            _userHelloMessaging.UnSubscribeUserObserver(userObserver);

            
            Assert.DoesNotContain(userObserver, _userHelloMessaging.GetObservers());
        }

        [Fact]
        public void NotifyAll_ShouldNotifyAllObservers()
        {
            
            var userObserver1 = new UserSMS("Bob");
            var userObserver2 = new UserEmails("Bob");
            _userHelloMessaging.SubscribeUserObserver(userObserver1);
            _userHelloMessaging.SubscribeUserObserver(userObserver2);
            

            
            var notifications = _userHelloMessaging.NotifyAll("Hello, Users!");

            
            Assert.Contains("Hello Bob ,  here's your SMS:Hello, Users!", notifications);
            Assert.Contains("Hello Bob ,  here's your Email:Hello, Users!", notifications);
        }
    }
}

using FieldGroove.Domain.Models;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Configuration;
using Moq;
using FieldGroove.Application.validation;
using FieldGroove.Domain.Interfaces;


namespace FieldGrooveApi.Test
{
    public class AccountTest
    {
        private readonly RegisterValidator _RegisterValidator;
        private readonly LoginValidator _LoginValidator;
        private readonly Mock<IConfiguration> configuration;
        private readonly Mock<IUnitOfWork> unitOfWork;
        private readonly List<RegisterModel> registeredUsers;

        public AccountTest()
        {
            _RegisterValidator = new RegisterValidator();
            _LoginValidator = new LoginValidator();
            configuration = new Mock<IConfiguration>();
            unitOfWork = new Mock<IUnitOfWork>();
            registeredUsers = new List<RegisterModel>();

            unitOfWork.Setup(u => u.UserRepository.Create(It.IsAny<RegisterModel>()))
           .ReturnsAsync((RegisterModel model) =>
           {
               if (registeredUsers.Any(user => user.Email == model.Email))
               {
                   return false;
               }
               registeredUsers.Add(model);
               return true;
           });

            unitOfWork.Setup(u => u.UserRepository.IsValid(It.Is<LoginModel>(login =>
            login.Email == "test@gmail.com" && login.Password == "Test@123"))).ReturnsAsync(true);
        }

        private void InitializeDataBase()
        {
            registeredUsers.Clear();
            registeredUsers.Add(new RegisterModel
            {
                Email = "test@gmail.com",
                Password = "Test@123",
                PasswordAgain = "Test@123",
                CompanyName = "CIDC",
                FirstName = "Nithish",
                LastName = "sakthivel",
                Phone = 7904352633,
                City = "salem",
                State = "TamilNadu",
                StreetAddress1 = "Something",
                StreetAddress2 = "blah blah",
                TimeZone = "Mountain timeZone",
                Zip = "636139"
            });
        }

        [Fact]
        public async Task New_Register_Should_Return_True()
        {
            var RegisterData = new RegisterModel
            {
                Email = "test1@gmail.com",
                Password = "Test@123",
                PasswordAgain = "Test@123",
                CompanyName = "CIDC",
                FirstName = "Nithish",
                LastName = "sakthivel",
                Phone = 7904352633,
                City = "salem",
                State = "TamilNadu",
                StreetAddress1 = "Something",
                StreetAddress2 = "blah blah",
                TimeZone = "Mountain timeZone",
                Zip = "636139"
            };

            // Act
            bool response = await unitOfWork.Object.UserRepository.Create(RegisterData);

            // Assert
            Assert.True(response);

        }

        [Fact]
        public async Task User_Already_Registered_Should_Return_False()
        {
            //Arrange
            InitializeDataBase();
            var RegisterData = new RegisterModel
            {
                Email = "test@gmail.com",
                Password = "Test@123",
                PasswordAgain = "Test@123",
                CompanyName = "CIDC",
                FirstName = "Nithish",
                LastName = "sakthivel",
                Phone = 7904352633,
                City = "salem",
                State = "TamilNadu",
                StreetAddress1 = "Something",
                StreetAddress2 = "blah blah",
                TimeZone = "Mountain timeZone",
                Zip = "636139"
            };

            // Act
            var result = await unitOfWork.Object.UserRepository.Create(RegisterData);
            // Assert
            Assert.False(result);

        }

        [Fact]
        public void Register_Email_Validation()
        {
            var RegisterData = new RegisterModel
            {
                Email = "testgmail.com",
                Password = "Test@123",
                PasswordAgain = "Test@123",
                CompanyName = "CIDC",
                FirstName = "Nithish",
                LastName = "sakthivel",
                Phone = 7904352633,
                City = "salem",
                State = "TamilNadu",
                StreetAddress1 = "Something",
                StreetAddress2 = "blah blah",
                TimeZone = "Mountain timeZone",
                Zip = "636139"
            };

            var result = _RegisterValidator.TestValidate(RegisterData);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Register_Phone_Validation()
        {
            var RegisterData = new RegisterModel
            {
                Email = "test@gmail.com",
                Password = "Test@123",
                PasswordAgain = "Test@123",
                CompanyName = "CIDC",
                FirstName = "Nithish",
                LastName = "sakthivel",
                Phone = 79043,
                City = "salem",
                State = "TamilNadu",
                StreetAddress1 = "Something",
                StreetAddress2 = "blah blah",
                TimeZone = "Mountain timeZone",
                Zip = "636139"
            };

            var result = _RegisterValidator.TestValidate(RegisterData);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Register_Password_Validation()
        {
            var RegisterData = new RegisterModel
            {
                Email = "test@gmail.com",
                Password = "Test@123",
                PasswordAgain = "Test@1234",
                CompanyName = "CIDC",
                FirstName = "Nithish",
                LastName = "sakthivel",
                Phone = 7904352633,
                City = "salem",
                State = "TamilNadu",
                StreetAddress1 = "Something",
                StreetAddress2 = "blah blah",
                TimeZone = "Mountain timeZone",
                Zip = "636139"
            };

            var result = _RegisterValidator.TestValidate(RegisterData);
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task Registered_User_Login_Should_Return_True()
        {
            InitializeDataBase();

            var loginData = new LoginModel { Email = "test@gmail.com", Password = "Test@123" };

            // Act
            var result = await unitOfWork.Object.UserRepository.IsValid(loginData);

            // Assert
            Assert.True(result);

        }

        [Fact]
        public async Task Not_Registered_User_Login_Should_Return_False()
        {
            InitializeDataBase();

            var loginData = new LoginModel { Email = "test1@gmail.com", Password = "Test@123" };

            // Act
            var result = await unitOfWork.Object.UserRepository.IsValid(loginData);

            // Assert
            Assert.False(result);

        }

        [Fact]
        public void Login_Email_Validation()
        {
            InitializeDataBase();

            var loginData = new LoginModel { Email = "testgmail.com", Password = "Test@123" };
            var result = _LoginValidator.TestValidate(loginData);
            Assert.False(result.IsValid);

        }

        [Fact]
        public void Login_Password_Validation()
        {
            InitializeDataBase();

            var loginData = new LoginModel { Email = "testgmail.com", Password = "Test@123" };
            var result = _LoginValidator.TestValidate(loginData);
            Assert.False(result.IsValid);

        }
    }
}
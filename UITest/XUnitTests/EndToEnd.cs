using System.Diagnostics;
using UITest.PageObjects;
using UITest.TestDriver;
using UITest.TestEngine;
using Xunit.Abstractions;
using static System.Net.WebRequestMethods;

namespace XUnitTests
{

    public class EndToEnd
    {
        private ITestOutputHelper _Output;
        public EndToEnd(ITestOutputHelper output)
        {
            this._Output = output;
        }

        [Fact]
        public void TestUserRegisterAndSignInScenario()
        {
            //Generate Test Username and Password

            Random rnd = new Random();

            string _UserID = ((short)rnd.Next(1,9999)).ToString();
            string _Username = "User" + _UserID;
            string _Password = "password1234";
            string _LastName = "TestUser" + _UserID + "_LastName";
            string _StreetAddress = "123 Mocking Bird Lane";
            string _PhoneNumber = "999-999-9999";
            string _City = "Vancouver";
            string _Province = "BC";
            string _PostalCode = "postal";

            IDriver Driver = new SeleniumDriver(SeleniumDriver.DRIVERTYPE.CHROME);

            Engine engine = new Engine();

            LoginPage loginPage = new LoginPage(Driver);
            SignupPage signupPage = new SignupPage(Driver);
            DashboardPage dashboardPage = new DashboardPage(Driver);
            CreateApplicationPage createApplicationPage = new CreateApplicationPage(Driver);

            try
            {
                engine.ExecuteTest(new List<Func<bool>>()
                {
                    () => { _Output.WriteLine("Starting Test"); return(true); },
                    //Navigate to main page and Register
                    () => {_Output.WriteLine("loginPage.Navigate()"); return(loginPage.Navigate()); },

                    //Register New User
                    () => {_Output.WriteLine("signupPage.Navigate()"); return(signupPage.Navigate()); },
                    //() => { _Output.WriteLine("loginPage.signupButton.Click()"); loginPage.SignupButton.WaitFor(); return(loginPage.SignupButton.Click()); },
                    () => { _Output.WriteLine("signupPage.UserName.EnterText(" + _Username + ")"); signupPage.UserName.WaitFor(); return(signupPage.UserName.EnterText(_Username)); },
                    () => { _Output.WriteLine("signupPage.Password.EnterText(" + _Password + ")"); return(signupPage.Password.EnterText(_Password)); },
                    () => { _Output.WriteLine("signupPage.LastName.EnterText(" + _LastName + ")"); return(signupPage.LastName.EnterText(_LastName)); },
                    () => { _Output.WriteLine("signupPage.StreetAddress.EnterText(" + _StreetAddress + ")"); return(signupPage.StreetAddress.EnterText(_StreetAddress)); },
                    () => { _Output.WriteLine("signupPage.City.EnterText(" + _City + ")"); return(signupPage.City.EnterText(_City)); },
                    () => { _Output.WriteLine("signupPage.Province.EnterText(" + _Province + ")"); return(signupPage.Province.EnterText(_Province)); },
                    () => { _Output.WriteLine("signupPage.Province.EnterText(" + _PostalCode+ ")"); return(signupPage.PostalCode.EnterText(_PostalCode)); },
                    () => { _Output.WriteLine("signupPage.PhoneNumber.EnterText(" + _PhoneNumber + ")"); return(signupPage.Phone.EnterText(_PhoneNumber)); },
                    () => { _Output.WriteLine("signupPage.SubmitButton.WaitFor()"); return(signupPage.SubmitButton.WaitFor());  },
                    () => { _Output.WriteLine(Driver.GetCurrentURL()); return((Driver.GetCurrentURL().ToUpper() == "http://localhost:5002/Register".ToUpper()));  },

                    //Sleep for Submit button to be ready
                    () => {  Thread.Sleep(20000); return(true);  },

                    () => { _Output.WriteLine("signupPage.SubmitButton.Click()"); signupPage.SubmitButton.WaitFor(); return(signupPage.SubmitButton.Click()); },                    
                   
                    //Validate that signup was successful by verifying that the current page is now the login page and login
                    () => { _Output.WriteLine(Driver.GetCurrentURL()); return((Driver.GetCurrentURL().ToUpper() == "http://localhost:5002/login".ToUpper()));  },
 
                    () => { _Output.WriteLine("Sleep for 3 seconds for demo before continuing)"); Thread.Sleep(3000); return(true);  },

                    () => { _Output.WriteLine("loginPage.UserName.EnterText(_Username)");  loginPage.UserName.WaitFor(); return(loginPage.UserName.EnterText(_Username)); },
                    () => { _Output.WriteLine("loginPage.Password.WaitFor()");  return(loginPage.Password.WaitFor()); },
                    () => { _Output.WriteLine("loginPage.Password.EnterText(_Password)");  return(loginPage.Password.EnterText(_Password)); },
 
                    //Sleep for Submit button to be ready
                    () => {  Thread.Sleep(20000); return(true);  },

                    () => { _Output.WriteLine("loginPage.LoginButton.Click()");  return(loginPage.Signin.Click()) ; },

                    //Validate:
                    //Signin was successful by verifying that the current page is now the dashboard
                    () => { _Output.WriteLine(Driver.GetCurrentURL()); return((Driver.GetCurrentURL().ToUpper() == "http://localhost:5002/dashboard".ToUpper())); },

                    //Create an Application
                    () => { _Output.WriteLine("dashboardPage.AddApplicationButton.Click()");  return(dashboardPage.AddApplicationButton.Click()) ; },

                    () => { _Output.WriteLine("createApplicationPage.PropertyStreetAddress.EnterText");  createApplicationPage.PropertyStreetAddress.WaitFor(Seconds:2); return(createApplicationPage.PropertyStreetAddress.EnterText("123 Mockingbird lane")); },
                    () => { _Output.WriteLine("createApplicationPage.City.EnterText");  return(createApplicationPage.City.EnterText("Vancouver")); },
                    () => { _Output.WriteLine("createApplicationPage.Province.EnterText");  return(createApplicationPage.Province.EnterText("BC")); },
                    () => { _Output.WriteLine("createApplicationPage.PostalCode.EnterText");  return(createApplicationPage.PostalCode.EnterText("postal")); },
                    //DropDown is not yet implemented for Angular Combo Box
                    //() => { _Output.WriteLine("createApplicationPage.ZoningTypeDropDown.EnterText");createApplicationPage.ZoningTypeDropDown.WaitFor();  return(createApplicationPage.ZoningTypeDropDown.SelectFirstListItem()); },
                    () => { _Output.WriteLine("createApplicationPage.SquareFootage.EnterText");  return(createApplicationPage.SquareFootage.EnterText("5200")); },
                     //DropDown is not yet implemented for Angular Combo Box
                    //() => { _Output.WriteLine("createApplicationPage.STRAffiliateDropDown.SelectFirstListItem()");  return(createApplicationPage.STRAffiliateDropDown.SelectFirstListItem()); },
                    () => { _Output.WriteLine("createApplicationPage.IsPropertyOwnerPrimaryResidence.Click()");  return(createApplicationPage.IsPropertyOwnerPrimaryResidence.Click()); },

                    () => {  Thread.Sleep(15000); return(true);  },
                    //Disabling until Dropdown control is modified to work with Angular ComboBox since submit will fail
                    //() => { _Output.WriteLine("createApplicationPage.SubmitButton.Click()");  return(createApplicationPage.SubmitButton.Click()); },
                    () => { _Output.WriteLine("createApplicationPage.CancelButton.Click()");  return(createApplicationPage.CancelButton.Click()); },


                    ////Logout
                    () => {  Thread.Sleep(5000); return(true);  },
                    () => { _Output.WriteLine("dashboardPage.LogoutButton.Click()"); dashboardPage.LogoutButton.WaitFor(); return (dashboardPage.LogoutButton.Click()); },
                    () => { _Output.WriteLine("Ending Test"); return(true); },

               });
            }
            finally
            {
                Driver.Close();
            }
        }

    }
}
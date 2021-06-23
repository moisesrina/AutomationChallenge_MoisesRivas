using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit;

namespace AutomationChallenge_MoisesRivas.Steps
{
    [Binding]
    public class LoginPage
    {

        #region Variables
        private Actions actions;
        private ProductsPage productsPage;
        #endregion

        #region Constructor
        public LoginPage(Actions driver, ScenarioContext scenarioContext)
        {
            actions = driver;
            productsPage = new ProductsPage(driver, scenarioContext);
            InitializeWebDriver();
        }
        #endregion

        #region Driver
        public void InitializeWebDriver()
        {
            actions.SetupDriver(System.IO.Directory.GetCurrentDirectory(), "Chrome");
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        //Method to Quit & Dispose Driver
        [AfterScenario]
        public void AfterScenario()
        {
            actions.Quit();
        }
        #endregion

        #region Page Validation

        [Given(@"A web browser is at the login page")]
        public void GivenABrowserIsAtLoginPage()
        {
            actions.Driver.FindElement(By.Id("login-button"));
            actions.TakeSnapshot(true, "The login page is displayed");
            Assert.True(true, "The login page is displayed");
        }

        #endregion

        #region Actions Steps

        [When(@"The user enters correct authentication credentials into the login page")]
        public void WhenTheUserEntersCorrectCredentials()
        {
            Login();
        }

        [When(@"The user enters incorrect authentication credentials into the login page")]
        public void WhenTheUserEntersIncorrectCredentials()
        {
            IncorrectLogin();
        }

        [When(@"The user click the logout button")]
        public void WhenTheUserCLickLogoutButton()
        {
            Logout();
        }

        [Given(@"The user login and is at the products page")]
        public void GivenTheUserLoginAtProductsPage()
        {
            WaitUntilLoginPage_IsDisplayed();
            Login();
            productsPage.WaitUntilProductsPage_IsDisplayed();
        }

        #endregion

        #region Validation Steps

        [Then(@"The user validate the login page is displayed")]
        public void ThenTheUserValidateTheLoginPageIsDisplayed()
        {
            WaitUntilLoginPage_IsDisplayed();
        }

        [Then(@"The user validate login error message is displayed")]
        public void ThenTheUserValidateErrorMessageIsDisplayed()
        {
            string errorMessage = actions.Driver.FindElement(By.ClassName("error-message-container")).Text.ToString();
            Assert.Equal("Epic sadface: Username and password do not match any user in this service", errorMessage);
            Assert.True(true, "Error message is displayed");
        }

        #endregion

        #region Repository

        #region Object Repository
        public IWebElement UserName_Field => actions.Driver.FindElement(By.Id("user-name"));
        public IWebElement Password_Field => actions.Driver.FindElement(By.Id("password"));
        public IWebElement Login_Button => actions.Driver.FindElement(By.Id("login-button"));
        public IWebElement Menu_Button => actions.Driver.FindElement(By.Id("react-burger-menu-btn"));
        public IWebElement Logout_Button => actions.Driver.FindElement(By.Id("logout_sidebar_link"));

        #endregion

        #region Wait
        private void WaitUntilLoginPage_IsDisplayed()
        {
            actions.Driver.FindElement(By.Id("login-button"));
            actions.TakeSnapshot(true, "The login page is displayed");
            Assert.True(true, "The login page is displayed");
        }

        #endregion

        #region Actions Repository

        public void Login()
        {
            try
            {
                //Read & use excel data
                ReadingDataFromExcel(System.IO.Directory.GetCurrentDirectory());
                UserName_Field.SendKeys(ExcelOperations.ReadData(1, "Username"));
                Password_Field.SendKeys(ExcelOperations.ReadData(1, "Password"));
                Login_Button.Click();
            }
            catch (Exception e)
            {
                actions.TakeSnapshot(false, "Login error");
                Assert.True(false, "Login error: " + e.Message);
            }
        }

        public void IncorrectLogin()
        {
            try
            {
                //Read & use excel data
                ReadingDataFromExcel(System.IO.Directory.GetCurrentDirectory());
                UserName_Field.SendKeys(ExcelOperations.ReadData(2, "Username"));
                Password_Field.SendKeys(ExcelOperations.ReadData(2, "Password"));
                Login_Button.Click();
            }
            catch (Exception e)
            {
                actions.TakeSnapshot(false, "Login error");
                Assert.True(false, "Login error: " + e.Message);
            }
        }

        public void Logout()
        {
            try
            {
                Menu_Button.Click();
                Thread.Sleep(2000);
                Logout_Button.Click();
            }
            catch (Exception e)
            {
                actions.TakeSnapshot(false, "Logout error");
                Assert.True(false, "Logout error: " + e.Message);
            }
        }

        #endregion

        #region Validations Repository

        #endregion

        #region Private
        public void ReadingDataFromExcel(string proyectPath)
        {
            try
            {
                ExcelOperations.PopulateInCollection(proyectPath + @"\DataProviders\LoginData.xlsx");
            }
            catch (Exception e)
            {
                Assert.True(false, "Reading excel error: " + e.Message);

            }
        }
        #endregion

        #endregion

    }
}

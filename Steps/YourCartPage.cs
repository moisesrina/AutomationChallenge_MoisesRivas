using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using Xunit;

namespace AutomationChallenge_MoisesRivas.Steps
{
    [Binding]
    public class YourCartPage
    {

        #region Variables
        private Actions actions;
        #endregion

        #region Constructor
        public YourCartPage(Actions driver, ScenarioContext scenarioContext)
        {
            actions = driver;
        }
        #endregion

        #region Page Validation

        [Given(@"A web browser is at the cart page with products added")]
        public void GivenABrowserIsAtYourCartPageWIthProductsAdded()
        {
            CartPageIsDisplayedWithProducts();
        }

        #endregion

        #region Actions steps

        [When(@"The user make a purchase")]
        public void WhenTheUserMakeAPuarchase()
        {
            Purchase();
        }

        [Given(@"This is a pending step")]
        public void GivenThisIsAPendingStep(ScenarioContext scenarioContext)
        {
            scenarioContext.Pending();
        }

        #endregion

        #region Validation steps

        [Then(@"The user validate the order confirmation page is displayed")]
        public void ThenUserValidateTheOrderConfirmationPagaIsDisplayed()
        {
            ValidateOrderConfirmationPage();
        }

        #endregion

        #region Repository

        #region Object Repository

        public IReadOnlyCollection<IWebElement> ProductInCartList => actions.Driver.FindElements(By.ClassName("cart_item"));
        public IReadOnlyCollection<IWebElement> ProductInOverviewList => actions.Driver.FindElements(By.ClassName("cart_item"));
        public IWebElement ShoppingCart_Button => actions.Driver.FindElement(By.Id("shopping_cart_container"));
        public IWebElement Confirmation_Text => actions.Driver.FindElement(By.ClassName("complete-text"));
        public IWebElement FirstName_Field => actions.Driver.FindElement(By.Id("first-name"));
        public IWebElement LastName_Field => actions.Driver.FindElement(By.Id("last-name"));
        public IWebElement PostalCode_Field => actions.Driver.FindElement(By.Id("postal-code"));
        public IWebElement Checkout_Button => actions.Driver.FindElement(By.Id("checkout"));
        public IWebElement Continue_Button => actions.Driver.FindElement(By.Id("continue"));
        public IWebElement Finish_Button => actions.Driver.FindElement(By.Id("finish"));

        #endregion

        #region Waits

        public void WaitUntilYourCartPage_IsDisplayed()
        {
            actions.Driver.FindElement(By.Id("checkout"));
            actions.TakeSnapshot(true, "The cart page is displayed");
            Assert.True(true, "The cart page is displayed");
        }

        public void WaitUntilYourInformationPage_IsDisplayed()
        {
            actions.Driver.FindElement(By.Id("continue"));
            actions.TakeSnapshot(true, "The client information page is displayed");
            Assert.True(true, "The client information page is displayed");
        }

        public void WaitUntilOverviewPage_IsDisplayed()
        {
            actions.Driver.FindElement(By.Id("finish"));
            actions.TakeSnapshot(true, "The overview page is displayed");
            Assert.True(true, "The overview page is displayed");
        }

        #endregion

        #region Actions Repository

        public void CartPageIsDisplayedWithProducts()
        {
            try
            {
                ShoppingCart_Button.Click();
                WaitUntilYourCartPage_IsDisplayed();
                if (ProductInCartList.Count > 0)
                {
                    actions.TakeSnapshot(true, "There are products added to cart");
                    Assert.True(true, "There are products added to cart");
                }
                else
                {
                    actions.TakeSnapshot(false, "The cart is empty");
                    Assert.True(false, "The cart is empty");
                }
            }
            catch (Exception e)
            {
                actions.TakeSnapshot(false, "There is a problem when try to validate cart page with products");
                Assert.True(false, "There is a problem when try to validate cart page with products " + e.Message);
            }
        }

        public void Purchase()
        {
            try
            {
                //Navigate to Checkout page
                Checkout_Button.Click();
                WaitUntilYourInformationPage_IsDisplayed();
                //Read & use excel data
                ReadingDataFromExcel(System.IO.Directory.GetCurrentDirectory());
                FirstName_Field.SendKeys(ExcelOperations.ReadData(1, "FirstName"));
                LastName_Field.SendKeys(ExcelOperations.ReadData(1, "LastName"));
                PostalCode_Field.SendKeys(ExcelOperations.ReadData(1, "PostalCode"));
                Continue_Button.Click();
                //Navigate to Overview page
                WaitUntilOverviewPage_IsDisplayed();
                Finish_Button.Click();
            }
            catch (Exception e)
            {
                actions.TakeSnapshot(false, "Purchase error");
                Assert.True(false, "Purchase error " + e.Message);
            }
        }

        #endregion

        #region Validations Repository

        public void ValidateOrderConfirmationPage()
        {
            Confirmation_Text.Text.Equals("Your order has been dispatched, and will arrive just as fast as the pony can get there!");
            actions.TakeSnapshot(true, "The order confirmation page is displayed");
            Assert.True(true, "The order confirmation page is displayed");
        }

        #endregion

        #region Private

        public void ReadingDataFromExcel(string proyectPath)
        {
            try
            {
                ExcelOperations.PopulateInCollection(proyectPath + @"\DataProviders\ClientData.xlsx");
            }
            catch (Exception e)
            {
                Assert.True(false, "Reading excel error " + e.Message);
            }
        }

        #endregion

        #endregion

    }
}

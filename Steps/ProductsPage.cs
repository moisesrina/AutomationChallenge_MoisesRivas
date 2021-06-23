using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Xunit;

namespace AutomationChallenge_MoisesRivas.Steps
{
    [Binding]
    public class ProductsPage
    {

        #region Variables
        private Actions actions;
        #endregion

        #region Constructor
        public ProductsPage(Actions driver, ScenarioContext scenarioContext)
        {
            actions = driver;
        }
        #endregion

        #region Page Validation
        [Given(@"A web browser is at the products page")]
        public void GivenABrowserIsAtProductsPage()
        {
            WaitUntilProductsPage_IsDisplayed();
        }

        #endregion

        #region Actions steps

        [When(@"The user sort products by (.*)")]
        public void WhenTheUserSortProductsBy(string filter)
        {
            SortProductsBy(filter);
        }

        [When(@"The user add multiple items to the shopping cart")]
        public void WhenTheUserAddMultipleItemsToTheShoppingCart()
        {
            AddMultipleProductsToCart();
        }

        [When(@"The user add specific product to the shopping cart")]
        public void WhenTheUserAddSpecificProductToTheShoppingCart()
        {
            AddSpecificProductToCart();
        }

        #endregion

        #region Validation steps

        [Then(@"The user validate the products have been sorted by price correctly")]
        public void ThenTheUserValidatateTheProductsHaveBeenSortedByPriceCorrectly()
        {
            ValidateProductsHaveBeenSorted();
        }

        [Then(@"The user validate all the items have been added to the shopping cart")]
        public void ThenTheUserValidatateAllTheItemsHaveBeenAddedToCart()
        {
            ValidateProductsHaveBeenAddedToCart();
        }

        [Then(@"The user validate the correct product was added to the cart")]
        public void ThenTheUserValidatateTheCorrectProductWasAddedToCart()
        {
            ValidateSpecificProductWasAddedToCart();
        }

        [Then(@"The user validate the product page is displayed")]
        public void ThenTheUserValidatateTheProductPageIsDisplayed()
        {
            WaitUntilProductsPage_IsDisplayed();
        }

        #endregion

        #region Repository

        #region Object Repository

        public IWebElement FilterDropDown => actions.Driver.FindElement(By.ClassName("select_container"));
        public IReadOnlyCollection<IWebElement> InventoryContainerList => actions.Driver.FindElements(By.ClassName("inventory_list"));
        public IReadOnlyCollection<IWebElement> ProductsToAddList => actions.Driver.FindElements(By.ClassName("btn_inventory"));
        public IReadOnlyCollection<IWebElement> ProductsInCartList => actions.Driver.FindElements(By.ClassName("cart_item"));
        public IWebElement SpecificItem => actions.Driver.FindElement(By.Id("add-to-cart-sauce-labs-onesie"));
        public IWebElement Menu_Button => actions.Driver.FindElement(By.Id("menu_button_container"));
        public IWebElement ShoppingCart_Button => actions.Driver.FindElement(By.Id("shopping_cart_container"));
        public IWebElement ShoppingCart_Badge => actions.Driver.FindElement(By.ClassName("shopping_cart_badge"));
        public IWebElement InventoryItemName => actions.Driver.FindElement(By.ClassName("inventory_item_name"));

        #endregion

        #region Waits

        public void WaitUntilProductsPage_IsDisplayed()
        {
            actions.Driver.FindElement(By.Id("inventory_container"));
            actions.TakeSnapshot(true, "The products page is displayed");
            Assert.True(true, "The products page is displayed");
        }

        #endregion

        #region Actions Repository

        public void SortProductsBy(string filter)
        {
            try
            {
                SelectElement selection;

                FilterDropDown.Click();
                selection = new SelectElement(actions.Driver.FindElement(By.ClassName("product_sort_container")));
                selection.SelectByText(filter);
            }
            catch (Exception e)
            {
                actions.TakeSnapshot(false, "The sort actions fail");
                Assert.True(false, "The sort actions fail " + e.Message);
            }
        }

        public void AddMultipleProductsToCart()
        {
            try
            {
                foreach (IWebElement product in ProductsToAddList)
                {
                    product.Click();
                }
            }
            catch (Exception e)
            {
                actions.TakeSnapshot(false, "The products can't be added");
                Assert.True(false, "The products can't be added " + e.Message);
            }
        }

        public void AddSpecificProductToCart()
        {
            try
            {
                SpecificItem.Click();
            }
            catch (Exception e)
            {
                actions.TakeSnapshot(false, "The specific product was not added to cart");
                Assert.True(false, "The specific product was not added to cart " + e.Message);
            }
        }

        #endregion

        #region Validations Repository

        public void ValidateProductsHaveBeenSorted()
        {
            try
            {
                string firstItem = InventoryContainerList.FirstOrDefault().FindElement(By.ClassName("inventory_item_name")).Text;
                Assert.Equal("Sauce Labs Onesie", firstItem);
                actions.TakeSnapshot(true, "The product have been sorted correctly");
                Assert.True(true, "The product have been sorted correctly");
            }
            catch (Exception e)
            {
                actions.TakeSnapshot(false, "The products are not sorted");
                Assert.True(false, "The products are not sorted " + e.Message);
            }
        }

        public void ValidateProductsHaveBeenAddedToCart()
        {
            try
            {
                if (ShoppingCart_Badge.Displayed && ShoppingCart_Badge.Text.Contains("6"))
                {
                    ShoppingCart_Button.Click();
                    if (ProductsInCartList.Count.Equals(6))
                    {
                        actions.TakeSnapshot(true, "All the products have been to cart");
                        Assert.True(true, "All the products have been to cart");
                    }
                    else
                    {
                        actions.TakeSnapshot(false, "Not all the products were fin in the cart");
                        Assert.True(false, "Not all the products were fin in the cart");
                    }
                }
            }
            catch (Exception e)
            {
                actions.TakeSnapshot(false, "There is a problem when try to add all the products to cart");
                Assert.True(true, "There is a problem when try to add all the products to cart " + e.Message);
            }
        }

        public void ValidateSpecificProductWasAddedToCart()
        {
            try
            {
                if (ShoppingCart_Badge.Displayed && ShoppingCart_Badge.Text.Contains("1"))
                {
                    ShoppingCart_Button.Click();
                    if (ProductsInCartList.Count.Equals(1) && InventoryItemName.Text.Equals("Sauce Labs Onesie"))
                    {
                        actions.TakeSnapshot(true, "The specific product was added correctly");
                        Assert.True(true, "The specific product was added correctly");
                    }
                }
                else
                {
                    actions.TakeSnapshot(false, "The specific product was not added");
                    Assert.True(false, "The specific product was not added");
                }
            }
            catch (Exception e)
            {
                actions.TakeSnapshot(false, "There is a problem when try to add an specific product to cart");
                Assert.True(false, "There is a problem when try to add an specific product to cart " + e.Message);
            }
        }
        #endregion

        #endregion

    }
}

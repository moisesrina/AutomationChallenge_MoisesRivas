Feature: Products
	Products page scenarios

Background: 
	Given A web browser is at the login page
	When The user enters correct authentication credentials into the login page

#TestCase
#Created by Moises Rivas
@SmokeTest
Scenario: TC_Sort_products_by_price_(Low_To_High)
	Given A web browser is at the products page
	When The user sort products by Price (low to high)
	Then The user validate the products have been sorted by price correctly

#TestCase
#Created by Moises Rivas
@SmokeTest
Scenario: TC_Add_multiple_items_to_the_shooping_cart
	Given A web browser is at the products page
	When The user add multiple items to the shopping cart
    Then The user validate all the items have been added to the shopping cart

#TestCase
#Created by Moises Rivas
@SmokeTest
Scenario: TC_Add_the_specific_product_sauce_labs_onesie_to_the_shopping_cart
	Given A web browser is at the products page
	When The user add specific product to the shopping cart
	Then The user validate the correct product was added to the cart
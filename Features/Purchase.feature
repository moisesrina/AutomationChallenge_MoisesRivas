Feature: Purchase
	Purchase page scenarios

Background: 
	Given The user login and is at the products page
	When The user add multiple items to the shopping cart

#TestCase
#Created by Moises Rivas
@SmokeTest
Scenario: TC_Complete_a_purchase
	Given A web browser is at the cart page with products added
	When The user make a purchase
	Then The user validate the order confirmation page is displayed

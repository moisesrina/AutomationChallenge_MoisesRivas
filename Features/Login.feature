Feature: Login
	Login page scenarios

#TestCase
#Created by Moises Rivas
@SmokeTest
Scenario: TC_Login_with_a_valid_user
	Given A web browser is at the login page
	When The user enters correct authentication credentials into the login page
	Then The user validate the product page is displayed

#TestCase
#Created by Moises Rivas
@SmokeTest
Scenario: TC_Login_with_an_invalid_user
	Given A web browser is at the login page
	When The user enters incorrect authentication credentials into the login page
    Then The user validate login error message is displayed

#TestCase
#Created by Moises Rivas
@SmokeTest
Scenario: TC_Logout_from_the_home_page
	Given The user login and is at the products page
	When The user click the logout button
	Then The user validate the login page is displayed
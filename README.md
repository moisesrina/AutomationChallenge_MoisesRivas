# AutomationChallenge_MoisesRivas
Automation challenge - Project created by Moises Rivas Navarro
June 2021, Guadalajara, Jalisco, Mex.

This project was created in Selenium with Specflow using the best practices of POM & Gherkin syntax to develop the following 
7 automated scenarios based in a BDD methodology.

1.- Login with a valid user. / Validate the user navigates to the products page when logged in.  
2.- Login with an invalid user. / Validate error message is displayed.  
3.- Logout from the home page. / Validate the user navigates to the login page.  
4.- Sort products by Price (low to high). / Validate the products have been sorted by price correctly.  
5.- Add multiple items to the shopping cart. / Validate all the items that have been added to the shopping cart.  
6.- Add the specific product ‘Sauce Labs Onesie’ to the shopping cart. / Validate the correct product was added to the cart.  
7.- Complete a purchase. / Validate the user navigates to the order confirmation page.

In order to develop the project the following IDE and nuggets were required to be installed. 

IDE: 
-VS Community 2019.

Nuggets:
-DotNetSeleniumExtras.WaitHelpers (3.11.0).  
-ExcelDataReader (3.6.0).  
-ExcelDataReader.DataSet (3.6.0).  
-ExtentReports (4.1.0).  
-Microsoft.NET.Test.Sdk (16.10.0).  
-MSTest.TestAdapter (2.2.4).  
-MSTest.TestFramework (2.2.4).  
-NewtonsoftJson (13.0.1).  
-NUnit3TestAdapter (4.0.0).  
-Selenium.Support (3.141.0).   
-Selenium.WebDriver (3.141.0).   
-Specflow (3.8.14).   
-Specflow.Assist.Dynamic (1.4.2).   
-Specflow.NUnit	(3.8.14).  
-Specflow.Plus.LivingDocPlugin (1.4.2).  
-Specflow.Tools.MsBuild.Generation (3.8.14).  
-System.Configuration.ConfigurationManager (5.0.0).  
-System.Text.Encoding.CodePages (5.0.0).  
-xunit (2.4.1).  
-xunit.runner.visualstudio (2.4.3).  

To run de project, once the IDE is open, it must be compiled, this action will allow the test scenarios to be shown in the "Test Explorer" window (if the window is not shown, it can be activated in the toolbar Test/Test Explorer), when the test scenarios are displayed, can be executed by group or individualy following the next steps.  
1.- Select the test or group of tests with the mouse by left-click.  
2.- Rigth-click from the mouse to deploy the actions menu.  
3.- Click in Run/First option of the actions menu.

If is required to have all the specflow features, the specflow extention for VS 2019 must be downloaded and installed in the extentions window of the IDE.

-The project use excel archives as data providers.  
-The code follows the best practices including regions to make it more mantainable and try catch for each action method.  
-With the librery "ExtentReports", a TestReport is created automaticatly while the scenarios are running and a single 
file is created with the results (pass or fail) of each TestScenario and TestStep executed.
The TestReport is an html file created in the following route, (to open the TestReport is necesary to doble click the html file):

C:projectroot\bin\Debug\netcoreapp3.1\TestReport

-The proyect can be run in the newest versions of Chrome & Firefox Browsers.  
-The project also uses a method to take snapshots in the execution process, this snapshots are taken in all the asserts instructions and in 
all the catch exceptions in order to provide the screenshot of the validation, as a method it can be used wherever it's requiered, this snapshots are
saved in the following route: (if the route doesn't exist, it will be created in the first action TakeSnapshot event).

C:projectroot\bin\Debug\netcoreapp3.1\Snapshots.

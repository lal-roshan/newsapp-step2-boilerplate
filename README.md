# Seed code - Boilerplate for News-App Step2 Assignment

## Assignment Step Description

In this case study: News-App Step2, we will create an Application which accepts Title, Content, PublishedAt and Image(if any) as input from the front end and stores in a table in SQL Server. We also want to display the list of news(specific to the user) in tabular format on the same page just below the form.

`Here you are supposed to use Entity Framework Core Code first approach with Fluent API to generate database.
Any kind of configuration should only be configured using Fluent API.`

### Problem Statement

In this case study: News-App Step 2 we will create an application that requires us to implement four functionalities. They are as follows:

1. Design the database using EF Core code first with an exisiting record of a user.
2. Login route of your application should route to login view to let the user login to the application.
   (We'll use the existing user created during database creation, so no register feature required.).
3. Add new record of News using a form.
4. Displaying only Title and image of the News with a button to Delete. Title should be displayed as link to see the complete details of the News in separate View.
 
`Note: For detailed clarity on the class files, kindly go thru the Project Structure`

### Expected solution

- A form containing three text fields (Title,Content, PublishedAt), one file control for image upload and Add button.
- Right below to the form a table should display Title(in hyperlink form) and image of the News with a button to Delete. `This tabular view should be created and rendered as partial view.`
- When the user fills the data in form (image is optional) and clicks on Add button, it gets stored in the database and immediately added in the table displayed in the View.

### Steps to be followed:

    Step 1: Fork and Clone the boilerplate in a specific folder in your local machine. 
    Step 2: Define the models for the application using the classes in Models folder.
    Step 3: Provide the implementation for all the methods of IUserRepository and INewsRepository interface in respective classes.
    Step 4: Run the test cases for UserRepository class (UserRepositoryTest.cs) and NewsRepository class (NewsRepositoryTest.cs).
    Step 5: Define handler methods in LoginController and NewsController.
    Step 6: Deisgn the Views as per the requirement.
    Step 7: Run the testcases for LoginController and NewsController.
    Step 8: Run the application on a configured web server.

### Project structure

The folders and files you see in this repositories is how it is expected to be in projects, which are submitted for automated evaluation by Hobbes

```
📦News-Step-2
 ┣ 📂News-WebApp
 ┃ ┣ 📂Controllers
 ┃ ┃ ┣ 📜LoginController.cs //class to provide login route to the user
 ┃ ┃ ┗ 📜NewsController.cs  //class to provide route for news related 
 ┃ ┃                          functionality to the user
 ┃ ┣ 📂Models
 ┃ ┃ ┣ 📜News.cs //class for defining data structure for news
 ┃ ┃ ┣ 📜NewsDbContext.cs //class for defining EF core specific configuration
 ┃ ┃ ┗ 📜User.cs //class for defining data structure for User
 ┃ ┣ 📂Repository
 ┃ ┃ ┣ 📜INewsRepository.cs //Interface to define contract for News
 ┃ ┃ ┣ 📜IUserRepository.cs //Interface to define contract for User
 ┃ ┃ ┣ 📜NewsRepository.cs  //Implementation of INewsRepository
 ┃ ┃ ┗ 📜UserRepository.cs  //Implementation of IUserRepository
 ┃ ┣ 📂Views
 ┃ ┣ 📂wwwroot
 ┃ ┃ ┣ 📂css
 ┃ ┃ ┣ 📂images //folder to store the uploaded images for news
 ┃ ┃ ┣ 📂js
 ┃ ┣ 📜appsettings.json //to define application specific settings. e.g connection string
 ┃ ┣ 📜News-WebApp.csproj
 ┃ ┣ 📜Program.cs
 ┃ ┗ 📜Startup.cs //Registering required services for DI
 ┣ 📂test
 ┃ ┣ 📂ControllerTests
 ┃ ┃ ┣ 📜ControllersIntegrationTest.cs
 ┃ ┃ ┣ 📜LoginControllerTest.cs
 ┃ ┃ ┗ 📜NewsControllerTest.cs
 ┃ ┣ 📂RepositoryTests
 ┃ ┃ ┣ 📜NewsRepositoryTest.cs
 ┃ ┃ ┗ 📜UserRepositoryTest.cs
 ┃ ┣ 📜DatabaseFixture.cs //don't change this
 ┃ ┣ 📜NewsWebApplicationFactory.cs don't change this
 ┃ ┣ 📜PriorityOrderer.cs don't change this
 ┃ ┗ 📜test.csproj
 ┗ 📜News-Step-2.sln
 ```
# core-strength-yoga-products-management﻿

**Module Title:** Advanced Programming

**Module Code:** B8IT150

**Module Leader:** Paul Laird

**Student Name:** Susan Traynor

**Student Number:** 10621088

***Aim:***

The aim of the this assignment was to set up an information system using a back-end API with
database with a front end web application.
The chosen topic was a stock management system for a yoga products company.
In a previous module, I had completed the back end API system for an e commerce website for
Core Strength Yoga Products. I leveraged off this API as a starting point for the current assignment.
The assignment required the use of CRUD operations and at least two user groups with differing privileges.

***Requirements:***

The sytems should enable the user to create a profile with a role attached 
and dependent on that role it should allow the user to:
Update products
Add products 
Delete products
View stock reports
View sales reports

There should be four user groups. 
These are Product Executive, Product Manager, Business Analyst and Admin.
The role of the Product Executive within the company would be to update stock levels.
These users would have limited rights. 
They can view products but may only update stock levels on the product.

The Product Manager would have the same rights as the Product Executive.
They are also able to update product details other than stock level
They can add and delet products.

The Business Analyst cannot view products as the staff involved in product management can.
They have the ability to view reports in order to aid in business critical decisions.
The should entail details on stock changes and sales.

The above outlines the scope of this assignment. 

***Set-up and implementation:***

As stated previously, I had completed the back end API for a previous module.


**Packages to add & Commands to run:**

**Backend:**

dotnet add package Microsoft.AspNet.WebApi.Core

dotnet add package Microsoft.AspNetCore. Wuthentication.JwtBearer

dotnet add package Microsoft.ApsNetCore.Identity.EntityFrameworkCore

dotnet add package Microsoft.AspNetCore.Identity.UI

dotnet add package Microsoft.AspNetCore.Open.Api

dotnet add package Microsoft.EntityFrameworkCore

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet add package Microsoft.EntityFrameworkCore.Sqlite

dotnet add package Microsoft.EntityFrameworkCore.Tools

dotnet add package Swashbuckle.AspNetCore

dotnet add package System.IdentityModel.Tokens.Jwt

dotnet ef database update

**Frontend:**

dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore

dotnet add package Microsoft.AspNetCore.Identity.UI

dotnet add package Microsoft.Data.Sqlite.Core

dotnet add package Microsoft.EntityFrameworkCore.Sqlite

dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet add package Microsoft.EntityFrameworkCore.Tools

dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design

dotnet add package NonFactors.Grid.Mvc6

dotnet ef database update

API must be run prior to running the app

Within the start up of the API, data will be seeded and the there will be data generated for orders
and stock changes. 
The data generation was necessary in order to have the reports functionality.
Below are the details which are currently in the system. These can be altered if necessary in appsettings.json.

![dataGenerationAppSettings](https://github.com/traynosm/core-strength-yoga-products-management/assets/105241393/3e93ed36-b2ac-4772-9253-c2eacbe4c859)

Four users (information below), a customer, 25 products, orders and stock changes will be seeded.

***Instructions for use:***

The flow of the web app has been made as user friendly as possible to prevent incorrect input.
There are drop down menus wherever possible to reduce manual input.

**Login:**

There are four users prepopulated in the back end seed data which can be used for login.
You may also create your own username(email) and password.
It is set up so that when an account is being set up, the user selects their own user rights.
In a real life scenario, this would be completed by IT and the user would be in the active directory.

The four users all have the same password: Testing123!

Their username is based on their role.

Product Executive: productexecutive@email.com

Product Manager: productmanager@email.com

Business Analyst: businessanalyst@email.com

Admin: admin@email.com

The admin user was kept in as a user for practical reasons when testing 
but it would also exist in a real world environment.

Upon login, a JWT token is created for that session. 
You will be automatically logged out on its expiration. On closing the browser, the token also expires.

**Product Executive use:**

Once logged in you will be directed to the landing page where your user role is displayed.
You may view and edit all products by navigating via the products tab in the navigation bar.

i![ProdExec login 1](https://github.com/traynosm/core-strength-yoga-products-management/assets/105241393/c0de8c04-ca5f-457d-b958-df0ac22a4003)

This user does not have access to reports.
An access denied message will display if they click on the reports tab.

Once in products, the list of products will be displayed. 
These products can be filtered by product type.
The Product Executive can enter the edit space.
The only area they are able to edit is the stock level as this is their job within the organisation.
Currently they executive can update multiple fields. This would need to be secured in the next version.

As stock level is all that can be edited by this user once the stock level has been changed the update button
is clicked. The change can be viewed on the products index page.

**Product Manager use:**

Same as Product Executive but with additional functionality.
The manager can Edit all aspects of the products except for Image.
Once an image has been added the product would need to be deleted in order to change it.
This would need to be altered in future versions.

The Product Attributes of gender, size and colour cannot be edited. The product must be deleted if any of 
these attributes needs to be changed. This is an area for development.

The Manager can also add and delete products to and from the database.
These features are accessed from the product index page.
In order to add a product, the manager with select add new product.
They will be redirected to a page where they should enter the details necessary 
for the overarching product.

![addProdDetails](https://github.com/traynosm/core-strength-yoga-products-management/assets/105241393/b0da0c97-16c9-4188-a47e-e1a30d767249)


Upload an image by first selecting the image from a directory on their computer.

![chooseFile](https://github.com/traynosm/core-strength-yoga-products-management/assets/105241393/8c540004-ad00-4f20-bdc7-837f30986d52)

Then clicking the Upload Image button.

![uploadImage](https://github.com/traynosm/core-strength-yoga-products-management/assets/105241393/a5b6b805-5671-4b6f-b6aa-3edd1b3d60a8)

 They can then add the attributes.

 ![addNewAttribute](https://github.com/traynosm/core-strength-yoga-products-management/assets/105241393/f29f37c9-3176-4379-b88f-8ea54f9b7480)

 Once this is completed the click update and it is added to the database.

 The delete function is accessed through the product index page.

 Business Analyst use:

 The login experience is the same for this user.

 They cannot view/edit/add/delete products.

 They can view reports by clicking the Reports tab in the navigation bar.

 This leads to two report options: 
 Stock Audits where changes to stock can be reviewed in terms of stock in and out
 Sales Audits where orders over a period of time can be reviewed.
 This section of the application uses MvcGrid.

 The stock level changes report is paginated.
 The changed at, product Id, Product Attr Id, Product details and changed by fields are all filterable and
 sortable.

 They are filtered using the A button next to the header in the table.

 ![Filter](https://github.com/traynosm/core-strength-yoga-products-management/assets/105241393/0591b15c-1ce0-4eec-a023-74166d0534b1)

 They are sortable using the button to the left of the filter .

 Sort on:

 ![SortOn](https://github.com/traynosm/core-strength-yoga-products-management/assets/105241393/0c8f0987-625d-466e-878d-20dd3d73db3d)

 Sort off:

 ![SortOff](https://github.com/traynosm/core-strength-yoga-products-management/assets/105241393/09672005-cc68-4390-b8a1-fa01b9d33cf4)

 When the business analyst clicks on the product details, they can view a line chart showing changes in
 stock level(y-axis) over time(x-axis).

![lineGraph](https://github.com/traynosm/core-strength-yoga-products-management/assets/105241393/a76325e3-bb1a-4af1-99e3-41edd4b267ed)

 The sales reports has and additional feature to the stock Audit report in that it has a footer at the end
 of the table which displays the total value for all sales orders.
 In order to display this particular item which was felt would be useful within a sales report, the pagination
 was removed as it only totalled the amount on display on a given page.
 
 Notes: 
 
 The original concept for the reports was to use dataframes however while attempting to implement this feature
 I came across MvcGrid which provided most of the features needed in the style of report I wanted.
 It could have been utilised in the product management aspect of this project also.
 It was left as is to show the evolution of the project and it also give a clear distinction of which element
 of the app you are utilising as the styles are quite different.

 **Link to Repo:**

 API: https://github.com/traynosm/core-strength-yoga-products-api
 
 App: https://github.com/traynosm/core-strength-yoga-products-management


 References:
 
 https://www.w3schools.com/howto/howto_css_loader.asp 
 
 Used in adding the loading feature when buttons are clicked
 
 https://aspnet-core-grid.azurewebsites.net/
 
 MvcGrid reference. Used multiple pages in here including:
 
 Quickstart, Installation, Change log, Formatting, Encoding, Fitlering, Sorting, Paging & Footer




 


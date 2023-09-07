﻿Module Title: Advanced Programming

Module Code: B8IT150

Module Leader: Paul Laird

Student Name: Susan Traynor

Student Number: 10621088

Aim:

The aim of the this assignment was to set up an information system using a back-end API with
database with a front end web application.
The chosen topic was a stock management system for a yoga products company.
In a previous module, I had completed the back end API system for an e commerce website for
Core Strength Yoga Products. I leveraged off this API as a starting point for the current assignment.
The assignment required the use of CRUD operations and at least two user groups with differing privileges.

Requirements:

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

Set-up and implementatio:
As stated previously, I had completed the back end API for a previous module.
Scripts to run:







Within the start up of the API, data will be seeded and the there will be data generated for orders
and stock changes. 
The data generation was necessary in order to have the reports functionality.
Below are the details which are currently in the system. These can be altered if necessary in appsettings.json

***img: dataGenerationAppSettings***

Four users will be seeded(information below) as well as information for 25 products.

Instructions for use:

The flow of the web app has been made as user friendly as possible to prevent incorrect input.
There are drop down menus wherever possible to reduce manual input.

Login:
There are four users prepopulated in the back end seed data which can be used for login.
You may also create your own username(email) and password.
It is set up so that when an account is being set up, the user selects their own user rights.
In a real life scenario, this would be completed by IT and the user would be in the active directory.
The four users all have the same password: Testing123!
Their username is based on their role.
Product Executive: productexecutive@email.com
Product Manager: productmanager@email.com
Business Analyst: businessanalyst@email.com
Admin: Admin@email.com
The admin user was kept in as a user for practical reasons when testing 
but it would also exist in a real world environment.

Upon login, a JWT token is created for that session which expires after XXmin. 
You will be automatically logged out on its expiration but you can close the browser and remain signed
in until the token expires.

Product Executive use:
Once logged in you will be directed to the landing page where your user role is displayed.
You may view and edit all products by navigating via the products tab in the navigation bar.
img: prod exec login 1

This user does not have access to reports.
An access denied message will display if they click on the reports tab.

Once in products, the list of products will be displayed. 
These products can be filtered by product type.
The Product Executive can enter the edit space.
The only area they are able to edit is the stock level as this is their job within the organisation.

***All other functionality has been greyed out.***

As stock level is all that can be edited by this user once the stock level has been changed the update button
is clicked. The change can be viewed on the products index page

Product Manager use:
Same as Product Executive but with additional functionality.
The manager can Edit all aspects of the products except for Image.
Once an image has been added the product would need to be deleted in order to change it.
This would need to be altered in future versions.

***The Product Attributes of gender, size and colour cannot be edited. The product must be deleted if any of 
these attributes needs to be changed. This is an area for development.***

The Manager can also add and delete products to and from the database.
These features are accessed from the product index page.
In order to add a product, the manager with select add new product.
They will be redirected to a page where they should enter the details necessary 
for the overarching product

***img:addProdDetails***

Upload an image by first selecting the image from a directory on their computer

***img:chooseImage***

Then clicking the Upload Image button.

***img:uploadImage***
 They can then add the attributes.
 ***img:addAttributes

 Once this is completed the click update and it is added to the database.

 The delete function is accessed through the product index page.

 Business Analyst use:

 The login experience is the same for this user.

 They cannot view/edit/add/delete products.

 They can view reports by clicking the Reports tab in the navigation bar.

 This leads to two report options: 
 Stock Audits where changes to stock can be reviewed in terms of stock in and out
 Sales Audits where orders over a period of time can be reviewed.
 This section of the application uses MvcGrid

 The stock level changes report is paginated.
 The changed at, product Id, Product Attr Id, Product details and changed by fields are all filterable and
 sortable.

 They are filtered using the A button next to the header in the table.
 ***img:Filter***

 They are sortable using the button to the left of the filter button
 ***img: sortOn*** ***img: sortOff***

 When the business analyst clicks on the product details, they can view a line chart showing changes in
 stock level(y-axis) over time(x-axis)

 The sales reports has and additional feature to the stock Audit report in that it has a footer at the end
 of the table which displays the total value for all sales orders.
 In order to display this particular item which was felt would be useful within a sales report, the pagination
 was removed as it only totalled the amount on display on a given page

 ***img: lineGraph***
 


 Notes: 
 The original concept for the reports was to use dataframes however while attempting to implement this feature
 I came across MvcGrid which provided most of the features needed in the style of report I wanted.
 It could have been utilised in the product management aspect of this project also.
 It was left as is to show the evolution of the project and it also give a clear distinction of which element
 of the app you are utilising as the styles are quite different.

 References:
 https://www.w3schools.com/howto/howto_css_loader.asp 
 Used in adding the loading feature when buttons are clicked
 https://aspnet-core-grid.azurewebsites.net/
 MvcGrid reference. Used multiple pages in here including:
 Quickstart, Installation, Change log, Formatting, Encoding, Fitlering, Sorting, Paging & Footer




 

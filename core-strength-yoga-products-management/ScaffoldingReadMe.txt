Support for ASP.NET Core Identity was added to your project.

For setup and configuration information, see https://go.microsoft.com/fwlink/?linkid=2116645.

Module Title: Advanced Programming

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

Set-up:
As stated previously, I had completed the back end API for a previous module.
Scripts to run:





Instructions for use:

Login:
There are four users prepopulated in the back end seed data which can be used for login.
You may also create your own username(email) and password.
It is set up so that when an account is being set up, the user selects their own user rights.
In a real life scenario, this would be completed by IT and the user would be in the active directory.
The four users all have the same password: Testing123!
Their username is based on their role.
Product Executive: ProductExecutive@email.com
Product Manager: ProductManager@email.com
Business Analyst: BusinessAnalyst@email.com
Admin: Admin@email.com
The admin user was kept in as a user for practical reasons when testing 
but it would also exist in a real world environment.

Upon login, a JWT token is created for that session.

Product Executive use:
Once logged in you will be directed to the landing page where your user role is displayed.
You may view and edit all products by navigating via the products tab in the navigation bar.









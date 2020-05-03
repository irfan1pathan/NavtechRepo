                                
 						OrderManagementSystem
						======================


Technologies
============
Asp.net Core
Sql Server


Used Json Technology for the two way communication ((i.e) API to DB and DB to API) because it will help to reduce the creation of models for all stored procedures and processing the data faster


Tasks
=====
1.Create Tables,Stored Procedures for OrderManagement tables
2.Create Web rest APIs for Products,Users,Orders Functionalities
3.Create Global Exceptional Handling Technique
4.Create Role Based Access for the Roles such as Admin,Users using JWT Token authentication

=============================================================================================================================

Description of Tasks
====================


1.Create Tables,Stored Procedures for OrderManagement tables

Table Names:
------------

Users       :    Which contains Users Information including Role of that particular user
Products    :    Contains Products related information columns in this table
Order       :    Contains Orders related information where Users have selected their appropriate orders

========================================================================================================================

2.Create Web rest APIs for Products,Users,Orders Functionalities


Controllers:
------------

UserAdminController
ProductsController
OrderManagementController

Iinterfaces:IBL
--------------

IProducts
Iuser
IOrderService

Implementation Class:BL
-----------------------

Products
user
OrderService


==================================================================================================================

3.Create Global Exceptional Handling Technique


In BL Folder I have created one class in which i have implemented the Try catch block and initialized to middleware
ClassName : Errorhandling.cs

===================================================================================================================

4.Create Role Based Access for the Roles such as Admin,Users using JWT Token authentication

I have used JWT Roles Based Authentication for Accessing the methhod based on role

Controller
----------
userController


Interface
---------
IUserAuth


Implementation
--------------
UserAuth.cs

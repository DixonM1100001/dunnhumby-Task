# ProductDashboard Management Guide

This project is a simple Product Dashboard Management containing a frontend to display and interact with Product Data and a REST API to manage products.

## Installing / Getting started

Please install the following software, following the guides for the operating system you are using:
- SQLite
- Visual Studio (or alternatives)
- Visual Studio Code (or alternatives)
- .NET 8
- Node.js

Once installed, please clone the 'dunnhumby-Task' repo by running the following command:
```shell
git clone https://github.com/DixonM1100001/dunnhumby-Task.git
```

### Initalising the Database

The repository you have cloned will contain a Database folder. This contains a DB script called 'ProductManagement.db' which has both the schema for a table 'products' and some initial data.
You can open this in SQLite to view the data easily. It is referenced in the API solution with the correct folder structure. 

### Building the Frontend project

The Frontend project will be found in the UI folder of the repository you have cloned.
To start building this project please open the folder 'product-management-dashboard > src' inside the UI folder and run the following commands:

```shell
npm install
npm start
```

There is also a README.md file in the UI folder with more details of available scripts for this React App

### Building the API project

The API project will be found in the API folder of the repository you have cloned. Please navigate into the folder 'ProductmanagementAPI' and then open the ProductManagementAPI.sln.
To start building this project, simply press the build button for the 'ProductManagementAPI' project. 
Whilst running the API in development mode, a SwaggerUI will be launched with interactive documentation of the available API requests.
You can also view the API Documentation section below for similar details.

## API Documentation

The ProductManagementAPI supports two publicly exposed APIs that do not need any authentication to access and no rate limiting or other limitations.
There is an API so list all the products in your database, and an API to create a product.

### Schemas
The API uses the following schemas and are referenced in the section below. 
- Category
```shell 
	Enum: 
	[ Food, Electronics, Clothes ]
```
- Product
```shell
{
	category	Categorys string 
			Enum: Array [ 3 ]
	name		string
	productCode	string
	price		number($double)
	priceSterling	string 
			readOnly: true
	sku		string
	stockQuantity	integer($int32)
	dateAdded	string($date-time)
}
```
- ProductRequest
```shell
{
	category	Category string
			Enum: Array [ 3 ]
	name	        string
	productCode	string
	price	        number($double)
	sku	        string
	stockQuantity	integer($int32)
}
```

### Endpoints

1) GET api/product \
This API endpoint allows the user to see all of the products in their database. The endpoint does not take any Parameters or a Request Body. 
If the GET is successful, a 200 Status code, Ok, will be returned along with a Response. The Response scehma will look like the following example value of the Product Schema in an array:
```shell
[
  {
    "category": "Food",
    "name": "string",
    "productCode": "string",
    "price": 0,
    "priceSterling": "string"
    "sku": "string",
    "stockQuantity": 0,
    "dateAdded": "2024-09-24T15:35:38.467Z"
  }
]
```

If the GET fails because of an internal error, a 500 status code will be returned along with the error message.

2) POST api/product \
This API endpoint allows the user to create a product and add it to their database. The endpoint does not take any Parameters but does take a Request body that will look like the following example value of the ProductRequest Schema:
```shell 
{
  "category": "Food",
  "name": "string",
  "productCode": "string",
  "price": 0,
  "sku": "string",
  "stockQuantity": 0
}
```
If the POST is successful, a 200 Code will be returned along with the Response Schema that will look like the following example value of the Product Schema:
```shell
{
  "category": "Food",
  "name": "string",
  "productCode": "string",
  "price": 0,
  "priceSterling": "string"
  "sku": "string",
  "stockQuantity": 0,
  "dateAdded": "2024-09-24T15:47:21.741Z"
}
```
The dateAdded property will be the DateTime of the request and inserted automatically during the creation process.

If the POST fails due to validation errors, a 422 status code, UnprocessableEntity, will be returned along with the a Response Schema that is an array of a list of errors and looks like the following example value:
```shell
[
  "'Name' must not be empty.",
  "'Product Code' must not be empty."
]
``` 

If the POST fails because of an internal error, a 500 status code will be returned along with the error message.

### Tools
The 'ProductManagementAPI' project launches a SwaggerUI that you can use to try out the above APIs. Alternatively, you can use Postman or similar API software.

## Docker

This project contains script to create images and containers of the application.
You can access the frontend UI and backend API service and SwaggerUI. However, I cannot get it to connect to the database file as written above and so the docker images do not fully work and you cannot test the application through docker.
The purpose of this section is to illistrate what I have done. 

### Creating the images
In the repository you have pulled down, there is a docker-compose.yml file. This file references a backend.dockerfile (in API/ProductManagement directory) and a frontend.dockerfile (in UI/project-management-dashboard directory).
To run and create the docker images, please follow these steps:
1. Download Docker Desktop for your OS and sign in/continue without signing in
2. Open a Powershell or CMD and run the following command in the directory where the docker-compose.yml file exists.
```shell
docker-compose -f docker-compose.yml up
```
If this is the first time running it, the script may take a while. \
3. Once the images are built, you can open https:localhost:3000 for the UI and http:localhost:7030/index.html for the SwaggerUI page for the API. (Note that as no certificate has been created for https, docker will only allow http)

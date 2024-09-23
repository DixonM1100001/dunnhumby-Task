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
To start building this project please open the folder 'product-management-dashboard > src' inside the UI folder and run the following commands

```shell
npm install
npm start
```

There is also a README.md file in the UI folder with more details of available scripts for this React App

### Building the API project

The API project will be found in the API folder of the repository you have cloned. Please navigate into the folder 'ProductmanagementAPI' and then open the ProductManagementAPI.sln.
To start building this project, simply press the build button for the 'https' project. 
Whilst running the API in development mode, a Swagger UI will be launched with interactive documentation of the available API requests.
You can also view the API Documentation section below for similar details.

## API Documentation

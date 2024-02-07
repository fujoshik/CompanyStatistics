# CompanyStatistics  

## Overview
This project is an ASP.NET Core-based REST API about a company statistics system where users can register, create, read and update companies, download PDF with information about them, view company statistics. The main purpose of this application is to read CSV files with data about organizations and store it in a database. The program automatically checks for new files to read every six hours and every day at midnight creates a JSON file with statistics about the previous day.  
The project supports two roles: **Admin** and **Regular**.

## Authentication  
CompanyStatistics uses API keys for authentication. Users need to include their API key in the request headers for authentication purposes.  

### Authentication Header
```http  
Authorization: Bearer YOUR_API_KEY
```

## Roles
- The **Admin** role has access to absolutely everything on the program - they can view, create, delete and update all objects on the API for which such functionality is implemented. Only the Admin is able to delete companies and users.   
- **Regular** users are able to create, update and read companies and also download a PDF with information about a certain company.  
- Users without registration on the platform are only able to view statistics for companies and read the information about them.  

## Error Handling
The API handles various error scenarios gracefully, providing meaningful error messages and appropriate HTTP status codes to clients.

### Routes  
- #### Authentication  
    - **POST api/authentication/register** -> Register   
    - **POST api/authentication/login** -> Login  

- #### Company 
    - **GET api/companies/read-data** -> Endpoint manually reading the CSV files in the directory and saving the information in the database  
    - **POST api/companies** -> Endpoint for creating a company
    - **PUT api/companies/{id}** -> Endpoint for updating a company by provided id  
    - **GET api/companies/{id}** -> Endpoint for getting a company by provided id
    - **GET api/companies** -> Endpoint getting all companies from the database  
    - **DELETE api/companies/{id}** -> Endpoint for deleting a company by id *(available only for the ADMIN role)*  

- #### User (available only for the ADMIN role)  
    - **POST api/users** -> Endpoint for creating a user
    - **PUT api/users/{id}** -> Endpoint for updating a user by provided id  
    - **GET api/users/{id}** -> Endpoint for getting a user by provided id
    - **GET api/users** -> Endpoint getting all users in the database  
    - **DELETE api/users/{id}** -> Endpoint for deleting a user by id  

- #### Statistics   
    - **GET api/statistics/employee-count-by-industry** ->  Returns the employee count of each industry *(cached response)*  
    - **GET api/statistics/get-top-n-companies-by-employee-count** ->  Gets the first top N companies with the highest most employees *(cached response)*  
    - **GET api/statistics/group-companies-by-country-and-industry** -> Filters the companies by provided country and/or industry and returns the first 10 *(cached response)*  

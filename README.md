# Movie Db Web API 

## Technologies
- Entity Framework Core – Code First
- MsSql
- Repository Pattern
- Fluent Validation
- Automapper
- JWT Authentication,Refresh Token
- Redis Cache 
- MassTransit + RabbitMQ
- Docker


## Introduction

this is an API project developed using Net 7, Sql Server, RabbitMQ, Redis and Docker.

## Auth:
JWT Refresh Token mechanism has been developed with the asp net identity library.


## Endpoint

Movies:
- Movie list: An endpoint where all movies can be taken page by page. The page size is taken as a parameter.
- Viewing movies with Id: The average score, the score given by the user and the notes added are displayed together with the movie information.
- Adding notes and points to a selected movie: The note is taken as text. The score must be an integer between 1-10.
- Recommend Selected Movie: An e-mail is sent to a given e-mail address.

Auth:
- UserRegister: Required information for User Registration is requested and a new user is created.
- Login: Users registered in the system send their login information to the system. Returns token information.
- RefreshTokenLogin: With the login endpoint, the refresh token value is returned and with this value, the system is logged in again without requiring a login.

## Redis Cache

It is used to store Movies information. When the query is made, it tries to fetch the values from Redis Cache first, if there is no Cache, it pulls it from the Db and saves the value to the Cache.


## RabbitMq

Movie Suggestions are sent via Email and a queue structure is used for Emails. Email sending request is added to the queue and then this queue is listened by a method and sending is done. Success/Failed transaction logs are saved in a folder under "wwwroot" for tracking mail transactions.

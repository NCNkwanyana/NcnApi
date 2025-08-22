#Assessmenet Tasks API 
An ASP.NET CORE Wen API for task and user management. Tasks can be filtered by assigned user or due date and users. Tasks can be created , read, patch and deleted.JWt authentication is implemeted for security endpoints.

#Installation 
1.Check whether you have ".NET 9 SDK" installed in ypur PC &
-SQL server or SQLite/EF core
-Git
2.Copy this repository: http://github.com/NCNkwanyana/NcnApi.git
3.Open the project folder : cd NcnApi
4.Restore dependencies : dotnet restore
5. To create SQLite database , applu databse migrations: dotnet ef dtabase update

## Feaatures
1.User registration  and Login
CRUD operations for Users & Tasks
2.Task filtering:-Expired tasks
                 -Active tasks
                 -Tasks by date
                 -Tasks by Assigned user

#Running the API
 In the project folder, execute the following command: dotnet run
 *It will default and start at : http://localhost:5203
 *To test the endpoints , open Swagger  at :  http://localhost:5203/swagger
 *Once Swagger is open , use Registration endpoint to register user in the system: Username:"" , email: "", password:""
 *Use the login endpoint to get your Jwt token: email:"put your email that you registered with",
'password:"put your password that you registered with"
  if successful you will recieve a response with your Jwt token.
 *Copy the token select Authorize in swagger then paste it : Bearer "paste your token".
 *All endpoints (usr/tk) are protected  and require JWT tokens to access.

 ## API Endpoints
 ### TK
 -'GET/api/tk'- Get all tasks
 -'GET/api/tk'- Get tasks by ID
 -POST/api/tk'- Create a new task
 -'PUT/api/tk/{id}'- Update task
 -'DELETE/api/tk/{id}'- Delete task 
 -'GET/api/tk/exprired'- Get expired tasks
 -'GET/api/tk/active'- Get active tasks
 -'GET/api/tk/date/{date}'- Get tasks by date 
 -'GET/api/tk/user/{userId}'- Get tasks by assigned user

### Usr
-'GET/api/usr' -Get all users
-'GET/api/usr/{id}'- Get user by ID
-'POST/api/usr'- Create new user
-'PUT/api/usr/{id}'- Update User
-'DELETE/api/usr/{id}'-Delete User

##Database
Entity Framework Core and SQLite are used in this project. The 'UsersTasks.db' database file generates automatically within the project folder. CRUD activities are done using EF Core.

By NCNkwanyana

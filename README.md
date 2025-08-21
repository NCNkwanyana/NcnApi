#Assessmenet Tasks API 
An ASP.NET CORE Wen API for task and user management. Tasks can be filtered by assigned user or due date and users. Tasks can be created , read, patch and deleted.

#Installation 
1.Check whether you have ".NET 9 SDK" installed in ypur PC.
2.Copy this repository: http://github.com/NCNkwanyana/NcnApi.git
3.Open the project folder : cd NcnApi
4.Restore dependencies : dotnet restore
5. To create SQLite database , applu databse migrations: dotnet ef dtabase update

#Running the API
 In the project folder, execute the following command: dotnet run
 *It will default and start at : http://localhost:5203
 *To test the endpoints , open Swagger  at :  http://localhost:5203/swagger

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

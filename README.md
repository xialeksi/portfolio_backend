# portfolio_backend
asp.net backend for my portfolio app

uses a MySQL database to store data about projects and images

## how to use

add a file called MyEnvironment.cs with this code snippet in it and set the values as they are for you
```
namespace portfolio_backend
{
    public static class MyEnvironment
    {
        public static void SetMySQLConnention(){
             System.Environment.SetEnvironmentVariable("DATABASE_URL", "server=127.0.0.1;user id=USERNAME;password=PASSWORD;port=3306;database=DATABASENAME;");}
    }
}
```
<!--remember to add this:

namespace portfolio_backend
{
    public static class MyEnvironment
    {
        public static void SetMySQLConnention(){
             System.Environment.SetEnvironmentVariable("DATABASE_URL", "server=127.0.0.1;user id=USERNAME;password=PASSWORD;port=3306;database=DATABASENAME;");}
    }
} 

also, Iguess I forgot to add .gitignore, but you should ditch the .vs,obj and bin I think-->



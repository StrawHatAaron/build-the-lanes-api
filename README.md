# Build The Lanes - CSc 174 Group Project



## To Rebuild the whole database do the following

1. Delete the files in the **Migrations** directory

   ```ba
   dotnet ef migrations remove
   ```

2. Delete the entire remote database **webapp**.

   ```bash
   dotnet ef database drop
   ```

3. Adds a new migration with the name **Initial**.

   ```ba
   dotnet ef migrations add Initial
   ```

4. To autogenerate the **sql** file/script.

   ```bash
   dotnet ef migrations script -o initial.sql
   ```

5. Updates the database to the last migration or to a specified migration.

   ```ba
   dotnet ef database update
   ```




Donators.js EngineerCertifications.js EngineerDegrees.js Projects.js Responsibilities.js Staffs.js ApplicableStandards.js Donates





# **Scaffold Database**

```bash
Scaffold-DbContext "<ConnectionString>" Microsoft.EntityFrameworkCore.SqlServer -t <tablename> -f

```



```bash
dotnet ef dbcontext scaffold "Data Source=build-the-lanes-0.cz837oegnsiw.us-west-1.rds.amazonaws.com,1433;Initial Catalog=webapp;User id=admin;Password=test1234" Microsoft.EntityFrameworkCore.SqlServer --context-dir Helpers --output-dir Models -t Donates -t ApplicableStandards -f
```











## Reverse Engineer the Database maybe? Will have to try maybe

This could be an answer to my prayers. I could possible take the whole data model from the SQL Marina and Hecotr write

```ba
dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chinook" Microsoft.EntityFrameworkCore.SqlServer
```


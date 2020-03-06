using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;


namespace BuildTheLanesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        
        public ProjectController()
        {

        }

        [HttpPost]
        public HttpResponseMessage PostProjectItem()
        {
            //open the connection
            using var con = new MySqlConnection(Database.cs);
            Console.WriteLine(Database.cs);
            con.Open();

            try
            {
                //Get the MySQL Version - Its 8
                Console.WriteLine($"MySQL version : {con.ServerVersion}");
                //do a test Insertion
                using var cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO Project(project_num, start_date, status, city, zip_code) VALUES (2, '2020-03-6', 'NEW', 'Rocklin', '95765')";
                cmd.ExecuteNonQuery();
                con.Close();
                return new HttpResponseMessage(System.Net.HttpStatusCode.Created);
            }
            catch(Exception e)
            {
                con.Close();
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
            
        }
    }
}

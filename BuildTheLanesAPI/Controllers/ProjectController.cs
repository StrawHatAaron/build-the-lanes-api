using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;


namespace BuildTheLanesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {


        [HttpGet("{ProjectNumber}")]
        public HttpResponseMessage GetProject(long ProjectNumber)
        {
            using var con = new MySqlConnection(Database.cs);
            Console.WriteLine(Database.cs);
            con.Open();
            try
            {
                using var cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"SELECT * FROM Project WHERE project_num = {ProjectNumber};";
                cmd.ExecuteNonQuery();
                con.Close();
                return new HttpResponseMessage(System.Net.HttpStatusCode.Created);
            }
            catch(Exception e)
            {
                Console.Write(e);
                con.Close();
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            }

        }

        [HttpPost]
        public HttpResponseMessage PostProject([FromBody] Project project)
        {
            //open the connection
            using var con = new MySqlConnection(Database.cs);
            Console.WriteLine(Database.cs);
            con.Open();
            try
            {
                Console.WriteLine($"Project zip code: {project.ZipCode}");
                //do a test Insertion
                using var cmd = new MySqlCommand();
                cmd.Connection = con;
                string SQLText = $"INSERT INTO Project(project_num, start_date, status, city, zip_code) " +
                        $"VALUES ({project.ProjectNumber},  '{project.StartDate}',  '{project.Status}',  '{project.City}', '{project.ZipCode}');";
                Console.WriteLine(SQLText);
                cmd.CommandText = SQLText;
                cmd.ExecuteNonQuery();
                con.Close();
                return new HttpResponseMessage(System.Net.HttpStatusCode.Created);
            }
            catch(Exception e)
            {
                Console.Write(e);
                con.Close();
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
            
        }
    }
}

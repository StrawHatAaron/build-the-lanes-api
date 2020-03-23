using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BuildTheLanesAPI.Controllers
{
    [Route(Constants.api+"/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        public IConfiguration Configuration { get; }

        public ProjectController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Project> projectList = new List<Project>();

            //string connectionString = Constants.cs;
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            Console.WriteLine("connectionString: "+connectionString);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //SqlDataReader
                connection.Open();

                string sql = "Select * From Project";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    Console.WriteLine("Made it here");
                    while (dataReader.Read())
                    {
                        Project project = new Project();
                        Console.WriteLine("dataReader:" + dataReader);
                        project.ProjectNumber = Convert.ToInt32(dataReader["project_num"]);
                        project.StartDate = Convert.ToString(dataReader["start_date"]);
                        project.Status = Convert.ToString(dataReader["status"]);
                        project.City = Convert.ToString(dataReader["city"]);
                        project.ZipCode= Convert.ToString(dataReader["zip_code"]);
                        projectList.Add(project);
                    }
                }

                connection.Close();
            }
            //Console.WriteLine("I have been callllllleeeeddddd!!!");
            return Ok(projectList);
        }



        [HttpGet("{ProjectNumber}")]
        public HttpResponseMessage GetProject(long ProjectNumber)
        {
            using var con = new MySqlConnection(Constants.cs);
            Console.WriteLine(Constants.cs);
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
            using var con = new MySqlConnection(Constants.cs);
            Console.WriteLine(Constants.cs);
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

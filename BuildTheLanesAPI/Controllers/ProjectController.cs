using System;
using Microsoft.AspNetCore.Mvc;
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
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Project";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Project project = new Project();
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
            return Ok(projectList);
        }



        [HttpGet("{ProjectNumber}")]
        public IActionResult GetProject(int ProjectNumber)
        {
            Project project = new Project();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"SELECT * FROM Project WHERE project_num={ProjectNumber}";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        project.ProjectNumber = Convert.ToInt32(dataReader["project_num"]);
                        project.StartDate = Convert.ToString(dataReader["start_date"]);
                        project.Status = Convert.ToString(dataReader["status"]);
                        project.City = Convert.ToString(dataReader["city"]);
                        project.ZipCode = Convert.ToString(dataReader["zip_code"]);
                    }
                }
                connection.Close();
            }
            return Ok(project);
        }

        

        [HttpPost]
        public IActionResult PostProject([FromBody] Project project)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"INSERT INTO Project(start_date, status, city, zip_code) " +
                        $"VALUES ('{project.StartDate}',  '{project.Status}',  '{project.City}', '{project.ZipCode}');";
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteReader();
                connection.Close();
            }
            return Ok(project);
        }
    }
}

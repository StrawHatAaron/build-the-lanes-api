using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using BuildTheLanesAPI.Services;
using BuildTheLanesAPI.Entities;
using BuildTheLanesAPI.Models;

namespace BuildTheLanesAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route(Constants.api + "/[controller]")]    
    public class ProjectController : Controller
    {
        public IConfiguration Configuration { get; }
        private readonly string connectionString;
        private IUserService _userService;

        public ProjectController(IConfiguration configuration, IUserService userService)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _userService = userService;
        }


        [Authorize(Roles = Roles.User)]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Project> projectList = new List<Project>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
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
                            project.ZipCode = Convert.ToString(dataReader["zip_code"]);
                            projectList.Add(project);
                        }
                    }
                    connection.Close();
                    return Ok(projectList);
                }
                catch(SqlException ex)
                {
                    connection.Close();
                    return BadRequest(ex);
                }

            }
            
        }



        [HttpGet("{ProjectNumber}")]
        [Authorize(Roles = Roles.User)]
        public IActionResult GetProject(int ProjectNumber)
        {
            Project project = new Project();
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
        [Authorize(Roles = Roles.Admin)]
        public IActionResult PostProject([FromBody] Project project)
        {
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


        [HttpDelete]
        public IActionResult DeleteProject(int project_num)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"DELETE FROM Project WHERE project_num='{project_num}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        //ViewBag.Result = "Operation got error:" + ex.Message;
                    }
                    connection.Close();
                }
            }

            return RedirectToAction("Index");
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using BuildTheLanesAPI.Services;
using BuildTheLanesAPI.Entities;
using BuildTheLanesAPI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildTheLanesAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route(Constants.api + "/[controller]")]
    public class DonatorController : Controller
    {
        public IConfiguration Configuration { get; }
        private readonly string connectionString;
        private IUserService _userService;

        public DonatorController(IConfiguration configuration, IUserService userService)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _userService = userService;
        }

        [Authorize(Roles.Admin)]
        [HttpGet]
        public IActionResult GetAllStaff()
        {
            List<Donator> donatorList = new List<Donator>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    string sql = "SELECT * FROM Donator;";
                    SqlCommand command = new SqlCommand(sql, connection);
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Donator donator = new Donator();
                            donator.Email = Convert.ToString(dataReader["email"]);
                            donator.Password = Convert.ToString(dataReader["password"]);
                            donator.Token = Convert.ToString(dataReader["token"]);
                            donator.FirstName = Convert.ToString(dataReader["f_name"]);
                            donator.LastName = Convert.ToString(dataReader["l_name"]);
                            donator.Roles = Convert.ToString(dataReader["roles"]);
                            donator.AmountDonated = Convert.ToDecimal("amount_donated");
                            donatorList.Add(donator);
                        }
                    }
                    connection.Close();
                    return Ok(donatorList);
                }
                catch (SqlException ex)
                {
                    connection.Close();
                    return BadRequest(ex);
                }

            }
        }

        [Authorize(Roles.Staff)]
        [HttpGet("{email}")]
        public IActionResult GetDonator(string email)
        {
            Donator donator = new Donator();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"SELECT * FROM Donator WHERE email={email}";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        donator.Email = Convert.ToString(dataReader["email"]);
                        donator.Password = Convert.ToString(dataReader["password"]);
                        donator.Token = Convert.ToString(dataReader["token"]);
                        donator.FirstName = Convert.ToString(dataReader["f_name"]);
                        donator.LastName = Convert.ToString(dataReader["l_name"]);
                        donator.Roles = Convert.ToString(dataReader["roles"]);
                        donator.AmountDonated = Convert.ToDecimal("amount_donated");
                    }
                }
                connection.Close();
            }
            return Ok(donator);
        }

        [Authorize(Roles.Staff)]
        [HttpPost]
        public IActionResult PostDonator([FromBody] Donator donator)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    //there is a trigger in database that ensures a insertion into Donator table
                    string sql = $"INSERT INTO User(email, password, token, f_name, l_name, roles, amount_donated) " +
                        $"VALUES ('{donator.Email}', '{donator.Password}', '{donator.Token}', '{donator.FirstName}'," +
                        $"{donator.LastName}, '{donator.Roles}', '{donator.AmountDonated}');";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteReader();
                    connection.Close();
                    return Ok(donator);
                }
                catch(Exception e)
                {
                    connection.Close();
                    return BadRequest(e);
                }  
            }
        }

        [HttpDelete]
        public IActionResult DeleteDonator(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"DELETE FROM Donator WHERE email='{email}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                        connection.Close();
                        return Ok();
                    }
                    catch (SqlException e)
                    {
                        connection.Close();
                        return BadRequest(e);
                    }
                }
            }
        }
    }
}

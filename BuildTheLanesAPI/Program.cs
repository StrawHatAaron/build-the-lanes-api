using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace BuildTheLanesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Connection String 
            string cs = @"server=csc174.cjr83nh0ihji.us-west-1.rds.amazonaws.com;userid=admin;password=rook-bide-bamboo-eng-exit-reenact;database=csc174";
            //open the connection
            using var con = new MySqlConnection(cs);
            con.Open();

            //Get the MySQL Version - Its 8
            Console.WriteLine($"MySQL version : {con.ServerVersion}");
            //do a test Insertion
            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Project(project_num, start_date, status, city, zip_code) VALUES (1, '2020-03-6', 'NEW', 'Rocklin', '95765')";
            cmd.ExecuteNonQuery();


            //From the todo item API tutorial I think 
            CreateHostBuilder(args).Build().Run();
        }



        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System;
using CourseRequest.Models;
using System.Data.SqlClient;

namespace CourseRequest.Controllers
{
    public class RequestController : Controller
    {

        public IActionResult RequestList()
        {
            List<Request> requests = GetRequestsFromDB();
            return View("~/Views/Home/RequestList.cshtml", requests);
        }

        private readonly IConfiguration _configuration;

        public RequestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        private List<Request> GetRequestsFromDB()
        {
            List<Request> requests = new List<Request>();

            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Requests";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Request request = new Request
                    {
                        
                        FullName = reader.GetString(0),
                        Department = reader.GetString(1),
                        Position = reader.GetString(2),
                        CourseName = reader.GetString(3),
                        CourseType = reader.GetString(4),
                        Notation = reader.GetString(5),
                        Status = reader.GetString(6),
                        CourseBeginning = reader.GetDateTime(7),
                        CourseEnd = reader.GetDateTime(8),
                        Year = reader.GetInt32(9)
                    };

                    requests.Add(request);
                }

                reader.Close();
            }

            return requests;
        }


    }
}

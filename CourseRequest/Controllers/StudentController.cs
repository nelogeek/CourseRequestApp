using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System;
using CourseRequest.Models;
using System.Data.SqlClient;

namespace CourseRequest.Controllers
{
    public class StudentController : Controller
    {

        public IActionResult RequestList()
        {
            List<Student> students = GetStudentsFromDB();
            return View("~/Views/Home/RequestList.cshtml", students);
        }

        private readonly IConfiguration _configuration;

        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        private List<Student> GetStudentsFromDB()
        {
            List<Student> students = new List<Student>();

            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Students";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Student student = new Student
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Age = reader.GetInt32(3),
                        Email = reader.GetString(4)
                    };

                    students.Add(student);
                }

                reader.Close();
            }

            return students;
        }

    }
}

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
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        FullName = reader.GetString(reader.GetOrdinal("full_name")),
                        Department = reader.GetString(reader.GetOrdinal("department")),
                        Position = reader.GetString(reader.GetOrdinal("position")),
                        CourseName = reader.GetString(reader.GetOrdinal("course_name")),
                        CourseType = reader.GetString(reader.GetOrdinal("course_type")),
                        Notation = reader.GetString(reader.GetOrdinal("notation")),
                        Status = reader.GetString(reader.GetOrdinal("status")),
                        CourseBeginning = reader.GetDateTime(reader.GetOrdinal("course_beginning")),
                        CourseEnd = reader.GetDateTime(reader.GetOrdinal("course_end")),
                        Year = reader.GetInt32(reader.GetOrdinal("year")),
                        Username = reader.GetString(reader.GetOrdinal("user"))
                    };



                    requests.Add(request);
                }

                reader.Close();
            }

            return requests;
        }

        [HttpPost]
        public IActionResult CreateRequest(Request request)
        {
            if (ModelState.IsValid)
            {
                // Добавить код для сохранения данных в базу данных
                // Используйте объект request для доступа к значениям полей формы

                // Пример кода для сохранения данных в базу данных с использованием System.Data.SqlClient:
                string connectionString = _configuration.GetConnectionString("connectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Requests (full_name, department, position, course_name, course_type, notation, status, course_beginning, course_end, year, [user]) " +
                                   "VALUES (@FullName, @Department, @Position, @CourseName, @CourseType, @Notation, @Status, @CourseBeginning, @CourseEnd, @Year, @User)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FullName", request.FullName);
                    command.Parameters.AddWithValue("@Department", request.Department);
                    command.Parameters.AddWithValue("@Position", request.Position);
                    command.Parameters.AddWithValue("@CourseName", request.CourseName);
                    command.Parameters.AddWithValue("@CourseType", request.CourseType);
                    command.Parameters.AddWithValue("@Notation", request.Notation);
                    command.Parameters.AddWithValue("@Status", request.Status);
                    command.Parameters.AddWithValue("@CourseBeginning", request.CourseBeginning);
                    command.Parameters.AddWithValue("@CourseEnd", request.CourseEnd);
                    command.Parameters.AddWithValue("@Year", request.Year);
                    command.Parameters.AddWithValue("@User", request.Username);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        // Успешно сохранено
                        return RedirectToAction("Index"); // Перенаправление на страницу списка заявок или другую страницу
                    }
                    else
                    {
                        // Ошибка при сохранении
                        // Вернуть представление с сообщением об ошибке или выполнить другую логику обработки ошибки
                        return View("Error");
                    }
                }
            }
            else
            {
                // Некорректные данные формы, вернуть представление с сообщением об ошибке или выполнить другую логику обработки ошибки
                return View("Error");
            }
        }



    }
}

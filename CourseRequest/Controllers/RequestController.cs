using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System;
using CourseRequest.Models;
using System.Data.SqlClient;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;

namespace CourseRequest.Controllers
{
    public class RequestController : Controller
    {
        private readonly IConfiguration _configuration;

        public RequestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult RequestList()
        {
            List<RequestOut> requests = GetRequestsFromDB();
            return View("~/Views/Home/RequestList.cshtml", requests);
        }

        private string GetCurrentUser()
        {
            WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();
            string userName = currentIdentity?.Name ?? "Неизвестно";
            return userName;
        }

        private int GetRequestCountByUser(string userName)
        {
            string connectionString = _configuration.GetConnectionString("connectionString");
            int requestCount = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Requests WHERE [user] = @UserName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserName", userName);
                requestCount = (int)command.ExecuteScalar();
            }

            return requestCount;
        }

       
        private List<RequestOut> GetRequestsFromDB()
        {
            List<RequestOut> requests = new List<RequestOut>();

            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Requests";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    RequestOut request = new RequestOut
                    {
                        Id = (int)reader["id"],
                        FullName = (string)reader["full_name"],
                        Department = (string)reader["department"],
                        Position = (string)reader["position"],
                        CourseName = (string)reader["course_name"],
                        CourseType = GetCourseTypeName( (int)reader["course_type"]), 
                        Notation = (string)reader["notation"],
                        Status = GetStatusName( (int)reader["status"]),
                        CourseStart = (DateTime)reader["course_start"],
                        CourseEnd = (DateTime)reader["course_end"],
                        Year = (int)reader["year"],
                        Username = (string)reader["user"]
                    };

                    requests.Add(request);
                }

                reader.Close();
            }

            return requests;
        }

        private string GetCourseTypeName(int typeId)
        {
            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT type FROM Type WHERE id = @TypeId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TypeId", typeId);
                string typeName = (string)command.ExecuteScalar();
                return typeName;
            }
        }

        private string GetStatusName(int statusId)
        {
            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT status FROM Status WHERE id = @StatusId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StatusId", statusId);
                string statusName = (string)command.ExecuteScalar();
                return statusName;
            }
        }


        [HttpPost]
        public IActionResult CreateRequest(Request request)
        {
            if (ModelState.IsValid)
            {
                string connectionString = _configuration.GetConnectionString("connectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Requests (full_name, department, position, course_name, course_type, notation, status, course_start, course_end, year, [user]) " +
                                   "VALUES (@FullName, @Department, @Position, @CourseName, @CourseTypeId, @Notation, @StatusId, @CourseStart, @CourseEnd, @Year, @User)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FullName", request.FullName);
                    command.Parameters.AddWithValue("@Department", request.Department);
                    command.Parameters.AddWithValue("@Position", request.Position);
                    command.Parameters.AddWithValue("@CourseName", request.CourseName);
                    command.Parameters.AddWithValue("@CourseTypeId", request.CourseTypeId); // Используем новое свойство CourseTypeId
                    command.Parameters.AddWithValue("@Notation", request.Notation);
                    command.Parameters.AddWithValue("@StatusId", request.StatusId); // Используем новое свойство StatusId
                    command.Parameters.AddWithValue("@CourseStart", request.CourseStart.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@CourseEnd", request.CourseEnd.ToString("yyyy-MM-dd"));
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

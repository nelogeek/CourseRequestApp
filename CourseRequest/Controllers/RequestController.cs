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
            /*List<RequestOut> requests = GetRequestsFromDB();*/
            List<RequestOut> requests = GetFilteredRequestsFromDB(GetCurrentUser());
            return View("~/Views/Home/RequestList.cshtml", requests);
        }

        private string GetCurrentUser()
        {
            WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();
            string userName = currentIdentity?.Name ?? "Неизвестно";
            return userName;
        }

        /*private int GetRequestCountByUser(string userName)
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
        }*/


        private List<RequestOut> GetRequestsFromDB()
        {
            List<RequestOut> requests = new List<RequestOut>();

            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Requests ORDER BY id DESC";
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
                        CourseType = GetCourseTypeName((int)reader["course_type"]),
                        Notation = reader["notation"].ToString(),
                        Status = GetStatusName((int)reader["status"]),
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
                    command.Parameters.AddWithValue("@CourseTypeId", request.CourseTypeId); 
                    command.Parameters.AddWithValue("@Notation", (object)request.Notation ?? DBNull.Value); // Используем DBNull.Value для передачи NULL значения, если примечание не заполнено
                    command.Parameters.AddWithValue("@StatusId", request.StatusId); 
                    command.Parameters.AddWithValue("@CourseStart", request.CourseBeginning.ToString("yyyy-MM-dd"));
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

        


        [HttpPost]
        public IActionResult FilteredRequests(int year, string status, string department, string courseBegin, string courseEnd, string fullName, string requestNumber)
        {
            List<RequestOut> filteredRequests = GetFilteredRequestsFromDB(GetCurrentUser() ,year, status, department, courseBegin, courseEnd, fullName, requestNumber);
            return PartialView("_RequestTable", filteredRequests);
        }


        private List<RequestOut> GetFilteredRequestsFromDB(string userName, int year = 0, string status = "", string department = "", string courseBegin = "", string courseEnd = "", string fullName = "", string requestNumber = "")
        {
            List<RequestOut> filteredRequests = new List<RequestOut>();

            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Requests WHERE [user] = @userName"; // Базовый SQL-запрос

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userName", userName);

                // Добавляем условия фильтрации на основе переданных значений
                if (year != 0)
                {
                    query += " AND (YEAR(course_start) = @Year OR YEAR(course_end) = @Year)";
                    command.Parameters.AddWithValue("@Year", year);
                }
                if (!string.IsNullOrEmpty(status))
                {
                    query += " AND status = @Status";
                    command.Parameters.AddWithValue("@Status", status);
                }
                if (!string.IsNullOrEmpty(department))
                {
                    query += " AND department LIKE @Department";
                    command.Parameters.AddWithValue("@Department", department);
                }
                if (!string.IsNullOrEmpty(courseBegin) && DateTime.TryParse(courseBegin, out DateTime startDate))
                {
                    query += " AND course_start >= @CourseBegin";
                    command.Parameters.AddWithValue("@CourseBegin", startDate);
                }
                if (!string.IsNullOrEmpty(courseEnd) && DateTime.TryParse(courseEnd, out DateTime endDate))
                {
                    query += " AND course_end <= @CourseEnd";
                    command.Parameters.AddWithValue("@CourseEnd", endDate);
                }
                if (!string.IsNullOrEmpty(fullName))
                {
                    query += " AND full_name LIKE @FullName";
                    command.Parameters.AddWithValue("@FullName", "%" + fullName + "%");
                }
                if (!string.IsNullOrEmpty(requestNumber))
                {
                    query += " AND id = @RequestNumber";
                    command.Parameters.AddWithValue("@RequestNumber", requestNumber);
                }

                query += " ORDER BY id DESC";
                command.CommandText = query;

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
                        CourseType = GetCourseTypeName((int)reader["course_type"]),
                        Notation = reader["notation"].ToString(),
                        Status = GetStatusName((int)reader["status"]),
                        CourseStart = (DateTime)reader["course_start"],
                        CourseEnd = (DateTime)reader["course_end"],
                        Year = (int)reader["year"],
                        Username = (string)reader["user"]
                    };

                    filteredRequests.Add(request);
                }

                reader.Close();
            }

            return filteredRequests;
        }


    }
}

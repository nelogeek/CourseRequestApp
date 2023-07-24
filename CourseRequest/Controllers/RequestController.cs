using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System;
using CourseRequest.Models;
using System.Data.SqlClient;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using CourseRequest.Data;
using System.Linq;

namespace CourseRequest.Controllers
{
    public class RequestController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public RequestController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public IActionResult RequestList()
        {
            /*List<RequestOut> requests = GetRequestsFromDB();*/

            string userName = GetCurrentUser();

            UserRole userRole = GetUserRole(userName);

            ViewBag.UserRoles = userRole;

            List<RequestOut> requests = GetFilteredRequestsFromDB(userName, userRole);
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
            if (ModelState.IsValid /*&& !string.IsNullOrEmpty(request.Full_Name)*/)
            {
                // Создаем новый объект Request с переданными данными
                var newRequest = new Request
                {
                    Full_Name = request.Full_Name,
                    Department = request.Department,
                    Position = request.Position,
                    Course_Name = request.Course_Name,
                    Course_Type = request.Course_Type,
                    Notation = request.Notation,
                    Status = request.Status,
                    Course_Start = request.Course_Start,
                    Course_End = request.Course_End,
                    Year = request.Course_Start.Year,
                    User = request.User
                };

                // Добавляем новую заявку в контекст базы данных
                _context.Requests.Add(newRequest);

                // Сохраняем изменения в базе данных
                _context.SaveChanges();

                // Успешно сохранено, перенаправляем на страницу списка заявок или другую страницу
                return RedirectToAction("Index");
            }
            else
            {
                // Некорректные данные формы, вернуть представление с сообщением об ошибке или выполнить другую логику обработки ошибки
                return View("Error");
            }
        }


        private UserRole GetUserRole(string userName)
        {
            // Получение ролей пользователя из базы данных
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);

            UserRole userRole = UserRole.None;

            if (user != null)
            {
                if (user.RoleId == (int)UserRole.Coordinator)
                    userRole = UserRole.Coordinator;

                else if (user.RoleId == (int)UserRole.Initiator)
                    userRole = UserRole.Initiator;

                
            }

            return userRole;
        }


        [HttpPost]
        public IActionResult FilteredRequests(int year, string status, string department, string courseBegin, string courseEnd, string fullName, string requestNumber)
        {
            string userName = GetCurrentUser();
            UserRole userRole = GetUserRole(userName);
            List<RequestOut> filteredRequests = GetFilteredRequestsFromDB(userName, userRole, year, status, department, courseBegin, courseEnd, fullName, requestNumber);
            return PartialView("_RequestTable", filteredRequests);
        }


        private List<RequestOut> GetFilteredRequestsFromDB(string userName, UserRole userRole, int year = 0, string status = "", string department = "", string courseBegin = "", string courseEnd = "", string fullName = "", string requestNumber = "")
        {
            List<RequestOut> filteredRequests = new List<RequestOut>();

            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Requests WHERE 1=1"; // Базовый SQL-запрос

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

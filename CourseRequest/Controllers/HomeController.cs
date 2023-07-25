using CourseRequest.Data;
using CourseRequest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CourseRequest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ApplicationDbContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        public IActionResult Index()
        {
            string userName = GetCurrentUser();

            UserRole userRole = GetUserRole(userName);

            ViewBag.UserRoles = userRole;

            if (userRole != UserRole.Initiator)
            {
                return RedirectToAction("RequestList");
            }

            // Получение таблицы заявок и их количества
            int requestCount = GetRequestCountForUser(userName);
            ViewData["RequestCount"] = requestCount;

            List<RequestOut> requests = GetRequestsByStatusAndUserName(1, userName);

            if (requests.Count == 0)
            {
                return View();
            }

            return View(requests);
        }

        public IActionResult RequestList()
        {
            string userName = GetCurrentUser();

            UserRole userRole = GetUserRole(userName);

            ViewBag.UserRoles = userRole;

            List<RequestOut> requests = GetFilteredRequestsFromDB(userName, GetUserRole(userName));
            return View("~/Views/Home/RequestList.cshtml", requests);
        }

        private UserRole GetUserRole(string userName)
        {
            // Получение ролей пользователя из базы данных
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);

            UserRole userRole = UserRole.None;

            if (user != null)
            {
                if (user.RoleId == (int)UserRole.Initiator)
                    userRole = UserRole.Initiator;

                else if (user.RoleId == (int)UserRole.Coordinator)
                    userRole = UserRole.Coordinator;

                
            }

            return userRole;
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

        private string GetCurrentUser()
        {
            WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();
            string userName = currentIdentity?.Name ?? "Неизвестно";
            return userName;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        private int GetRequestCountForUser(string username)
        {
            string connectionString = _configuration.GetConnectionString("connectionString");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string countQuery = "SELECT COUNT(*) FROM Requests WHERE [user] = @User";
                SqlCommand countCommand = new SqlCommand(countQuery, connection);
                countCommand.Parameters.AddWithValue("@User", username);
                int requestCount = (int)countCommand.ExecuteScalar();
                connection.Close();

                return requestCount;
            }
        }


        private List<RequestOut> GetRequestsByStatusAndUserName(int statusId, string userName)
        {
            string connectionString = _configuration.GetConnectionString("connectionString");

            List<RequestOut> requests = new List<RequestOut>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Department, Course_Start, Course_End, Full_Name, Notation, Position, Course_Name, Course_Type, Year FROM Requests WHERE Status = @Status AND [user] = @UserName  ORDER BY id DESC";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status", statusId);
                command.Parameters.AddWithValue("@UserName", userName);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RequestOut request = new RequestOut();

                        request.Id = Convert.ToInt32(reader["Id"]);
                        request.FullName = reader["Full_Name"].ToString();
                        request.Department = reader["Department"].ToString();
                        request.Position = reader["Position"].ToString();
                        request.CourseName = reader["Course_Name"].ToString();
                        request.CourseType = GetCourseTypeName((int)reader["Course_Type"]);
                        request.Notation = reader["Notation"].ToString();
                        request.Status = GetStatusName(statusId);
                        request.CourseStart = Convert.ToDateTime(reader["Course_Start"]);
                        request.CourseEnd = Convert.ToDateTime(reader["Course_End"]);
                        request.Year = (int)reader["Year"];

                        requests.Add(request);
                    }
                }
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



        [HttpGet]
        public IActionResult GetSimilarNames(string name)
        {
            var similarNames = _context.TempUser
                .Where(u => u.Fio.Contains(name))
                .Join(
                    _context.TempDept,
                    user => user.Dept_Id,
                    dept => dept.Id,
                    (user, dept) => new
                    {
                        UserName = user.Fio,
                        Dept_Id = user.Dept_Id,
                        Position = user.Position,

                        DeptName = dept.Name
                    })
                .ToList();

            return Json(similarNames);
        }



    }
}

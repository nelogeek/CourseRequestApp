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

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();
            string userName = currentIdentity?.Name ?? "Неизвестно";

            int requestCount = GetRequestCountForUser(userName);
            ViewData["RequestCount"] = requestCount;

            List<RequestOut> requests = GetRequestsByStatusAndUserName(1, userName);

            if (requests.Count == 0)
            {
                return View();
            }

            return View(requests);
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
                        request.CourseType = GetCourseTypeName( (int)reader["Course_Type"]);
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

    }
}

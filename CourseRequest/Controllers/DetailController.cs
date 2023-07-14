using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System;
using Microsoft.Extensions.Configuration;

namespace CourseRequest.Controllers
{
    public class DetailController : Controller
    {
        private readonly IConfiguration _configuration;

        public DetailController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Details(int id)
        {
            // Получите данные выбранной строки по идентификатору
            var request = GetRequestOutById(id);

            if (request == null)
            {
                // Обработка случая, когда данные не найдены
                return NotFound();
            }

            // Передайте данные в представление
            return View(request);
        }


        private RequestOut GetRequestOutById(int id)
        {
            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Status, Department, Course_Start, Course_End, Full_Name, Notation FROM Requests WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        RequestOut request = new RequestOut();
                        request.Id = Convert.ToInt32(reader["Id"]);
                        request.Status = GetStatusName((int)reader["Status"]).ToString();
                        request.Department = reader["Department"].ToString();
                        request.CourseStart = Convert.ToDateTime(reader["Course_Start"]);
                        request.CourseEnd = Convert.ToDateTime(reader["Course_End"]);
                        request.FullName = reader["Full_Name"].ToString();
                        request.Notation = reader["Notation"].ToString();

                        return request;
                    }
                }
            }

            return null;
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

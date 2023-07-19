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


        private Request GetRequestOutById(int id)
        {
            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Status, Department, Course_Start, Course_End, Full_Name, Notation, Position, Course_Name, Course_Type, Year FROM Requests WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Request request = new Request();
                        request.Id = Convert.ToInt32(reader["Id"]);
                        request.FullName = reader["Full_Name"].ToString();
                        request.Department = reader["Department"].ToString();
                        request.Position = reader["Position"].ToString();
                        request.CourseName = reader["Course_Name"].ToString();
                        request.CourseTypeId = (int)reader["Course_Type"];
                        request.Notation = reader["Notation"].ToString();
                        request.StatusId = (int)reader["Status"];
                        request.CourseBeginning = Convert.ToDateTime(reader["Course_Start"]);
                        request.CourseEnd = Convert.ToDateTime(reader["Course_End"]);
                        request.Year = (int)reader["Year"];

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

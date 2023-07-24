using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System;
using Microsoft.Extensions.Configuration;
using CourseRequest.Models;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using CourseRequest.Data;
using System.Linq;

namespace CourseRequest.Controllers
{
    public class DetailController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public DetailController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public IActionResult Details(int id)
        {
            string userName = GetCurrentUser();

            UserRole userRole = GetUserRole(userName);

            ViewBag.UserRoles = userRole;


            // получение данных выбранной строки по идентификатору
            var request = GetRequestOutById(id);

            if (request == null)
            {
                // Обработка случая, когда данные не найдены
                return NotFound();
            }

            // Передача данных в представление
            return View(request);
        }

        private string GetCurrentUser()
        {
            WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();
            string userName = currentIdentity?.Name ?? "Неизвестно";
            return userName;
        }

        private UserRole GetUserRole(string userName)
        {
            // Получение ролей пользователя из базы данных
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);

            UserRole userRole = UserRole.None;

            if (user != null)
            {
                if (user.RoleId == (int)UserRole.Coordinator)
                    userRole |= UserRole.Coordinator;

                if (user.RoleId == (int)UserRole.Initiator)
                    userRole |= UserRole.Initiator;

                if (user.RoleId == (int)UserRole.Trainee)
                    userRole |= UserRole.Trainee;
            }

            return userRole;
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
                        request.Full_Name = reader["Full_Name"].ToString();
                        request.Department = reader["Department"].ToString();
                        request.Position = reader["Position"].ToString();
                        request.Course_Name = reader["Course_Name"].ToString();
                        request.Course_Type = (int)reader["Course_Type"];
                        request.Notation = reader["Notation"].ToString();
                        request.Status = (int)reader["Status"];
                        request.Course_Start = Convert.ToDateTime(reader["Course_Start"]);
                        request.Course_End = Convert.ToDateTime(reader["Course_End"]);
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


        [HttpPost]
        public IActionResult SaveChanges(int requestId, Request model)
        {
            // Временный вывод в консоль для отладки
            Console.WriteLine("SaveChanges called with requestId: " + requestId);
            Console.WriteLine("FullName: " + model.Full_Name);
            // и так далее, выводите остальные свойства модели

            // Находим заявку по её идентификатору (Id)
            var request = _context.Requests.FirstOrDefault(r => r.Id == requestId);

            if (request != null)
            {
                // Обновляем данные заявки из переданной модели
                request.Full_Name = model.Full_Name;
                request.Course_Name = model.Course_Name;
                request.Status = model.Status;
                request.Department = model.Department;
                request.Course_Type = model.Course_Type;
                request.Course_Start = model.Course_Start;
                request.Course_End = model.Course_End;
                request.Position = model.Position;
                request.Notation = model.Notation;

                // Сохранение изменений в бд
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

    }
}

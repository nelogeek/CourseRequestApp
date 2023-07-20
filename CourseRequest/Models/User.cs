using System.Data;

namespace CourseRequest.Models
{
    public class User
    {
        public string UserName { get; set; }
        public int RoleId { get; set; }

        // Навигационное свойство для связи с таблицей Roles
        public Role Role { get; set; }
    }
}

using System.Collections.Generic;

namespace CourseRequest.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        // Навигационное свойство для связи с таблицей Users
        public ICollection<User> Users { get; set; }
    }
}

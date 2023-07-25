using System.ComponentModel.DataAnnotations.Schema;

namespace CourseRequest.Models
{
    [Table("Temp_user")]
    public class TempUser
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public string LoginName { get; set; }
        public string Room { get; set; }
        public string Phone_Internal { get; set; }
        public string Position { get; set; }
        public string Phone_City { get; set; }
        public int Dept_Id { get; set; }
        public string Empl_Index { get; set; }
        public string Photo_Link { get; set; }
        public string Additionally { get; set; }

        
        
    }
}

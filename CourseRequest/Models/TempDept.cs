using System.ComponentModel.DataAnnotations.Schema;

namespace CourseRequest.Models
{
    [Table("TEMP_DEPT")]
    public class TempDept
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public int DeptIndex { get; set; }
    }
}

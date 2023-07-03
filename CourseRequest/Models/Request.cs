using System;

namespace CourseRequest.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string CourseName { get; set; }
        public string CourseType { get; set; }
        public string Notation { get; set; }
        public string Status { get; set; }
        public DateTime CourseBeginning { get; set; }
        public DateTime CourseEnd { get; set; }
        public int Year { get; set; }
    }
}

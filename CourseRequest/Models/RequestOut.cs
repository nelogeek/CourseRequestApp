using System;

public class RequestOut
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Department { get; set; }
    public string Position { get; set; }
    public string CourseName { get; set; }
    public string CourseType { get; set; } // Добавлено новое свойство
    public string Notation { get; set; }
    public string Status { get; set; } // Добавлено новое свойство
    public DateTime CourseStart { get; set; } // Обновлено название свойства
    public DateTime CourseEnd { get; set; }
    public int Year { get; set; }
    public string Username { get; set; }
}

using System;

namespace CourseRequest.Models
{
    [Flags]
    public enum UserRole
    {
        None = 0,
        Coordinator = 1,
        Initiator = 2,
        Trainee = 4
    }
}

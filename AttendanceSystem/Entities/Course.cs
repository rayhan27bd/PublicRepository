using System;
using System.Text;

namespace AttendanceSystem.Entities
{
    public sealed class Course
    {
        public uint Id { get; set; }
        public string CourseName { get; set; }
        public double CourseFees { get; set; }
        public string CreationDate { get; set; }

        public string? Weekly1stClassDay { get; set; }
        public string? ClassStartTime1 { get; set; }
        public string? ClassEndedTime1 { get; set; }

        public string? Weekly2ndClassDay { get; set; }
        public string? ClassStartTime2 { get; set; }
        public string? ClassEndedTime2 { get; set; }

        public ushort TotalClass { get; set; }
        public Boolean HasSchedule { get; set; }

    }
}

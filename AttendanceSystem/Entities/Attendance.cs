using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AttendanceSystem.Entities
{
    public sealed class Attendance
    {
        public uint Id { get; set; }
        public EntityUser Student { get; set; }
        public Course Course { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ClassDate { get; set; }
        public string ClassWeekOfDay { get; set; }
        public string ClassStartEndTime { get; set; }
        public DateTime? PresentDate { get; set; }
        public Boolean IsPresent { get; set; }
        /*
        public ICollection<EntityUser> Students { get; set; }
        */
    }
}

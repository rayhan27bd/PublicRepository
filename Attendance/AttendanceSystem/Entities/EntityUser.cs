namespace AttendanceSystem.Entities
{
    public class EntityUser
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public DateTime RegisterDate { get; set; }
        public Course? Course { get; set; }
        public string? EnrollmentDate { get; set; }

        public EntityUser(UserType userType)
            : this (string.Empty, string.Empty, string.Empty, userType)
        {
        }
        public EntityUser(string name, string userName, string password, UserType userType)
        {
            Name = name;
            UserName = userName;
            Password = password;
            UserType = userType;
        }
    }
}

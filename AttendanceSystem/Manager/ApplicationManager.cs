using System.Data;
using AttendanceSystem.Utility;
using AttendanceSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Linq;

namespace AttendanceSystem.Manager
{
    public class ApplicationManager
    {
        private static string _name;
        private static byte _userChoice;
        private static string _userName;
        private static string _password;
        private static int _rowAffected;
        private static UserType _userType;
        private readonly ApplicationDbContext dbContext;

        public ApplicationManager()
        {
            _userType = new UserType();
            dbContext = new ApplicationDbContext();
        }


        public UserType UserLogin()
        {
        LoginAgain:
            _userName = AppHelper.SetUserName();
            _password = AppHelper.SetPassword();

            var isUser = dbContext.Users.SingleOrDefault(u => u.UserName == _userName && u.Password == _password);
            if (isUser != null)
                _userType = isUser.UserType;
            else
            {
                AppHelper.InvalidInfo("Invalid! Username or Password.\n");
                goto LoginAgain;
            }

            return _userType;
        }

        public void CreateUserOrCourse()
        {
        RedirectToUser:
            try
            {
                Console.Write("\nCreate:\n ");
                Console.Write("1.Teacher 2.Student 3.Course 4.Admin\nOption No.: ");
                _userChoice = Byte.Parse(Console.ReadLine().Trim());
            }
            catch (Exception ex)
            {
                AppHelper.InvalidInfo($"{ex.Message}\nPlease try again..");
                goto RedirectToUser;
            }

            if (_userChoice == 1)
            {
                Console.Write("\nTeacher ");
                _name = AppHelper.SetName();

            RedirectToTeacher:
                _userName = AppHelper.SetUserName();
                var isExist = dbContext.Users.Any(u => u.UserName == _userName);
                if (isExist)
                {
                    AppHelper.MessageInfo("This Username Already Exist.\n");
                    goto RedirectToTeacher;
                }
                else
                {
                    _password = AppHelper.SetPassword();
                    var teacher = new EntityUser(_name, _userName, _password, UserType.Teacher)
                    {
                        RegisterDate = DateTime.Now
                    };

                    dbContext.Users.Add(teacher);
                    _rowAffected = dbContext.SaveChanges();
                    if (_rowAffected > 0)
                        AppHelper.SuccessInfo($"Success! {teacher.UserType} Registration.");
                    else
                    {
                        AppHelper.FailureInfo($"Failure! {teacher.UserType} Registration.");
                        goto RedirectToUser;
                    }
                }
            }
            else if (_userChoice == 2)
            {
                Console.Write("\nStudent ");
                _name = AppHelper.SetName();

            RedirectToStudent:
                _userName = AppHelper.SetUserName();
                var isExist = dbContext.Users.Any(x => x.UserName == _userName);
                if (isExist)
                {
                    AppHelper.MessageInfo("This Username Already Exist.\n");
                    goto RedirectToStudent;
                }
                else
                {
                    _password = AppHelper.SetPassword();
                    var student = new EntityUser(_name, _userName, _password, UserType.Student)
                    {
                        RegisterDate = DateTime.Now
                    };

                    dbContext.Users.Add(student);
                    _rowAffected = dbContext.SaveChanges();
                    if (_rowAffected > 0)
                        AppHelper.SuccessInfo($"Success! {student.UserType} Registration.");
                    else
                    {
                        AppHelper.FailureInfo($"Failure! {student.UserType} Registration.");
                        goto RedirectToUser;
                    }
                }
            }
            else if (_userChoice == 3)
            {
            RedirectToCourse:
                var course = new Course();
                Console.Write("\nCourse ");
                course.CourseName = AppHelper.SetName();
                var isTrue = dbContext.Courses.Any(x => x.CourseName == course.CourseName);
                if (isTrue)
                {
                    AppHelper.MessageInfo("Already Exist This Course Title.");
                    goto RedirectToCourse;
                }
                else
                {
                    course.CourseFees = AppHelper.SetFees();
                    course.CreationDate = DateTime.Now;
                    try
                    {
                        dbContext.Courses.Add(course);
                        _rowAffected = dbContext.SaveChanges();
                        if (_rowAffected > 0)
                            AppHelper.SuccessInfo("Success! To Create New Course.");
                        else
                        {
                            AppHelper.FailureInfo("Failure! To Create New Course.");
                            goto RedirectToUser;
                        }
                    }
                    catch (Exception ex) { AppHelper.InvalidInfo($"{ex.Message}"); }
                }
            }
            else if (_userChoice == 4)
            {
                Console.Write("\nAdmin ");
                _name = AppHelper.SetName();

            RedirectToAdmin:
                _userName = AppHelper.SetUserName();
                var adminExist = dbContext.Users.Any(x => x.UserName == _userName);
                if (adminExist)
                {
                    AppHelper.MessageInfo("This Username Already Exist.\n");
                    goto RedirectToAdmin;
                }
                else
                {
                    _password = AppHelper.SetPassword();
                    var _admin = new EntityUser(_name, _userName, _password, UserType.Admin)
                    {
                        RegisterDate = DateTime.Now
                    };

                    dbContext.Users.Add(_admin);
                    _rowAffected = dbContext.SaveChanges();
                    if (_rowAffected > 0)
                        AppHelper.SuccessInfo($"Success! {_admin.UserType} Registration.\n");
                    else
                    {
                        AppHelper.FailureInfo($"Failure! {_admin.UserType} Registration.\n");
                        goto RedirectToUser;
                    }
                }
            }
            else
            {
                AppHelper.InvalidInfo("Invalid! Input Option No. Isn't Correct.");
                goto RedirectToUser;
            }
        }

        public void SettingClassSchedule()
        {
        StartAgain:
            Console.WriteLine("\nCourse List=>");
            List<Course> courses = dbContext.Courses.ToList();
            if (courses.Count > 0)
                foreach (var c in courses)
                    Console.WriteLine($"Id: {c.Id}  Name: {c.CourseName}");
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("No Course Found, Please add a Course.");
                Console.ResetColor();
                CreateUserOrCourse(); goto StartAgain;
            }

            Console.Write("\nCourse ");
            var courseName = AppHelper.SetName();
            var selectCourse = courses.FirstOrDefault(x => x.CourseName == courseName);
            if (selectCourse != null)
            {
            SetScheduleAgain:
                try
                {
                    Console.Write("Weekly 1st Day: ");
                    selectCourse.Weekly1stClassDay = Console.ReadLine();

                    Console.Write("Start Time: ");
                    selectCourse.ClassStartTime1 = Convert.ToDateTime(Console.ReadLine()).ToString("t");

                    Console.Write("Ended Time: ");
                    selectCourse.ClassEndedTime1 = Convert.ToDateTime(Console.ReadLine()).ToString("t");


                    Console.Write("Weekly 2nd Day: ");
                    selectCourse.Weekly2ndClassDay = Console.ReadLine();

                    Console.Write("Start Time: ");
                    selectCourse.ClassStartTime2 = Convert.ToDateTime(Console.ReadLine()).ToString("t");

                    Console.Write("Ended Time: ");
                    selectCourse.ClassEndedTime2 = Convert.ToDateTime(Console.ReadLine()).ToString("t");


                    Console.Write("Total Classes: ");
                    selectCourse.TotalClasses = ushort.Parse(Console.ReadLine());
                    selectCourse.HasSchedule = Convert.ToBoolean(bool.TrueString);
                    dbContext.Courses.Update(selectCourse);  // UpdateDataToDbRow
                }
                catch (Exception ex) { AppHelper.InvalidInfo($"{ex.Message}\n"); goto SetScheduleAgain; }
            }

            _rowAffected = dbContext.SaveChanges();
            if (_rowAffected > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Success! Class Schedule for Course.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Failure! Class Schedule for Course.");
                Console.ResetColor(); goto StartAgain;
            }
        }

        public void TakeStudentAttendance()
        {
            var selectUser = dbContext.Users
                .Where(s => s.UserName == _userName && s.Password == _password)
                .Include(s => s.Course).SingleOrDefault();

            Console.WriteLine($"\n{selectUser.UserType} Name: {selectUser.Name}");
            if (selectUser.Course != null && selectUser.UserType == UserType.Student)
            {
                Console.WriteLine($"Course Title: {selectUser.Course.CourseName}");
                if (selectUser.Course.HasSchedule == true)
                {
                    var attendance = new Attendance()
                    {
                        Student = selectUser, Course = selectUser.Course
                    };

                    Console.WriteLine($"Class Schedule: " +
                        $"{selectUser.Course.Weekly1stClassDay} ({selectUser.Course.ClassStartTime1} - {selectUser.Course.ClassEndedTime1}), " +
                        $"{selectUser.Course.Weekly2ndClassDay} ({selectUser.Course.ClassStartTime2} - {selectUser.Course.ClassEndedTime2})");


                    bool _1stClassDayTime = selectUser.Course.Weekly1stClassDay == Convert.ToString(DateTime.Now.DayOfWeek) &
                        Convert.ToDateTime(selectUser.Course.ClassStartTime1) <= Convert.ToDateTime(DateTime.Now.ToString("t")) &
                        Convert.ToDateTime(selectUser.Course.ClassEndedTime1) >= Convert.ToDateTime(DateTime.Now.ToString("t"));

                    bool _2ndClassDayTime = selectUser.Course.Weekly2ndClassDay == Convert.ToString(DateTime.Now.DayOfWeek) &
                        Convert.ToDateTime(selectUser.Course.ClassStartTime2) <= Convert.ToDateTime(DateTime.Now.ToString("t")) &
                        Convert.ToDateTime(selectUser.Course.ClassEndedTime2) >= Convert.ToDateTime(DateTime.Now.ToString("t"));


                    //if ((selectUser.Course.Weekly1stClassDay == Convert.ToString(DateTime.Now.DayOfWeek) &
                    //    Convert.ToDateTime(selectUser.Course.ClassStartTime1) <= Convert.ToDateTime(DateTime.Now.ToString("t")) &
                    //    Convert.ToDateTime(selectUser.Course.ClassEndedTime1) >= Convert.ToDateTime(DateTime.Now.ToString("t"))) |
                    //    (selectUser.Course.Weekly2ndClassDay == Convert.ToString(DateTime.Now.DayOfWeek) &
                    //    Convert.ToDateTime(selectUser.Course.ClassStartTime2) <= Convert.ToDateTime(DateTime.Now.ToString("t")) &
                    //    Convert.ToDateTime(selectUser.Course.ClassEndedTime2) >= Convert.ToDateTime(DateTime.Now.ToString("t"))))

                    if (_1stClassDayTime | _2ndClassDayTime)
                    {
                        var isPresent = dbContext.Attendances
                            .Any(x => x.Student.Id == selectUser.Id && x.PresentDate.Date == DateTime.Now.Date);

                        if (isPresent == false)
                        {
                            if (_1stClassDayTime)
                            {
                                attendance.ClassDayTime = $"{selectUser.Course.Weekly1stClassDay}, " +
                                    $"{Convert.ToDateTime(selectUser.Course.ClassStartTime1):t} - {Convert.ToDateTime(selectUser.Course.ClassEndedTime1):t}";
                            }
                            else
                            {
                                attendance.ClassDayTime = $"{selectUser.Course.Weekly2ndClassDay}, " +
                                    $"{Convert.ToDateTime(selectUser.Course.ClassStartTime2):t} - {Convert.ToDateTime(selectUser.Course.ClassEndedTime2):t}";
                            }

                        TakeAttendance:
                            Console.Write($"\nDo you want to given attendance?? \n[Y/N]: ");
                            ConsoleKey yesOrNo = Console.ReadKey().Key; Console.Write("\n");

                            if (yesOrNo == ConsoleKey.Y)
                            {
                                attendance.IsPresent = true;
                                attendance.PresentDate = DateTime.Now;
                                dbContext.Attendances.Add(attendance);

                                _rowAffected = dbContext.SaveChanges();
                                if (_rowAffected > 0)
                                    AppHelper.SuccessInfo("Success! Accepted your attendance.");
                                else
                                    AppHelper.FailureInfo($"Failure! Can't accept attendance.");
                            }
                            else if (yesOrNo != ConsoleKey.N)
                            {
                                AppHelper.InvalidInfo("Invalid Attempt! Please try again..");
                                goto TakeAttendance;
                            }
                        }
                        else if (isPresent == true)
                        { AppHelper.MessageInfo("Info! You already given attendance today."); }
                    }
                    else { AppHelper.InvalidInfo($"Info! Class time is not start yet."); }
                }
                else { AppHelper.InvalidInfo("Info! Class schedule not set in course."); }
            }
            else { AppHelper.InvalidInfo("Info! You aren't enrolled in course."); }
        }

        public void CheckStudentAttendance()
        {
            var selectUser = dbContext.Users
                .Where(s => s.UserName == _userName && s.Password == _password)
                .Include(s => s.Course).SingleOrDefault();

            Console.WriteLine($"\n{selectUser.UserType} Name: {selectUser.Name}");
            Console.WriteLine($"Course Title: {((selectUser.Course != null)? selectUser.Course.CourseName : "N/A")}");

            if (selectUser.Course != null && selectUser.UserType == UserType.Teacher)
            {
        
                var attendances = dbContext.Attendances.Where(x => x.Course == selectUser.Course).Include(s => s.Student).ToList();
                if (attendances.Count > 0)
                {
                    AppHelper.MessageInfo("\nClass-Date\tStudent-Name\tPresent?");
                    foreach (var item in attendances)
                    {
                        if (item.IsPresent)
                        {
                            Console.Write($"{item.PresentDate:d}\t {item.Student.Name}");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\t √"); Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($"{item.PresentDate:d}\t {item.Student.Name}");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\t A"); Console.ResetColor();
                        }

                        //Console.WriteLine($"{item.PresentDate:d} \t {item.Student.Name} \t {(item.IsPresent ? present: absent)}");
                    }
                }
                else { AppHelper.MessageInfo("Student Attendance Not Found."); }
            }
            else { AppHelper.InvalidInfo("Info! You aren't assigned in course."); }
        }

        public void AssignOrEnrollCourse(byte selectUser)
        {

        AfterCreateNewCourse:
            Console.WriteLine("\nCourse List=> ");
            List<Course> courses = dbContext.Courses.ToList();
            if (courses.Count > 0)
            {
                foreach (var course in courses)
                    Console.Write($"Id: {course.Id}  Name: {course.CourseName}\n");
            }
            else
            {
                AppHelper.InvalidInfo("No Course Found, Please add a Course.\n");
                CreateUserOrCourse(); goto AfterCreateNewCourse;
            }

        SelectUserAndCourse:
            var user = new EntityUser(_userType);
            if (selectUser == 2)
            {
            SelectTeacherAgain:
                Console.WriteLine("\nTeacher List=>");
                List<EntityUser> teachers = dbContext.Users.
                    Where(t => t.UserType == UserType.Teacher && t.Course == null).ToList();

                if (teachers.Count > 0)
                {
                    foreach (var teacher in teachers)
                        Console.WriteLine($"Name: {teacher.Name}  Username: {teacher.UserName}");

                    Console.Write("\nTeacher ");
                    _userName = AppHelper.SetUserName();
                    user = teachers.SingleOrDefault(x => x.UserName == _userName);
                }
                else
                {
                    AppHelper.MessageInfo("No Unassign Teacher Found, Please add new Teacher.");
                    CreateUserOrCourse(); goto SelectTeacherAgain;
                }
            }
            if (selectUser == 3)
            {
            SelectStudentAgain:
                Console.WriteLine("\nStudent List=>");
                List<EntityUser> students = dbContext.Users
                    .Where(s => s.UserType == UserType.Student && s.Course == null).ToList();

                if (students.Count > 0)
                {
                    foreach (var student in students)
                        Console.WriteLine($"Name: {student.Name}  Username: {student.UserName}");

                    Console.Write("\nStudent ");
                    _userName = AppHelper.SetUserName();
                    user = students.SingleOrDefault(x => x.UserName == _userName);
                }
                else
                {
                    AppHelper.MessageInfo("No Unassign Student Found, Please add new Student.");
                    CreateUserOrCourse(); goto SelectStudentAgain;
                }
            }
            if (user != null && user.UserName == _userName)
            {

            SelectCourseAgain:
                Console.Write("Enter Course");
                var userCourse = AppHelper.SetName();
                var selectCourse = courses.FirstOrDefault(x => x.CourseName == userCourse);

                if (selectCourse != null)
                {
                    user.Course = selectCourse;
                    user.EnrollmentDate = Convert.ToString(DateTime.Now);
                    dbContext.Users.Update(user);
                    _rowAffected = dbContext.SaveChanges();
                }
                else
                {
                    AppHelper.InvalidInfo("Info! Selected Course isn't Correct.\n");
                    goto SelectCourseAgain;
                }

                if (_rowAffected > 0)
                    AppHelper.SuccessInfo($"Success! {user.UserType} {((user.UserType == UserType.Teacher) ? "Assigned" : "Enrolled")} Course.");
                else
                {
                    AppHelper.FailureInfo($"Failure! To {user.UserType} {((user.UserType == UserType.Teacher) ? "Assign" : "Enroll")} Course.");
                    Console.WriteLine(); goto SelectCourseAgain;
                }
            }
            else
            {
                AppHelper.InvalidInfo("Info! Selected Username isn't Correct.");
                goto SelectUserAndCourse;
            }
        }
    }
}

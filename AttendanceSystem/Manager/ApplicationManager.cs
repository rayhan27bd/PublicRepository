﻿using System.Data;
using AttendanceSystem.Utility;
using AttendanceSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Manager
{
    public class ApplicationManager
    {

        private static string _name;
        private static int _userChoice;
        private static string _userName;
        private static string _password;
        private static int _rowAffected;
        private static UserType _userType;
        private readonly ApplicationDbContext _dbContext;

        public ApplicationManager()
        {
            _userType = new UserType();
            _dbContext = new ApplicationDbContext();
        }


        public UserType UserLogin()
        {
            LoginAgain:
            _userName = AppHelper.SetUserName();
            _password = AppHelper.SetPassword();

            var loginUser = _dbContext.Users.SingleOrDefault(u => u.UserName == _userName && u.Password == _password);
            if (loginUser != null)
                _userType = loginUser.UserType;
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
                _userChoice = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                AppHelper.InvalidInfo($"{ex.Message}\nPlease try again..");
                goto RedirectToUser;
            }

            if (_userChoice == 0)
            {
                Program program = new ();
            }
            else if (_userChoice == 1)
            {
                Console.Write("\nTeacher ");
                _name = AppHelper.SetName();

                RedirectToTeacher:
                _userName = AppHelper.SetUserName();
                var isExist = _dbContext.Users.Any(u => u.UserName == _userName);
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

                    _dbContext.Users.Add(teacher);
                    _rowAffected = _dbContext.SaveChanges();
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
                var isExist = _dbContext.Users.Any(x => x.UserName == _userName);
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

                    _dbContext.Users.Add(student);
                    _rowAffected = _dbContext.SaveChanges();
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
                var isTrue = _dbContext.Courses.Any(x => x.CourseName == course.CourseName);
                if (isTrue)
                {
                    AppHelper.MessageInfo("Already Exist This Course Title.");
                    goto RedirectToCourse;
                }
                else
                {
                    course.CourseFees = AppHelper.SetFees();
                    course.CreationDate = DateTime.Now.ToString("G");
                    try
                    {
                        _dbContext.Courses.Add(course);
                        _rowAffected = _dbContext.SaveChanges();
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
                var adminExist = _dbContext.Users.Any(x => x.UserName == _userName);
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

                    _dbContext.Users.Add(_admin);
                    _rowAffected = _dbContext.SaveChanges();
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
            List<Course> courses = _dbContext.Courses.ToList();
            if (courses.Count > 0)
                foreach (var c in courses)
                    Console.WriteLine($"Course Title: {c.CourseName}");
            else
            {
                AppHelper.MessageInfo("No Course Found, Please add a Course.");
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
                    selectCourse.TotalClass = ushort.Parse(Console.ReadLine());
                    selectCourse.HasSchedule = Convert.ToBoolean(bool.TrueString);
                    _dbContext.Courses.Update(selectCourse);  // UpdateDataToDbRow
                }
                catch (Exception ex) { AppHelper.InvalidInfo($"{ex.Message}\n"); goto SetScheduleAgain; }
            }

            _rowAffected = _dbContext.SaveChanges();
            if (_rowAffected > 0)
            {
                AppHelper.SuccessInfo("Success! Class Schedule for Course.");
            }
            else
            {
                AppHelper.FailureInfo("Failure! Class Schedule for Course.");
                goto StartAgain;
            }
        }

        public void GetStudentAttendance()
        {

            var loginUser = _dbContext.Users
                .Where(t => t.UserName == _userName && t.Password == _password).Include(s => s.Course).SingleOrDefault();


            Console.WriteLine($"\n{loginUser.UserType} Name: {loginUser.Name}");
            if (loginUser.Course != null && loginUser.UserType == UserType.Student)
            {

                Console.WriteLine($"Course Title: {loginUser.Course.CourseName}");
                if (loginUser.Course.HasSchedule == true)
                {

                    Console.WriteLine($"Class Schedule: " +
                        $"{loginUser.Course.Weekly1stClassDay} ({loginUser.Course.ClassStartTime1} - {loginUser.Course.ClassEndedTime1}), " +
                        $"{loginUser.Course.Weekly2ndClassDay} ({loginUser.Course.ClassStartTime2} - {loginUser.Course.ClassEndedTime2})");


                    bool DateTimeOf1stClassDay = loginUser.Course.Weekly1stClassDay == Convert.ToString(DateTime.Now.DayOfWeek) &
                        Convert.ToDateTime(loginUser.Course.ClassStartTime1) <= Convert.ToDateTime(DateTime.Now.ToString("t")) &&
                        Convert.ToDateTime(loginUser.Course.ClassEndedTime1) >= Convert.ToDateTime(DateTime.Now.ToString("t"));

                    bool DateTimeOf2ndClassDay = loginUser.Course.Weekly2ndClassDay == Convert.ToString(DateTime.Now.DayOfWeek) &
                        Convert.ToDateTime(loginUser.Course.ClassStartTime2) <= Convert.ToDateTime(DateTime.Now.ToString("t")) &&
                        Convert.ToDateTime(loginUser.Course.ClassEndedTime2) >= Convert.ToDateTime(DateTime.Now.ToString("t"));


                    if (DateTimeOf1stClassDay || DateTimeOf2ndClassDay)
                    {

                        var attendee = _dbContext.Attendances
                            .Where(x => x.Student == loginUser && x.Course == loginUser.Course && x.ClassDate == DateTime.Now.Date)
                            .SingleOrDefault();

                        if (attendee != null)
                        {

                            TakeAttendance:
                            Console.Write($"\nDo you want to given attendance?? \n[Y/N]: ");
                            ConsoleKey yesOrNo = Console.ReadKey().Key; Console.Write("\n");

                            if (yesOrNo == ConsoleKey.Y && attendee.IsPresent == false)
                            {
                                attendee.IsPresent = true;
                                attendee.PresentDate = DateTime.Now;
                                _dbContext.Attendances.Update(attendee);

                                _rowAffected = _dbContext.SaveChanges();
                                if (_rowAffected > 0)
                                    AppHelper.SuccessInfo("Success! Accepted your attendance.");
                                else
                                    AppHelper.FailureInfo($"Failure! Can't accept attendance.");
                            }
                            else if (yesOrNo != ConsoleKey.N && yesOrNo != ConsoleKey.Y)
                            {
                                AppHelper.InvalidInfo("Invalid Attempt! Please try again..");
                                goto TakeAttendance;
                            }
                            else if (attendee != null && attendee.IsPresent == true)
                            {
                                AppHelper.MessageInfo("Info! You already given attendance today.");
                            }
                        }
                        else { AppHelper.InvalidInfo("Teacher attendance schedule not set yet."); }
                    }
                    else { AppHelper.InvalidInfo($"Info! Class time is not start yet."); }
                }
                else { AppHelper.InvalidInfo("Info! Class schedule not set in course."); }
            }
            else { AppHelper.InvalidInfo("Info! You aren't enrolled in course."); }
        }

        public void SetStudentAttendance()
        {

            var loginUser = _dbContext.Users
                .Where(t => t.UserName == _userName && t.Password == _password).Include(c => c.Course).SingleOrDefault();

            Console.WriteLine($"\n{loginUser.UserType} Name: {loginUser.Name}");
            Console.WriteLine($"Course Title: {((loginUser.Course != null) ? loginUser.Course.CourseName : "N/A")}");


            if (loginUser.Course != null && loginUser.UserType == UserType.Teacher)
            {

                if (loginUser.Course.HasSchedule == true)
                {

                    Console.WriteLine($"Class Schedule: " +
                        $"{loginUser.Course.Weekly1stClassDay} ({loginUser.Course.ClassStartTime1} - {loginUser.Course.ClassEndedTime1}), " +
                        $"{loginUser.Course.Weekly2ndClassDay} ({loginUser.Course.ClassStartTime2} - {loginUser.Course.ClassEndedTime2})");


                    List<EntityUser> students = _dbContext.Users
                        .Where(x => x.Course == loginUser.Course && x.UserType == UserType.Student).ToList();

                    foreach (var student in students)
                    {

                        var isSchedule = _dbContext.Attendances.
                            Any(x => x.Student == student && x.Course == student.Course && x.ClassDate == DateTime.Now.Date);


                        bool _1stClassDayTime = student.Course.Weekly1stClassDay == Convert.ToString(DateTime.Now.DayOfWeek) &&
                            //Convert.ToDateTime(student.Course.ClassStartTime1) <= Convert.ToDateTime(DateTime.Now.ToString("t")) &
                            Convert.ToDateTime(student.Course.ClassEndedTime1) >= Convert.ToDateTime(DateTime.Now.ToString("t"));

                        bool _2ndClassDayTime = student.Course.Weekly2ndClassDay == Convert.ToString(DateTime.Now.DayOfWeek) &&
                            //Convert.ToDateTime(student.Course.ClassStartTime2) <= Convert.ToDateTime(DateTime.Now.ToString("t")) &
                            Convert.ToDateTime(student.Course.ClassEndedTime2) >= Convert.ToDateTime(DateTime.Now.ToString("t"));


                        if (isSchedule == false && (_1stClassDayTime || _2ndClassDayTime))
                        {

                            var setSchedule = new Attendance() { Student = student, Course = student.Course };
                            if (_1stClassDayTime)
                            {
                                setSchedule.ClassWeekOfDay = $"{student.Course.Weekly1stClassDay}";
                                setSchedule.ClassStartEndTime = $"{student.Course.ClassStartTime1} - {Convert.ToDateTime(student.Course.ClassEndedTime1):t}";
                            }

                            if (_2ndClassDayTime)
                            {
                                setSchedule.ClassWeekOfDay = $"{loginUser.Course.Weekly2ndClassDay}";
                                setSchedule.ClassStartEndTime = $"{student.Course.ClassStartTime2} - {Convert.ToDateTime(student.Course.ClassEndedTime2):t}";
                            }

                            setSchedule.CreationDate = DateTime.Now;
                            setSchedule.ClassDate = DateTime.Now.Date;
                            _dbContext.Attendances.Add(setSchedule);
                            _rowAffected = _dbContext.SaveChanges();
                        }
                    }

                    if (_rowAffected > 0)
                        AppHelper.SuccessInfo("Success! Attendance schedule setting.");

                    var attendances = _dbContext.Attendances.Where(x => x.Course == loginUser.Course).Include(s => s.Student).ToList();
                    if (attendances.Count > 0)
                    {
                        AppHelper.MessageInfo("\nClass-Date\tStudent-Name\tPresent?");
                        foreach (var item in attendances.OrderByDescending(x => x.Id))
                        {
                            if (item.IsPresent)
                            {
                                Console.Write($"{item.ClassDate:d}\t {item.Student.Name}");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\t   √"); Console.ResetColor();
                            }
                            else
                            {
                                Console.Write($"{item.ClassDate:d}\t {item.Student.Name}");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\t   A"); Console.ResetColor();
                            }
                        }
                    }
                    else { AppHelper.MessageInfo("Info! Student Attendance Not Found."); }
                }
                else { AppHelper.InvalidInfo("Info! Class schedule not set in course."); }
            }
            else { AppHelper.InvalidInfo("Info! You aren't assigned in course."); }
        }

        public void ViewAttendanceOfCourses()
        {

            var loginUser = _dbContext.Users
                .Where(t => t.UserName == _userName && t.Password == _password).Include(c => c.Course).SingleOrDefault();


            Console.WriteLine("\nCourse List=>");
            List<Course> courses = null;
            if (loginUser.Course == null)
                courses = _dbContext.Courses.ToList();
            else
                courses = _dbContext.Courses.Where(u => u.CourseName != loginUser.Course.CourseName).ToList();


            if (courses.Count > 0)
            {
                uint index = 1;
                foreach (var c in courses)
                    Console.WriteLine($"{index++}. {c.CourseName}");
            }
            //else { AppHelper.InvalidInfo("Info! Course Not Found."); }


            SelectCourseAgain:
            Console.Write("Select Course: "); var courseName = Console.ReadLine();
            var selectCourse = courses.FirstOrDefault(x => x.CourseName == courseName);
            if (selectCourse != null)
            {
                List<Attendance> attendances = _dbContext.Attendances
                    .Where(x => x.Course == selectCourse).Include(s => s.Student).ToList();

                if (attendances.Count > 0)
                {
                    AppHelper.MessageInfo("\nClass-Date\tStudent-Name\tPresent?");
                    foreach (var item in attendances.OrderByDescending(x => x.CreationDate))
                    {
                        if (item.IsPresent)
                        {
                            Console.Write($"{item.ClassDate:d}\t {item.Student.Name}");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\t   √"); Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($"{item.ClassDate:d}\t {item.Student.Name}");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\t   A"); Console.ResetColor();
                        }
                    }
                }
                else { AppHelper.InvalidInfo("Info! Student Attendance Not Found."); }
            }
            else
            {
                AppHelper.InvalidInfo("Info! Selected Course isn't Correct.\n");
                goto SelectCourseAgain;
            }
        }

        public void AssignOrEnrollCourse(int userType)
        {

            AfterCreateNewCourse:
            Console.WriteLine("\nCourse List=> ");
            List<Course> courses = _dbContext.Courses.ToList();
            if (courses.Count > 0)
            {
                foreach (var course in courses)
                    Console.Write($"Title: {course.CourseName}\n");
            }
            else
            {
                AppHelper.InvalidInfo("No Course Found, Please add a Course.\n");
                CreateUserOrCourse(); goto AfterCreateNewCourse;
            }

            SelectUserAndCourse:
            var user = new EntityUser(_userType);
            if (userType == 2)
            {
                SelectTeacherAgain:
                Console.WriteLine("\nTeacher List=>");
                List<EntityUser> teachers = _dbContext.Users.Where(t => t.UserType == UserType.Teacher && t.Course == null).ToList();

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
                    AppHelper.MessageInfo("Unassign Teacher Not Found, Please Add New Teacher.");
                    CreateUserOrCourse(); goto SelectTeacherAgain;
                }
            }

            if (userType == 3)
            {
                SelectStudentAgain:
                Console.WriteLine("\nStudent List=>");
                List<EntityUser> students = _dbContext.Users.Where(s => s.UserType == UserType.Student && s.Course == null).ToList();

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
                    AppHelper.MessageInfo("Unassign Student Not Found, Please Add New Student.");
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
                    _dbContext.Users.Update(user);
                    _rowAffected = _dbContext.SaveChanges();
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
            else { AppHelper.InvalidInfo("Info! Selected Username isn't Correct."); goto SelectUserAndCourse; }
        }

    }
}

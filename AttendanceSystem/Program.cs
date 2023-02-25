using AttendanceSystem.Manager;
using AttendanceSystem.Entities;
using AttendanceSystem.Utility;

public class Program
{
    static void Main(string[] args)
    {

        UserType userType;
        var appManager = new ApplicationManager();
        //appManager.CreateUserOrCourse();
        userType = appManager.UserLogin();

        // Task :: 01 to 07
        if (userType == UserType.Admin)
        {
            StartAgain:
            int userChoice;
            Console.Write("\n1.Create User or Course??\n2.Assign Teacher Course??");
            Console.Write("\n3.Enroll Student Course??\n4.Layout Class Schedule??");
            Console.Write("\n0.Logout\nOption No.: ");

            try
            {
                userChoice = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                AppHelper.InvalidInfo($"{ex.Message}\nPlease try again..");
                goto StartAgain;
            }

            switch (userChoice)
            {
                case 0:
                    AppHelper.SuccessInfo("Success! Admin Logout.");
                    break;
                case 1:
                    appManager.CreateUserOrCourse();
                    goto StartAgain;//break; 

                case 2:
                    appManager.AssignOrEnrollCourse(userChoice);
                    goto StartAgain;//break

                case 3:
                    appManager.AssignOrEnrollCourse(userChoice);
                    goto StartAgain;//break;

                case 4:
                    appManager.SettingClassSchedule();
                    goto StartAgain;//break;

                default:
                    AppHelper.InvalidInfo("Invalid! Entered Option No. Incorrect.");
                    goto StartAgain;//break;
            }
        }

        // Task :: 08
        if (userType == UserType.Student)
        {
            appManager.GetStudentAttendance();
        }

        // Task :: 09
        if (userType == UserType.Teacher)
        {
            // view attendance assigned course
            appManager.SetStudentAttendance();

        ViewAttendance: // view attendance of another courses
            Console.Write("\nDo you want to view attendance another courses?? \n[Y/N]: ");
            ConsoleKey yesOrNo = Console.ReadKey().Key; Console.Write("\n");
            if (yesOrNo == ConsoleKey.Y)
            {
                appManager.ViewAttendanceOfCourses();
            }
            else if (yesOrNo != ConsoleKey.N)
            {
                AppHelper.InvalidInfo("Invalid Attempt! Please try again..");
                goto ViewAttendance;
            }

            /*
            GotoAgain:
            byte teacherChoice;
            Console.Write("\nView Attendance:-");
            Console.Write("\n 1.Assigned Course??\n");
            Console.Write(" 2.Another Courses??\nOption No.: ");

            try
            {
                teacherChoice = Byte.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                AppHelper.InvalidInfo($"{ex.Message}\nPlease try again..");
                goto GotoAgain;
            }

            switch (teacherChoice)
            {
                case 0:
                    AppHelper.SuccessInfo("Success! Teacher Logout.");
                    break;
                case 1:
                    appManager.SetStudentAttendance(); 
                    goto GotoAgain;
                case 2:
                    appManager.ViewAttendanceOfCourses();
                    goto GotoAgain;
                default:
                    AppHelper.InvalidInfo("Invalid! Input Option No. Incorrect.");
                    goto GotoAgain;
            }
            */

        }

    }
}
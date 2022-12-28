// attendance system
using AttendanceSystem.Manager;
using AttendanceSystem.Entities;
using AttendanceSystem.Utility;

public class Program
{
    static void Main(string[] args)
    {

        ApplicationManager appManager = new();
        //appManager.CreateUserOrCourse();
        UserType userType = appManager.UserLogin();

        // Task :: 01 to 07
        if (userType == UserType.Admin)
        {
            StartAgain:
            byte userChoice;
            Console.WriteLine("\n1.Create User or Course??");
            Console.WriteLine("2.Assign Teacher Course??");
            Console.WriteLine("3.Enroll Student Course??");
            Console.Write("4.Layout Class Schedule??\nOption No.: ");

            try
            {
                userChoice = Byte.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                AppHelper.InvalidInfo($"{ex.Message}\nPlease try again..");
                goto StartAgain;
            }

            switch (userChoice)
            {
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
                    AppHelper.InvalidInfo("Invalid! Input Option No. Incorrect.");
                    goto StartAgain;//break;
            }
        }
        // Task :: 08
        if (userType == UserType.Student) { appManager.TakeStudentAttendance(); }
        // Task :: 09
        if (userType == UserType.Teacher) { appManager.CheckStudentAttendance(); }

    }
}
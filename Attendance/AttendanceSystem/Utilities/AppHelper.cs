using System.Text;

namespace AttendanceSystem.Utilities
{
    public static class AppHelper
    {
        private static string _name;
        private static double _fees;
        private static string _userName;
        private static string _password;

        public static string SetName()
        {
            SetName:
            Console.Write("Name: ");
             _name = Console.ReadLine().Trim();

            if (_name.Length < 3)
            {
                AppHelper.InvalidInfo("Name length tobe minimum three.");
                goto SetName;
            }

            foreach (var n in _name)
            {
                if (Char.IsDigit(n))
                {
                    AppHelper.InvalidInfo("Name do not accept nummbes.");
                    goto SetName;
                }
            }

            return _name;
        }

        public static double SetFees()
        {
            Console.Write("Course Fees: ");
            _fees = double.Parse(Console.ReadLine().Trim());

            if (_fees < 0)
                AppHelper.InvalidInfo("Course fees can't be negative.");

            return _fees;
        }

        public static string SetUserName()
        {
            TryAgain:
            Console.Write("Username: ");
            _userName = Console.ReadLine().Trim();

            if (_userName.Length < 5)
            {
                AppHelper.InvalidInfo("Username length tobe min five.");
                goto TryAgain;
            }
            else if (_userName.Contains(' '))
            {
                AppHelper.InvalidInfo("Username can't contains space.");
                goto TryAgain;
            }
            else if (_userName != _userName.ToLower())
            {
                AppHelper.InvalidInfo("Username to be lowercase Only.");
                goto TryAgain;
            }

            return _userName;
        }

        public static string SetPassword()
        {
            SetPasswordAgain:
            try // GenerateEncryptedPassword
            {
                Console.Write("Password: ");
                _password = Console.ReadLine().Trim();
                if (_password.Length < 5)
                {
                    AppHelper.InvalidInfo("Password length tobe min five.");
                    goto SetPasswordAgain;
                }
                else if (_password.Contains(' '))
                {
                    AppHelper.InvalidInfo("Password can't contains space.");
                    goto SetPasswordAgain;
                }

                byte[] encData_byte = new byte[_password.Length];
                encData_byte = Encoding.UTF8.GetBytes(_password);
                var encodedData = Convert.ToBase64String(encData_byte);
                _password = encodedData;
            }
            catch (Exception ex)
            {
                AppHelper.InvalidInfo($"Error! Password: {ex.Message}");
            }

            return _password;
        }

        public static void SuccessInfo(string mgs)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mgs); Console.ResetColor();
        }
        public static void FailureInfo(string mgs)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mgs);Console.ResetColor();
        }   
        public static void MessageInfo(string mgs)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(mgs);Console.ResetColor();
        }
        public static void InvalidInfo(string mgs)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(mgs);  Console.ResetColor();
        }

    }
}

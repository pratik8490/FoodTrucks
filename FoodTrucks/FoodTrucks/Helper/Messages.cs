using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTrucks.Helper
{
    public class Messages
    {
        public static string Ok = "OK";
        public static string Cancel = "CANCEL";
        public static string Yes = "YES";
        public static string No = "NO";
        public static string Back = "Back";

        public static string Error = "Error";
        public static string DataNotAvailable = "No data available";
        public static string User = "User";
        public static string Provider = "Provider";

        public class Login
        {
            public static string UsernamePasswordRequired = "Username and password are required.";
            public static string InvalidUserNameOrPassword = "Username or password are incorrect.";
        }
    
        public class CustomMessage
        {
            public static string ChooseProviderUser = "Please select register as?";
        }
    }
}

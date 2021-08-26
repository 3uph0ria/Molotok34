using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Molotok34.Class
{
    public class CurrentUser
    {
        public static int Id { get; set; }
        public static string PermissionName { get; set; }
        public static bool AccessProducts { get; set; }
        public static string Login { get; set; }
        public static string Password { get; set; }
    }
}
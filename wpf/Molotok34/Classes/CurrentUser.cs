using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molotok34.Classes
{
    class CurrentUser
    {
        public static string PermissionName { get; set; }
        public static bool AccessClients { get; set; }
        public static bool AccessProducts { get; set; }
        public static bool AccessCategories { get; set; }
    }
}

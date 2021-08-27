using DivaRouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivaRouse.Classes
{
    class CurrentUser
    {
        public static List<Services> currentServices = new List<Services>();
        public static int currentCost { get; set; }
        public static string FullName { get; set; }
        public static string PermissionName { get; set; }
        public static bool AccessClients { get; set; }
        public static bool AccessServices { get; set; }
        public static bool AccessClientsServicrs { get; set; }
        public static bool AccessReport { get; set; }
    }
}

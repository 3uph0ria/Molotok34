﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ApiHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(Molotok34.Api)))
            {
                host.Open();
                Console.WriteLine("Host started...");
                Console.ReadLine();
            }
        }
    }
}

using System;
using Microsoft.Owin.Hosting;

namespace OwinSelfhost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (WebApp.Start<Startup>(url: "http://localhost:5000"))
            {
                Console.ReadLine();
            }
        }
    }
}

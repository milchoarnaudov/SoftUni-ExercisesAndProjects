using MvcApp.Controllers;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MvcApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            await Host.CreateHostAsync(new Startup());
        }
    }
}

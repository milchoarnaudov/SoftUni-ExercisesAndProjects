using Microsoft.EntityFrameworkCore;
using MvcApp.Controllers;
using MvcApp.Data;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvcApp
{
    class Startup : IMvcApplication
    {
        public void ConfigureServices()
        {
            throw new NotImplementedException();
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}

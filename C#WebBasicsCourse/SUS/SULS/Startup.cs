using Microsoft.EntityFrameworkCore;
using SULS.Data;
using SULS.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;

namespace SULS
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IProblemsService, ProblemsService>();
            serviceCollection.Add<ISubmissionsService, SubmissionsService>();
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}

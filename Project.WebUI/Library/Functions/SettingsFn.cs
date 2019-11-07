using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Data.Concrete.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebUI.Library.Functions
{
    public class SettingsFn
    {
        public static void CreateDatabaseService(IServiceCollection services, IConfiguration Configuration, IWebHostEnvironment env)
        {
            string mainDB = Configuration.GetConnectionString("Main");

            Console.WriteLine("##### [INFO] - MainDB : " + mainDB);

                services.AddDbContext<EFProjectContext>(option => option.UseSqlServer(mainDB));
        }
    }
}

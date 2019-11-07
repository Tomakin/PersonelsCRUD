using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.WebUI.Library.Functions
{
    public class PersonelFn
    {
        IWebHostEnvironment _env;
        public PersonelFn(IWebHostEnvironment env)
        {
            _env = env;
        }
        public List<string> GetContactTypes()
        {

            string path = _env.ContentRootPath + "/Library/Json/ContactTypes.json";
            string read = File.ReadAllText(path);

            return JsonSerializer.Deserialize<List<string>>(read);
        }

        public List<string> GetCountries()
        {

            string path = _env.ContentRootPath + "/Library/Json/Countries.json";
            string read = File.ReadAllText(path);

            return JsonSerializer.Deserialize<List<string>>(read);
        }
    }
}

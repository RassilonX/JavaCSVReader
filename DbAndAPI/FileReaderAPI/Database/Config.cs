using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database;

public static class Config
{
    private static readonly IConfiguration _configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

    public static readonly string DbConnectionString = _configuration.GetConnectionString("sql_db");
}

﻿using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;
using TaskMonitoring.Cards.DataAccess;

namespace TaskMonitoring.Bootsrap
{
	public static class Configurator
	{
		public static void ConfigureDatabase()
		{
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();

            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();
            var options = optionsBuilder.UseNpgsql(connectionString).Options;
        }
    }
}

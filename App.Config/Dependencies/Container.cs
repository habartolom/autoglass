using App.Config.DIAutoRegister;
using App.Domain.Services.Products;
using App.Infrastructure.Database.Context;
using App.Infrastructure.Repositories;
using App.Infrastructure.Repositories.Products;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Config.Dependencies
{
	public class Container
	{
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {

            #region Mapper

            services.AddMemoryCache();

            services.AddAutoMapper(typeof(Container));

            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            var mapper = configMapper.CreateMapper();

            services.AddSingleton(mapper);

            #endregion

            

            #region Conexion Base de datos

            services.AddSingleton<IConfiguration>(configuration);

            services.AddScoped<AutoglassContext, AutoglassContext>();

			#endregion

			#region Register DI
			var assembliesToScan = new[]
            {
                Assembly.GetExecutingAssembly(),
                Assembly.Load("App.Common"),
                Assembly.Load("App.Domain"),
                Assembly.Load("App.Infrastructure"),
            };

            services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
                .Where(c => c.Name.EndsWith("Repository") || c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();
            #endregion


            #region Others
            services.AddSingleton<IConfiguration>(configuration);
            #endregion
        }

        public static void CreateDatabase(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AutoglassContext>();
                    DbInitializer.Initialize(context);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}

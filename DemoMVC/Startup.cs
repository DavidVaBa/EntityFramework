using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoMVC.Context;
using DemoMVC.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoMVC
{
    public class Startup
    {
        public IConfigurationRoot  Configuration { get;  }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("./Views/appsettings.json", optional: false, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<EFDemoContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{Id?}");
            });

            GenerateDB(serviceProvider);
        }

        public void GenerateDB(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<EFDemoContext>();
            if (context.Database.EnsureCreated())
            {
                var list = new List<Movie>()
                {
                    new Movie { Title = "Civil War", ReleaseYear = 2016, Genre = "Superheores" },
                    new Movie { Title = "Shape of Water", ReleaseYear = 2018, Genre = "Romance" },
                    new Movie { Title = "Showman", ReleaseYear = 2017, Genre = "Musical" },
                    new Movie { Title = "Inception", ReleaseYear = 2013, Genre = "Action" },
                    new Movie { Title = "Jumanji", ReleaseYear = 2017, Genre = "Adventure" },
                    new Movie { Title = "Infinity War", ReleaseYear = 2018, Genre = "Superheores" },
                    new Movie { Title = "Paprika", ReleaseYear = 2014, Genre = "Anime" },
                    new Movie { Title = "It", ReleaseYear = 2017, Genre = "Terror" },
                    new Movie { Title = "Thor: Ragnarok", ReleaseYear = 2017, Genre = "Superheores" },
                    new Movie { Title = "Black Panther", ReleaseYear = 2018, Genre = "Superheores" }
                };
                context.AddRange(list);
                context.SaveChanges();
            }

        }
    }
}

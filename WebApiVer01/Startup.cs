namespace WebApiVer01
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using System;
    using System.IO;
    using System.Reflection;
    using WebApiVer01.Context;
    using WebApiVer01.Entitys;
    using WebApiVer01.Helpers;
    using WebApiVer01.Models;
    using WebApiVer01.Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<Autor, AutorDTO>().ReverseMap();
                configuration.CreateMap<AutorCreacionDTO, Autor>().ReverseMap();
            }, typeof(Startup));
            services.AddScoped<MiFiltroDeAccion>();
            services.AddDbContext<ApplicacionDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllers(options =>
            {
                options.Filters.Add(new MiFiltroDeException());
            });
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<HATEOASAuthorFilterAttribute>();
            services.AddScoped<GeneradorEnlaces>();

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "Mi Web API",
                    Description = "API servicio Hospital",
                    TermsOfService = new Uri("https://www.udemy.com/user/felipegaviln/"),
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("http://bfy.tw/4nqh")
                    },
                    Contact = new OpenApiContact()
                    {
                        Name = "Andres Leon",
                        Email = "dandresleon64@gmail.com",
                        Url = new Uri("https://gavilan.blog/")
                    }
                });


                
                

            });


            

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer();
            services.AddTransient<IClaseB, ClaseB2>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(config => {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");


            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseResponseCaching();

            //app.UseAuthentication();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
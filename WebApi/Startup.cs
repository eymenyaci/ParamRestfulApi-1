using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Microsoft.OpenApi.Models;
using WebApi.Services;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using WebApi.Interfaces;
using MediatR;
using WebApi.Mapper;
using WebApi.Dto;
using WebApi.Models.Entity;
using WebApi.Dtos;
using BookStore.Api.Interfaces;
using WebApi.Models.Context;

namespace WebApi
{
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

            services.AddControllers();
            services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName : "BookStoreDB"));
            services.AddScoped<IBookService, BookService>();
            //services.AddScoped<ILogService, LogService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IBookStoreDbContext, BookStoreDbContext>();
            services.AddMediatR(typeof(Startup));
            services.AddAutoMapper(typeof(AutoMapperConfig));
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Book, BookDto>();
                cfg.CreateMap<BookDto, Book>();
                cfg.CreateMap<Author,AuthorDto>();
                cfg.CreateMap<AuthorDto,Author>();
                cfg.CreateMap<Genre,GenreDto>();
                cfg.CreateMap<GenreDto, Genre>();
                
            });
            AutoMapperConfig.Init(mapperConfig);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
            var issuer = Configuration.GetSection("JwtConfig").GetValue<string>("Issuer");
            var audience = Configuration.GetSection("JwtConfig").GetValue<string>("Audience");
            var signingKey = Configuration.GetSection("JwtConfig").GetValue<string>("SigningKey");
            IdentityModelEventSource.ShowPII = true;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
                };
            });
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseMiddleware<ExceptionHandlerMiddleware>();
                app.UseMiddleware<LoggingMiddleware>();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

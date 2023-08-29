using System.Data.Common;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using MySqlConnector;
using MISA.AMIS.CTM.Infrastructure;
using MISA.AMIS.CTM.Application;
using MISA.AMIS.CTM.Domain;
using MISA.AMIS.CTM.Infrastructure.UnitOfWork;

namespace MISA.AMIS.CTM.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services

            // Add controller
            builder.Services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.PropertyNamingPolicy = null);

            // Add dbconnection
            builder.Services.AddTransient<DbConnection>(options =>
                    new MySqlConnection(builder.Configuration.GetConnectionString("AmisDb")));

            // Add unit of work
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add services
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            // Add domain services
            builder.Services.AddScoped<IEmployeeDomainService, EmployeeDomainService>();
            builder.Services.AddScoped<IDepartmentDomainService, DepartmentDomainService>();

            // Add helper
            builder.Services.AddTransient<IApplicationHelper, ApplicationHelper>();
            builder.Services.AddTransient<IInfrastructureHelper, InfrastructureHelper>();

            // Add repositories
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            // Add mapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Add middleware
            builder.Services.AddSingleton<ExceptionMiddleware>();

            // Add localization services
            builder.Services.AddLocalization();

            // Configure supported cultures
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("vi-VN"),
                };
                options.DefaultRequestCulture = new RequestCulture("vi-VN");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.ApplyCurrentCultureToResponseHeaders = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
            {
                build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

            #endregion

            var app = builder.Build();

            #region Configures

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enable localization middleware
            app.UseRequestLocalization();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("MyCors");

            app.MapControllers();

            app.UseMiddleware<ExceptionMiddleware>();

            app.Run();

            #endregion
        }
    }
}
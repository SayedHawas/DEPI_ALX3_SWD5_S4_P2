using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;


namespace WebApiDay5Lab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var myCors = "MyCors";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            //Add Context with Connection
            builder.Services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            //Configuration For swagger Docs
            builder.Services.AddSwaggerGen(doc =>
            {
                var filepath = Path.Combine(System.AppContext.BaseDirectory, "APIDocs.xml");
                doc.IncludeXmlComments(filepath);
                doc.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Smart API For DEPI",
                    Version = "v1",
                    Description = " ASP.NET Core WebAPI Course ",
                    TermsOfService = new Uri("https://github.com/SayedHawas/DEPI_ALX3_SWD5_S5"),
                    Contact = new OpenApiContact
                    {
                        Name = "Sayed Hawas",
                        Email = "sout_2000@hotmail.com",
                    }
                });
            });

            builder.Services.AddCors(options => options.AddPolicy(myCors, CorsPolicyBuilder =>
            {
                CorsPolicyBuilder
                                .AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();

            }));

            //builder.Services.AddScoped<Employee>();
            //Registration resolve service
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            builder.Services.AddScoped<IServiceDepartment, ServiceDepartment>();
            //-------------------------------------------------------
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors(myCors);

            app.MapControllers();

            app.Run();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MVCDemoLab.Data;

namespace MVCDemoLab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<MVCDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            //app.UseSession();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Site}/{action=Index}")
                //pattern: "{controller=Users}/{action=Login}")
                //pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            #region Custom MiddleWare
            //Custom Middleware
            //app.Use(async (HttpContext, next) =>
            //{
            //    //Execute  Request
            //    await HttpContext.Response.WriteAsync("1) Execute From MiddleWare ... \n");
            //    //Call Next 
            //    await next.Invoke();

            //    //Execute
            //    await HttpContext.Response.WriteAsync("5) Return Execute From MiddleWare ... \n");
            //});
            //app.Use(async (HttpContext, next) =>
            //{
            //    //Execute  Request
            //    await HttpContext.Response.WriteAsync("2) Execute From MiddleWare ... \n");
            //    //Call Next 
            //    await next.Invoke();

            //    //Execute
            //    await HttpContext.Response.WriteAsync("4) Return Execute From MiddleWare ... \n");
            //});

            //app.Map("/Home/Index", apiApp =>
            //{

            //    apiApp.Use(async (Context, next) =>
            //    {
            //        await Context.Response.WriteAsync("Home Controller Calling With Index Method ... \n");
            //        await next();
            //        //Execute
            //        await Context.Response.WriteAsync("Finish Map Return Execute From MiddleWare ... \n");
            //    });
            //    apiApp.Run(async context =>
            //    {
            //        await context.Response.WriteAsync(" Execute From MAP MiddleWare ... \n");
            //    });
            //});

            //app.Run(async (HttpContext) =>
            //{
            //    //Execute  Request
            //    await HttpContext.Response.WriteAsync("3) Execute From MiddleWare ... \n");
            //});
            #endregion
            app.Run();
        }
    }
}

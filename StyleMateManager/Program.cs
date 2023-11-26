using StyleMate.Data;
using StyleMate.Service.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace StyleMate.API
{
    /// <summary>
    /// Entry point for API
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Startup method
        /// </summary>
        /// <param name="args">The args</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "StyleMate API",
                    Description = "API for the StyleMate app"
                    //TermsOfService = new Uri("https://example.com/terms")
                });

                // using System.Reflection;
                //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            // add services to DI
            builder.Services.AddScoped<IGarmentService, GarmentService>();
            builder.Services.AddScoped<ISourceGarmentService, SourceGarmentService>();

            // ad db context to DI
            builder.Services.AddStyleMateContext(DatabaseConnectionHandler.Instance.Connection.ConnectionString);

            // add automapper to DI (https://code-maze.com/automapper-net-core/)
            //builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            //Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
using StyleMate.Data;
using Microsoft.OpenApi.Models;
using System.Reflection;

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
                    Title = "Fuelregistration API",
                    Description = "API for the fuelregistration app",
                    //TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Meer info",
                        Url = new Uri("https://github.com/LegeDoos/FuelRegistrationCore")
                    },
                    //  License = new OpenApiLicense
                    //  {
                    //      Name = "Deze licentie is van toepassing",
                    //      Url = new Uri("https://example.com/license")
                    //  }
                });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            // add peopleservice to DI
            builder.Services.AddScoped<IPeopleService, PeopleService>();
            // ad db context to DI
            var constring = DatabaseConnectionHandler.Instance.Connection.ConnectionString;
            builder.Services.AddFuelRegistrationContext(DatabaseConnectionHandler.Instance.Connection.ConnectionString);
            // add automapper to DI (https://code-maze.com/automapper-net-core/)
            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
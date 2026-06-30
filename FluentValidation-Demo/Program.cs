using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation_Demo.Resources;
using FluentValidation_Demo.Validators;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;

namespace FluentValidation_Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Localization
            builder.Services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

            // Controllers
            builder.Services.AddControllers()
                .AddDataAnnotationsLocalization();

            // FluentValidation
            builder.Services.AddValidatorsFromAssemblyContaining<ProductDtoValidator>();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.OperationFilter<AcceptLanguageHeaderOperationFilter>();
            });

            var app = builder.Build();

            // Supported languages
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ar")
            };

            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(localizationOptions);

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

    public class AcceptLanguageHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Required = false,
                Description = "Language (en or ar)",
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("en")
                }
            });
        }
    }
}
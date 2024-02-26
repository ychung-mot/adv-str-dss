using Asp.Versioning;
using NetCore.AutoRegisterDi;
using StrDss.Common;
using StrDss.Data;
using StrDss.Model;
using StrDss.Service;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new HeaderApiVersionReader("version");
    options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
});

var assemblies = Assembly.GetExecutingAssembly()
    .GetReferencedAssemblies()
    .Where(a => a.FullName.StartsWith("AdvSol"))
    .Select(Assembly.Load).ToArray();

//Services
builder.Services.RegisterAssemblyPublicNonGenericClasses(assemblies)
     .Where(c => c.Name.EndsWith("Service"))
     .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

//Repository
builder.Services.RegisterAssemblyPublicNonGenericClasses(assemblies)
     .Where(c => c.Name.EndsWith("Repository"))
     .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

//Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Headers
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

//FieldValidationService as Singleton
builder.Services.AddSingleton<IFieldValidatorService, FieldValidatorService>();

//RegexDefs as Singleton
builder.Services.AddSingleton<RegexDefs>();

//Add logging
builder.Services.AddLogging(builder => builder.AddConsole());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

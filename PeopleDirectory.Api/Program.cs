using Microsoft.EntityFrameworkCore;
using PeopleDirectory.Api.Extensions;
using PeopleDirectory.Api.Filters;
using PeopleDirectory.Api.Middlewares;
using PeopleDirectory.BusinessLogic;
using PeopleDirectory.DataAccess;
using PeopleDirectory.DataContracts.Resources;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddBusinessLogic();

builder.Services.AddLocalization(options => { options.ResourcesPath = "Resources"; });

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ModelValidationFilter>();
})
.AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
    factory.Create(typeof(ValidationMessages));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(a =>
{
    a.OperationFilter<FileUploadOperationFilter>();
    a.OperationFilter<LanguageHeaderFilter>();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    DatabaseSeeder.Seed(context);
}

var supportedCultures = new[] { "en", "ka" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionLoggingMiddleware>();

app.UseMiddleware<LocalizationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

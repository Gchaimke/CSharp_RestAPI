using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpLogging;
using Avdor.Api.Repositories;
using Avdor.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddW3CLogging(logging =>
{
    // Log all W3C fields
    logging.LoggingFields = W3CLoggingFields.All;

    logging.FileSizeLimit = 5 * 1024 * 1024;
    logging.RetainedFileCountLimit = 2;
    logging.FileName = "api_log_";
    logging.LogDirectory = @".\logs\";
    logging.FlushInterval = TimeSpan.FromSeconds(2);
});

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
builder.Services.AddDbContext<SqlDbRepository>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));


var app = builder.Build();

app.UseW3CLogging();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();

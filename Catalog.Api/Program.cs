using SecuringWebApiUsingApiKey.Middleware;
using Microsoft.EntityFrameworkCore;
using Catalog.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

// builder.Services.AddSingleton<IItemsRepository, InMemItemsRepository>();   
builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
builder.Services.AddDbContext<SqlDbRepository>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();

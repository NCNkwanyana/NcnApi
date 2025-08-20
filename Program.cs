using Microsoft.EntityFrameworkCore;
using NcnApi.Data;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=UsersTasks.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c=>
{
    c.SwaggerDoc("v1",new OpenApiInfo
{Title ="Assessment Tasks API",
Version ="v1",
Description ="Assessement API .Net"

});

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {

    c.SwaggerEndpoint("/swagger/v1/swagger.json","Users & Assessment Tasks API v1");

});
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

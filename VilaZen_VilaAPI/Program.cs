using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VilaZen_VilaAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDataContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("DafaultSQLConnection"));
});

builder.Services.AddControllers(option =>
{
    /*option.ReturnHttpNotAcceptable=true*/
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

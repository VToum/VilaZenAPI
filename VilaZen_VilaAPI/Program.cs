using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VilaZen_VilaAPI;
using VilaZen_VilaAPI.Data;
using VilaZen_VilaAPI.Repositorio;
using VilaZen_VilaAPI.Repositorio.IRepositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDataContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.AddScoped<IVillaRepositorio, VillaRepositorio>();
builder.Services.AddScoped<IVillaNumberRepositorio, VillaNumberRepositorio>();

builder.Services.AddAutoMapper(typeof(MapperConfig));
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

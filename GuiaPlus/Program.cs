using GuiaPlus.Application.Mapping;
using GuiaPlus.Application.Services;
using GuiaPlus.Domain.Entities;
using GuiaPlus.Domain.Interfaces.Services;
using GuiaPlus.Infrastructure.Data.Context;
using GuiaPlus.Infrastructure.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Infrastrucute Services
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IServicoService, ServicoService>();
builder.Services.AddScoped<IGuiaService, GuiaService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

//app.UseHttpsRedirection();

app.MapGroup("/identity").MapIdentityApi<IdentityUser>();

app.UseAuthorization();

app.MapControllers();

app.Run();

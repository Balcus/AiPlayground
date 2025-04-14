using AiPlayground.BusinessLogic.Interfaces;
using AiPlayground.BusinessLogic.Services;
using AiPlayground.DataAccess;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepository<Scope>, BaseRepository<Scope>>();
builder.Services.AddScoped<IScopeService, ScopeService>();
builder.Services.AddScoped<IRepository<Platform>, BaseRepository<Platform>>();
builder.Services.AddScoped<IPlatformService, PlatformService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AiPlaygroundContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

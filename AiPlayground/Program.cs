using AiPlayground.BusinessLogic.Interfaces;
using AiPlayground.BusinessLogic.Services;
using AiPlayground.DataAccess;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        b =>
        {
            b.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddScoped<IRepository<Scope>, ScopeRepository>();
builder.Services.AddScoped<IRepository<Platform>, PlatformRepository>();
builder.Services.AddScoped<IRepository<Model>, BaseRepository<Model>>();
builder.Services.AddScoped<IRepository<Prompt>, BaseRepository<Prompt>>();

builder.Services.AddScoped<IScopeService, ScopeService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScoped<IPromptService, PromptService>();

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
app.UseCors("AllowAnyOrigin");

app.Run();

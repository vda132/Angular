using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DBLayer.Context.TasksContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"),
        b => b.MigrationsAssembly("DBLayer")));

builder.Services.AddScoped<DBLayer.Repository.ITaskRepository, DBLayer.Repository.TaskRepository>();
builder.Services.AddScoped< Logic.Logic.ITaskLogic, Logic.Logic.TaskLogic>();

var allowResources = "AllowResources";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowResources,
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasks"));
}

app.UseCors(allowResources);

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();

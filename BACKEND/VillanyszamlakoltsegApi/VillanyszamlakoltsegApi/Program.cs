var builder = WebApplication.CreateBuilder(args);

// CORS engedélyezése, hogy a frontendet bárhonnan el tudd érni
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors();
app.MapControllers();

app.Run();
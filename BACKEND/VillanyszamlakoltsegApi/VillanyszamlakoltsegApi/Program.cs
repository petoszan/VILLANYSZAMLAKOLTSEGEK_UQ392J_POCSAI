var builder = WebApplication.CreateBuilder(args);

// CORS enged�lyez�se, hogy a frontendet b�rhonnan el tudd �rni
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
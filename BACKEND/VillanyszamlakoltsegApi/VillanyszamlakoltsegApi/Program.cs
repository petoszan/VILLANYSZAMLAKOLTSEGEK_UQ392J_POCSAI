var builder = WebApplication.CreateBuilder(args);



// Swagger
builder.Services.AddSwaggerGen();

// CORS engedélyezése, hogy a frontendet bárhonnan el tudjam érni
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader());
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.MapControllers();

app.Run();
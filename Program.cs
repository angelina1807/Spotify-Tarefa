using Microsoft.EntityFrameworkCore;
using TarefaSpotfyApi.Context;

var builder = WebApplication.CreateBuilder(args);

// Conexão com SQL Server
builder.Services.AddDbContext<SpotfyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

// Adiciona CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()    // Permite qualquer origem
              .AllowAnyHeader()    // Permite qualquer cabeçalho
              .AllowAnyMethod();   // Permite GET, POST, PUT, DELETE
    });
});

builder.Services.AddControllers();

// Swagger (opcional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// **Ativa CORS antes do UseAuthorization**
app.UseCors("PermitirTudo");

app.UseAuthorization();

app.MapControllers();

app.Run();

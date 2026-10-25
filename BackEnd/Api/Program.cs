using Microsoft.EntityFrameworkCore;
using Repositorio.Contexto;
using Dominio.Interfaces;
using Repositorio.Repositorios;
using Repositorio;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var chaveJwt = builder.Configuration["Jwt:JogosIndiesSaoGeralmenteMelhores"] 
    ?? throw new InvalidOperationException("Chave JWT não configurada.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveJwt)),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Emissor"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audiencia"],
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddDbContext<ProjetoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

builder.Services.AddScoped(typeof(IBaseRepositorio<>), typeof(BaseRepositorio<>));
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IJogoRepositorio, JogoRepositorio>();
builder.Services.AddScoped<IGuiaRepositorio, GuiaRepositorio>();
builder.Services.AddScoped<IComentarioRepositorio, ComentarioRepositorio>();
builder.Services.AddScoped<IGuiaSalvoRepositorio, GuiaSalvoRepositorio>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("PermitirFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
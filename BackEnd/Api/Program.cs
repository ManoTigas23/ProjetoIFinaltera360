using Microsoft.EntityFrameworkCore;
using Repositorio.Contexto;
using Repositorio.Interfaces;
using Repositorio.Repositorios;
using Aplicacao;
using Repositorio;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Aplicacao.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Aplicacao
builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();
builder.Services.AddScoped<IJogoAplicacao, JogoAplicacao>();
builder.Services.AddScoped<IGuiaAplicacao, GuiaAplicacao>();

//Repositorio

builder.Services.AddScoped(typeof(IBaseRepositorio<>), typeof(BaseRepositorio<>));
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IJogoRepositorio, JogoRepositorio>();
builder.Services.AddScoped<IGuiaRepositorio, GuiaRepositorio>();


var chaveJwt = builder.Configuration["Jwt:JogosIndiesSaoGeralmenteMelhores"] 
    ?? throw new InvalidOperationException("Chave JWT não configurada.");



builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

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
    options.UseSqlite(
        builder.Configuration.GetConnectionString("ConexaoPadrao"),
        b => b.MigrationsAssembly("Api")
    ));



builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("PermitirFrontend");
app.UseHttpsRedirection();       

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
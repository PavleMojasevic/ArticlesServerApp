using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServerApp.Infrastucture;
using ServerApp.Interfaces;
using ServerApp.Mapper;
using ServerApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddDbContext<VoziNaStrujuDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("VoziNaStrujuDatabase")));

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

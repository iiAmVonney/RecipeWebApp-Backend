using Microsoft.EntityFrameworkCore;
using RecipeAPI.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddDbContext<RecipeAPIDbContext>(options => options.UseInMemoryDatabase("RecipesDB"));
builder.Services.AddDbContext<RecipeAPIDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RecipeDbConnectionString")));

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corspolicy");

/*app.UseFileServer();*/

app.UseAuthorization();

app.MapControllers();

app.Run();

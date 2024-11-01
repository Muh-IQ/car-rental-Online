using Business;
using Data;
using Data.Module;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conection"),
    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

builder.Services.AddTransient(typeof(GeneralRepositoryBusiness<>), typeof(GeneralRepositoryBusiness<>));
builder.Services.AddTransient(typeof(IGeneralRepository<>), typeof(GeneralRepository_Data<>));

builder.Services.AddTransient(typeof(GeneralTransactionRelationRepository_Business<>),
    typeof(GeneralTransactionRelationRepository_Business<>));
builder.Services.AddTransient(typeof(IGeneralTransactionRelationRepository<>),
    typeof(GeneralTransactionRelationRepository_Data<>));










var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// إضافة CORS
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();

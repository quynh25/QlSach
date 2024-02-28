using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QlSach.Data;
using QlSach.Interface;
using QlSach.Interfaces;
using QlSach.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

///
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IChuDeRepository,ChuDeRepository>();
builder.Services.AddScoped<ISachRepository,SachRepository>();
builder.Services.AddScoped<INXBRepository,NXBRepository>();
builder.Services.AddScoped<ITacGiaRepository,TacGiaRepository>();
builder.Services.AddScoped<IDonHangRepository,DonHangRepository>();
builder.Services.AddScoped<IKhachHangRepository,KhachHangRepository>();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefautlConnection"));
});
///

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

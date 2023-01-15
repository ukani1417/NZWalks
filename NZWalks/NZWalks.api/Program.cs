using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NZWalks.api.Data;
using NZWalks.api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region services

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
#region database_services
builder.Services.AddDbContext<NZWalksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks"));
});
#endregion

#region repository_services
builder.Services.AddScoped<IWalkDiffcultyRepository, WalkDiffcultyRepository>();
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IWalkRepository, WalkRepository>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
#endregion

#endregion

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

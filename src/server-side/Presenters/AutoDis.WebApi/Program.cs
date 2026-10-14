using AutoDis.Persistense;
using AutoDis.Persistense.EntityFramework;
using AutoDis.UseCases;
using AutoDis.UseCases.Cars;
using AutoDis.WebApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddPersistense(builder.Configuration);
builder.Services.AddUseCases();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    ICarSeedService carSeedService = new CarSeedService(context);
    await carSeedService.LoadSeeds();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

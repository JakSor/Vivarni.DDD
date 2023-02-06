using Application;
using Application.Specifications.GuestMessages;
using Ardalis.Specification;
using Core.Entities;
using Infrastructure;
using Persistence;
using Shared.Shared.DTO;
using Vivarni.DDD.Core.Repositories;
using Vivarni.DDD.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapPost("/GuestMessages", async (IGenericRepository<GuestMessage> guestMessageRepository, GuestMessageCreateDTO insertModel) =>
{
    var entity = new GuestMessage(insertModel);
    return await guestMessageRepository.AddAsync(entity);
});
app.MapGet("/GuestMessages", async (IGenericRepository<GuestMessage> guestMessageRepository) =>
{
    return await guestMessageRepository.ListAsync();
});



app.Run();



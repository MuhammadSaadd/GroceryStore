using GroceryStore.Gateway;
using GroceryStore.Infrastructure.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterLayers(builder.Configuration);


var app = builder.Build();

await app.Services.MigrateDatabase(builder.Configuration);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
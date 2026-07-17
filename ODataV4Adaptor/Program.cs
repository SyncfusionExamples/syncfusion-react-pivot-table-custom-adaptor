using Newtonsoft.Json.Serialization;
using Microsoft.OData.ModelBuilder;
using ODataV4Adaptor.Models;
using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);

// Create an ODataConventionModelBuilder to build the OData model
var modelBuilder = new ODataConventionModelBuilder();

// Register the "Orders" entity set with the OData model builder
modelBuilder.EntitySet<OrdersDetails>("Orders");

// Add services to the container.

builder.Services.AddMvc().AddNewtonsoftJson(options =>
{
  options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(policy =>
  {
    policy.AllowAnyOrigin()      // Allow requests from any origin.
          .AllowAnyMethod()       // Allow GET, POST, PUT, DELETE, etc.
          .AllowAnyHeader();      // Allow any request headers.
  });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers().AddOData(
    options => options
        .Count()
        .AddRouteComponents("odata", modelBuilder.GetEdmModel()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();

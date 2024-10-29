using CGMWPF.Back.DataAccess;
using CGMWPF.Back.WebAPI;
using CGMWPF.Common;
using DataAccess.Repositories;
using Microsoft.OpenApi.Models;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
     c =>
     {
         c.SwaggerDoc("v1", new OpenApiInfo { Title = "CGM Swagger API", Version = "v1" });
         // Add default header value and key
         c.OperationFilter<CustomHeader>();

     }
    );
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Enable middleware to serve generated Swagger as a JSON endpoint
    app.UseSwagger();
    app.UseSwaggerUI(
        c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "CGM Swagger API V1");
        }
        );
}
APISetting.Configuration = app.Configuration;

CommonSetting.ConnectString = iConvert.ToString(app.Configuration.GetValue(typeof(string), APISetting.KEY_CONNECTSTR));
CommonSetting.PackageConnectSting = iConvert.ToString(app.Configuration.GetValue(typeof(string), APISetting.KEY_CONNECTSTR_PACKAGE));
CommonSetting.LogOutputDirectory = iConvert.ToString(app.Configuration.GetValue(typeof(string), APISetting.KEY_LOGDIRECTORY));
CommonSetting.Log = new iLog(180, CommonSetting.LogOutputDirectory);
// Configure the HTTP request pipeline.
app.MapControllers();
app.Run();



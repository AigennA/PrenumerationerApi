using Microsoft.Extensions.FileProviders;
using PrenumerationerApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileStorage>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp", policy =>
        policy.WithOrigins("http://localhost:5173", "http://localhost:8081")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(app.Services.GetRequiredService<FileStorage>().FolderPath),
    RequestPath = FileStorage.RequestPath
});
app.UseCors("AllowWebApp");
app.MapControllers();

app.Run();

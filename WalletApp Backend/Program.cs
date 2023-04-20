using Microsoft.EntityFrameworkCore;
using WalletApp_Backend;
using WalletApp_Backend.Authorization;
using WalletApp_Backend.DataBase;
using WalletApp_Backend.User;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation();
builder.Services.AddDataBase(builder.Configuration);
builder.Services.AddAuthorizationConfig(builder.Configuration);
builder.Services.AddUserConfig(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
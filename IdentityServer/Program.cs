using IdentityServer;
using IdentityServer4.Models;
using IdentityServer4.Test;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer()
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddDeveloperSigningCredential();

var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseIdentityServer();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();

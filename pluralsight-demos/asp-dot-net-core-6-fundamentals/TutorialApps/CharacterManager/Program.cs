var builder = WebApplication.CreateBuilder(args);

#region Register Services

builder.Services.AddControllersWithViews();

#endregion

var app = builder.Build();

#region Create Middleware Pipeline

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute();    // endpoint middleware

#endregion

app.Run();

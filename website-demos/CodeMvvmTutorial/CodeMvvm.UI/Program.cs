using CodeMvvm.Data.Models;
using CodeMvvm.Data.Repositories;
using CodeMvvm.Data.DefaultData;
using CodeMvvm.ViewModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Services
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContextFactory<CodeMvvmDbContext>(opt =>
{
    opt.UseInMemoryDatabase("CodeMvvmDb");
});
builder.Services.AddScoped<IConditionRepository, ConditionRepository>();
builder.Services.AddScoped<ConditionViewModel>();
#endregion

var app = builder.Build();

// Initialize our in memory data. Once this is SQL, do this via migrations
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CodeMvvmDbContext>();
    context.Database.EnsureCreated();
}

#region Pipeline
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
#endregion

app.Run();

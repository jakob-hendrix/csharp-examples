using MediatRDemo.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMediatR(config =>
{
});
builder.Services.AddDbContext<ContactsContext>(options =>
{
    options.UseInMemoryDatabase("MediatRDemoDb");
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<ContactsContext>();
    context?.Database.EnsureCreated();
}

app.Run();
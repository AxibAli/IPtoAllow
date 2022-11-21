var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, configuration) =>
{
    configuration.Sources.Clear();

    configuration
        .AddXmlFile("Settings.config.xml", optional: true, reloadOnChange: true);

    configuration.AddEnvironmentVariables();

    if (args is { Length: > 0 })
    {
        configuration.AddCommandLine(args);
    }
});

//builder.Services.AddSingleton<IConfiguration>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

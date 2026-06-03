using RealTimeChatApp.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add standard MVC controller services to the web app core engine
builder.Services.AddControllersWithViews();

// Register the built-in Real-time SignalR service context middleware
builder.Services.AddSignalR();

var app = builder.Build();

// Setup the HTTP context pipelining rules
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Directly points the app launch page to the default Home controller index path
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map the custom background websocket route path matching our front-end JavaScript client initialization
app.MapHub<ChatHub>("/chatHub");

app.Run();
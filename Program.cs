using Microsoft.AspNetCore.Authentication;
using Module4.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<PaymentOptions>()
    .BindConfiguration("Payments")
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddAuthentication("Training")
    .AddScheme<AuthenticationSchemeOptions, TrainingAuthHandler>(
        "Training",
        options => { });

builder.Services.AddAuthorization();

builder.Services.AddControllers();  // ✅ ADD THIS LINE
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers()
   .RequireAuthorization();

app.Run();
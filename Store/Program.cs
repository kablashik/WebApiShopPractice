using System.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Prometheus;
using Quartz;
using Quartz.AspNetCore;
using Serilog;
using Serilog.Formatting.Compact;
using WebApplicationL5.Data.EF;
using WebApplicationL5.Data.Email;
using WebApplicationL5.Data.Models;
using WebApplicationL5.Jobs;
using WebApplicationL5.ModelMappers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login2";
        options.AccessDeniedPath = "/login2";
    });

//builder.Services.Configure<SmtpSettings>(Configuration.GetSection("smtp"));

//builder.Services.Configure<SmtpSettings>(smtpSettings =>
//{
//    smtpSettings.Server = "smtp.gmail.com";
//    smtpSettings.Port = 465;
//    smtpSettings.SenderName = "Konstantin";
//    smtpSettings.SenderEmail = "aspnetsendertest@gmail.com";
//    smtpSettings.UserName = "aspnetsendertest@gmail.com";
//    smtpSettings.Password = "-";
//    smtpSettings.DefaultCredentials = false;
//    smtpSettings.SSL = true;
//});

//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = "MyApp",
//        ValidAudience = "MyClient",
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_secret_long_key"))
//    };
//    options.Events = new JwtBearerEvents
//    {
//        OnMessageReceived = context =>
//        {
//            context.Token = context.Request.Cookies["token"];
//            return Task.CompletedTask;
//        }
//    };
//})


builder.Services.AddScoped<IOrderModelMapper, OrderModelMapper>();
builder.Services.AddScoped<ICustomerModelMapper, CustomerModelMapper>();
//builder.Services.AddSingleton<IEmailSender, EmailSenderService>();
//builder.Services.AddQuartz(q =>
//{
//    q.AddJob<EmailSender>(op => op.WithIdentity("job"));
//    q.AddTrigger(t =>
//    {
//        t.ForJob("job");
//        t.WithIdentity("trigger1", "group1")
//            .StartNow()
//            .WithSimpleSchedule(c => c
//                .WithIntervalInSeconds(10)
//                .RepeatForever());
//    });
//});
//builder.Services.AddQuartzServer();


builder.Services.AddDbContext<EFDataContext>();
builder.Host.UseSerilog((context, configuration) =>
{
    var customTemplate =
        "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}";

    configuration
        .WriteTo.Console(outputTemplate: customTemplate)
        .WriteTo.File(new CompactJsonFormatter(), "Logs/MyLog.txt", rollingInterval: RollingInterval.Day);
});

//.AddRazorRuntimeCompilation()
//.AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamePolicy();
//    options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString |
//                                                   JsonNumberHandling.AllowNamedFloatingPointLiterals;
//    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
//    options.JsonSerializerOptions.Converters.Add(new DoubleConverter());
//});

//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{productId?}");

app.Use(async (context, next) =>
{
    //var userId = context.User.Claims.FirstOrDefault(user => user.Type == "UserId")?.Value;
    var userId = "2";
    using (app.Logger.BeginScope(new Dictionary<string, int>() { { "UserId", int.Parse(userId) } }))
    {
        await next.Invoke();
    }
});

app.UseMetricServer();

app.Run();
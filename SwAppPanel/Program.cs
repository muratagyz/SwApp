using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SwAppData.EntityFramework;
using SwAppData.Enum;
using SwAppService.AutoFac;
using SwAppService.Mapping;
using SwAppService.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<HomeSliderValidatior>());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ElevatedRights", policy =>
        policy.RequireRole(Role.Admin.ToString(), Role.Editor.ToString()));
});

builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromDays(1); });

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder => { builder.RegisterModule(new AutoFacServiceModule()); });

builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"),
        options => { options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name); });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePages();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.Use(async (context, next) =>
{
    var JWToken = context.Session.GetString("JWToken");
    if (!string.IsNullOrEmpty(JWToken)) context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
    await next();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Auth}/{action=Login}/{id?}");

app.Run();
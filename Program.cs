using EasyConnect.Application.Services.Base;
using EasyConnect.Data;
using EasyConnect.Data.DataSeeders;
using EasyConnect.Extensions;
using EasyConnect.Models.Entities;
using EasyConnect.Models.Repositories;
using EasyConnect.Application.Services;
using EasyConnect.Models.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;
using EasyConnect.Models.Dtos;
using EasyConnect.Models.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);


builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Sql")));

// Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IService<,>), typeof(Service<,>));
builder.Services.AddScoped<IBusinessService, BusinessService>();
builder.Services.AddScoped<IWorkingHourService, WorkingHourService>();
builder.Services.AddScoped<IHolidayService, HolidayService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();




// CORS Ayarları
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .SetIsOriginAllowed(origin =>
            {
                if (Uri.TryCreate(origin, UriKind.Absolute, out var uri))
                {
                    return uri.Host == "localhost"; // Tüm localhost adreslerini kabul et
                }
                return false;
            })
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders("X-Pagination");
});
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.LoginPath = "/login"; // Oturum açma sayfası
    options.AccessDeniedPath = "/denied"; // Yetkisiz erişim yönlendirmesi

});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await SeedRoles.Initialize(services);
        await SeedUsers.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veri tabanı seed işlemi sırasında bir hata oluştu.");
    }
}

app.MapIdentityApi<User>();

app.MapPost("/logout", async (SignInManager<User> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization();

app.MapGet("/me", async (UserManager<User> userManager, ClaimsPrincipal user) =>
{
    var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
    if (userId == null)
    {
        return Results.NotFound(new { message = "Kullanıcı bulunamadı." });
    }

    var currentUser = await userManager.FindByIdAsync(userId);

    var userRoles = await userManager.GetRolesAsync(currentUser);

    if (currentUser == null)
    {
        return Results.NotFound(new { message = "Kullanıcı bulunamadı." });
    }

    return Results.Ok(new
    {
        userId = currentUser.Id,
        email = currentUser.Email,
        roles = userRoles.ToArray()
    });
}).RequireAuthorization();

app.MapPost("/register/business", async (
        RegisterBusinessDto dto,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<User> signIn,
        AppDbContext db) =>
{
    if (!await roleManager.RoleExistsAsync("Business"))
        await roleManager.CreateAsync(new IdentityRole("Business"));

    var user = new User
    {
        UserName = dto.Email,
        Email = dto.Email,
        FullName = dto.FullName, 
        UserType = UserType.Business
    };

    var create = await userManager.CreateAsync(user, dto.Password);
    if (!create.Succeeded)
        return Results.BadRequest(create.Errors);

    await userManager.AddToRoleAsync(user, "Business");

    return Results.Ok(new { message = "Business registered" });
});

app.MapPost("/register/customer", async (
    RegisterCustomerDto dto,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager) =>
{
    if (!await roleManager.RoleExistsAsync("Customer"))
        await roleManager.CreateAsync(new IdentityRole("Customer"));

    var user = new User
    {
        UserName = dto.Email,
        Email = dto.Email,
        FullName = dto.FullName,
        UserType = UserType.Customer
    };

    var result = await userManager.CreateAsync(user, dto.Password);
    if (!result.Succeeded)
        return Results.BadRequest(result.Errors);

    await userManager.AddToRoleAsync(user, "Customer");

    return Results.Ok(new { message = "Customer registered" });
});

app.MapPost("/login/business", async (
    LoginDto dto,
    SignInManager<User> signInManager,
    UserManager<User> userManager) =>
{
    var user = await userManager.FindByEmailAsync(dto.Email);
    if (user == null || user.UserType != UserType.Business)
        return Results.Unauthorized();

    var result = await signInManager.PasswordSignInAsync(user, dto.Password, isPersistent: true, lockoutOnFailure: false);

    if (!result.Succeeded)
        return Results.Unauthorized();

    return Results.Ok(new { message = "Business login successful", userType = user.UserType.ToString() });
});

app.MapPost("/login/customer", async (
    LoginDto dto,
    SignInManager<User> signInManager,
    UserManager<User> userManager) =>
{
    var user = await userManager.FindByEmailAsync(dto.Email);
    if (user == null || user.UserType != UserType.Customer)
        return Results.Unauthorized();

    var result = await signInManager.PasswordSignInAsync(user, dto.Password, isPersistent: true, lockoutOnFailure: false);

    if (!result.Succeeded)
        return Results.Unauthorized();

    return Results.Ok(new { message = "Customer login successful", userType = user.UserType.ToString() });
});





if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

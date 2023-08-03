using AspNetCoreHero.ToastNotification;
using AutoMapper;
using ENB.Church.Members.EF;
using ENB.Church.Members.EF.Repositories;
using ENB.Church.Members.Entities;
using ENB.Church.Members.Entities.Repositories;
using ENB.Church.Members.Infrastructure;
using ENB.Church.Members.MVC.Factory;
using ENB.Church.Members.MVC.Models;
using ENB.SchoolTimetables.EF;
using ENB.SchoolTimetables.MVC.Help;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ChurchMembersContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("ChurchMemberCtr1")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
               opt =>
               {
                   opt.Password.RequiredLength = 7;
                   opt.Password.RequireDigit = false;
                   opt.Password.RequireUppercase = false;
               })
                .AddEntityFrameworkStores<ChurchMembersContext>();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomClaimsFactory>();
builder.Services.AddAutoMapper(typeof(ChurchMemberProfile));
builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddScoped<IAsyncMemberRepository, AsyncMemberRepository>();
builder.Services.AddScoped<IAsyncStaffRepository, AsyncStaffRepository>();
builder.Services.AddScoped<IAsyncMinistryRepository, AsyncMinistryRepository>();
builder.Services.AddScoped<IAsyncActivityRepository, AsyncActivityRepository>();
builder.Services.AddScoped<IAsyncUnitOfWorkFactory, AsyncEFUnitOfWorkFactory>();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
builder.Services.AddScoped<IValidator<CreateAndEditMember>, CreateAndEditMemberValidator>();
builder.Services.AddScoped<IValidator<CreateAndEditStaff>, CreateAndEditStaffValidator>();
builder.Services.AddScoped<IValidator<CreateAndEditMinistry>, CreateAndEditMinistryValidator>();

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

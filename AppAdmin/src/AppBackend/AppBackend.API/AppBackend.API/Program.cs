
using AppBackend.Application.Common.Interface;
using AppBackend.Application.Common.Services;
using AppBackend.Application.Interface;
using AppBackend.Application.Services;
using AppBackend.Data.Context;
using AppBackend.Data.Models.Email;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var Configuration = builder.Configuration;
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(options =>
	{
		options.SaveToken = true;
		options.RequireHttpsMetadata = false;
		options.TokenValidationParameters = new TokenValidationParameters()
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidAudience = Configuration["JWT:ValidAudience"],
			ValidIssuer = Configuration["JWT:ValidIssuer"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
		};
	});
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("author2", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey
	});
	options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.Configure<IdentityOptions>(options => options.SignIn.RequireConfirmedEmail = true);
builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddRoles<IdentityRole>().
	AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddHangfire(config =>
{
	config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddSignalR();

builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IShipmentService, ShipmentService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IWorkAttendanceService, WorkAttendanceService>();
builder.Services.AddScoped<ISalaryService, SalaryService>();
builder.Services.AddScoped<IRegisterDayLonganService, RegisterDayLonganService>();
builder.Services.AddScoped<IRegisterRemainningLonganService, RegisterRemainningLonganService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllLocalhost", builder =>
	{
		builder
			.SetIsOriginAllowed(_ => true) // Allow all origins
			.AllowAnyMethod()              // Allow all HTTP methods
			.AllowAnyHeader()              // Allow all headers
			.AllowCredentials();           // Allow credentials (cookies, authorization headers, etc.)
	});
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
//app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHangfireServer();
app.UseHangfireDashboard();
app.MapGet("/", () => "Hangfire is running!");
#pragma warning disable CS0618 // Type or member is obsolete
RecurringJob.AddOrUpdate<WorkAttendanceService>(x => x.Post(), "0 4 * * *", TimeZoneInfo.Local);
RecurringJob.AddOrUpdate<WorkAttendanceService>(x => x.Remove(), "0 23 * * *", TimeZoneInfo.Local);
RecurringJob.AddOrUpdate<WorkAttendanceService>(x => x.SendMailToEmployee(), "10 2 * * *", TimeZoneInfo.Local);
#pragma warning restore CS0618 // Type or member is obsolete

app.MapIdentityApi<IdentityUser>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAllLocalhost");
app.MapHub<NotificationHub>("/notificationHub").RequireCors("AllowAllLocalhost");
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{

	//endpoints.MapHub<NotificationHub>("/notificationHub");

});
app.MapControllers();


app.Run();

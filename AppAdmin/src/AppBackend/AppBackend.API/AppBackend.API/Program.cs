
using AppBackend.Application.Common.Interface;
using AppBackend.Application.Common.Services;
using AppBackend.Application.Interface;
using AppBackend.Application.Services;
using AppBackend.Data.Context;
using AppBackend.Data.Models.Email;
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

//builder.Services.AddHangfire(config =>
//{
//	config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
//});

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//app.UseHangfireServer();
//app.UseHangfireDashboard();

//app.MapGet("/", () => "Hangfire is running!");


app.MapIdentityApi<IdentityUser>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

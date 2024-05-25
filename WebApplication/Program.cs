using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Profiles;
using WebApplication.Repositories;
using WebApplication.Services;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<PersonContext>(opt =>
    opt.UseInMemoryDatabase("PersonList"));
builder.Services.AddAutoMapper(typeof(PersonProfile));
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

SeedData(app);

app.Run();

void SeedData(Microsoft.AspNetCore.Builder.WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<PersonContext>();
        context.Database.EnsureCreated();
        if (!context.Persons.Any())
        {
            context.Persons.AddRange(
                new Person { FirstName = "Phan Thành Công", LastName = "Đặng", Gender = Gender.Male, DateOfBirth = new DateTime(2000, 6, 15), PhoneNumber = "0375.284.637", Birthplace = "Lâm Đồng", IsGraduated = true },
                new Person { FirstName = "Mỹ Linh", LastName = "Nguyễn", Gender = Gender.Female, DateOfBirth = new DateTime(1995, 6, 2), PhoneNumber = "0375.284.636", Birthplace = "Hà Nội", IsGraduated = true },
                new Person { FirstName = "Mai Phương", LastName = "Trần", Gender = Gender.Female, DateOfBirth = new DateTime(2001, 4, 5), PhoneNumber = "0375.284.635", Birthplace = "Hải Phòng", IsGraduated = false },
                new Person { FirstName = "Thu Hà", LastName = "Phạm", Gender = Gender.Female, DateOfBirth = new DateTime(2002, 1, 1), PhoneNumber = "0375.284.634", Birthplace = "Thái Bình", IsGraduated = false },
                new Person { FirstName = "Minh Quang", LastName = "Nguyễn", Gender = Gender.Male, DateOfBirth = new DateTime(1994, 7, 4), PhoneNumber = "0375.284.633", Birthplace = "Hà Nội", IsGraduated = true }
            );
            context.SaveChanges();
        }
    }
}
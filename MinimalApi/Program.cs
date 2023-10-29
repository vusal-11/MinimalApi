using InstantAPIs;
using Microsoft.EntityFrameworkCore;
using MinimalApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StudentsMVCContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddInstantAPIs();

var app = builder.Build();

app.MapInstantAPIs<StudentsMVCContext>();   

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/test",async (StudentsMVCContext context) =>
{
   return await context.Students.ToListAsync();
});

app.MapPost("/test", async (Student student) =>
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<StudentsMVCContext>();        

    context.Students.Add(student);
    await context.SaveChangesAsync();

    return Results.Created($"/test/{student.Id}", student);



});


app.Run();


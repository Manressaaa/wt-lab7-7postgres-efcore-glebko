using Microsoft.EntityFrameworkCore;
using wt_lab7_7postgres_efcore_glebko.Data;
using wt_lab7_7postgres_efcore_glebko.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Умная настройка для Render
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrWhiteSpace(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':', 2);
    connectionString = $"Host={uri.Host};" +
                       $"Port={uri.Port};" +
                       $"Database={uri.AbsolutePath.TrimStart('/')};" +
                       $"Username={Uri.UnescapeDataString(userInfo[0])};" +
                       $"Password={Uri.UnescapeDataString(userInfo[1])};" +
                       "SSL Mode=Disable";
}

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("/", () => "Minimal API работает! Перейдите на /swagger для тестирования.");
app.MapGet("/api/info", () => new { Application = "Web Technologies", Technology = "ASP.NET Core Minimal API", Version = "1.0" });

app.MapGet("/api/flights", async (AppDbContext db) => await db.Flights.ToListAsync());

app.MapGet("/api/flights/search", async (string? search, AppDbContext db) =>
{
    var query = db.Flights.AsQueryable();
    if (!string.IsNullOrWhiteSpace(search))
    {
        query = query.Where(x => x.gorod_vilet.ToLower().Contains(search.ToLower())
                              || x.gorod_prilet.ToLower().Contains(search.ToLower()));
    }
    return Results.Ok(await query.ToListAsync());
});

app.MapGet("/api/flights/{id}", async (int id, AppDbContext db) =>
{
    var flight = await db.Flights.FirstOrDefaultAsync(x => x.nomer_reisa == id);
    return flight == null ? Results.NotFound() : Results.Ok(flight);
});

app.MapPost("/api/flights", async (Flight flight, AppDbContext db) =>
{
    db.Flights.Add(flight);
    await db.SaveChangesAsync();
    return Results.Created($"/api/flights/{flight.nomer_reisa}", flight);
});

app.MapPut("/api/flights/{id}", async (int id, Flight updatedFlight, AppDbContext db) =>
{
    var flight = await db.Flights.FirstOrDefaultAsync(x => x.nomer_reisa == id);
    if (flight == null) return Results.NotFound();

    flight.gorod_vilet = updatedFlight.gorod_vilet;
    flight.gorod_prilet = updatedFlight.gorod_prilet;
    flight.vremya_vileta = updatedFlight.vremya_vileta;
    flight.cena = updatedFlight.cena;
    flight.mesta = updatedFlight.mesta;
    flight.dostupno = updatedFlight.dostupno;

    await db.SaveChangesAsync();
    return Results.Ok(flight);
});

app.MapDelete("/api/flights/{id}", async (int id, AppDbContext db) =>
{
    var flight = await db.Flights.FirstOrDefaultAsync(x => x.nomer_reisa == id);
    if (flight == null) return Results.NotFound();

    db.Flights.Remove(flight);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// --- ШАГ 19: Дополнительные LINQ-запросы ---

// LINQ 1: Фильтрация — получить только рейсы дороже 200
app.MapGet("/api/flights/linq/filter", async (AppDbContext db) =>
{
    var flights = await db.Flights
        .Where(x => x.cena > 200)
        .ToListAsync();
    return Results.Ok(flights);
});

// LINQ 2: Сортировка — получить рейсы, отсортированные по цене (от дешевых к дорогим)
app.MapGet("/api/flights/linq/sort", async (AppDbContext db) =>
{
    var flights = await db.Flights
        .OrderBy(x => x.cena)
        .ToListAsync();
    return Results.Ok(flights);
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");
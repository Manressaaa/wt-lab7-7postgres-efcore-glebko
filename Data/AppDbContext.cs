using Microsoft.EntityFrameworkCore;
using wt_lab7_7postgres_efcore_glebko.Models;

namespace wt_lab7_7postgres_efcore_glebko.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Flight> Flights => Set<Flight>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Flight>().HasKey(f => f.nomer_reisa);

        modelBuilder.Entity<Flight>().HasData(
            new Flight { nomer_reisa = 1, gorod_vilet = "Нюйорк", gorod_prilet = "Горки)", vremya_vileta = new DateTime(2026, 11, 15, 8, 30, 0), cena = 250, mesta = 45, dostupno = true, CreatedAt = new DateTime(2026, 1, 1) },
            new Flight { nomer_reisa = 2, gorod_vilet = "Минск", gorod_prilet = "Горки", vremya_vileta = new DateTime(2026, 11, 16, 12, 0, 0), cena = 180, mesta = 30, dostupno = true, CreatedAt = new DateTime(2026, 1, 2) },
            new Flight { nomer_reisa = 3, gorod_vilet = "Москва", gorod_prilet = "Горки", vremya_vileta = new DateTime(2026, 11, 17, 22, 15, 0), cena = 400, mesta = 0, dostupno = false, CreatedAt = new DateTime(2026, 1, 3) },
            new Flight { nomer_reisa = 4, gorod_vilet = "Гомель", gorod_prilet = "Горки", vremya_vileta = new DateTime(2026, 12, 20, 14, 0, 0), cena = 300, mesta = 50, dostupno = true, CreatedAt = new DateTime(2026, 1, 4) },
            new Flight { nomer_reisa = 5, gorod_vilet = "Витебск", gorod_prilet = "Горки", vremya_vileta = new DateTime(2026, 12, 25, 6, 45, 0), cena = 150, mesta = 20, dostupno = true, CreatedAt = new DateTime(2026, 1, 5) }
        );
    }
}
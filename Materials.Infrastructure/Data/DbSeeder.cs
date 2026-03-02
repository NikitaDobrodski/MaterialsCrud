using Materials.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Materials.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(MaterialsDbContext db)
    {
        await db.Database.MigrateAsync();

        if (!await db.Genres.AnyAsync())
        {
            db.Genres.AddRange(
                new Genre { Name = "Фантастика" },
                new Genre { Name = "Боевик" },
                new Genre { Name = "Мультфильм" },
                new Genre { Name = "Комедия" },
                new Genre { Name = "Драма" },
                new Genre { Name = "Триллер" }
            );
            await db.SaveChangesAsync();
        }

        if (!await db.Titles.AnyAsync())
        {
            // Пример словаря. Потом расширишь.
            var genres = await db.Genres.ToListAsync();
            Genre G(string name) => genres.Single(x => x.Name == name);

            var t1 = new Title { Name = "Матрица" };
            t1.Genres.Add(new TitleGenre { GenreId = G("Фантастика").Id });
            t1.Genres.Add(new TitleGenre { GenreId = G("Боевик").Id });

            var t2 = new Title { Name = "Шрек" };
            t2.Genres.Add(new TitleGenre { GenreId = G("Мультфильм").Id });
            t2.Genres.Add(new TitleGenre { GenreId = G("Комедия").Id });

            var t3 = new Title { Name = "Интерстеллар" };
            t3.Genres.Add(new TitleGenre { GenreId = G("Фантастика").Id });
            t3.Genres.Add(new TitleGenre { GenreId = G("Драма").Id });

            db.Titles.AddRange(t1, t2, t3);
            await db.SaveChangesAsync();
        }
    }
}
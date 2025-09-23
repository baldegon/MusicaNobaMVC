
namespace MusicaNobaMVC.Data
{
    public class DbSeeder
    {

        public static void Seed(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (!db.Generos.Any())
            {
                db.Generos.AddRange(
                    new Models.Genero { Nombre = "Rock" },
                    new Models.Genero { Nombre = "Pop" },
                    new Models.Genero { Nombre = "Jazz" },
                    new Models.Genero { Nombre = "Classical" }
                );
                db.SaveChanges();
            }
            if (!db.Albums.Any())
            {
                db.Albums.AddRange(
                    new Models.Album { Titulo = "Shout At The Devil" },
                    new Models.Album { Titulo = "Appetite For Destruction" },
                    new Models.Album { Titulo = "Whiplash" }
                );
            }
            db.SaveChanges();
        }   
    }
}

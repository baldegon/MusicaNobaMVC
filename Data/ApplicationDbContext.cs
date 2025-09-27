using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicaNobaMVC.Models;

namespace MusicaNobaMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Album> Albums => Set<Album>();
        public DbSet<Cancion> Canciones => Set<Cancion>();
        public DbSet<Genero> Generos => Set<Genero>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*
            modelBuilder.Entity<Genero>(tb =>
            {
                tb.HasData(
                    new Genero { IdGenero = 1, Nombre = "Rock" },
                    new Genero { IdGenero = 2, Nombre = "Pop" },
                    new Genero { IdGenero = 3, Nombre = "Jazz" }
                );
            });

            modelBuilder.Entity<Album>(tb =>
            {
                tb.HasData(
                    new Album { IdAlbum = 1, Titulo = "Shout At The Devil", Anio = 1984 },
                    new Album { IdAlbum = 2, Titulo = "Appetite For Destruction", Anio = 1986 },
                    new Album { IdAlbum = 3, Titulo = "Whiplash", Anio = 2014 }
                );
            });

            modelBuilder.Entity<Cancion>(tb =>
            {
                tb.HasData(
                    new Cancion { IdCancion = 2, Nombre = "Knockin' On Heaven's Door", Artista = "Motley Crue", AlbumId = 2, GeneroId = 1},
                    new Cancion { IdCancion = 3, Nombre = "Welcome To The Jungle", Artista = "Guns N' Roses", AlbumId = 2, GeneroId = 1},
                    new Cancion { IdCancion = 4, Nombre = "Sweet Child O' Mine", Artista = "Guns N' Roses", AlbumId = 2, GeneroId = 1},
                    new Cancion { IdCancion = 5, Nombre = "Whiplash", Artista = "Whiplash Soundtrack", AlbumId = 3, GeneroId = 3 }
                    );
            });

            modelBuilder.Entity<Cancion>()
                .HasIndex(c => new { c.AlbumId, c.GeneroId, c.Nombre });

            */


            modelBuilder.Entity<Cancion>()
                .HasOne(c => c.Album)
                .WithMany(a => a.Canciones)
                .HasForeignKey(c => c.AlbumId)
                .HasPrincipalKey(c => c.IdAlbum)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Genero>()
                .HasKey(g => g.IdGenero);
            
            modelBuilder.Entity<Cancion>()
                .HasOne(c => c.Genero)
                .WithMany(g => g.Canciones)
                .HasForeignKey(c => c.GeneroId)
                .HasPrincipalKey(c => c.IdGenero)
                .OnDelete(DeleteBehavior.Restrict);

            
        }


    }
}

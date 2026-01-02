using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repository.Models;

public partial class Imdbtop1000Context : DbContext
{
    public Imdbtop1000Context()
    {
    }

    public Imdbtop1000Context(DbContextOptions<Imdbtop1000Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer(GetConnectionString());

	private string GetConnectionString()
	{
		IConfiguration config = new ConfigurationBuilder()
			 .SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.json", true, true)
					.Build();
		var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];
		return strConn;
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PK__Actors__57B3EA2B661219E2");

            entity.Property(e => e.ActorId).HasColumnName("ActorID");
            entity.Property(e => e.ActorName).HasMaxLength(255);
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.DirectorId).HasName("PK__Director__26C69E267C48C016");

            entity.Property(e => e.DirectorId).HasColumnName("DirectorID");
            entity.Property(e => e.DirectorName).HasMaxLength(255);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__4BD2943A66E98264");

            entity.Property(e => e.MovieId).HasColumnName("MovieID");
            entity.Property(e => e.Certificate).HasMaxLength(10);
            entity.Property(e => e.DirectorId).HasColumnName("DirectorID");
            entity.Property(e => e.Genre).HasMaxLength(255);
            entity.Property(e => e.Imdbrating).HasColumnName("IMDBRating");
            entity.Property(e => e.Runtime).HasMaxLength(50);
            entity.Property(e => e.SeriesTitle).HasMaxLength(255);
            entity.Property(e => e.Star1Id).HasColumnName("Star1ID");
            entity.Property(e => e.Star2Id).HasColumnName("Star2ID");
            entity.Property(e => e.Star3Id).HasColumnName("Star3ID");
            entity.Property(e => e.Star4Id).HasColumnName("Star4ID");

            entity.HasOne(d => d.Director).WithMany(p => p.Movies)
                .HasForeignKey(d => d.DirectorId)
                .HasConstraintName("FK__Movies__Director__286302EC");

            entity.HasOne(d => d.Star1).WithMany(p => p.MovieStar1s)
                .HasForeignKey(d => d.Star1Id)
                .HasConstraintName("FK__Movies__Star1ID__29572725");

            entity.HasOne(d => d.Star2).WithMany(p => p.MovieStar2s)
                .HasForeignKey(d => d.Star2Id)
                .HasConstraintName("FK__Movies__Star2ID__2A4B4B5E");

            entity.HasOne(d => d.Star3).WithMany(p => p.MovieStar3s)
                .HasForeignKey(d => d.Star3Id)
                .HasConstraintName("FK__Movies__Star3ID__2B3F6F97");

            entity.HasOne(d => d.Star4).WithMany(p => p.MovieStar4s)
                .HasForeignKey(d => d.Star4Id)
                .HasConstraintName("FK__Movies__Star4ID__2C3393D0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

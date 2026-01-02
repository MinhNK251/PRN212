namespace Repository.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string? PosterLink { get; set; }

    public string? SeriesTitle { get; set; }

    public int? ReleasedYear { get; set; }

    public string? Certificate { get; set; }

    public string? Runtime { get; set; }

    public string? Genre { get; set; }

    public double? Imdbrating { get; set; }

	public string? Overview { get; set; }

    public int? MetaScore { get; set; }

    public int? DirectorId { get; set; }

    public int? Star1Id { get; set; }

    public int? Star2Id { get; set; }

    public int? Star3Id { get; set; }

    public int? Star4Id { get; set; }

    public int? NoOfVotes { get; set; }

    public virtual Director? Director { get; set; }

    public virtual Actor? Star1 { get; set; }

    public virtual Actor? Star2 { get; set; }

    public virtual Actor? Star3 { get; set; }

    public virtual Actor? Star4 { get; set; }
}

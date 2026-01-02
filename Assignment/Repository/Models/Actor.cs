namespace Repository.Models;

public partial class Actor
{
    public int ActorId { get; set; }

    public string ActorName { get; set; } = null!;

    public virtual ICollection<Movie> MovieStar1s { get; set; } = new List<Movie>();

    public virtual ICollection<Movie> MovieStar2s { get; set; } = new List<Movie>();

    public virtual ICollection<Movie> MovieStar3s { get; set; } = new List<Movie>();

    public virtual ICollection<Movie> MovieStar4s { get; set; } = new List<Movie>();
}

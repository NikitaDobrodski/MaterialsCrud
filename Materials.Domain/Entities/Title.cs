namespace Materials.Domain.Entities;

public class Title
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<TitleGenre> Genres { get; set; } = new List<TitleGenre>();
}
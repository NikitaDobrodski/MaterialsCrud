namespace Materials.Domain.Entities;

public class MaterialGenre
{
    public int MaterialId { get; set; }
    public Material Material { get; set; } = null!;

    public int GenreId { get; set; }
    public Genre Genre { get; set; } = null!;
}
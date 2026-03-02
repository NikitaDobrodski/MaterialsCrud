using static System.Net.WebRequestMethods;

namespace Materials.Domain.Entities;

public class Material
{
    public int Id { get; set; }

    public string MaterialID { get; set; } = null!;

    public string Tkin { get; set; } = null!;

    public DateTime DateEfir { get; set; }

    public decimal Rating { get; set; }

    public int TitleId { get; set; }
    public Title Title { get; set; } = null!;

    public ICollection<MaterialGenre> Genres { get; set; } = new List<MaterialGenre>();
}
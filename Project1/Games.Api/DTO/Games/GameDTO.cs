namespace Games.DTOs;

public class GameDto
{
    public int GameId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Developer { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public List<string> Platforms { get; set; } = new();
}
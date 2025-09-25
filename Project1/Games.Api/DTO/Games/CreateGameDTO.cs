namespace Games.DTOs;

public class CreateGameDto
{
    public string Title { get; set; } = string.Empty;
    public string Developer { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
}
namespace Games.DTOs;

public class UpdatePlatformDto
{
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
}
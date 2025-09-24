using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Games.Models;

public class Game
{
    public int GameId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Developer { get; set; } = string.Empty;
}
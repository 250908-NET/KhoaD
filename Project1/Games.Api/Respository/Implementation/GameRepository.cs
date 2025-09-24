using Games.Data;
using Games.Models;
using Microsoft.EntityFrameworkCore;

namespace Games.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GamesDbContext _context;

        public GameRepository(GamesDbContext context)
        {
            _context = context;
        }

        // returns a List<Game>
        public async Task<List<Game>> GetAllAsync()
        {
            return await _context.Games.ToListAsync();
        }

        // returns a single Game
        
        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _context.Games.FirstOrDefaultAsync(g => g.GameId == id);
        }

        // stages the add
        public async Task AddAsync(Game game)
        {
            await _context.Games.AddAsync(game);
        }

        // commits changes
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
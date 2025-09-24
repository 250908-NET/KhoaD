using Games.Models;

namespace Games.Services
{
    public interface IGameService
    {
        public Task<List<Game>> GetAllAsync();
        public Task<Game?> GetByIdAsync(int id);
        public Task CreateAsync(Game game);
    }
}
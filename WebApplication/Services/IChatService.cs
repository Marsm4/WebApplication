using MyWebApp.Models;

namespace MyWebApp.Services
{
    public interface IChatService
    {
        Task<IEnumerable<ChatMessage>> GetMessagesByMovieIdAsync(int movieId);
        Task<ChatMessage> SendMessageAsync(ChatMessage message);
        Task<IEnumerable<ChatMessage>> GetPrivateMessagesAsync(int userId);
        Task<ChatMessage> SendPrivateMessageAsync(int userId, ChatMessage message);
    }
}

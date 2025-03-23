using MyWebApp.Models;

namespace MyWebApp.Services
{
    public class ChatService : IChatService
    {
        // Здесь можно добавить зависимость от базы данных или другого хранилища
        private readonly List<ChatMessage> _messages = new List<ChatMessage>();

        public Task<IEnumerable<ChatMessage>> GetMessagesByMovieIdAsync(int movieId)
        {
            var messages = _messages.Where(m => m.MovieId == movieId).ToList();
            return Task.FromResult(messages.AsEnumerable());
        }

        public Task<ChatMessage> SendMessageAsync(ChatMessage message)
        {
            message.Id = _messages.Count + 1;
            message.Timestamp = DateTime.Now;
            _messages.Add(message);
            return Task.FromResult(message);
        }

        public Task<IEnumerable<ChatMessage>> GetPrivateMessagesAsync(int userId)
        {
            var messages = _messages.Where(m => m.UserId == userId).ToList();
            return Task.FromResult(messages.AsEnumerable());
        }

        public Task<ChatMessage> SendPrivateMessageAsync(int userId, ChatMessage message)
        {
            message.Id = _messages.Count + 1;
            message.UserId = userId;
            message.Timestamp = DateTime.Now;
            _messages.Add(message);
            return Task.FromResult(message);
        }
    }
}

namespace MyWebApp.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int MovieId { get; set; } // Идентификатор фильма, к которому относится сообщение
        public int UserId { get; set; } // Идентификатор пользователя, отправившего сообщение
        public string Message { get; set; } // Текст сообщения
        public DateTime Timestamp { get; set; } // Время отправки сообщения
    }
}

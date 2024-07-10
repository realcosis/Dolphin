using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.ChatlogPrivateDetails)]
    public class ChatlogPrivateDetailEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ChatlogId { get; set; }

        public int UserId { get; set; }

        public string? Message { get; set; }

        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(ChatlogId))]
        public ChatlogPrivateEntity? ChatlogPrivate { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity? User { get; set; }
    }
}
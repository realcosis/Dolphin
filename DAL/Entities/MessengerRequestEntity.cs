using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.MessengerRequests), PrimaryKey(nameof(SenderId), nameof(ReceiverId))]
    public class MessengerRequestEntity
    {
        [Required, Column("sender")]
        public int SenderId { get; set; }

        [Required, Column("receiver")]
        public int ReceiverId { get; set; }

        [ForeignKey(nameof(SenderId))]
        public UserEntity? Sender { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public UserEntity? Receiver { get; set; }
    }
}
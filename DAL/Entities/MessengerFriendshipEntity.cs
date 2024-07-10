using Dolphin.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.MessengerFriendships), PrimaryKey(nameof(SenderId), nameof(ReceiverId))]
    public class MessengerFriendshipEntity
    {
        [Required, Column("sender")]
        public int SenderId { get; set; }

        [Required, Column("receiver")]
        public int ReceiverId { get; set; }

        [Required]
        public RelationshipStatus? Relationship { get; set; } = RelationshipStatus.None;

        [ForeignKey(nameof(SenderId))]
        public UserEntity? Sender { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public UserEntity? Receiver { get; set; }
    }
}
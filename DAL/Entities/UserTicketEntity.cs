using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.UserTickets)]
    [Index(nameof(UserId), IsUnique = true)]
    public class UserTicketEntity
    {
        [Key, Column("userid")]
        public int UserId { get; set; }

        [Required, Column("sessionticket")]
        public string? SessionTicket { get; set; }

        [Required, Column("ipaddress")]
        public string? IpAddress { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity? User { get; set; }
    }
}
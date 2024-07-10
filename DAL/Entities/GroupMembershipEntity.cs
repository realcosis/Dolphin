using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.DAL.Entities
{
    [Table(DolphinTables.GroupsMemberships), PrimaryKey(nameof(UserId), nameof(GroupId))]
    public class GroupMembershipEntity
    {
        [Required, Column("userid"), Key]
        public int UserId { get; set; }

        [Required, Column("groupid"), Key]
        public int GroupId { get; set; }

        [Required]
        public int MemberRank { get; set; } = 3;

        [Required]
        public int IsCurrent { get; set; }

        [Required]
        public int IsPending { get; set; }

        public DateTime? JoinedAt { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity? User { get; set; }

        [ForeignKey(nameof(GroupId))]
        public GroupEntity? Group { get; set; }
    }
}
using Dolphin.DAL.Enums;
using Dolphin.HabboHotel.Users.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Users
{
    public class RelationshipStatusInfoMessageComposer(List<MessengerBuddy> buddies, int userId) : OutgoingHandler(ServerPacketCode.RelationshipStatusInfoMessageComposer)
    {
        public override void Compose()
        {
            var random = new Random();
            var total = 0;

            if (buddies.Any(b => b.Relationship == RelationshipStatus.InLove))
                total++;

            if (buddies.Any(b => b.Relationship == RelationshipStatus.Friends))
                total++;

            if (buddies.Any(b => b.Relationship == RelationshipStatus.Enemy))
                total++;

            var lovers = buddies.Where(b => b.Relationship == RelationshipStatus.InLove).ToList();
            var friends = buddies.Where(b => b.Relationship == RelationshipStatus.Friends).ToList();
            var enemies = buddies.Where(b => b.Relationship == RelationshipStatus.Enemy).ToList();

            Packet?.WriteInteger(userId);
            Packet?.WriteInteger(total);

            if (lovers.Count != 0)
            {
                var loversIndex = random.Next(0, lovers.Count-1);
                Packet?.WriteInteger(1);
                Packet?.WriteInteger(lovers.Count);
                var lover = lovers[loversIndex];
                Packet?.WriteInteger(lover.UserId);
                Packet?.WriteString(lover.Username!);
                Packet?.WriteString(lover.Look!);
            }

            if (friends.Count != 0)
            {
                var friendsIndex = random.Next(0, friends.Count-1);
                Packet?.WriteInteger(2);
                Packet?.WriteInteger(friends.Count);
                var friend = friends[friendsIndex];
                Packet?.WriteInteger(friend.UserId);
                Packet?.WriteString(friend.Username!);
                Packet?.WriteString(friend.Look!);
            }

            if (enemies.Count != 0)
            {
                var enemiesIndex = random.Next(0, enemies.Count-1);
                Packet?.WriteInteger(3);
                Packet?.WriteInteger(lovers.Count);
                var enemy = enemies[enemiesIndex];
                Packet?.WriteInteger(enemy.UserId);
                Packet?.WriteString(enemy.Username!);
                Packet?.WriteString(enemy.Look!);
            }
        }
    }
}
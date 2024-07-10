using Dolphin.DAL.Entities;
using Dolphin.DAL.Procedures;
using Dolphin.HabboHotel.Items.Mapper;
using Dolphin.HabboHotel.Users.Mappers;
using Dolphin.HabboHotel.Users.Models;
using Dolphin.Util;

namespace Dolphin.HabboHotel.Users.Mappers
{
    internal static class UserMappers
    {
        internal static UserWardrobeEntity MapAdd(this UserWardrobe model, int userId)
            => model.GetMap<UserWardrobe, UserWardrobeEntity>((dest => dest.UserId, src => userId));

        internal static UserWardrobeEntity MapUpdate(this UserWardrobeEntity entity, UserWardrobe model)
        {
            entity.Gender = model.Gender;
            entity.Look = model.Look;
            return entity;
        }

        internal static IgnoredUser Map(this UserIgnoreEntity entity)
            => entity.GetMap<UserIgnoreEntity, IgnoredUser>((dest => dest.IgnoredUsername!, src => src.IgnoredUser!.Username!));

        internal static UserEffect Map(this UserEffectEntity entity)
            => entity.GetMap<UserEffectEntity, UserEffect>((dest => dest.IsActivated, src => src.IsActivated.EnumToBool()));

        internal static UserBadgeEntity Map(this UserBadge badge, int userId)
            => badge.GetMap<UserBadge, UserBadgeEntity>((dest => dest.BadgeSlot, src => src.Slot),
                (dest => dest.BadgeId!, src => src.Code!),
                (dest => dest.UserId!, src => userId));

        internal static MessengerRequest MapSender(this MessengerRequestEntity request)
            => request.GetMap<MessengerRequestEntity, MessengerRequest>((dest => dest.FromUser, src => src.SenderId),
                (dest => dest.ToUser, src => src.ReceiverId),
                (dest => dest.Username!, src => src.Sender!.Username!),
                (dest => dest.Look!, src => src.Sender!.Look!));

        internal static MessengerBuddy MapReceiver(this MessengerFriendshipEntity friend)
            => friend.GetMap<MessengerFriendshipEntity, MessengerBuddy>((dest => dest.Look!, src => src.Receiver!.Look!),
                (dest => dest.Motto!, src => src.Receiver!.Motto!),
                (dest => dest.UserId!, src => src.Receiver!.Id!),
                (dest => dest.Username!, src => src.Receiver!.Username!),
                (dest => dest.Online!, src => src.Receiver!.Online!));

        internal static MessengerBuddy MapSender(this MessengerFriendshipEntity friend)
            => friend.GetMap<MessengerFriendshipEntity, MessengerBuddy>((dest => dest.Look!, src => src.Sender!.Look!),
                (dest => dest.Motto!, src => src.Sender!.Motto!),
                (dest => dest.UserId!, src => src.Sender!.Id!),
                (dest => dest.Username!, src => src.Sender!.Username!),
                (dest => dest.Online!, src => src.Sender!.Online!));

        internal static HabboData Map(this UserEntity entity)
            => entity.GetMap<UserEntity, HabboData>((dest => dest.Volume, src => src.Volume!.MapVolume()),
                (dest => dest.ChatPreference, src => src.ChatPreference!.EnumToBool()),
                (dest => dest.FocusPreference, src => src.FocusPreference!.EnumToBool()),
                (dest => dest.Online, src => src.Online!.EnumToBool()));

        internal static List<int> MapVolume(this string volume)
            => volume.Trim().Split(',').Select(int.Parse).ToList();

        internal static UserItem Map(this GetUserItemProcedure procedure)
            => procedure.GetMap<GetUserItemProcedure, UserItem>();

        internal static UserItem Map(this ItemUserEntity entity, ItemBaseEntity? itemBaseEntity = default)
            => entity.GetMap<ItemUserEntity, UserItem>((dest => dest.ItemBase!, src => src.Item!.ItemBase!.Map() ?? itemBaseEntity!.Map()),
                (dest => dest.LimitedNo, src => src.Item!.ItemLimited!.LimitedNo),
                (dest => dest.LimitedTot, src => src.Item!.ItemLimited!.LimitedTot),
                (dest => dest.ExtraData!, src => src.Item!.ItemExtraData!.Data!));

        internal static UserWardrobe Map(this UserWardrobeEntity entity)
            => entity.GetMap<UserWardrobeEntity, UserWardrobe>();

        internal static UserAchievement Map(this UserAchievementEntity entity)
            => entity.GetMap<UserAchievementEntity, UserAchievement>((dest => dest.AchievementGroup!, src => src.Group!));

        internal static UserBadge Map(this UserBadgeEntity entity)
            => entity.GetMap<UserBadgeEntity, UserBadge>((dest => dest.Code!, src => src.BadgeId!), (dest => dest.Slot!, src => src.BadgeSlot!));
    }
}
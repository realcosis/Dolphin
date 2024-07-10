using Dolphin.DAL.Entities;
using Dolphin.HabboHotel.Rooms.Models;
using Dolphin.HabboHotel.Rooms.Models.Navigators;

namespace Dolphin.HabboHotel.Rooms.Mappers
{
    internal static class RoomMappers
    {
        internal static Room Map(this RoomEntity entity)
            => entity.GetMap<RoomEntity, Room>();

        internal static NavigatorCategory Map(this NavigatorFlatCatEntity entity)
            => entity.GetMap<NavigatorFlatCatEntity, NavigatorCategory>();
    }
}
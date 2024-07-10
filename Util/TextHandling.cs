namespace Dolphin.Util
{
    internal static class TextHandling
    {
        internal static bool EnumToBool(this int? enumValue)
            => enumValue == 1;

        internal static bool EnumToBool(this int enumValue)
            => enumValue == 1;

        internal static bool EnumToBool(this string? enumValue)
            => enumValue == "1";

        public static double GetNow()
            => (DateTime.Now - Emulator.UnixStart).TotalSeconds;

        public static double GetTimeStamp(this DateTime? dateTime)
            => dateTime.HasValue ? (dateTime.Value - Emulator.UnixStart).TotalSeconds : 0;
    }
}
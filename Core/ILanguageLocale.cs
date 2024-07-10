namespace Dolphin.Core
{
    public interface ILanguageLocale
    {
        string? Format(string original, params object[] args);

        string GetValue(string value);
    }
}
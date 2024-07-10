namespace Dolphin.Core
{
    internal static class IniReader
	{
		internal static Dictionary<string, string> ReadFile(this string path)
		{
            var dictionary = new Dictionary<string, string>();			
			var fileLine = File.ReadAllLines(path);
			foreach (string text in fileLine)
			{
                if (text.Length != 0 && text.Contains('=') && text[..1] != "#" && text[..1] != "[")
				{
					var array = text.Split('=');
					dictionary.Add(array[0], array[1]);
				}
			}
			return dictionary;
		}
	}
}
using System.IO;

namespace GodvilleGuildChronicleGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var appSettings = new AppSettingsManager().LoadSettings();
            var pageText = File.ReadAllText(appSettings.guildPageFilePath);
            var parser = new GuildPageParser(appSettings);
            parser.Parse(pageText);
        }
    }
}

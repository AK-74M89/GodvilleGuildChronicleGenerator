using Newtonsoft.Json;
using System.IO;

namespace GodvilleGuildChronicleGenerator
{
    public class AppSettingsManager
    {
        private const string configPath = "config.json";

        public AppSettings LoadSettings()
        {
            /// создаём шаблон файла настроек, заполняем по умолчанию
            if (!File.Exists(configPath))
                SaveDefaultSettings();

            var rawSettings = File.ReadAllText(configPath);
            return JsonConvert.DeserializeObject<AppSettings>(rawSettings);
        }

        /// <summary>
        /// Сохранение настроек
        /// </summary>
        /// <param name="appSettings">Настройки</param>
        public void SaveSettings(AppSettings appSettings)
        {
            var settingsStr = JsonConvert.SerializeObject(appSettings, Formatting.Indented);
            File.WriteAllText(configPath, settingsStr);
        }

        /// <summary>
        /// Сохранение настроек по умолчанию, см <see cref="GuildPageStructureDefaultStrings"/>
        /// </summary>
        private void SaveDefaultSettings()
        {
            var guildPageStructureSettings = new GuildPageStructureSettings
            {
                ListStart = GuildPageStructureDefaultStrings.DefaultListStart,
                RawGodInfoBegin = GuildPageStructureDefaultStrings.DefaultRawGodInfoBegin,
                RawGodInfoEnd = GuildPageStructureDefaultStrings.DefaultRawGodInfoEnd,
                GodNameBegin = GuildPageStructureDefaultStrings.DefaultGodNameBegin,
                GodNameEnd = GuildPageStructureDefaultStrings.DefaultGodNameEnd,
                AwardBegin = GuildPageStructureDefaultStrings.DefaultAwardBegin,
                AwardEnd = GuildPageStructureDefaultStrings.DefaultAwardEnd,
                HeroNameBegin = GuildPageStructureDefaultStrings.DefaultHeroNameBegin,
                HeroNameEnd = GuildPageStructureDefaultStrings.DefaultHeroNameEnd
            };

            var defaultSettings = new AppSettings
            {
                guildPageFilePath = "guild_page.html",
                guildPageStructureSettings = guildPageStructureSettings
            };

            SaveSettings(defaultSettings);
        }
    }
}

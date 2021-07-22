using Newtonsoft.Json;

namespace GodvilleGuildChronicleGenerator
{
    [JsonObject]
    public class AppSettings
    {
        [JsonProperty("guild_page_file_path")]
        public string guildPageFilePath { get; set; }

        [JsonProperty("guild_page_structure_settings")]
        public GuildPageStructureSettings guildPageStructureSettings { get; set; }
    }
}

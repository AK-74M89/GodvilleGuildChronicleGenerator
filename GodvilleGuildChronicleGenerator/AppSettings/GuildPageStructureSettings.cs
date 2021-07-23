using Newtonsoft.Json;

namespace GodvilleGuildChronicleGenerator
{
    [JsonObject]
    public class GuildPageStructureSettings
    {
        [JsonProperty("list_start")]
        public string ListStart { get; set; }

        [JsonProperty("raw_god_info_begin")]
        public string RawGodInfoBegin { get; set; }

        [JsonProperty("raw_god_info_end")]
        public string RawGodInfoEnd { get; set; }

        [JsonProperty("god_name_begin")]
        public string GodNameBegin { get; set; }

        [JsonProperty("god_name_end")]
        public string GodNameEnd { get; set; }

        [JsonProperty("award_begin")]
        public string AwardBegin { get; set; }

        [JsonProperty("award_end")]
        public string AwardEnd { get; set; }

        [JsonProperty("hero_name_begin")]
        public string HeroNameBegin { get; set; }

        [JsonProperty("hero_name_end")]
        public string HeroNameEnd { get; set; }
    }
}

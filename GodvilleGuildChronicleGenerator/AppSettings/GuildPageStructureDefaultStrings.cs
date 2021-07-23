namespace GodvilleGuildChronicleGenerator
{
    // структура блока информации о божестве на 26.05.2021
    //        <tr >
    //			<td>1</td>
    //			<td><a href="/gods/%D0%9A%D0%B2%D0%B5%D1%80%D1%86" onclick="window.open(this.href);return false;">Кверц</a>
    //				<span class='t_award_w'><span class=' t_award_bgx t_awm' title='庙 - Храмовник с 19.01.2014 00:13
    //舟 - Корабел c 23.02.2015 14:58
    //畜 - Зверовод с 06.07.2014 15:03
    //商 - Лавочник с 01.06.2020 15:02
    //馴 - Тваревед с 03.12.2017 03:31'><span style='display:none;' class='t_award_d'><div style='display:block;'><span class='t_award_small t_award_bg1'>庙</span><span class='t_award_dt'>Храмовник с 19.01.2014 00:13</span></div><div style='display:block;'><span class='t_award_small t_award_bg1'>舟</span><span class='t_award_dt'>Корабел c 23.02.2015 14:58</span></div><div style='display:block;'><span class='t_award_small t_award_bg1'>畜</span><span class='t_award_dt'>Зверовод с 06.07.2014 15:03</span></div><div style='display:block;'><span class='t_award_small t_award_bg1'>商</span><span class='t_award_dt'>Лавочник с 01.06.2020 15:02</span></div><div style='display:block;'><span class='t_award_small t_award_bg1'>馴</span><span class='t_award_dt'>Тваревед с 03.12.2017 03:31</span></div></span><span>5</span></span></span>
    //				</td>
    //			<td>Кверцер</td>
    //			<td>

    //					Дайте воЙна! [☭] 🦊</td>

    //			<td class="tdc">134</td>
    //			<td>добродушный</td>
    //			<td>фырфыраон</td>
    //		</tr>
    public static class GuildPageStructureDefaultStrings
    {
        public const string DefaultListStart = "<h3>Состав";

        public const string DefaultRawGodInfoBegin = "<td><a href=";
        public const string DefaultRawGodInfoEnd = "</tr>";

        // <a href="/gods/%D0%9A%D0%B2%D0%B5%D1%80%D1%86" onclick="window.open(this.href);return false;">Кверц</a>
        public const string DefaultGodNameBegin = ";return false;\">";
        public const string DefaultGodNameEnd = "</a>";

        //<div style='display:block;'><span class='t_award_small t_award_bg1'>庙</span><span class='t_award_dt'>Храмовник с 19.01.2014 00:13</span></div><div style='display:block;'><span class='t_award_small t_award_bg1'>舟</span><span class='t_award_dt'>Корабел c 23.02.2015 14:58</span></div><div style='display:block;'><span class='t_award_small t_award_bg1'>畜</span><span class='t_award_dt'>Зверовод с 06.07.2014 15:03</span></div><div style='display:block;'><span class='t_award_small t_award_bg1'>商</span><span class='t_award_dt'>Лавочник с 01.06.2020 15:02</span></div><div style='display:block;'><span class='t_award_small t_award_bg1'>馴</span><span class='t_award_dt'>Тваревед с 03.12.2017 03:31</span></div></span><span>5</span></span></span>
        public const string DefaultAwardBegin = "<span class='t_award_dt'>";
        public const string DefaultAwardEnd = "</span>";

        // <td>Кверцер</td>
        public const string DefaultHeroNameBegin = "<td>";
        public const string DefaultHeroNameEnd = "</td>";
    }
}

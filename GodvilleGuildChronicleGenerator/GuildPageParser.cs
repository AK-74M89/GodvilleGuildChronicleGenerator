using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodvilleGuildChronicleGenerator
{
    /// <summary>
    /// Парсит текст веб-страницы гильдии, чтобы выбрать необходимую для хроники информацию
    /// </summary>
    public class GuildPageParser
    {
        /// <summary>
        /// Парсинг текста страницы гильдии, чтобы выбрать информацию о божествах.
        /// </summary>
        /// <param name="guildPageText">Текст страницы гильдии</param>
        /// <returns>Список объектов с информацией о божествах, класс <see cref="God"/></returns>
        public List<God> Parse(string guildPageText)
        {
            var godsList = new List<God>();

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
            var listStartPosition = guildPageText.IndexOf(GuildPageStructureDefaultStrings.DefaultListStart);

            if (listStartPosition == -1)
                throw new Exception("Не найдена таблица с составом гильдии");

            var cursorPosition = guildPageText.IndexOf(GuildPageStructureDefaultStrings.DefaultRawGodInfoBegin, listStartPosition);
            while (cursorPosition != -1)
            {
                var rawGodInfo = guildPageText.Substring(cursorPosition, guildPageText.IndexOf(GuildPageStructureDefaultStrings.DefaultRawGodInfoEnd, cursorPosition) - cursorPosition);

                // <a href="/gods/%D0%9A%D0%B2%D0%B5%D1%80%D1%86" onclick="window.open(this.href);return false;">Кверц</a>
                var godNameBeginPosition = rawGodInfo.IndexOf(GuildPageStructureDefaultStrings.DefaultGodNameBegin);
                if (godNameBeginPosition == -1)
                    throw new Exception("Не найдена начальная строка для выбора имени божества");

                godNameBeginPosition += GuildPageStructureDefaultStrings.DefaultGodNameBegin.Length;

                var godNameEndPosition = rawGodInfo.IndexOf(GuildPageStructureDefaultStrings.DefaultGodNameEnd, godNameBeginPosition);
                if (godNameEndPosition == -1)
                    throw new Exception("Не найдена конечная строка для выбора имени божества");

                var godName = rawGodInfo.Substring(godNameBeginPosition, godNameEndPosition - godNameBeginPosition);

                // <td>Кверцер</td>
                var heroNameBeginPosition = rawGodInfo.IndexOf(GuildPageStructureDefaultStrings.DefaultHeroNameBegin, godNameEndPosition);
                if (heroNameBeginPosition == -1)
                    throw new Exception("Не найдена начальная строка для выбора имени героя");

                heroNameBeginPosition += GuildPageStructureDefaultStrings.DefaultHeroNameBegin.Length;

                var heroNameEndPosition = rawGodInfo.IndexOf(GuildPageStructureDefaultStrings.DefaultHeroNameEnd, heroNameBeginPosition);
                if (heroNameEndPosition == -1)
                    throw new Exception("Не найдена конечная строка для выбора имени героя");

                var heroName = rawGodInfo.Substring(heroNameBeginPosition, heroNameEndPosition - heroNameBeginPosition);

                GetAwardsDates(rawGodInfo, out DateTime? templeDate, out DateTime? arkDate, out DateTime? petDate, out DateTime? beastDate, out DateTime? shopDate);

                godsList.Add(new God { Name = godName, HeroName = heroName, TempleDate = templeDate, ArkDate = arkDate, PetDate = petDate, BeastDate = beastDate, ShopDate = shopDate });

                cursorPosition = guildPageText.IndexOf(GuildPageStructureDefaultStrings.DefaultRawGodInfoBegin, ++cursorPosition);
            }

            return godsList;
        }

        /// <summary>
        /// Парсинг блока с достижениями, чтобы выбрать даты получения каждого достижения
        /// </summary>
        /// <param name="rawGodInfo">Блок с информацией о божестве</param>
        /// <param name="templeDate">Дата получения достижения "Храмовник"</param>
        /// <param name="arkDate">Дата получения достижения "Корабел"</param>
        /// <param name="petDate">Дата получения достижения "Зверовод"</param>
        /// <param name="beastDate">Дата получения достижения "Тваревед"</param>
        /// <param name="shopDate">Дата получения достижения "Лавочник"</param>
        private void GetAwardsDates(string rawGodInfo, out DateTime? templeDate, out DateTime? arkDate, out DateTime? petDate, out DateTime? beastDate, out DateTime? shopDate)
        {
            templeDate = arkDate = petDate = beastDate = shopDate = null;

            //<div style='display:block;'><span class='t_award_small t_award_bg1'>庙</span><span class='t_award_dt'>Храмовник с 19.01.2014 00:13</span></div><div style='display:block;'><span class='t_award_small t_award_bg1'>舟</span><span class='t_award_dt'>Корабел c 23.02.2015 14:58</span></div><div style='display:block;'><span class='t_award_small t_award_bg1'>畜</span><span class='t_award_dt'>Зверовод с 06.07.2014 15:03</span></div><div style='display:block;'><span class='t_award_small t_award_bg1'>商</span><span class='t_award_dt'>Лавочник с 01.06.2020 15:02</span></div><div style='display:block;'><span class='t_award_small t_award_bg1'>馴</span><span class='t_award_dt'>Тваревед с 03.12.2017 03:31</span></div></span><span>5</span></span></span>
            var awardCursorPosition = rawGodInfo.IndexOf(GuildPageStructureDefaultStrings.DefaultAwardBegin);

            // медалей у бога может и не быть
            // TODO: проверка на наличие медалей
            if (awardCursorPosition == -1)
                return;

            while (awardCursorPosition != -1)
            {
                awardCursorPosition += GuildPageStructureDefaultStrings.DefaultAwardBegin.Length;

                var awardEndPosition = rawGodInfo.IndexOf(GuildPageStructureDefaultStrings.DefaultAwardEnd, awardCursorPosition);
                if (awardEndPosition == -1)
                    throw new Exception("Не найдена конечная строка для поиска информации о медали");

                var rawAwardInfo = rawGodInfo.Substring(awardCursorPosition, awardEndPosition - awardCursorPosition);

                // в текущем формате дата - 16 символов в конце строки в формате dd.MM.yyyy hh:mm
                var strDate = new string(rawAwardInfo.Reverse().Take(16).Reverse().ToArray());
                if (!DateTime.TryParse(strDate, out DateTime awardDate))
                    throw new Exception($@"Не удалось преобразовать строку ""{strDate}"" в дату");

                if (rawAwardInfo.IndexOf("Храмовник") != -1)
                    templeDate = awardDate;
                else if (rawAwardInfo.IndexOf("Корабел") != -1)
                    arkDate = awardDate;
                else if (rawAwardInfo.IndexOf("Зверовод") != -1)
                    petDate = awardDate;
                else if (rawAwardInfo.IndexOf("Тваревед") != -1)
                    beastDate = awardDate;
                else if (rawAwardInfo.IndexOf("Лавочник") != -1)
                    shopDate = awardDate;

                awardCursorPosition = rawGodInfo.IndexOf(GuildPageStructureDefaultStrings.DefaultAwardBegin, awardCursorPosition);
            }
        }
    }
}

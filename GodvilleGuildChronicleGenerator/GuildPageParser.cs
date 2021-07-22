using System;
using System.Collections.Generic;
using System.Linq;

namespace GodvilleGuildChronicleGenerator
{
    /// <summary>
    /// Парсит текст веб-страницы гильдии, чтобы выбрать необходимую для хроники информацию
    /// </summary>
    public class GuildPageParser
    {
        private GuildPageStructureSettings _guildPageStructureSettings;

        public GuildPageParser(AppSettings settings)
        {
            _guildPageStructureSettings = settings.guildPageStructureSettings;
        }

        /// <summary>
        /// Парсинг текста страницы гильдии, чтобы выбрать информацию о божествах.
        /// </summary>
        /// <param name="guildPageText">Текст страницы гильдии</param>
        /// <returns>Список объектов с информацией о божествах, класс <see cref="God"/></returns>
        public List<God> Parse(string guildPageText)
        {
            var godsList = new List<God>();
            
            var listStartPosition = guildPageText.IndexOf(_guildPageStructureSettings.ListStart);

            if (listStartPosition == -1)
                throw new Exception("Не найдена таблица с составом гильдии");

            var cursorPosition = guildPageText.IndexOf(_guildPageStructureSettings.RawGodInfoBegin, listStartPosition);
            while (cursorPosition != -1)
            {
                var rawGodInfo = guildPageText.Substring(cursorPosition, guildPageText.IndexOf(_guildPageStructureSettings.RawGodInfoEnd, cursorPosition) - cursorPosition);
                
                var godNameBeginPosition = rawGodInfo.IndexOf(_guildPageStructureSettings.GodNameBegin);
                if (godNameBeginPosition == -1)
                    throw new Exception("Не найдена начальная строка для выбора имени божества");

                godNameBeginPosition += _guildPageStructureSettings.GodNameBegin.Length;

                var godNameEndPosition = rawGodInfo.IndexOf(_guildPageStructureSettings.GodNameEnd, godNameBeginPosition);
                if (godNameEndPosition == -1)
                    throw new Exception("Не найдена конечная строка для выбора имени божества");

                var godName = rawGodInfo.Substring(godNameBeginPosition, godNameEndPosition - godNameBeginPosition);

                var heroNameBeginPosition = rawGodInfo.IndexOf(_guildPageStructureSettings.HeroNameBegin, godNameEndPosition);
                if (heroNameBeginPosition == -1)
                    throw new Exception("Не найдена начальная строка для выбора имени героя");

                heroNameBeginPosition += _guildPageStructureSettings.HeroNameBegin.Length;

                var heroNameEndPosition = rawGodInfo.IndexOf(_guildPageStructureSettings.HeroNameEnd, heroNameBeginPosition);
                if (heroNameEndPosition == -1)
                    throw new Exception("Не найдена конечная строка для выбора имени героя");

                var heroName = rawGodInfo.Substring(heroNameBeginPosition, heroNameEndPosition - heroNameBeginPosition);

                GetAwardsDates(rawGodInfo, out DateTime? templeDate, out DateTime? arkDate, out DateTime? petDate, out DateTime? beastDate, out DateTime? shopDate);

                godsList.Add(new God { Name = godName, HeroName = heroName, TempleDate = templeDate, ArkDate = arkDate, PetDate = petDate, BeastDate = beastDate, ShopDate = shopDate });

                cursorPosition = guildPageText.IndexOf(_guildPageStructureSettings.RawGodInfoBegin, ++cursorPosition);
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

            var awardCursorPosition = rawGodInfo.IndexOf(_guildPageStructureSettings.AwardBegin);

            // медалей у бога может и не быть
            if (awardCursorPosition == -1)
                return;

            while (awardCursorPosition != -1)
            {
                awardCursorPosition += _guildPageStructureSettings.AwardBegin.Length;

                var awardEndPosition = rawGodInfo.IndexOf(_guildPageStructureSettings.AwardEnd, awardCursorPosition);
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

                awardCursorPosition = rawGodInfo.IndexOf(_guildPageStructureSettings.AwardBegin, awardCursorPosition);
            }
        }
    }
}

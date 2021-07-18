using System;

namespace GodvilleGuildChronicleGenerator
{
    /// <summary>
    /// Информация о божестве
    /// </summary>
    public class God
    {
        /// <summary>
        /// Имя бога
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Имя героя
        /// </summary>
        public string HeroName { get; set; }

        /// <summary>
        /// Дата получения медали Храмовника
        /// </summary>
        public DateTime? TempleDate { get; set; }

        /// <summary>
        /// Дата получения медали Корабела
        /// </summary>
        public DateTime? ArkDate { get; set; }

        /// <summary>
        /// Дата получения медали Зверовода
        /// </summary>
        public DateTime? PetDate { get; set; }

        /// <summary>
        /// Дата получения медали Твареведа
        /// </summary>
        public DateTime? BeastDate { get; set; }

        /// <summary>
        /// Дата получения медали Лавочника
        /// </summary>
        public DateTime? ShopDate { get; set; }
    }
}

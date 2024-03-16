using OriginsOfDestiny.Data.Models.Items;
using OriginsOfDestiny.Data.Models.Items.InteractiveItems;
using OriginsOfDestiny.Data.Models.Locations;
using OriginsOfDestiny.DataObjects.Models.Items.Pickups;
using Stream = OriginsOfDestiny.Data.Models.Items.InteractiveItems.Stream;

namespace OriginsOfDestiny.StartArc.TemporaryTestData
{
    public static class TemporaryTestData
    {
        public static readonly Note StrangeNote = new()
        {
            Name = "Таинственная записка 📜",
            Description = "Какое-то описание странной записки. П.с. ОТ ДРУГА"
        };

        public static readonly Stream StrangeStream = new() { Name = "Серебристый ручей" };
        public static readonly Duplo StrangeDuplo = new() { 
            Name = "Дерево с очень интересным дуплом",
            Loot = new HashSet<Item>() { 
                StrangeNote
            }
        };

        public static readonly Area DownEAForest = new()
        {
            Name = "Чаща Вечно-осеннего леса 🍂",
            Description = "Самый центр Вечно-осеннего леса. Возможно, в нём есть своя тайна.",
            InteractiveItems = new HashSet<InteractiveItem>()
            {
                StrangeStream,
                StrangeDuplo
            }
        };
    }
}

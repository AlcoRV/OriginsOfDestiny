using OriginsOfDestiny.Data.Models.Items.InteractiveItems;
using OriginsOfDestiny.Data.Models.Locations;
using OriginsOfDestiny.DataObjects.Interfaces.InteractiveItems;
using OriginsOfDestiny.DataObjects.Interfaces.Items;
using OriginsOfDestiny.DataObjects.Models.Items.Pickups;
using static OriginsOfDestiny.Data.Constants.DConstants.Files.Pictures;
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
        public static readonly Hollow StrangeDuplo = new() { 
            Name = "Дерево с очень интересным дуплом",
            Loot = new HashSet<IItem>() { 
                StrangeNote
            }
        };

        public static readonly Area DownEAForest = new()
        {
            Name = "Чаща Вечно-осеннего леса 🍂",
            Description = "Самый центр Вечно-осеннего леса. Возможно, в нём есть своя тайна.",
            InteractiveItems = new HashSet<IInteractiveItem>()
            {
                StrangeStream,
                StrangeDuplo
            },
            Picture = "Locations/eaforest.jpg"
        };
    }
}

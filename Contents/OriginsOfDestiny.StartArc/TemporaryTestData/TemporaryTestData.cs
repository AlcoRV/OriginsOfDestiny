using OriginsOfDestiny.Data.Models.Items.InteractiveItems;
using OriginsOfDestiny.Data.Models.Locations;
using Stream = OriginsOfDestiny.Data.Models.Items.InteractiveItems.Stream;

namespace OriginsOfDestiny.StartArc.TemporaryTestData
{
    public static class TemporaryTestData
    {
        public static readonly Stream StrangeStream = new() { Name = "Серебристый ручей" };
        public static readonly Duplo StrangeDuplo = new() { Name = "Дерево с очень интересным дуплом" };

        public static readonly Area DownEAForest = new()
        {
            Name = "Чаща Вечно-осеннего леса",
            Description = "Самый центр Вечно-осеннего леса. Возможно, в нём есть своя тайна.",
            InteractiveItems = new HashSet<InteractiveItem>()
            {
                StrangeStream,
                StrangeDuplo
            }
        };
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace OriginsOfDestiny.Models.Characters
{
    public class Character
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public bool IsAlive => Health > 0;

        public string AttributesJson { get; set; }

        [NotMapped]
        public Dictionary<string, int> Attributes
        {
            get => string.IsNullOrEmpty(AttributesJson)
                ? []
                : JsonSerializer.Deserialize<Dictionary<string, int>>(AttributesJson)!;
            set => AttributesJson = JsonSerializer.Serialize(value);
        }
    }
}

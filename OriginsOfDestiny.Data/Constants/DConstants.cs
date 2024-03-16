namespace OriginsOfDestiny.Data.Constants
{
    public static class DConstants
    {
        public static class Files
        {
            public static class Pictures
            {
                public static class Characters
                {
                    public static readonly string Simon = nameof(Characters) + "/simon.jpg";
                }

                public static class Locations
                {
                    public static readonly string EAForest = nameof(Locations) + "/eaforest.jpg";
                }
            }
        }
        public static class Messages
        {
            public static class Effects
            {
                public static readonly string Positive = "POSITIVE";
                public static readonly string Negative = "NEGATIVE";
            }
            public static class Items
            {
                public static readonly string Use = "USE";
            }
            public static class Out
            {
                public static readonly string Found = "FOUND";
                public static readonly string NotFound = "NOTFOUND";
            }
        }
        
    }
}

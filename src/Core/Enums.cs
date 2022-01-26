namespace Bcan.Backend.Core
{
    public class DanceType : Enumeration
    {
        public static DanceType Salsa = new(1, nameof(Salsa));
        public static DanceType Bachata = new(2, nameof(Bachata));
        public static DanceType ChaCha = new(3, nameof(ChaCha));

        private DanceType(int id, string name) : base(id, name) { }
    }

    public class Level : Enumeration
    {
        public static Level Beginner = new(1, nameof(Beginner));
        public static Level Intermediate = new(2, nameof(Intermediate));
        public static Level Advanced = new(3, nameof(Advanced));
        public static Level AllLevels = new(4, "All Levels");

        private Level(int id, string name) : base(id, name){}
    }

    public enum IndividualType
    {
        Undefined,
        Regular,
        Student,
        Veteran
    }

    public enum PaymentType
    {
        Undefined,
        OneTime,
        Financed
    }
}
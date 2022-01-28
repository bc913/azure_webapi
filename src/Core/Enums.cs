namespace Bcan.Backend.Core
{
    public class DanceType : Enumeration
    {
        public static DanceType Salsa = new(1, nameof(Salsa));
        public static DanceType Bachata = new(2, nameof(Bachata));
        public static DanceType ChaCha = new(3, nameof(ChaCha));

        private DanceType(int id, string name) : base(id, name) { }
    }

    public class DanceLevel : Enumeration
    {
        public static DanceLevel Beginner = new(1, nameof(Beginner));
        public static DanceLevel Intermediate = new(2, nameof(Intermediate));
        public static DanceLevel Advanced = new(3, nameof(Advanced));
        public static DanceLevel All = new(4, nameof(All));

        private DanceLevel(int id, string name) : base(id, name){}
    }

    public enum IndividualType
    {
        Undefined,
        Regular,
        Student,
        Veteran,
        All
    }

    public enum PaymentType
    {
        Undefined,
        NoPayment,
        OneTime,
        Financed
    }
}
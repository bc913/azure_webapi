namespace Bcan.Backend.Persistence.Options
{
    public class SqlDbOptions
    {
        public const string Key = "ConnectionStrings";

        public string AppDbConnection { get; set; }
        public string AuthDbConnection { get; set; }
    }    
}
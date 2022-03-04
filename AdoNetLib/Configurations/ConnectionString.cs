namespace AdoNetLib.Configurations
{
    public static class ConnectionString
    {
        public static string MsSqlConnection => @"Data Source=.\SQLEXPRESS;Database=testing;Trusted_Connection=True; Trust Server Certificate = true;";
    }
}
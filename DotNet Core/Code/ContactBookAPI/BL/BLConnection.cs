namespace ContactBookAPI.BL
{
    /// <summary>
    /// 
    /// </summary>
    public class BLConnection
    {
        /// <summary>
        /// 
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public static void Initialize(IConfiguration configuration)
        {
            // Read the connection string from the configuration
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}

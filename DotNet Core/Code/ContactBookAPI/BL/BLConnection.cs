namespace ContactBookAPI.BL
{
    /// <summary>
    /// The BLConnection class is responsible for managing the connection string used to connect to the database.
    /// </summary>
    public class BLConnection
    {
        /// <summary>
        /// Gets or sets the connection string used to connect to the database.
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Initializes the connection string using the provided configuration.
        /// </summary>
        /// <param name="configuration">The configuration object that contains the connection string.</param>
        public static void Initialize(IConfiguration configuration)
        {
            // Read the connection string from the configuration
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
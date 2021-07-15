using Microsoft.EntityFrameworkCore;

namespace AzureIntegrationDemo.Models
{
    public class DemoContext: DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Each time we put, record the query params, headers, and body in demo items
        /// </summary>
        public DbSet<SearchRequest> DemoItems { get; set; }
    }
}

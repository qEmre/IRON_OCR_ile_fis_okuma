using Microsoft.EntityFrameworkCore;
using OCRproject.Models;

namespace OCRproject.DataLayer
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {

        }
        public DbSet<documentContent> documentContentTable { get; set; }
        public DbSet<Document> documentTable { get; set; }
    }
}
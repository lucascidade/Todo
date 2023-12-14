using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Todo.Domain.Entitites;

namespace Todo.Domain.Infra.Contexts
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> Todos { get; set; }
        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        //  => options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Todos; User ID=sa; Integrated Security=true, Encrypt=false");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().ToTable("Todos");
            modelBuilder.Entity<TodoItem>().Property(x => x.Id);
            modelBuilder.Entity<TodoItem>().Property(x => x.User).HasMaxLength(120).HasColumnType("varchar(120)");
            modelBuilder.Entity<TodoItem>().Property(x => x.Title).HasMaxLength(160).HasColumnType("varchar(160)");
            modelBuilder.Entity<TodoItem>().Property(x => x.Done).HasColumnType("bit");
            modelBuilder.Entity<TodoItem>().Property(x => x.Date);
            modelBuilder.Entity<TodoItem>().HasIndex(b => b.User);
        }
    }
}
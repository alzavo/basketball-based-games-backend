using Domain.App;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<PlayedGame> PlayedGames { get; set; } = null!;
        public DbSet<Friendship> Friendships { get; set; } = null!;

        public override DbSet<User> Users { get; set; } = null!;
        public override DbSet<Role> Roles { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Friendship>()
                .HasOne(f => f.User)
                .WithMany(u => u.PersonalFriendships)
                .HasForeignKey(f => f.UserId);

            builder.Entity<Friendship>()
                .HasOne(f => f.Friend)
                .WithMany(u => u.FriendshipsWithUser)
                .HasForeignKey(f => f.FriendId);

            // disable cascade delete initially for everything
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }
}

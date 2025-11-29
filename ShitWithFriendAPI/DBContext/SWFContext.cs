using Microsoft.EntityFrameworkCore;
using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.DBContext
{
    public class SWFContext : DbContext
    {
        public SWFContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<HighScore> HighScores { get; set; }
        public DbSet<Poop> Poops { get; set; }
        public DbSet<UserGroupEmoji> UsersGroupsEmoji { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ignoriamo la proprietà User se non rappresenta una relazione
            modelBuilder.Entity<Group>()
                .Ignore(g => g.User);

            // Relazione uno-a-uno con YearPoopKingUser
            modelBuilder.Entity<Group>()
                .HasOne(g => g.YearPoopKingUser)
                .WithMany()
                .HasForeignKey(g => g.YearPoopKing)
                .OnDelete(DeleteBehavior.Restrict);

            // Relazione uno-a-uno con MonthPoopKingUser
            modelBuilder.Entity<Group>()
                .HasOne(g => g.MonthPoopKingUser)
                .WithMany()
                .HasForeignKey(g => g.MonthPoopKing)
                .OnDelete(DeleteBehavior.Restrict);

            // Relazione molti-a-molti tra Group e Users
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Users)
                .WithMany(u => u.Groups)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupUsers",
                    r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                    l => l.HasOne<Group>().WithMany().HasForeignKey("GroupId").OnDelete(DeleteBehavior.Cascade)
                );

            // Relazione molti-a-molti tra Group e Administrators
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Administrators)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "GroupAdministrators",
                    r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                    l => l.HasOne<Group>().WithMany().HasForeignKey("GroupId").OnDelete(DeleteBehavior.Cascade)
                );

            // Relazione per UserGroupEmoji
            modelBuilder.Entity<UserGroupEmoji>()
                .HasKey(uge => uge.Id);
            modelBuilder.Entity<UserGroupEmoji>()
                .HasOne(uge => uge.User)
                .WithMany(u => u.UserGroupEmojis)
                .HasForeignKey(uge => uge.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserGroupEmoji>()
                .HasOne(uge => uge.Group)
                .WithMany(g => g.UserGroupEmojis)
                .HasForeignKey(uge => uge.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relazione per HighScore
            modelBuilder.Entity<HighScore>()
                .HasOne(hs => hs.User)
                .WithMany(u => u.Highscores)
                .HasForeignKey(hs => hs.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<HighScore>()
                .HasOne(hs => hs.Game)
                .WithMany()
                .HasForeignKey(hs => hs.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relazione per Poop
            modelBuilder.Entity<Poop>()
                .HasOne(p => p.User)
                .WithMany(u => u.Poops)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}

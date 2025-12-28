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
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }

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

            modelBuilder.Entity<Achievement>().HasData(
                // QUANTITÀ
                new Achievement { Code = "POOP_1", Name = "Il Primo Passo", Description = "La tua prima cacca registrata.", IsSecret = false, XpValue = 10 },
                new Achievement { Code = "POOP_10", Name = "Riscaldamento", Description = "10 cacche registrate.", IsSecret = false, XpValue = 20 },
                new Achievement { Code = "POOP_25", Name = "Apprendista", Description = "25 cacche registrate.", IsSecret = false, XpValue = 30 },
                new Achievement { Code = "POOP_50", Name = "Cintura Marrone", Description = "50 cacche. Rispetto.", IsSecret = false, XpValue = 50 },
                new Achievement { Code = "POOP_100", Name = "Il Centurione", Description = "100 cacche. Un esercito.", IsSecret = false, XpValue = 100 },
                new Achievement { Code = "POOP_250", Name = "Maestro Zen", Description = "250 cacche. Arte pura.", IsSecret = false, XpValue = 250 },
                new Achievement { Code = "POOP_500", Name = "Il Trono di Ceramica", Description = "500 cacche. Regale.", IsSecret = true, XpValue = 500 },
                new Achievement { Code = "POOP_1000", Name = "Divinità Intestinale", Description = "1000 cacche. Leggendario.", IsSecret = true, XpValue = 1000 },
                // ORARI
                new Achievement { Code = "EARLY_BIRD", Name = "Il Gallo", Description = "Posta tra le 05:00 e le 07:00.", IsSecret = false, XpValue = 20 },
                new Achievement { Code = "LUNCH_TIMER", Name = "Pausa Pranzo", Description = "Posta tra le 12:30 e le 14:00.", IsSecret = false, XpValue = 15 },
                new Achievement { Code = "NIGHT_OWL", Name = "Il Vampiro", Description = "Posta tra le 03:00 e le 05:00.", IsSecret = true, XpValue = 50 },
                new Achievement { Code = "WEEKEND_WARRIOR", Name = "Guerriero del Weekend", Description = "Cacca sia Sabato che Domenica nello stesso weekend.", IsSecret = false, XpValue = 30 },
                // DATE
                new Achievement { Code = "NEW_YEAR", Name = "Anno Nuovo", Description = "Posta il 1° Gennaio.", IsSecret = false, XpValue = 50 },
                new Achievement { Code = "VALENTINE", Name = "Innamorato", Description = "Posta il 14 Febbraio.", IsSecret = false, XpValue = 20 },
                new Achievement { Code = "FERRAGOSTO", Name = "Cacca Caliente", Description = "Posta il 15 Agosto.", IsSecret = false, XpValue = 20 },
                new Achievement { Code = "HALLOWEEN", Name = "Terrore in Bagno", Description = "Posta il 31 Ottobre.", IsSecret = true, XpValue = 30 },
                new Achievement { Code = "XMAS", Name = "Regalino di Natale", Description = "Posta il 25 Dicembre.", IsSecret = true, XpValue = 50 },
                // STREAK
                new Achievement { Code = "STREAK_3", Name = "Tris", Description = "3 giorni consecutivi.", IsSecret = false, XpValue = 20 },
                new Achievement { Code = "STREAK_7", Name = "Settimana Santa", Description = "7 giorni consecutivi.", IsSecret = false, XpValue = 50 },
                new Achievement { Code = "STREAK_30", Name = "Iron Man", Description = "30 giorni consecutivi.", IsSecret = true, XpValue = 150 },
                // GIOCHI & SOCIAL
                new Achievement { Code = "GAMER_ROOKIE", Name = "Insert Coin", Description = "Gioca la tua prima partita.", IsSecret = false, XpValue = 10 },
                new Achievement { Code = "GAMER_COMPLETIONIST", Name = "Nerd da Bagno", Description = "Gioca a tutti i minigiochi.", IsSecret = false, XpValue = 100 },
                new Achievement { Code = "KING_MONTH", Name = "Barone del Trono", Description = "Vinci un titolo di Re del Mese.", IsSecret = false, XpValue = 100 },
                new Achievement { Code = "KING_YEAR", Name = "Imperatore Supremo", Description = "Vinci un titolo di Re dell'Anno.", IsSecret = false, XpValue = 500 },
                // EXTRA
                new Achievement { Code = "TURBO_POOPER", Name = "Mitragliatrice", Description = "2 cacche in meno di 1 ora.", IsSecret = true, XpValue = 50 },
                new Achievement { Code = "TYPE_LIQUID", Name = "Idrante", Description = "5 cacche di tipo Liquida.", IsSecret = false, XpValue = 30 }
            );
        }


    }
}

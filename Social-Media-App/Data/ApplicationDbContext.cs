namespace Social_Media_App.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Social_Media_App.Data.Models;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "UserFriends",
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("FriendId"),
                    j =>
                    {
                        j.HasKey("UserId", "FriendId");
                    }
                );

            modelBuilder.Entity<User>()
                .HasMany(u => u.Chats)
                .WithMany(c => c.Users)
                .UsingEntity<Dictionary<string, object>>(
                        "UserChats",
                        j => j.HasOne<Chat>().WithMany().HasForeignKey("ChatId"),
                        j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "ChatId");
                        });

            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.SenderUser)
                .WithOne()
                .HasForeignKey<FriendRequest>(fr => fr.SenderUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.ReceiverUser)
                .WithOne()
                .HasForeignKey<FriendRequest>(fr => fr.ReceiverUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
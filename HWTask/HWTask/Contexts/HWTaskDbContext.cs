using HWTask.CoreModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWTask.Contexts
{
    public class HWTaskDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }

        public HWTaskDbContext(DbContextOptions<HWTaskDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFriend>().HasKey(k => new { k.UserId, k.FriendId });
        }
    }
}

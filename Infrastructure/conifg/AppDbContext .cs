using Application.IRepo;
using Domin.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.conifg
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<BookGoal> BookGoals => Set<BookGoal>();
        public DbSet<Chapter> Chapters => Set<Chapter>();
        public DbSet<ChapterGoal> ChapterGoals => Set<ChapterGoal>();
        public DbSet<MarginNote> MarginNotes => Set<MarginNote>();
        public DbSet<NoteImage> NoteImages => Set<NoteImage>();
        public DbSet<BookVideo> BookVideos => Set<BookVideo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}

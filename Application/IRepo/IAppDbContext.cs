using Domin.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepo
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Book> Books { get; }
        DbSet<BookGoal> BookGoals { get; }
        DbSet<Chapter> Chapters { get; }
        DbSet<ChapterGoal> ChapterGoals { get; }
        DbSet<MarginNote> MarginNotes { get; }
        DbSet<NoteImage> NoteImages { get; }
        DbSet<BookVideo> BookVideos { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

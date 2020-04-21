using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new OnlineLibraryContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<OnlineLibraryContext>>()))
            {
                // Look for any books
                if (context.Book.Any())
                {
                    return;   // DB has been seeded
                }
                context.Book.AddRange(
                    new Book
                    {
                        Title = "War and Peace",
                        Author = "Lev Tolstoi",
                        ReleaseDate = DateTime.Parse("02-02-1878"),
                        PublishingHouse = "Art",
                        Collection = "Classics",
                        Img="War and Peace.jpg"
                    },
                    new Book
                    {
                        Title = "Jane Eyre",
                        Author = "Charlotte Bronte",
                        ReleaseDate = DateTime.Parse("11.06.1850"),
                        PublishingHouse = "Litera",
                        Collection = "Classics",
                        Img = "Jane Eyre.jpg"
                    },
                    new Book
                    {
                        Title = "Animal Farm",
                        Author = "George Orwell",
                        ReleaseDate = DateTime.Parse("02.07.1948"),
                        PublishingHouse = "Polirom",
                        Collection = "Top 10+",
                        Img = "Animal Farm.jpg"
                    }
             );
                context.SaveChanges();
            }
        }
    }
}

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if ( context.Books.Any())
                {
                    return;
                }

                context.Books.AddRange(
                    new Book{
                        //Id = 1,
                        Title = "Kitap1", //Personal Grow-up
                        GenreId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001,06,12)

                    },

                    new Book{
                        //Id = 2,
                        Title = "Kitap2", //Science-fiction
                        GenreId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(2010,06,12)

                    },

                    new Book{
                        //Id = 3,
                        Title = "Kitap3", //Science-fication
                        GenreId = 2,
                        PageCount = 540,
                        PublishDate = new DateTime(2001,12,21)

                    }
                );

                context.SaveChanges();
            }
        }
    }
}
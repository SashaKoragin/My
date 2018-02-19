using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebTest.Models
{
    public class BookContext : DbContext

    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }

    public class BookDbInitializer : DropCreateDatabaseAlways<BookContext>
    {
        protected override void Seed(BookContext db)
        {
            db.Books.Add(new Book() {Name = "Война и мир", Author = "Л. Толстой", Price = 240});
            db.Books.Add(new Book() { Name = "Отци и дети", Author = "И. Тургенев", Price = 140 });
            db.Books.Add(new Book() { Name = "Чайка", Author = "А. Чехов", Price = 290 });
            base.Seed(db);
        }
    }
}
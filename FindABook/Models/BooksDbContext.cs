using FindABook.Models.UtillityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FindABook.Models
{
    public class BooksDbContext:IdentityDbContext<ApplicationUser>
    {

        private readonly DbConnectionSettings _connectionSettings;

        public DbSet<Book> Books { get; set; }
        public DbSet<BookSubscription> BookSubscriptions { get; set; }
        public DbSet<Category> Category { get; set; }

        public BooksDbContext(DbConnectionSettings dbConnectionSettings)
        {
            _connectionSettings = dbConnectionSettings;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.FirstName)
                .HasMaxLength(250);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.LastName)
                .HasMaxLength(250);

            modelBuilder.Entity<Category>(m =>
            {
                m.HasData(
                    new Category() { CategoryId = 1, CategoryName = "BestSellers"},
                    new Category() { CategoryId = 2, CategoryName = "Fiction" },
                    new Category() { CategoryId = 3, CategoryName = "Cookery" }

                  );
            });

            modelBuilder.Entity<Book>(m =>
            {
                m.HasData(
                    new Book() { BookId = 1
                                 ,Title = "The Stellenbosch Mafia"
                                 ,Description = "About 50km outside of Cape Town lies the beautiful town of Stellenbosch, nestled against vineyards and blue mountains that stretch to the sky. Here reside some of South Africa’s wealthiest individuals: all male, all Afrikaans – and all stinking rich. Johann Rupert, Jannie Mouton, Markus Jooste and Christo Weise, to name a few. Julius Malema refers to them scathingly as ‘The Stellenbosch Mafia’, the very worst example of white monopoly capital."
                                 ,ImageUrl = "images/bookImages/Mafia.jpg"
                                 ,CategoryId = 1
                                 ,Price = 200.00
                                 ,Publisher = "Jonathan Ball Publishers SA"
                                 ,BestSeller = true },

                    new Book() { BookId = 2
                                 ,Title = "Rich Dad Poor Dad"
                                 ,Description = "Rich Dad Poor Dad is Robert’s story of growing up with two dads — his real father and the father of his best friend, his rich dad — and the ways in which both men shaped his thoughts about money and investing."
                                 ,ImageUrl = "images/bookImages/Rich-Dad.jpg"
                                 ,CategoryId = 1
                                 ,Price = 160.00
                                 ,Publisher = "Jonathan Ball Publishers SA"
                                 ,BestSeller = true },

                     new Book() { BookId = 3
                                  ,Title = "Atomic Habits by James Clear"
                                  ,Description = "People think when you want to change your life, you need to think big. But world-renowned habits expert James Clear has discovered another way.."
                                  ,ImageUrl = "images/bookImages/Atomic-Habits.jpg"
                                  ,CategoryId = 2
                                  ,Price = 280.00
                                  ,Publisher = "Jonathan Ball Publishers SA"
                                  ,BestSeller = false }


                  );

            });

            modelBuilder.Entity<BookSubscription>(m =>
            {
                m.HasData(
                    new BookSubscription() { SubscriptionId = 1, UserId = { }, BookId = 1 ,Subscribed= true }

                  );
            });

            modelBuilder.Entity<IdentityRole>(m =>
            {
                m.HasData(
                    new IdentityRole(){ Id="1", Name="Standard Member", NormalizedName="STANDARD MEMBER"}
                );
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (_connectionSettings.ServerType.ToLower().Contains("sqlite"))
                {
                    optionsBuilder.UseSqlite($"FileName={Directory.GetCurrentDirectory()}\\wwwroot\\{_connectionSettings.Database}.db");
                }
                if (_connectionSettings.ServerType.ToLower().Contains("mysql"))
                {
                    string _connectionString = $"server={_connectionSettings.Server};port={_connectionSettings.Port};user={_connectionSettings.User}; password={_connectionSettings.Password};database={_connectionSettings.Database};Convert Zero Datetime=True;Default Command Timeout=300000;";
                    optionsBuilder.UseMySQL(_connectionString);
                }
                if (_connectionSettings.ServerType.ToLower().Contains("mssql"))
                {
                    string _connectionString = string.Empty;
                    if (_connectionSettings.WindowsAuth == true)
                    {
                         _connectionString = $"server={_connectionSettings.Server};Integrated Security=true;database={_connectionSettings.Database};";

                    }
                    else
                    {
                        _connectionString = $"server={_connectionSettings.Server};user={_connectionSettings.User}; password={_connectionSettings.Password};database={_connectionSettings.Database};";

                    }
                    optionsBuilder.UseSqlServer(_connectionString);
                }
            }
        }
    }
}

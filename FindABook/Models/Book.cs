using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FindABook.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }

        public bool BestSeller { get; set; }

        public string Publisher { get; set; }

        public string ImageUrl { get; set; }

        
        public virtual Category Category { get; set; }

        public virtual ICollection<BookSubscription> BookSubscription { get; set; }
    }
}

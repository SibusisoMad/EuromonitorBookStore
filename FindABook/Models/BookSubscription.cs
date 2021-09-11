using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FindABook.Models
{
    public class BookSubscription
    {
        [Key]
        public int SubscriptionId { get; set; }

        public string UserId { get; set; }

       public int BookId { get; set; }

        public bool Subscribed { get; set; }
        
        public DateTime? LastModified { get; set; }

        public virtual Book Book { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Models
{
    public class Review
    {
        public int ReviewId{ get; set; }
        public int BookId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public decimal Rating { get; set; }

        public Review()
        {
            Rating = 0;
            Date = DateTime.Now;
            AuthorName = "";
            Content = "";
        }
    }
}

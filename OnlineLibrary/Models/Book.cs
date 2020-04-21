using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Release Date")]
        public DateTime ReleaseDate { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;

        [Display(Name="Publishing House")]
        public string PublishingHouse { get; set; }
        public string Collection{ get; set; }
        public decimal Rating { get; set; } = 0;
        public ICollection<Review> Reviews { get; set; }

        public string Img{ get; set; }
        public Book()
        {
            Reviews = new List<Review>();
        }
    }
}

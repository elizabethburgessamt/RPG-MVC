using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RPGBookDB
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Game { get; set; }
        [Display(Name = "Book Type")]
        public string BookType { get; set; }
        public string System { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        [Display(Name = "Has Played")]
        public bool HasPlayed { get; set; }
        public string Rating { get; set; }
    }

    public class RPGBookDBContext : DbContext
    {
        public DbSet<RPGBookDB> RPGBooks { get; set; }
    }

}
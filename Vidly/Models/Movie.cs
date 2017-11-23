using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public DateTime? MovieReleaseDate { get; set; }
        public DateTime? DateAddedTOCollection { get; set; }
        public int NumberInStocks { get; set; }
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }
    }
}
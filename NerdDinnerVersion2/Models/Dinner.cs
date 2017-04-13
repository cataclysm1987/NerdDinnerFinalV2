using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NerdDinnerVersion2.Models
{
    public class Dinner
    {
        public int DinnerId { get; set; }
        [Required(ErrorMessage = "Please Enter a Dinners Title")]
        [StringLength(20, ErrorMessage = "Title is too long")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the date of the dinner")]
        public DateTime EventDate { get; set; }
        [Required(ErrorMessage = "Please enter the location of the dinner")]
        [StringLength(30, ErrorMessage = "Address is too long")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid e-mail address")]
        public string HostedBy { get; set; }

        public Dinner()
        {
            List<RSVP> RSVPs = new List<RSVP>();
        }

        public virtual ICollection<RSVP> RSVPs { get; set; }
    }
}
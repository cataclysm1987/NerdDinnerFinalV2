using System.ComponentModel.DataAnnotations;

namespace NerdDinnerVersion2.Models
{
    public class RSVP
    {
        [Key]
        public int RsvpId { get; set; }
        public int DinnerId { get; set; }
        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid e-mail address")]
        public string AttendeeEmail { get; set; }

        public virtual Dinner Dinner { get; set; }
    }
}
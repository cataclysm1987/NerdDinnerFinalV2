using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NerdDinnerVersion2.Models
{
    public class DinnerDetailsViewModel
    {
        public Dinner Dinner { get; set; }
        public PagedList<RSVP> RSVPs { get; set; }
        public int RSVPCount { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SBShared.Models
{
    public class ReservationModel
    {
        [Required]
        public string ReservationName { get; set; }
        [Required]
        public string ReservationIdentifier { get; set; }
    }
}

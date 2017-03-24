using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AarhusWebDevCoop.ViewModels
{
    public class ContactForm
    {
        [Required(ErrorMessage = "Please add a name. This field can't be empty.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="This is not a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please add a subject. This field can't be empty.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please add a message. This field can't be empty.")]
        public string Message { get; set; }
    }
}
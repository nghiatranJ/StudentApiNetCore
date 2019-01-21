using System;
using System.Collections.Generic;

namespace APINetCore.Models
{
    public partial class TblStudent
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool? Status { get; set; }
        public string Password { get; set; }
    }
}

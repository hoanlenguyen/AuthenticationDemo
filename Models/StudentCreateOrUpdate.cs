using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationDemo.Models
{
    public class StudentCreateOrUpdate
    {
        [DefaultValue(default(int))]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }       
        public string UserId { get; set; }
    }
}

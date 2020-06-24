using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AuthenticationDemo.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        //public virtual ICollection<Enrollment> Enrollments { get; set; }
        //[JsonIgnore]
        //private List<Enrollment> enrollments = new List<Enrollment>();
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [NotMapped]
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}

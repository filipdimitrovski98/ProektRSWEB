using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_1.Models
{
    public class Student
    {

        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string StudentId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Current Semestar")]
        public int CurrentSemestar { get; set; }
        [Display(Name = "Acquired Credits")]
        public int AcquiredCredits { get; set; }
        [Display(Name = "Education Level")]
        [StringLength(25)]
        public string EducationLevel { get; set; }
        public ICollection<Enrollment> Courses { get; set; }



        [NotMapped]
        public int Age
        {
            get
            {
                TimeSpan span = DateTime.Now - EnrollmentDate;
                double years = (double)span.TotalDays / 365.2425;
                return (int)years;
            }
        }
        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }


    }
}

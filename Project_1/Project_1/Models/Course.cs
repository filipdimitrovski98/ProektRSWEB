using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_1.Models
{
    public class Course
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]

        public int Credits { get; set; }
        [Required]

        public int Semester { get; set; }
        [StringLength(100)]
        public string Programme { get; set; }
        [StringLength(25)]
        [Display(Name = "Education Level")]
        public string EducationLevel { get; set; }
        
        
        public int FirstTeacherId { get; set; }
        public Teacher FirstTeacher { get; set; }
      
        
        public int SecondTeacherId { get; set; }
        
        public Teacher SecondTeacher { get; set; }
        public ICollection<Enrollment> Students { get; set; }

    }
}

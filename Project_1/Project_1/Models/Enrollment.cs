using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_1.Models
{
    public class Enrollment
    {
        //internal object Course;

        [Required]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public Course Course { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public Student Student { get; set; }
        [StringLength(10)]
        public string Semestar { get; set; }
        public int Year { get; set; }
        public int Grade { get; set; }
        [Display(Name = "Seminal Url")]
        [StringLength(255)]
        public string SeminalUrl { get; set; }
        [Display(Name = "Project Url")]
        [StringLength(255)]
        public string ProjectUrl { get; set; }
        [Display(Name = "Exam Points")]
        public int ExamPoints { get; set; }
        [Display(Name = "Seminal Points")]
        public int SeminalPoints { get; set; }
        [Display(Name = "Project Points")]
        public int ProjectPoints { get; set; }
        [Display(Name = "Additional Points")]
        public int AdditionalPoints { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Finish Date")]
        public DateTime FinishDate { get; set; }

    }
}

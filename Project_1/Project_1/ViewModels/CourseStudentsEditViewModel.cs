using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_1.ViewModels
{
    public class CourseStudentsEditViewModel
    {
        public Course Course { get; set; }
        public IEnumerable<int> SelectedStudents{ get; set; }
        public IEnumerable<SelectListItem> StudentList { get; set; }
    }
}

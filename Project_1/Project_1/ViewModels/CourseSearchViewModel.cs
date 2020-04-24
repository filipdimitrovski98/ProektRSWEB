using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Project_1.ViewModels
{
    public class CourseSearchViewModel
    {
        public IList<Course> Courses { get; set; }
        
        public string title { get; set; }
        public string programme { get; set; }
        public int semmester { get; set; }


    }
}
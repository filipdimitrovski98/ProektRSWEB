using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Models;
using System.Collections.Generic;
using System.Linq;
using System;


namespace Project_1.ViewModels
{
    public class TeacherSearchViewModel
    {
        public IList<Teacher> Teachers { get; set; }
        public string SearchFirstName { get; set; }
        public string SearchLastName { get; set; }
        public string SearchAcademicRank { get; set; }
        public string SearchDegree { get; set; }

    }
}

using Project_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_1.ViewModels
{
    public class StudentsSearchViewModel
    {
        public IList<Student> Students { get; set; }
        public string studentId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        

    }
}

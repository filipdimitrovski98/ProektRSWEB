using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_1.Data;
using Project_1.Models;
using System.Linq;
using System.Threading.Tasks;
using Project_1.ViewModels;
using System.Collections.Generic;
using System;

namespace Project_1.Controllers
{
    public class CoursesController : Controller
    {
        private readonly MVCProject_1Context _context;

        public CoursesController(MVCProject_1Context context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index(string searchtitle,string searchprogramme, int searchsemmester)
        {
            IQueryable<Course> courses = _context.Course.AsQueryable();
            courses = _context.Course.Include(c => c.FirstTeacher).Include(c => c.SecondTeacher).Include(m => m.Students).ThenInclude(m => m.Student);


            if (!string.IsNullOrEmpty(searchtitle))
            {
                courses = courses.Where(s => s.Title.Contains(searchtitle));
            }
            if (searchsemmester != 0)
            {
                courses = courses.Where(x => x.Semester.Equals(searchsemmester));
            }
            if (!string.IsNullOrEmpty(searchprogramme))
            {
                courses = courses.Where(s => s.Programme.Contains(searchprogramme));
            }
           

            var courseSearchVM = new CourseSearchViewModel
            {
                Courses = await courses.ToListAsync(),
                title = searchtitle,
                programme = searchprogramme,
                semmester= searchsemmester

            };

            return View(courseSearchVM);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.FirstTeacher)
                .Include(c => c.SecondTeacher).Include(m => m.Students).ThenInclude(m => m.Student)

                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create  
        public IActionResult Create()
        {
            ViewData["FirstTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName");
            ViewData["SecondTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Credits,Semester,Programme,EducationLevel,FirstTeacherId,SecondTeacherId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirstTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName", course.FirstTeacherId);
            ViewData["SecondTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName", course.SecondTeacherId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        //tuka
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = _context.Course.Where(m => m.Id == id).Include(m => m.Students).First();


            course= await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            //tuka
            CourseStudentsEditViewModel viewmodel = new CourseStudentsEditViewModel
            {
                Course = course,
                StudentList = new MultiSelectList(_context.Student.OrderBy(s => s.StudentId),"Id", "FullName"),
                SelectedStudents = course.Students.Select(sa => sa.StudentId)
            };
            

            ViewData["FirstTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName", course.FirstTeacherId);
            ViewData["SecondTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName", course.SecondTeacherId);
            return View(viewmodel); //tuka
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseStudentsEditViewModel viewmodel)  /*Edit(int id, [Bind("Id,Title,Credits,Semester,Programme,EducationLevel,FirstTeacherId,SecondTeacherId")] Course course)*/
        {
            if (id != viewmodel.Course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.Course);
                    await _context.SaveChangesAsync();
                    IEnumerable<int> listStudents = viewmodel.SelectedStudents;
                    IQueryable<Enrollment> toBeRemoved = _context.Enrollment.Where(s => !listStudents.Contains(s.StudentId) && s.CourseId == id);
                    _context.Enrollment.RemoveRange(toBeRemoved);
                    IEnumerable<int> existStudents = _context.Enrollment.Where(s => listStudents.Contains(s.StudentId) && s.CourseId == id).Select(s => s.StudentId);
                    IEnumerable<int> newStudents = listStudents.Where(s => !existStudents.Contains(s));
                    foreach (int studentId in newStudents)
                        _context.Enrollment.Add(new Enrollment { StudentId =studentId , CourseId = id });

                    //_context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(viewmodel.Course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirstTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName", viewmodel.Course.FirstTeacherId);
            ViewData["SecondTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName", viewmodel.Course.SecondTeacherId);
            return View(viewmodel);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.FirstTeacher)
                .Include(c => c.SecondTeacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Course.FindAsync(id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }
    }
}

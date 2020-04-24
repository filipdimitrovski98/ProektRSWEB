using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_1.Data;
using Project_1.Models;
using Project_1.ViewModels;

namespace Project_1.Controllers
{
    public class TeachersController : Controller
    {
        private readonly MVCProject_1Context _context;

        public TeachersController(MVCProject_1Context context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index(string searchfirstname,string searchlastname,string searchacademicrank,string searchdegree)
        {
            IQueryable<Teacher> teachers = _context.Teacher.AsQueryable();
           
            if (!string.IsNullOrEmpty(searchfirstname))
            {
                teachers = teachers.Where(x => x.FirstName == searchfirstname);
            }
            if (!string.IsNullOrEmpty(searchlastname))
            {
                teachers = teachers.Where(x => x.LastName == searchlastname);
            }
            if (!string.IsNullOrEmpty(searchacademicrank))
            {
                teachers = teachers.Where(x => x.AcademicRank == searchacademicrank);
            }
            if (!string.IsNullOrEmpty(searchdegree))
            {
                teachers = teachers.Where(x => x.Degree == searchdegree);
            }


            var teacherSearchVM = new TeacherSearchViewModel
            {

                Teachers = await teachers.ToListAsync(),
                SearchFirstName = searchfirstname,
                SearchLastName = searchlastname,
                SearchAcademicRank = searchacademicrank,
                SearchDegree = searchdegree
            };
            return View(teacherSearchVM);
            //return View(await _context.Teacher.ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Degree,AcademicRank,OfficeNumber,HireDate")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Degree,AcademicRank,OfficeNumber,HireDate")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
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
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teacher.Any(e => e.Id == id);
        }
    }
}

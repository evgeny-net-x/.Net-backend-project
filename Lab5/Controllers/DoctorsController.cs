using System;
using System.Collections.Generic;
using System.Linq;
using Backend5.Data;
using System.Collections.ObjectModel;   
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using Backend5.Models;
using Backend5.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend5.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext context;

        public DoctorsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: /Doctor
        public async Task<IActionResult> Index()
        {
            return View(await this.context.Doctors.ToListAsync());
        }

        // GET: /Doctor/Create
        public IActionResult Create()
        {
            return this.View(new DoctorCreateModel());
        }

        // POST: /Doctor/Create
        [HttpPost]
        public async Task<IActionResult> Create(DoctorCreateModel model)
        {
            if (this.ModelState.IsValid)
            {
                var doctor = new Doctor
                {
                    Name = model.Name,
                    Speciality = model.Speciality
                };

                this.context.Add(doctor);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        // GET: /Doctor/Details
        public async Task<IActionResult> Details(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            Doctor doctor = await this.context.Doctors.SingleOrDefaultAsync<Doctor>(m => m.Id == id);
            if (doctor == null)
                return this.NotFound();

            return this.View(doctor);
        }

        // GET: /Doctor/Edit
        public async Task<IActionResult> Edit(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            Doctor doctor = await this.context.Doctors.SingleOrDefaultAsync<Doctor>(m => m.Id == id);
            if (doctor == null)
                return this.NotFound();

            var model = new DoctorEditModel
            {
                Name = doctor.Name,
                Speciality = doctor.Speciality
            };
            
            return this.View(model);
        }

        // POST: /Doctor/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Int32? id, DoctorEditModel model)
        {
            if (id == null)
                return this.NotFound();

            var doctor = await this.context.Doctors.SingleOrDefaultAsync<Doctor>(m => m.Id == id);
            if (doctor == null)
                return this.NotFound();

            if (this.ModelState.IsValid)
            {
                doctor.Name = model.Name;
                doctor.Speciality = model.Speciality;

                this.context.Update<Doctor>(doctor);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        // GET: Doctor/Delete
        public async Task<IActionResult> Delete(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var doctor = await this.context.Doctors.SingleOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
                return this.NotFound();

            return this.View(doctor);
        }

        // POST: Doctor/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var doctor = await this.context.Doctors.SingleOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
                return this.NotFound();

            this.context.Remove(doctor);
            await this.context.SaveChangesAsync();

            return this.RedirectToAction("Index");
        }
    }
}

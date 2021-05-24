using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend5.Data;
using Backend5.Models;
using Backend5.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend5.Controllers
{
    public class PatientController : Controller
    {
        public readonly ApplicationDbContext context;

        public PatientController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: /Patient
        public async Task<IActionResult> Index()
        {
            return View(await this.context.Patients.ToListAsync());
        }

        // GET: /Patient/Create
        public IActionResult Create()
        {
            return this.View(new PatientCreateModel());
        }

        // POST: /Patient/Create
        [HttpPost]
        public async Task<IActionResult> Create(PatientCreateModel model)
        {
            if (this.ModelState.IsValid)
            {
                var patient = new Patient
                {
                    Name = model.Name,
                    Address = model.Address,
                    Birthday = model.Birthday,
                    Gender = model.Gender
                };

                this.context.Add(patient);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        // GET: Patient/Details
        public async Task<IActionResult> Details(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var patient = await this.context.Patients.SingleOrDefaultAsync(m => m.Id == id);
            if (patient == null)
                return this.NotFound();

            return this.View(patient);
        }

        // GET: Patient/Edit
        public async Task<IActionResult> Edit(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var patient = await this.context.Patients.SingleOrDefaultAsync(m => m.Id == id);
            if (patient == null)
                return this.NotFound();

            var model = new PatientCreateModel // The same with PatientEditModel
            {
                Name = patient.Name,
                Address = patient.Address,
                Birthday = patient.Birthday,
                Gender = patient.Gender
            };

            this.ViewBag.Id = id;
            return this.View(model);
        }

        // POST: Patient/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Int32? id, PatientCreateModel model)
        {
            if (id == null)
                return this.NotFound();

            var patient = await this.context.Patients.SingleOrDefaultAsync(m => m.Id == id);
            if (patient == null)
                return this.NotFound();

            if (this.ModelState.IsValid)
            {
                patient.Name = model.Name;
                patient.Address = model.Address;
                patient.Birthday = model.Birthday;
                patient.Gender = model.Gender;

                this.context.Update(patient);
                await this.context.SaveChangesAsync();
                return this.View("Index");
            }

            this.ViewBag.Id = id;
            return this.View(model);
        }

        // GET: Patient/Delete
        public async Task<IActionResult> Delete(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var patient = await this.context.Patients.SingleOrDefaultAsync(m => m.Id == id);
            if (patient == null)
                return this.NotFound();

            return this.View(patient);
        }

        // POST: Patient/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Int32 id)
        {
            var patient = await this.context.Patients.SingleOrDefaultAsync(m => m.Id == id);
            this.context.Remove(patient);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }
    }
}

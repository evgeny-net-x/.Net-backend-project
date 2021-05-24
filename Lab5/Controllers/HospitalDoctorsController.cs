using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Backend5.Models;
using Backend5.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Backend5.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend5.Controllers
{
    public class HospitalDoctorsController : Controller
    {
        public readonly ApplicationDbContext context;
        
        public HospitalDoctorsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: /HospitalDoctor
        public async Task<IActionResult> Index(Int32? hospitalId)
        {
            if (hospitalId == null)
                return this.NotFound();

            var hospital = await this.context.Hospitals.SingleOrDefaultAsync(m => m.Id == hospitalId);

            var items = await this.context.HospitalDoctors
                .Include(h => h.Hospital)
                .Include(h => h.Doctor)
                .Where(m => m.HospitalId == hospitalId)
                .ToListAsync();

            this.ViewBag.Hospital = hospital;
            return View(items);
        }

        // GET: /HospitalDoctor/Create
        public async Task<IActionResult> Create(Int32? hospitalId)
        {
            if (hospitalId == null)
                return this.NotFound();

            var hospital = await this.context.Hospitals.SingleOrDefaultAsync(m => m.Id == hospitalId);
            if (hospital == null)
                return this.NotFound();

            this.ViewBag.Hospital = hospital;
            this.ViewBag.DoctorId = new SelectList(this.context.Doctors, "Id", "Name");
            return this.View(new HospitalDoctorCreateModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Int32? hospitalId, HospitalDoctorCreateModel model)
        {
            if (hospitalId == null)
                return this.NotFound();

            var hospital = await this.context.Hospitals.SingleOrDefaultAsync(m => m.Id == hospitalId);
            if (hospital == null)
                return this.NotFound();

            if (this.ModelState.IsValid)
            {
                var hospitalDoctor = new HospitalDoctor
                {
                    HospitalId = hospital.Id,
                    DoctorId = model.DoctorId
                };

                this.context.Add(hospitalDoctor);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index", new { hospitalId = hospital.Id });
            }

            this.ViewBag.Hospital = hospital;
            this.ViewBag.DoctorId = new SelectList(this.context.Doctors, "Id", "Name");
            return this.View(model);
        }

        // GET: /HospitalDoctor/Delete
        public async Task<IActionResult> Delete(Int32? hospitalId, Int32? doctorId)
        {
            if (hospitalId == null)
                return this.NotFound();

            var hospitalDoctor = await this.context.HospitalDoctors
                .Include(h => h.Hospital)
                .Include(h => h.Doctor)
                .SingleOrDefaultAsync(m => m.HospitalId == hospitalId);

            return this.View(hospitalDoctor);
        }

        // POST: /HospitalDoctor/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Int32 hospitalId, Int32 doctorId)
        {
            var hospitalDoctor = await this.context.HospitalDoctors.SingleOrDefaultAsync(m => m.HospitalId == hospitalId && m.DoctorId == doctorId);
            if (hospitalDoctor == null)
                return this.NotFound();

            this.context.Remove(hospitalDoctor);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index", new { HospitalId = hospitalId });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backend5.Data;
using Backend5.Models;
using Backend5.Models.ViewModels;

namespace Backend5.Controllers
{
    public class DoctorPatientsController : Controller
    {
        private readonly ApplicationDbContext context;

        public DoctorPatientsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: /DoctorPatients
        public async Task<IActionResult> Index(Int32? patientId)
        {
            if (patientId == null)
                return this.NotFound();

            var patient = await this.context.Patients.SingleOrDefaultAsync(m => m.Id == patientId);
            if (patient == null)
                return this.NotFound();

            this.ViewBag.Patient = patient;
            return this.View(await this.context.DoctorPatients
                .Include(m => m.Doctor)
                .Include(m => m.Patient)
                .Where(m => m.PatientId == patientId)
                .ToListAsync()
            );
        }

        // GET: /DoctorPatients/Create
        public async Task<IActionResult> Create(Int32? patientId)
        {
            if (patientId == null)
                return this.NotFound();

            var patient = await this.context.Patients.SingleOrDefaultAsync(m => m.Id == patientId);
            if (patient == null)
                return this.NotFound();

            ViewBag.Patient = patient;
            ViewBag.DoctorId = new SelectList(this.context.Doctors, "Id", "Name");
            return this.View(new DoctorPatientCreateModel());
        }

        // POST: /DoctorPatients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Int32? patientId, DoctorPatient doctorPatient)
        {
            if (patientId == null)
                return this.NotFound();

            var patient = await this.context.Patients.SingleOrDefaultAsync(m => m.Id == patientId);
            if (patient == null)
                return this.NotFound();

            if (this.ModelState.IsValid)
            {
                this.context.Add(doctorPatient);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index", new { patientId = patient.Id });
            }

            ViewBag.Patient = patient;
            ViewBag.DoctorId = new SelectList(this.context.Patients, "Id", "Name", doctorPatient.PatientId);
            return this.View(doctorPatient);
        }

        // GET: /DoctorPatients/Delete
        public async Task<IActionResult> Delete(Int32? patientId, Int32? doctorId)
        {
            if (patientId == null || doctorId == null)
                return this.NotFound();

            var doctorPatient = await this.context.DoctorPatients
                .Include(d => d.Doctor)
                .Include(d => d.Patient)
                .SingleOrDefaultAsync(m => m.PatientId == patientId && m.DoctorId == doctorId);

            if (doctorPatient == null)
                return this.NotFound();

            return this.View(doctorPatient);
        }

        // POST: /DoctorPatients/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Int32 patientId, Int32 doctorId)
        {
            var doctorPatient = await this.context.DoctorPatients.SingleOrDefaultAsync(m => m.PatientId == patientId && m.DoctorId == doctorId);
            this.context.DoctorPatients.Remove(doctorPatient);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }
    }
}

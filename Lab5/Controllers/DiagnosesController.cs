using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend5.Data;
using Microsoft.EntityFrameworkCore;
using Backend5.Models.ViewModels;
using Backend5.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend5.Controllers
{
    public class DiagnosesController : Controller
    {
        private readonly ApplicationDbContext context;

        public DiagnosesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: /Diagnosis
        public async Task<IActionResult> Index(Int32? patientId)
        {
            if (patientId == null)
                return this.NotFound();

            this.ViewBag.PatientId = patientId;
            return this.View(await this.context.Diagnoses
                .Where(m => m.PatientId == patientId)
                .ToListAsync()
            );
        }

        // GET: /Diagnosis/Create
        public async Task<IActionResult> Create(Int32? patientId)
        {
            if (patientId == null)
                return this.NotFound();

            var patient = await this.context.Patients.SingleOrDefaultAsync(m => m.Id == patientId);
            if (patient == null)
                return this.NotFound();
            
            this.ViewBag.PatientId = patientId;
            return this.View(new DiagnosisCreateModel());
        }

        // POST: /Diagnosis/Create
        [HttpPost]
        public async Task<IActionResult> Create(Int32? patientId, DiagnosisCreateModel model)
        {
            if (patientId == null)
                return this.NotFound();

            var patient = await this.context.Patients.SingleOrDefaultAsync(m => m.Id == patientId);
            if (patient == null)
                return this.NotFound();

            if (this.ModelState.IsValid)
            {
                var diagnosis = new Diagnosis
                {
                    PatientId = patient.Id,
                    Type = model.Type,
                    Details = model.Details,
                    Complications = model.Complications
                };

                this.context.Add(diagnosis);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index", new { patientId = diagnosis.PatientId });
            }

            this.ViewBag.PatientId = patientId;
            return this.View(model);
        }

        // GET: /Diagnoses/Details
        public async Task<IActionResult> Details(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            return this.View(await this.context.Diagnoses.SingleOrDefaultAsync(m => m.Id == id));
        }

        // GET: /Diagnoses/Edit
        public async Task<IActionResult> Edit(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var diagnosis = await this.context.Diagnoses.SingleOrDefaultAsync(m => m.Id == id);
            if (diagnosis == null)
                return this.NotFound();

            var model = new DiagnosisCreateModel
            {
                Type = diagnosis.Type,
                Details = diagnosis.Details,
                Complications = diagnosis.Complications
            };

            this.ViewBag.PatientId = diagnosis.PatientId;
            return this.View(model);
        }

        // POST: /Diagnoses/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Int32? id, DiagnosisCreateModel model)
        {
            if (id == null)
                return this.NotFound();

            var diagnosis = await this.context.Diagnoses.SingleOrDefaultAsync(m => m.Id == id);
            if (diagnosis == null)
                return this.NotFound();

            if (this.ModelState.IsValid)
            {
                diagnosis.Type = model.Type;
                diagnosis.Details = model.Details;
                diagnosis.Complications = model.Complications;

                this.context.Update(diagnosis);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index", new { patientId = diagnosis.PatientId });
            }

            this.ViewBag.PatientId = diagnosis.PatientId;
            this.ViewBag.Id = id;
            return this.View(model);
        }

        // GET: /Diagnoses/Delete
        public async Task<IActionResult> Delete(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var diagnosis = await this.context.Diagnoses.SingleOrDefaultAsync(m => m.Id == id);
            if (diagnosis == null)
                return this.NotFound();
            
            return this.View(diagnosis);
        }

        // POST: /Diagnoses/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Int32 id)
        {
            var diagnosis = await this.context.Diagnoses.SingleOrDefaultAsync(m => m.Id == id);
            this.context.Remove(diagnosis);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index", new { patientId = diagnosis.PatientId });
        }
    }
}

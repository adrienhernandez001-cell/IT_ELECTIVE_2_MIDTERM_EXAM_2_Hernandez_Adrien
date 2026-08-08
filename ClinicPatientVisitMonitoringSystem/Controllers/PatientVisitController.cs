using ClinicPatientVisitMonitoringSystem.Models;
using ClinicPatientVisitMonitoringSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicPatientVisitMonitoringSystem.Controllers
{
    [Authorize]
    public class PatientVisitController : Controller
    {
        private readonly PatientVisitRepository _repository;

        public PatientVisitController()
        {
            _repository = new PatientVisitRepository();
        }

        // =========================
        // PATIENT MONITORING LIST
        // =========================

        [HttpGet]
        public IActionResult Index(string? searchTerm)
        {
            var visits = _repository.Search(searchTerm ?? string.Empty);

            ViewBag.SearchTerm = searchTerm;

            return View(visits);
        }


        // =========================
        // REGISTER PATIENT VISIT
        // =========================

        [HttpGet]
        public IActionResult Create()
        {
            var visit = new PatientVisit
            {
                ArrivalDateTime = DateTime.Now,
                Status = "Waiting"
            };

            return View(visit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PatientVisit visit)
        {
            if (!ModelState.IsValid)
            {
                return View(visit);
            }

            visit.Status = "Waiting";
            visit.ConsultationCompletedDateTime = null;

            _repository.Add(visit);

            TempData["SuccessMessage"] =
                "Patient visit registered successfully.";

            return RedirectToAction(nameof(Index));
        }


        // =========================
        // VIEW DETAILS
        // =========================

        [HttpGet]
        public IActionResult Details(int id)
        {
            var visit = _repository.GetById(id);

            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }


        // =========================
        // EDIT PATIENT VISIT
        // =========================

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var visit = _repository.GetById(id);

            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PatientVisit visit)
        {
            if (!ModelState.IsValid)
            {
                return View(visit);
            }

            var existingVisit = _repository.GetById(visit.Id);

            if (existingVisit == null)
            {
                return NotFound();
            }

            if (existingVisit.Status == "Completed")
            {
                ModelState.AddModelError(
                    "",
                    "A completed consultation cannot be edited.");

                return View(existingVisit);
            }

            _repository.Update(visit);

            TempData["SuccessMessage"] =
                "Patient visit updated successfully.";

            return RedirectToAction(nameof(Index));
        }


        // =========================
        // COMPLETE CONSULTATION
        // =========================

        // Displays the Complete confirmation page
        [HttpGet]
        public IActionResult Complete(int id)
        {
            var visit = _repository.GetById(id);

            if (visit == null)
            {
                return NotFound();
            }

            if (visit.Status == "Completed")
            {
                TempData["SuccessMessage"] =
                    "This consultation is already completed.";

                return RedirectToAction(nameof(Index));
            }

            return View(visit);
        }


        // Processes the Complete confirmation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompleteConfirmed(int id)
        {
            var visit = _repository.GetById(id);

            if (visit == null)
            {
                return NotFound();
            }

            if (visit.Status == "Completed")
            {
                TempData["SuccessMessage"] =
                    "This consultation is already completed.";

                return RedirectToAction(nameof(Index));
            }

            var success = _repository.Complete(id);

            if (!success)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] =
                "Consultation marked as completed.";

            return RedirectToAction(nameof(Index));
        }
    }
}
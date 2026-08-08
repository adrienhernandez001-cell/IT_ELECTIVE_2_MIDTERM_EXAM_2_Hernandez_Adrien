using Microsoft.AspNetCore.Mvc;

namespace ClinicPatientVisitMonitoringSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "PatientVisit");
        }
    }
}
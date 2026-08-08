using ClinicPatientVisitMonitoringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicPatientVisitMonitoringSystem.Repositories
{
    public class PatientVisitRepository
    {
        private static readonly List<PatientVisit> Visits = new List<PatientVisit>();

        private static int NextVisitNumber = 1001;

        public List<PatientVisit> GetAll()
        {
            return Visits.ToList();
        }

        public PatientVisit? GetById(int id)
        {
            return Visits.FirstOrDefault(v => v.Id == id);
        }

        public void Add(PatientVisit visit)
        {
            visit.Id = Visits.Count == 0 ? 1 : Visits.Max(v => v.Id) + 1;

            visit.VisitNumber = NextVisitNumber++;

            if (visit.ArrivalDateTime == default)
            {
                visit.ArrivalDateTime = DateTime.Now;
            }

            if (string.IsNullOrWhiteSpace(visit.Status))
            {
                visit.Status = "Waiting";
            }

            Visits.Add(visit);
        }

        public bool Update(PatientVisit updatedVisit)
        {
            var existingVisit = GetById(updatedVisit.Id);

            if (existingVisit == null)
            {
                return false;
            }

            existingVisit.FirstName = updatedVisit.FirstName;
            existingVisit.LastName = updatedVisit.LastName;
            existingVisit.Age = updatedVisit.Age;
            existingVisit.Sex = updatedVisit.Sex;
            existingVisit.ContactNumber = updatedVisit.ContactNumber;
            existingVisit.Address = updatedVisit.Address;
            existingVisit.Physician = updatedVisit.Physician;
            existingVisit.VisitType = updatedVisit.VisitType;
            existingVisit.ArrivalDateTime = updatedVisit.ArrivalDateTime;
            existingVisit.Status = updatedVisit.Status;
            existingVisit.ChiefComplaint = updatedVisit.ChiefComplaint;
            existingVisit.Notes = updatedVisit.Notes;

            return true;
        }

        public bool Complete(int id)
        {
            var visit = GetById(id);

            if (visit == null)
            {
                return false;
            }

            visit.Status = "Completed";
            visit.ConsultationCompletedDateTime = DateTime.Now;

            return true;
        }

        public List<PatientVisit> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return GetAll();
            }

            searchTerm = searchTerm.Trim();

            return Visits.Where(v =>
                v.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                v.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                v.Physician.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                v.VisitType.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                v.Status.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                v.VisitNumber.ToString().Contains(searchTerm)
            ).ToList();
        }
    }
}
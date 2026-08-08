using System;

namespace ClinicPatientVisitMonitoringSystem.Models
{
    public class PatientVisit
    {
        public int Id { get; set; }

        public int VisitNumber { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Sex { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Physician { get; set; } = string.Empty;

        public string VisitType { get; set; } = string.Empty;

        public DateTime ArrivalDateTime { get; set; }

        public string Status { get; set; } = string.Empty;

        public string ChiefComplaint { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public DateTime? ConsultationCompletedDateTime { get; set; }


        // Compatibility properties for the current Views

        public string PatientName
        {
            get
            {
                return $"{FirstName} {LastName}".Trim();
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    FirstName = string.Empty;
                    LastName = string.Empty;
                    return;
                }

                string[] parts = value.Trim()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 1)
                {
                    FirstName = parts[0];
                    LastName = string.Empty;
                }
                else
                {
                    FirstName = parts[0];
                    LastName = string.Join(
                        " ",
                        parts,
                        1,
                        parts.Length - 1
                    );
                }
            }
        }

        public string DoctorName
        {
            get
            {
                return Physician;
            }
            set
            {
                Physician = value;
            }
        }

        public DateTime VisitDate
        {
            get
            {
                return ArrivalDateTime;
            }
            set
            {
                ArrivalDateTime = value;
            }
        }

        public string Reason
        {
            get
            {
                return ChiefComplaint;
            }
            set
            {
                ChiefComplaint = value;
            }
        }
    }
}
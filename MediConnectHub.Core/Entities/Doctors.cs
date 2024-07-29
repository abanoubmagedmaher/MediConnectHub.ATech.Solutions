using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnectHub.Core.Entities
{
    public class Doctors:BaseEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {MiddleName} {LastName}".Trim();
        public string PhoneNumber { get; set; }
        public string WhatsAppNumber { get; set; }
        public string NationalID { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public GenderEnum Gender { get; set; }
        public string Specialization { get; set; }
        public string SecondryPhoneNumber { get; set; }
        public bool? Master { get; set; }
        public string? MasterDetails { get; set; }

        public string? Description { get; set; }

        public string University { get; set; }
        public string GraduationYear { get; set; }
        public decimal GPA { get; set; }
        public MaritalStatusEnum Status { get; set; }

        public bool? HasChildren { get; set; }
        public int? NumberOfChildren { get; set; }
        public string PlacesWorkDetails { get; set; }
        public decimal TotalExperienceyears { get; set; }
        public decimal Weight { get; set; }
        public decimal height { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal MaxWeeklyHours { get; set; }
        public ReligionEnum Religion { get; set; }
          public DateTime CreatedDate { get; set; }

        public string GetFormattedBirthDate()
        {
            return BirthDate.ToString("dd-MM-yy");
        }
    }
 
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnectHub.Core.Entities
{
    public enum GenderEnum
    {
        Male,
        Female

    }
    public enum MaritalStatusEnum : byte
    {
        Single = 0,
        Married = 1,
        Other
    }
    public enum ReligionEnum
    {
        Christianity,
        Islam,
        Other
    }
    public class Patients:BaseEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {MiddleName} {LastName}".Trim();
        public string PhoneNumber { get; set; }
        public string WhatsAppNumber { get; set; }
        public string NationalID { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address  { get; set; }
        public string  Email { get; set; }
        public ReligionEnum Religion { get; set; }
        public DateTime CreatedDate { get; set; }
        public GenderEnum Gender { get; set; }
        public MaritalStatusEnum Status { get; set; }
        public string GetFormattedBirthDate()
        {
            return BirthDate.ToString("yyyy-mm-dd");
        }



    }
    public enum Gender
    {
        Male,
        Female

    }
    public enum MaritalStatus : byte
    {
        Single = 0,
        Married = 1,
        Other
    }
    public enum Religion
    {
        Christianity,
        Islam,
        Other
    }
}

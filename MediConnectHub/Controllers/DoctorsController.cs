using MediConnectHub.Core.Entities;
using MediConnectHub.DTOS;
using MediConnectHub.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediConnectHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly StoreContext _dbcotext;

        public DoctorsController(StoreContext dbcotext)
        {
            _dbcotext = dbcotext;
        }
        #region GetAllPatients

        [HttpGet("GetAllDoctors")]
        public IActionResult GetAllDoctors()
        {
            try
            {
                List<Doctors> doctors = _dbcotext.Doctors.ToList();
                if (doctors != null)
                {
                    return Ok(new { statuscode = 200, message = "Found Doctors", result = doctors });
                }
                return NotFound(new { statuscode = 404, message = "Doctors not found", result = doctors });
            }
            catch (Exception exp)
            {

                return StatusCode(500, new { statuscode = 500, message = "An error occurred while processing the request: " + exp.Message });

            }
        }
        #endregion

        #region GetDoctorsByCondition

        [HttpGet("GetDoctorsByCondition")]
        public IActionResult GetDoctorsByCondition(string search)
        {
            var SearchTerm = search.Trim();
            try
            {
                var doctors = _dbcotext.Doctors.FirstOrDefault(
                    p => p.Id == int.Parse(SearchTerm)
                 || p.NationalID == SearchTerm
                 || p.PhoneNumber == SearchTerm
                 || p.WhatsAppNumber == SearchTerm
                 || p.Email == SearchTerm
                    );
                if (doctors != null)
                {
                    return Ok(new { statuscode = 200, message = "Doctor Exsit", result = doctors });
                }
                return NotFound(new { statuscode = 404, message = "Doctors not found", result = doctors });
            }
            catch (Exception exp)
            {

                return StatusCode(500, new { statuscode = 500, message = "An error occurred while processing the request: " + exp.Message });

            }
        }
        #endregion

        #region AddDoctors

        [HttpPost("AddDoctors")]
        public IActionResult AddDoctors([FromBody] Doctors doctors)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    _dbcotext.Doctors.Add(doctors);
                    _dbcotext.SaveChanges();
                    return Ok(new { statuscode = 200, message = "Added Doctor" });
                }
                return BadRequest(new { statuscode = 400, message = "Doctor Not Added" });

            }
            catch (Exception exp)
            {

                return StatusCode(500, new { statuscode = 500, message = "An error occurred while processing the request: " + exp.Message });
            }
        }
        #endregion

        #region UpdateDoctors

        [HttpPut("UpdateDoctors")]
        public IActionResult UpdateDoctors(int id, Doctors doctors)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var OldPatient = _dbcotext.Patients.FirstOrDefault(P => P.Id == id);
                    if (OldPatient != null)
                    {
                        OldPatient.FirstName = doctors.FirstName;
                        OldPatient.LastName = doctors.LastName;
                        OldPatient.MiddleName = doctors.MiddleName;

                        OldPatient.PhoneNumber = doctors.PhoneNumber;
                        OldPatient.WhatsAppNumber = doctors.WhatsAppNumber;

                        OldPatient.Address = doctors.Address;
                        _dbcotext.SaveChanges();

                        var updatedPatientDto = new PatientsDto
                        {
                            FirstName = OldPatient.FirstName,
                            LastName = OldPatient.LastName,
                            MiddleName = OldPatient.MiddleName,
                            PhoneNumber = OldPatient.PhoneNumber,
                            WhatsAppNumber = OldPatient.WhatsAppNumber,
                            Address = OldPatient.Address

                        };

                        return Ok(new { statuscode = 200, message = "Doctor Updatted Successfully", result = doctors });

                    }
                    else
                    {
                        return NotFound(new { statusCode = 404, message = "Failed to update Doctor: Patient not found" });
                    }

                }
                else
                {
                    return BadRequest(new { statusCode = 400, message = "Invalid model state", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

                }


            }
            catch (Exception exp)
            {
                return StatusCode(500, new { statuscode = 500, message = "An error occurred while processing the request: " + exp.Message });


            }
        }
        #endregion

        #region Delete Patient

        [HttpDelete("DeleteDoctors")]
        public IActionResult DeleteDoctors(int id)
        {
            var doctors = _dbcotext.Doctors.FirstOrDefault(P => P.Id == id);
            if (doctors != null)
            {
                try
                {
                    _dbcotext.Doctors.Remove(doctors);
                    _dbcotext.SaveChanges();
                    return Ok(new { statuscode = 200, message = "Doctor Deleted Successfully", result = doctors });
                }
                catch (Exception exp)
                {

                    return StatusCode(500, new { statuscode = 500, message = "Cannot Delete Doctors : " + exp.Message });

                }
            }
            return NotFound(new { statusCode = 404, message = "Failed To Find Doctor : Doctor not found" });

        }

        #endregion
    }
}

using MediConnectHub.Core.Entities;
using MediConnectHub.DTOS;
using MediConnectHub.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediConnectHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PatientsController : ControllerBase
    {
        private readonly StoreContext _dbcotext;

        public PatientsController(StoreContext dbcotext)
        {
            _dbcotext = dbcotext;
        }
        #region GetAllPatients
        
        [HttpGet("GetAllPatients")]
        public IActionResult GetAllPatients()
        {
            try
            {
                List<Patients> patients = _dbcotext.Patients.ToList();
                if (patients != null)
                {
                    return Ok(new { statuscode = 200, message = "Found Patiens", result = patients });
                }
                return NotFound(new { statuscode = 404, message = "Patient not found", result = patients });
            }
            catch (Exception exp)
            {

                return StatusCode(500, new { statuscode = 500, message = "An error occurred while processing the request: " + exp.Message });

            }
        }
        #endregion

        #region GetPatientById
        
        [HttpGet("GetPatientByCondition")]
        public IActionResult GetPatientByCondition(string search)
        {
            var SearchTerm = search.Trim();
            try
            {
                var patient = _dbcotext.Patients.FirstOrDefault(
                    p => p.NationalID == SearchTerm
                 || p.PhoneNumber == SearchTerm
                 || p.WhatsAppNumber==SearchTerm
                 || p.Email ==SearchTerm
                    );
                if (patient != null)
                {
                    return Ok(new { statuscode = 200, message = "Patient Exsit", result = patient });
                }
                return NotFound(new { statuscode = 404, message = "Patient not found", result = patient });
            }
            catch (Exception exp)
            {

                return StatusCode(500, new { statuscode = 500, message = "An error occurred while processing the request: " + exp.Message });

            }
        }
        #endregion

        #region AddPatient
        
        [HttpPost("AddPatient")]
        public IActionResult AddPatient([FromBody]Patients Patient)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    _dbcotext.Patients.Add(Patient);
                    _dbcotext.SaveChanges();
                    return Ok(new { statuscode = 200, message = "Added Patient" });
                }
                return BadRequest(new { statuscode = 400, message = "Patient Not Added" });

            }
            catch (Exception exp)
            {

                return StatusCode(500, new { statuscode = 500, message = "An error occurred while processing the request: " + exp.Message });
            }
        }
        #endregion

        #region Update Patient
      
        [HttpPut("UpdatePatient")]
        public IActionResult UpdatePatient(int id, PatientsDto patient)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var OldPatient = _dbcotext.Patients.FirstOrDefault(P => P.Id == id);
                    if (OldPatient != null)
                    {
                        OldPatient.FirstName = patient.FirstName;
                        OldPatient.LastName = patient.LastName;
                        OldPatient.MiddleName = patient.MiddleName;

                        OldPatient.PhoneNumber = patient.PhoneNumber;
                        OldPatient.WhatsAppNumber = patient.WhatsAppNumber;

                        OldPatient.Address = patient.Address;
                        _dbcotext.SaveChanges();

                        var updatedPatientDto = new PatientsDto
                        {
                            FirstName = OldPatient.FirstName,
                            LastName = OldPatient.LastName,
                            MiddleName=OldPatient.MiddleName,
                            PhoneNumber =OldPatient.PhoneNumber,
                            WhatsAppNumber=OldPatient.WhatsAppNumber,
                            Address =OldPatient.Address

                        };

                        return Ok(new { statuscode = 200, message = "Patient Updatted Successfully", result = patient });

                    }
                    else
                    {
                        return NotFound(new { statusCode = 404, message = "Failed to update patient: Patient not found" });
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
      
        [HttpDelete("Deletepatient")]
        public IActionResult DeletePatient(int id)
        {
            var patient = _dbcotext.Patients.FirstOrDefault(P=>P.Id==id);
            if (patient != null)
            {
                try
                {
                    _dbcotext.Patients.Remove(patient);
                    _dbcotext.SaveChanges();
                    return Ok(new {statuscode=200,message= "Patient Deleted Successfully",result=patient });
                }
                catch (Exception exp)
                {

                    return StatusCode(500, new { statuscode = 500, message = "Cannot Delete Patient: " + exp.Message });

                }
            }
            return NotFound(new { statusCode = 404, message = "Failed To Find patient : Patient not found" });

        }

        #endregion
    }
}

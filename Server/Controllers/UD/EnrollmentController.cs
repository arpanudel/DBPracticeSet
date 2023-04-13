using DOOR.EF.Data;
using DOOR.EF.Models;
using DOOR.Server.Controllers.Common;
using DOOR.Shared.DTO;
using DOOR.Shared.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DOOR.Server.Controllers.UD
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : BaseController
    {
        public EnrollmentController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
        : base(_DBcontext, _OraTransMsgs)

        {
        }


        [HttpGet]
        [Route("GetEnrollment")]
        public async Task<IActionResult> GetEnrollment()
        {
            List<EnrollmentDTO> lst = await _context.Enrollments
                .Select(sp => new EnrollmentDTO
                {
                    CreatedBy = sp.CreatedBy,
                    FinalGrade = sp.FinalGrade,
                    EnrollDate = sp.EnrollDate,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = DateTime.Now,
                    SchoolId = sp.SchoolId,
                    SectionId = sp.SectionId,
                    StudentId = sp.StudentId,


                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetEnrollment/{_StudentId}/{_SectionId}/{_SchoolId}")]
        public async Task<IActionResult> GetEnrollment(int _StudentId, int _SectionId, int _SchoolId)
        {
            EnrollmentDTO? lst = await _context.Enrollments
                .Where(x => x.StudentId == _StudentId)
                .Select(sp => new EnrollmentDTO
                {
                    CreatedBy = sp.CreatedBy,
                    FinalGrade = sp.FinalGrade,
                    EnrollDate = sp.EnrollDate,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = DateTime.Now,
                    StudentId = sp.StudentId,
                    SectionId = sp.SectionId,
                    SchoolId = sp.SchoolId,

                }).FirstOrDefaultAsync();
            return Ok(lst);

        }


        [HttpPost]
        [Route("PostEnrollment")]
        public async Task<IActionResult> PostEnrollment([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            try
            {
                Enrollment e = await _context.Enrollments.Where(x => x.StudentId == _EnrollmentDTO.StudentId).FirstOrDefaultAsync();

                if (e == null)
                {
                    e = new Enrollment
                    {
                        SchoolId = _EnrollmentDTO.SchoolId,
                        SectionId = _EnrollmentDTO.SectionId,
                        StudentId = _EnrollmentDTO.StudentId,
                        ModifiedDate = DateTime.Now,
                        ModifiedBy = _EnrollmentDTO.ModifiedBy,
                        CreatedDate = DateTime.Now,
                        EnrollDate = DateTime.Now,
                        FinalGrade = _EnrollmentDTO.FinalGrade,
                        CreatedBy = _EnrollmentDTO.CreatedBy,

                    };
                    _context.Enrollments.Add(e);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }

        [HttpPut]
        [Route("PutEnrollment")]
        public async Task<IActionResult> PutEnrollment([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            try
            {
                Enrollment e = await _context.Enrollments.Where(x => x.StudentId == _EnrollmentDTO.StudentId).FirstOrDefaultAsync();

                if (e != null)
                {
                    e.SchoolId = _EnrollmentDTO.SchoolId;
                    e.SectionId = _EnrollmentDTO.SectionId;
                    e.StudentId = _EnrollmentDTO.StudentId;
                    e.ModifiedDate = DateTime.Now;
                    e.ModifiedBy = _EnrollmentDTO.ModifiedBy;
                    e.CreatedDate = DateTime.Now;
                    e.EnrollDate = DateTime.Now;
                    e.FinalGrade = _EnrollmentDTO.FinalGrade;
                    e.CreatedBy = _EnrollmentDTO.CreatedBy;

                    _context.Enrollments.Update(e);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }


        [HttpDelete]
        [Route("DeleteCourse/{_StudentId}/{_SectionId}/{_SchoolId}")]
        public async Task<IActionResult> DeleteCourse(int _StudentId, int _SectionId, int _SchoolId)
        {
            try
            {
                Enrollment en = await _context.Enrollments.Where(x => x.StudentId == _StudentId).FirstOrDefaultAsync();

                if (en != null)
                {
                    _context.Enrollments.Remove(en);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }



    }
}

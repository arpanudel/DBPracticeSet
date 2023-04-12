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
    public class StudentController : BaseController
    {
        public StudentController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)
        {
        }

        [HttpGet]
        [Route("GetStudent")]
        public async Task<IActionResult> GetStudent()
        {
            List<StudentDTO> lst = await _context.Students
                .Select(sp => new StudentDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = DateTime.Now,
                    Employer = sp.Employer,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = DateTime.Now,
                    Phone = sp.Phone,
                    RegistrationDate = DateTime.Now,
                    Salutation = sp.Salutation,
                    SchoolId = sp.SchoolId,
                    StreetAddress = sp.StreetAddress,
                    StudentId = sp.StudentId,
                    Zip = sp.Zip,


                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetStudent/{_StudentId}/{_SchoolId}")]
        public async Task<IActionResult> GetCourse(int _StudentId, int _SchoolId)
        {
            StudentDTO? lst = await _context.Students
                .Where(x => x.StudentId == _StudentId)
                .Where(x => x.SchoolId == _SchoolId)

                .Select(sp => new StudentDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = DateTime.Now,
                    Employer = sp.Employer,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = DateTime.Now,
                    Phone = sp.Phone,
                    RegistrationDate = DateTime.Now,
                    Salutation = sp.Salutation,
                    SchoolId = sp.SchoolId,
                    StreetAddress = sp.StreetAddress,
                    StudentId = sp.StudentId,
                    Zip = sp.Zip,


                }).FirstOrDefaultAsync();
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostStudent")]
        public async Task<IActionResult> PostStudent([FromBody] Student _StudentDTO)
        {
            try
            {
                Student s = await _context.Students.Where(x => x.StudentId == _StudentDTO.StudentId).FirstOrDefaultAsync();

                if (s == null)
                {
                    s = new Student
                    {
                        CreatedBy = _StudentDTO.CreatedBy,
                        CreatedDate = DateTime.Now,
                        Employer = _StudentDTO.Employer,
                        FirstName = _StudentDTO.FirstName,
                        LastName = _StudentDTO.LastName,
                        ModifiedBy = _StudentDTO.ModifiedBy,
                        ModifiedDate = DateTime.Now,
                        Phone = _StudentDTO.Phone,
                        RegistrationDate = DateTime.Now,
                        Salutation = _StudentDTO.Salutation,
                        SchoolId = _StudentDTO.SchoolId,
                        StreetAddress = _StudentDTO.StreetAddress,
                        StudentId = _StudentDTO.StudentId,
                        Zip = _StudentDTO.Zip,

                    };
                    _context.Students.Add(s);
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
        [Route("PutStudent")]
        public async Task<IActionResult> PutStudent([FromBody] StudentDTO _StudentDTO)
        {
            try
            {
                Student s = await _context.Students.Where(x => x.StudentId == _StudentDTO.StudentId).FirstOrDefaultAsync();

                if (s != null)
                {

                    s.CreatedBy = _StudentDTO.CreatedBy;
                    s.CreatedDate = DateTime.Now;
                    s.Employer = _StudentDTO.Employer;
                    s.FirstName = _StudentDTO.FirstName;
                    s.LastName = _StudentDTO.LastName;
                    s.ModifiedBy = _StudentDTO.ModifiedBy;
                    s.ModifiedDate = DateTime.Now;
                    s.Phone = _StudentDTO.Phone;
                    s.RegistrationDate = DateTime.Now;
                    s.Salutation = _StudentDTO.Salutation;
                    s.SchoolId = _StudentDTO.SchoolId;
                    s.StreetAddress = _StudentDTO.StreetAddress;
                    s.StudentId = _StudentDTO.StudentId;
                    s.Zip = _StudentDTO.Zip;

                    _context.Students.Update(s);
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
        [Route("DeleteStudent/{_StudentId}/{_SchoolId}")]
        public async Task<IActionResult> DeleteCourse(int _StudentId)
        {
            try
            {
                Student s = await _context.Students.Where(x => x.StudentId == _StudentId).FirstOrDefaultAsync();

                if (s != null)
                {
                    _context.Students.Remove(s);
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

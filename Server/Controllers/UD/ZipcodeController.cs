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
    public class InstructorController : BaseController
    {
        public InstructorController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
        : base(_DBcontext, _OraTransMsgs)

        {
        }


        [HttpGet]
        [Route("GetInstructor")]
        public async Task<IActionResult> GetInstructor()
        {
            List<InstructorDTO> lst = await _context.Instructors
                .Select(sp => new InstructorDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = DateTime.Now,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    InstructorId = sp.InstructorId,
                    Phone = sp.Phone,
                    Salutation = sp.Salutation,
                    SchoolId = sp.SchoolId,
                    StreetAddress = sp.StreetAddress,
                    Zip = sp.Zip,


                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetInstructor/{_InstructorId}/{_SchoolId}")]
        public async Task<IActionResult> GetInstructor(int _InstructorId, int _SchoolId)
        {
            InstructorDTO? lst = await _context.Instructors
                .Where(x => x.InstructorId == _InstructorId)
                .Select(sp => new InstructorDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = DateTime.Now,
                    Zip = sp.Zip,
                    FirstName = sp.FirstName,
                    InstructorId = sp.InstructorId,
                    Phone = sp.Phone,
                    LastName = sp.LastName,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = DateTime.Now,
                    StreetAddress = sp.StreetAddress,
                    Salutation = sp.Salutation,
                    SchoolId = sp.SchoolId,

                }).FirstOrDefaultAsync();
            return Ok(lst);
        }


        [HttpPost]
        [Route("PostInstructor")]
        public async Task<IActionResult> PostInstructor([FromBody] InstructorDTO _InstructorDTO)
        {
            try
            {
                Instructor i = await _context.Instructors.Where(x => x.InstructorId == _InstructorDTO.InstructorId).FirstOrDefaultAsync();

                if (i == null)
                {
                    i = new Instructor
                    {
                        ModifiedDate = DateTime.Now,
                        Phone = _InstructorDTO.Phone,
                        Salutation = _InstructorDTO.Salutation,
                        InstructorId = _InstructorDTO.InstructorId,
                        FirstName = _InstructorDTO.FirstName,
                        LastName = _InstructorDTO.LastName,
                        CreatedBy = _InstructorDTO.CreatedBy,
                        CreatedDate = DateTime.Now,
                        SchoolId = _InstructorDTO.SchoolId,
                        ModifiedBy = _InstructorDTO.ModifiedBy,
                        StreetAddress = _InstructorDTO.StreetAddress,
                        Zip = _InstructorDTO.Zip,


                    };
                    _context.Instructors.Add(i);
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
        [Route("PutInstructor")]
        public async Task<IActionResult> PutInstructor([FromBody] InstructorDTO _InstructorDTO)
        {
            try
            {
                Instructor i = await _context.Instructors.Where(x => x.InstructorId == _InstructorDTO.InstructorId).FirstOrDefaultAsync();

                if (i != null)
                {
                    i.ModifiedDate = DateTime.Now;
                    i.ModifiedDate = DateTime.Now;
                    i.Phone = _InstructorDTO.Phone;
                    i.Salutation = _InstructorDTO.Salutation;
                    i.InstructorId = _InstructorDTO.InstructorId;
                    i.FirstName = _InstructorDTO.FirstName;
                    i.LastName = _InstructorDTO.LastName;
                    i.CreatedBy = _InstructorDTO.CreatedBy;
                    i.CreatedDate = DateTime.Now;
                    i.SchoolId = _InstructorDTO.SchoolId;
                    i.ModifiedBy = _InstructorDTO.ModifiedBy;
                    i.StreetAddress = _InstructorDTO.StreetAddress;
                    i.Zip = _InstructorDTO.Zip;

                    _context.Instructors.Update(i);
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
        [Route("DeleteInstructor/{_InstructorId}/{_SchoolId}")]
        public async Task<IActionResult> DeleteCourse(int _InstructorId)
        {
            try
            {
                Instructor i = await _context.Instructors.Where(x => x.InstructorId == _InstructorId).FirstOrDefaultAsync();

                if (i != null)
                {
                    _context.Instructors.Remove(i);
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

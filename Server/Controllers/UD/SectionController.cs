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
    public class SectionController : BaseController
    {
        public SectionController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)
        {
        }

        [HttpGet]
        [Route("GetSection")]
        public async Task<IActionResult> GetSection()
        {
            List<SectionDTO> lst = await _context.Sections
                .Select(sp => new SectionDTO
                {
                    SectionId = sp.SectionId,
                    SchoolId = sp.SchoolId,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    Capacity = sp.Capacity,
                    CourseNo = sp.CourseNo,
                    InstructorId = sp.InstructorId,
                    Location = sp.Location,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SectionNo = sp.SectionNo,
                    StartDateTime = sp.StartDateTime,



                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetSection/{_SectionId}/{_SchoolId}")]
        public async Task<IActionResult> GetSection(int _SectionId, int _SchoolId)
        {
            SectionDTO? lst = await _context.Sections
                .Where(x => x.SectionId == _SectionId)
                .Where(x => x.SchoolId == _SchoolId)
                .Select(sp => new SectionDTO
                {
                    SectionId = sp.SectionId,
                    SchoolId = sp.SchoolId,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    Capacity = sp.Capacity,
                    CourseNo = sp.CourseNo,
                    InstructorId = sp.InstructorId,
                    Location = sp.Location,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SectionNo = sp.SectionNo,
                    StartDateTime = sp.StartDateTime



                }).FirstOrDefaultAsync();
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostSection")]
        public async Task<IActionResult> PostSection([FromBody] SectionDTO _SectionDTO)
        {
            try
            {
                Section sc = await _context.Sections.Where(x => x.CourseNo == _SectionDTO.SectionId).FirstOrDefaultAsync();

                if (sc == null)
                {
                    sc = new Section
                    {
                        SectionId = _SectionDTO.SectionId,
                        CourseNo = _SectionDTO.CourseNo,
                        StartDateTime = DateTime.Now,
                        SectionNo = _SectionDTO.SectionNo,
                        ModifiedBy = _SectionDTO.ModifiedBy,
                        Location = _SectionDTO.Location,
                        InstructorId = _SectionDTO.InstructorId,
                        Capacity = _SectionDTO.Capacity,
                        CreatedBy = _SectionDTO.CreatedBy,
                        CreatedDate = _SectionDTO.CreatedDate,
                        ModifiedDate = _SectionDTO.ModifiedDate,

                    };
                    _context.Sections.Add(sc);
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
        [Route("PutSection")]
        public async Task<IActionResult> PutSection([FromBody] SectionDTO _SectionDTO)
        {
            try
            {
                Section sc = await _context.Sections.Where(x => x.SectionId == _SectionDTO.SectionId).FirstOrDefaultAsync();

                if (sc != null)
                {
                    sc.SectionId = _SectionDTO.SectionId;
                    sc.CourseNo = _SectionDTO.CourseNo;
                    sc.StartDateTime = DateTime.Now;
                    sc.SectionNo = _SectionDTO.SectionNo;
                    sc.ModifiedBy = _SectionDTO.ModifiedBy;
                    sc.Location = _SectionDTO.Location;
                    sc.InstructorId = _SectionDTO.InstructorId;
                    sc.Capacity = _SectionDTO.Capacity;
                    sc.CreatedBy = _SectionDTO.CreatedBy;
                    sc.CreatedDate = _SectionDTO.CreatedDate;
                    sc.ModifiedDate = _SectionDTO.ModifiedDate;



                    _context.Sections.Update(sc);
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
        [Route("DeleteSection/{_SectionId}/{_SchoolId}")]
        public async Task<IActionResult> DeleteSection(int _SectionId, int _SchoolId)
        {
            try
            {
                Section sc = await _context.Sections.Where(x => x.SectionId == _SectionId).FirstOrDefaultAsync();

                if (sc != null)
                {
                    _context.Sections.Remove(sc);
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

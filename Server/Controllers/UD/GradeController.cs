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
    public class GradeController : BaseController
    {
        public GradeController(DOOROracleContext DBcontext,
            OraTransMsgs _OraTransMsgs) :
            base(DBcontext, _OraTransMsgs)
        {
        }

        [HttpGet]
        [Route("GetGrade")]
        public async Task<IActionResult> GetGrade()
        {
            List<GradeDTO> lst = await _context.Grades
                .Select(sp => new GradeDTO
                {
                    SchoolId = sp.SchoolId,
                    StudentId = sp.StudentId,
                    SectionId = sp.SectionId,
                    GradeTypeCode = sp.GradeTypeCode,
                    GradeCodeOccurrence = sp.GradeCodeOccurrence,
                    NumericGrade = sp.NumericGrade,
                    Comments = sp.Comments,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,

                }).ToListAsync();
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetGrade/{_GradeCodeOccurrence}/{_GradeTypeCode}/{_SchoolId}/{_SectionId}/{_StudentId}")]
        public async Task<IActionResult> GetGrade(byte _GradeCodeOccurrence, string _GradeTypeCode, int _SchoolId, int _SectionId, int _StudentId)
        {
            GradeDTO? lst = await _context.Grades
                .Where(x => x.GradeCodeOccurrence == _GradeCodeOccurrence)
                .Where(x => x.GradeTypeCode == _GradeTypeCode)
                .Where(x => x.SchoolId == _SchoolId)
                .Where(x => x.SectionId == _SectionId)
                .Where(x => x.StudentId == _StudentId)
                .Select(sp => new GradeDTO
                {
                    SchoolId = sp.SchoolId,
                    StudentId = sp.StudentId,
                    SectionId = sp.SectionId,
                    GradeTypeCode = sp.GradeTypeCode,
                    GradeCodeOccurrence = sp.GradeCodeOccurrence,
                    NumericGrade = sp.NumericGrade,
                    Comments = sp.Comments,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostGrade")]
        public async Task<IActionResult> PostGrade([FromBody] GradeDTO _GradeDTO)
        {
            try
            {
                Grade? gc = await _context.Grades
                    .Where(x => x.GradeCodeOccurrence == _GradeDTO.GradeCodeOccurrence)
                    .Where(x => x.GradeTypeCode == _GradeDTO.GradeTypeCode)
                    .Where(x => x.SchoolId == _GradeDTO.SchoolId)
                    .Where(x => x.SectionId == _GradeDTO.SectionId)
                    .Where(x => x.StudentId == _GradeDTO.StudentId).FirstOrDefaultAsync();

                if (gc == null)
                {
                    gc = new Grade
                    {
                        SchoolId = _GradeDTO.SchoolId,
                        StudentId = _GradeDTO.StudentId,
                        SectionId = _GradeDTO.SectionId,
                        GradeTypeCode = _GradeDTO.GradeTypeCode,
                        GradeCodeOccurrence = _GradeDTO.GradeCodeOccurrence,
                        NumericGrade = _GradeDTO.NumericGrade,
                        Comments = _GradeDTO.Comments,
                    };
                    _context.Grades.Add(gc);
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
        [Route("PutGrade")]
        public async Task<IActionResult> PutGrade([FromBody] GradeDTO _GradeDTO)
        {
            try
            {
                Grade? gc = await _context.Grades
                    .Where(x => x.GradeCodeOccurrence == _GradeDTO.GradeCodeOccurrence)
                    .Where(x => x.GradeTypeCode == _GradeDTO.GradeTypeCode)
                    .Where(x => x.SchoolId == _GradeDTO.SchoolId)
                    .Where(x => x.SectionId == _GradeDTO.SectionId)
                    .Where(x => x.StudentId == _GradeDTO.StudentId).FirstOrDefaultAsync();

                if (gc != null)
                {
                    gc.SchoolId = _GradeDTO.SchoolId;
                    gc.StudentId = _GradeDTO.StudentId;
                    gc.SectionId = _GradeDTO.SectionId;
                    gc.GradeTypeCode = _GradeDTO.GradeTypeCode;
                    gc.GradeCodeOccurrence = _GradeDTO.GradeCodeOccurrence;
                    gc.NumericGrade = _GradeDTO.NumericGrade;
                    gc.Comments = _GradeDTO.Comments;

                    _context.Grades.Update(gc);
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
        [Route("DeleteGrade/{_GradeCodeOccurrence}/{_GradeTypeCode}/{_SchoolId}/{_SectionId}/{_StudentId}")]
        public async Task<IActionResult> DeleteGrade(byte _GradeCodeOccurrence, string _GradeTypeCode, int _SchoolId, int _SectionId, int _StudentId)
        {
            try
            {
                Grade? gc = await _context.Grades
                    .Where(x => x.GradeCodeOccurrence == _GradeCodeOccurrence)
                    .Where(x => x.GradeTypeCode == _GradeTypeCode)
                    .Where(x => x.SchoolId == _SchoolId)
                    .Where(x => x.SectionId == _SectionId)
                    .Where(x => x.StudentId == _StudentId).FirstOrDefaultAsync();


                if (gc != null)
                {
                    _context.Grades.Remove(gc);
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

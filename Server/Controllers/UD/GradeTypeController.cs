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
    public class GradeTypeController : BaseController
    {
        public GradeTypeController(DOOROracleContext DBcontext,
            OraTransMsgs _OraTransMsgs) :
            base(DBcontext, _OraTransMsgs)
        {
        }

        [HttpGet]
        [Route("GetGradeType")]
        public async Task<IActionResult> GetGradeType()
        {
            List<GradeTypeDTO> lst = await _context.GradeTypes
                .Select(sp => new GradeTypeDTO
                {
                    SchoolId = sp.SchoolId,
                    GradeTypeCode = sp.GradeTypeCode,
                    Description = sp.Description,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate
                }).ToListAsync();
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetGradeType/{_SchoolId}/{_GradeTypeCode}")]
        public async Task<IActionResult> GetGradeType(int _SchoolId, string _GradeTypeCode)
        {
            GradeTypeDTO? lst = await _context.GradeTypes
              .Where(x => x.SchoolId == _SchoolId)
              .Where(x => x.GradeTypeCode == _GradeTypeCode)
              .Select(sp => new GradeTypeDTO
              {
                  SchoolId = sp.SchoolId,
                  GradeTypeCode = sp.GradeTypeCode,
                  Description = sp.Description,
                  CreatedBy = sp.CreatedBy,
                  CreatedDate = sp.CreatedDate,
                  ModifiedBy = sp.ModifiedBy,
                  ModifiedDate = sp.ModifiedDate
              }).FirstOrDefaultAsync();
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostGradeType")]
        public async Task<IActionResult> PostGradeType([FromBody] GradeTypeDTO _GradeTypeDTO)
        {
            try
            {
                GradeType? g = await _context.GradeTypes
                     .Where(x => x.SchoolId == _GradeTypeDTO.SchoolId)
                     .Where(x => x.GradeTypeCode == _GradeTypeDTO.GradeTypeCode).FirstOrDefaultAsync();

                if (g == null)
                {
                    g = new GradeType
                    {
                        SchoolId = _GradeTypeDTO.SchoolId,
                        GradeTypeCode = _GradeTypeDTO.GradeTypeCode,
                        Description = _GradeTypeDTO.Description
                    };
                    _context.GradeTypes.Add(g);
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
        [Route("PutGradeType")]
        public async Task<IActionResult> PutGradeType([FromBody] GradeTypeDTO _GradeTypeDTO)
        {
            try
            {
                GradeType? g = await _context.GradeTypes
                    .Where(x => x.SchoolId == _GradeTypeDTO.SchoolId)
                    .Where(x => x.GradeTypeCode == _GradeTypeDTO.GradeTypeCode).FirstOrDefaultAsync();

                if (g != null)
                {
                    g.SchoolId = _GradeTypeDTO.SchoolId;
                    g.GradeTypeCode = _GradeTypeDTO.GradeTypeCode;
                    g.Description = _GradeTypeDTO.Description;

                    _context.GradeTypes.Update(g);
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
        [Route("DeleteGradeType/{_SchoolId}/{_GradeTypeCode}")]
        public async Task<IActionResult> DeleteGradeType(int _SchoolId, string _GradeTypeCode)
        {
            try
            {
                GradeType? g = await _context.GradeTypes
                .Where(x => x.SchoolId == _SchoolId)
                    .Where(x => x.GradeTypeCode == _GradeTypeCode).FirstOrDefaultAsync();


                if (g != null)
                {
                    _context.GradeTypes.Remove(g);
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
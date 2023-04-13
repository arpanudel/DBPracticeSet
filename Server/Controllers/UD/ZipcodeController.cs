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
    public class ZipController : BaseController
    {
        public ZipController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }


        [HttpGet]
        [Route("GetZipcode")]
        public async Task<IActionResult> GetZipcode()
        {
            List<ZipcodeDTO> lst = await _context.Zipcodes
                .Select(sp => new ZipcodeDTO
                {
                    City = sp.City,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    State = sp.State,
                    Zip = sp.Zip
                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetZipcode/{_Zip}")]
        public async Task<IActionResult> GetZipcode(string _Zip)
        {
            ZipcodeDTO? lst = await _context.Zipcodes
                .Where(x => x.Zip == _Zip)
                .Select(sp => new ZipcodeDTO
                {
                    City = sp.City,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    State = sp.State,
                    Zip = sp.Zip

                }).FirstOrDefaultAsync();
            return Ok(lst);
        }


        [HttpPost]
        [Route("PostZipcode")]
        public async Task<IActionResult> PostZipcode([FromBody] ZipcodeDTO _ZipcodeDTO)
        {
            try
            {
                Zipcode zip = await _context.Zipcodes.Where(x => x.Zip == _ZipcodeDTO.Zip).FirstOrDefaultAsync();

                if (zip == null)
                {
                    zip = new Zipcode
                    {
                        City = _ZipcodeDTO.City,
                        CreatedBy = _ZipcodeDTO.CreatedBy,
                        CreatedDate = _ZipcodeDTO.CreatedDate,
                        ModifiedBy = _ZipcodeDTO.ModifiedBy,
                        ModifiedDate = _ZipcodeDTO.ModifiedDate,
                        State = _ZipcodeDTO.State,
                        Zip = _ZipcodeDTO.Zip

                    };
                    _context.Zipcodes.Add(z);
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
        [Route("PutZipcode")]
        public async Task<IActionResult> PutZipcode([FromBody] ZipcodeDTO _ZipcodeDTO)
        {
            try
            {
                Zipcode zip = await _context.Zipcodes.Where(x => x.Zip == _ZipcodeDTO.Zip).FirstOrDefaultAsync();

                if (zip != null)
                {
                    zip.City = _ZipcodeDTO.City;
                    zip.CreatedBy = _ZipcodeDTO.CreatedBy;
                    zip.CreatedDate = _ZipcodeDTO.CreatedDate;
                    zip.ModifiedBy = _ZipcodeDTO.ModifiedBy;
                    zip.ModifiedDate = _ZipcodeDTO.ModifiedDate;
                    zip.State = _ZipcodeDTO.State;
                    zip.Zip = _ZipcodeDTO.Zip;

                    _context.Zipcodes.Update(z);
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
        [Route("DeleteZipcode/{_Zip}")]
        public async Task<IActionResult> DeleteCourse(string _Zip)
        {
            try
            {
                Zipcode zip = await _context.Zipcodes.Where(x => x.Zip == _Zip).FirstOrDefaultAsync();

                if (zip != null)
                {
                    _context.Zipcodes.Remove(zip);
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

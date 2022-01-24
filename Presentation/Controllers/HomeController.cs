using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessDapper;

namespace Presentation.Controllers
{
    public class HomeController : ControllerBase
    {
        public DapperAccess DapperAccess { get; }
        public HomeController(DapperAccess dapperAccess)
        {
            DapperAccess = dapperAccess;
        }
        [Route("GetInvestorById/{id}")]
        [HttpGet]
        public async Task<ActionResult<IList<Investor>>> GetInvestorById(int id)
        {
            var result =  await DapperAccess.ReadInvestorById(6);
            return Ok(result);
        }

        [Route("GetAllInvestor")]
        [HttpGet]
        public async Task<ActionResult<IList<Investor>>> GetAllInvestor()
        {
            var result = await DapperAccess.ReadAllInvestor();
            return Ok(result);
        }

        [Route("AddInvestor")]
        [HttpPost]
        public async Task<ActionResult<int>> AddInvestor([FromBody] Investor investor)
        {
            var result = await DapperAccess.AddInvestor(investor.LegacyCode.Trim());
            return Ok(result);
        }

        [Route("RemoveInvestorById/{id}")]
        [HttpDelete]
        public async Task<IActionResult> RemoveInvestorById(int id)
        {
            var result = await DapperAccess.DeleteInvestor(id);

            return Ok(result);
        }
    }
}

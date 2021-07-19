/*
 * Files generated by SmartTools.Net Codeless
 * $GenDate$
 *
 */

using $Rootnamespace$.Data;
using $Rootnamespace$.Models;
using $Rootnamespace$.Services;
using Longnows.Saas.Framework.Mvc.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace $Rootnamespace$.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class $ModelName$Controller : BaseController
    {

        #region initialize
        public $ModelName$Service _service { get; set; }
        public $ModelName$Controller($ModelName$Service service)
        {
            _service = service;
        }
        #endregion

        #region get all data
        // GET: api/<CurrController>
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Get([FromBody]$ModelName$SearchDto searchDto)
        {
            return Json(_service.GetAll(searchDto));
        }
        #endregion
    }
}
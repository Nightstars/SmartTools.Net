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
using Longnows.Saas.Framework.Common.Entity;
using Longnows.Saas.Framework.Common.Attributes;
using Longnows.Saas.Framework.Log.Model;
using System.Threading.Tasks;
using System.Data;
using Longnows.Saas.Framework.Common.Excel;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace $Rootnamespace$.Controllers
{
    [Authorize]
    [Area("$area$")]
    public class $ModelName$Controller : SimpleCrudController<$ModelName$Service, $ModelName$, $ModelName$SearchDto>
    {
        #region initialize
        private readonly string moduleName = "$area$-";

        public $ModelName$Controller($ModelName$Service service)
            : base(service)
        {

        }
        #endregion

        #region Search
        public override ActionResult Search($ModelName$SearchDto searchModel)
        {
            searchModel.Site = _appUser.CurrentCorpInfo.Site;
            PageData<$ModelName$> pageData = GetLayUIPageInfo<$ModelName$>(" CREATE_DATE ", OrderBy.DESC);
            var result = Service.GetExecutePage(pageData, searchModel);
            InfoLogger(moduleName + "$title$", "query", "query successfully", LogType.BusinessOperations);
            return Json(result);
        }
        #endregion

        #region ExportToExcelByIds
        /// <summary>
        /// ExportToExcelByIds
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        [AccessLogAttribute("ExportToExcelByIds")]
        public async Task<ActionResult> ExportToExcelByIds($ModelName$SearchDto searchModel)
        {
            IDataReader dr = Service.GetDataReaderByIds(searchModel.keys, _appUser.CurrentCorpInfo.Site);
            var result = await ExcelHelper.ExportToExcel($"{WebSettings.TempDirectory}{searchModel.ExportFileName}{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx", dr, WebSettings.ConfigFileDirectory + searchModel.ExportXmlName, searchModel.ExportXmlNode, false);
            return Json(result);
        }
        #endregion
    }
}

/*
 * Files generated by SmartTools.Net Codeless
 * $GenDate$
 *
 */

using System;
using System.ComponentModel.DataAnnotations;
using Longnows.Saas.Framework.Common.Base;
using Longnows.Saas.Framework.Common.Entity;

namespace $Rootnamespace$
{
    public class $ModelName$SearchDto : BaseSearchDto
    {
        $EntityField$

        public $ModelName$SearchDto()
        {
            this.OrderBy = OrderBy.DESC;;
            this.Sorting = "CREATE_DATE";
        }
    }
}

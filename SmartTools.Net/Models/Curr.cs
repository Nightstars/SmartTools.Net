using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Net.Models
{
    class Curr
    {
        /// <summary>
        /// 币制代码
        /// </summary>
        [Display(Name = "币制代码")]

        public string CURR_CODE { get; set; }

        /// <summary>
        /// 币制名称
        /// </summary>
        [Display(Name = "币制名称")]
        public string CURR_NAME { get; set; }

    }
}

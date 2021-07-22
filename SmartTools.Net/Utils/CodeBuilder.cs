using SmartTools.Net.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Net.Utils
{
    public class CodeBuilder
    {
		#region initialize
		/// <summary>
		/// 字段信息
		/// </summary>
		private List<DbTableInfo> _list;
		/// <summary>
		/// 命名空间
		/// </summary>
		private string _rootnamespace;
		/// <summary>
		/// 模块名称
		/// </summary>
		private string _modelName;
		/// <summary>
		/// 选中的数据库
		/// </summary>
		private string _database;
		/// <summary>
		/// 选中的表
		/// </summary>
		private string _tbname;
		/// <summary>
		/// 生成路径
		/// </summary>
		private string _buildpath;
		/// <summary>
		/// 项目类型
		/// </summary>
		private string _projtype;
		/// <summary>
		/// 输出类型
		/// </summary>
		private string _outputtype;
		public CodeBuilder(List<DbTableInfo> ls, string rootnamespace, string modelName,string database,string tbname,string buildpath,string projtype,string outputtype)
        {
			_list = ls;
			_rootnamespace = rootnamespace;
			_modelName = modelName;
			_database = database;
            if (tbname.Contains("_"))
            {
				_tbname = tbname.Replace("_","");
            }
            else
            {
				_tbname = tbname;
            }
			_buildpath = buildpath;
			_projtype = projtype;
			_outputtype = outputtype;
        }
        #endregion

        #region build model for api_wechat
        /// <summary>
        /// BuildModel
        /// </summary>
        /// <param name="ls"></param>
        /// <param name="rootnamespace"></param>
        /// <param name="modelName"></param>
        public CodeBuilder BuildModel()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DbTableInfo item in _list)
			{
				if (!FiledUtil.modelSkipFileds.Contains(item.columnName))
				{
					item.columnDescription = item.columnDescription ?? item.columnName;
					if (stringBuilder.Length == 0)
					{
						stringBuilder.AppendLine("/// <summary>");
					}
					else
					{
						stringBuilder.AppendLine("        /// <summary>");
					}
					stringBuilder.AppendLine($"        /// {item.columnDescription}");
					stringBuilder.AppendLine("        /// </summary>");
					stringBuilder.AppendLine($"        [Display(Name = \"{item.columnDescription}\")]");
					//stringBuilder.AppendLine("        [TableColumn(\"" + item.columnName + "\")]");
					stringBuilder.AppendLine(string.Concat(new string[]
					{
						"        public ",
						FiledUtil.GetFiledType(item.type),
						" ",
						item.columnName,
						" { get; set; }"
					}));
					stringBuilder.AppendLine("");
				}
			}
			string text = FileUtil.GetTplContent("Tpl.wechatapi.SmartTools.Net.model.tpl");
			text = text.Replace("$Rootnamespace$", $"{_rootnamespace}.Models");
			text = text.Replace("$ModelName$", _modelName);
			text = text.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			text = text.Replace("$EntityField$", stringBuilder.ToString());
			if(_outputtype == "项目")
            {
				if (!Directory.Exists($"{_buildpath}/Models"))
					Directory.CreateDirectory($"{_buildpath}/Models");
				File.WriteAllText($"{_buildpath}/Models/{_modelName}.cs", text, Encoding.UTF8);
			}
            else
            {
				if (!Directory.Exists("./Oupput"))
					Directory.CreateDirectory("./Oupput");
				File.WriteAllText($"./Oupput/{_modelName}.cs", text, Encoding.UTF8);
			}

			return this;
		}
		#endregion

		#region build Searchmodel for api_wechat
		/// <summary>
		/// BuildSearchModel
		/// </summary>
		/// <param name="ls"></param>
		/// <param name="rootnamespace"></param>
		/// <param name="modelName"></param>
		public CodeBuilder BuildSearchModel()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DbTableInfo item in _list)
			{
				if (!FiledUtil.modelSkipFileds.Contains(item.columnName))
				{
					item.columnDescription = item.columnDescription ?? item.columnName;
					if (stringBuilder.Length == 0)
					{
						stringBuilder.AppendLine("/// <summary>");
					}
					else
					{
						stringBuilder.AppendLine("        /// <summary>");
					}
					stringBuilder.AppendLine($"        /// {item.columnDescription}");
					stringBuilder.AppendLine("        /// </summary>");
					stringBuilder.AppendLine($"        [Display(Name = \"{item.columnDescription}\")]");
					//stringBuilder.AppendLine("        [TableColumn(\"" + item.columnName + "\")]");
					stringBuilder.AppendLine(string.Concat(new string[]
					{
						"        public ",
						FiledUtil.GetFiledType(item.type),
						" ",
						item.columnName,
						" { get; set; }"
					}));
					stringBuilder.AppendLine("");
				}
			}
			string text = FileUtil.GetTplContent("Tpl.wechatapi.SmartTools.Net.searchmodel.tpl");
			text = text.Replace("$Rootnamespace$", $"{_rootnamespace}.Data");
			text = text.Replace("$ModelName$", _modelName);
			text = text.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			text = text.Replace("$EntityField$", stringBuilder.ToString());
			if (_outputtype == "项目")
			{
				if (!Directory.Exists($"{_buildpath}/Data"))
					Directory.CreateDirectory($"{_buildpath}/Data");
				File.WriteAllText($"{_buildpath}/Data/{_modelName}SearchDto.cs", text, Encoding.UTF8);
			}
			else
			{
				if (!Directory.Exists("./Oupput"))
					Directory.CreateDirectory("./Oupput");
				File.WriteAllText($"./Oupput/{_modelName}SearchDto.cs", text, Encoding.UTF8);
			}

			return this;
		}
		#endregion

		#region build service for api_wechat
		/// <summary>
		/// BuildService
		/// </summary>
		/// <param name="ls"></param>
		/// <param name="rootnamespace"></param>
		/// <param name="modelName"></param>
		public CodeBuilder BuildService()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DbTableInfo item in _list)
			{
				if (!FiledUtil.modelSkipFileds.Contains(item.columnName))
				{
					if (stringBuilder.Length == 0)
					{
						stringBuilder.AppendLine($".WhereIF(!string.IsNullOrWhiteSpace(searchDto.{item.columnName}),x=>x.{item.columnName}.Contains(searchDto.{item.columnName}))");
					}
                    else
                    {
						stringBuilder.AppendLine($"				.WhereIF(!string.IsNullOrWhiteSpace(searchDto.{item.columnName}),x=>x.{item.columnName}.Contains(searchDto.{item.columnName}))");
					}
				}
			}
			string text = FileUtil.GetTplContent("Tpl.wechatapi.SmartTools.Net.service.tpl");
			text = text.Replace("$Rootnamespace$", _rootnamespace);
			text = text.Replace("$ModelName$", _modelName);
			text = text.Replace("$Dadabase$", _database);
			text = text.Replace("$Dbtable$", _tbname);
			text = text.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			text = text.Replace("$SearchFiled$", stringBuilder.ToString());
			if (_outputtype == "项目")
			{
				if (!Directory.Exists($"{_buildpath}/Services"))
					Directory.CreateDirectory($"{_buildpath}/Services");
				File.WriteAllText($"{_buildpath}/Services/{_modelName}Service.cs", text, Encoding.UTF8);
			}
			else
			{
				if (!Directory.Exists("./Oupput"))
					Directory.CreateDirectory("./Oupput");
				File.WriteAllText($"./Oupput/{_modelName}Service.cs", text, Encoding.UTF8);
			}

			return this;
		}
		#endregion

		#region build controller for api_wechat
		/// <summary>
		/// BuildController
		/// </summary>
		public CodeBuilder BuildController()
		{
			string text = FileUtil.GetTplContent("Tpl.wechatapi.SmartTools.Net.controller.tpl");
			text = text.Replace("$Rootnamespace$", _rootnamespace);
			text = text.Replace("$ModelName$", _modelName);
			text = text.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			if (_outputtype == "项目")
			{
				if (!Directory.Exists($"{_buildpath}/Controllers"))
					Directory.CreateDirectory($"{_buildpath}/Controllers");
				File.WriteAllText($"{_buildpath}/Controllers/{_modelName}Controller.cs", text, Encoding.UTF8);
			}
			else
			{
				if (!Directory.Exists("./Oupput"))
					Directory.CreateDirectory("./Oupput");
				File.WriteAllText($"./Oupput/{_modelName}Controller.cs", text, Encoding.UTF8);
			}

			return this;
		}
		#endregion
	}
}

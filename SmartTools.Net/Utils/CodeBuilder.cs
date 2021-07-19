using SmartTools_CS.Models;
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
		private List<DbTableInfo> _list;
		private string _rootnamespace;
		private string _modelName;
		private string _database;
		private string _tbname;
		public CodeBuilder(List<DbTableInfo> ls, string rootnamespace, string modelName,string database,string tbname)
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
			text = text.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-ss HH:mm:ss"));
			text = text.Replace("$EntityField$", stringBuilder.ToString());
			if (!Directory.Exists("./Oupput"))
				Directory.CreateDirectory("./Oupput");
			File.WriteAllText($"./Oupput/{_modelName}.cs", text, Encoding.UTF8);

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
			text = text.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-ss HH:mm:ss"));
			text = text.Replace("$EntityField$", stringBuilder.ToString());
			if (!Directory.Exists("./Oupput"))
				Directory.CreateDirectory("./Oupput");
			File.WriteAllText($"./Oupput/{_modelName}SearchDto.cs", text, Encoding.UTF8);

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
			text = text.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-ss HH:mm:ss"));
			text = text.Replace("$SearchFiled$", stringBuilder.ToString());
			if (!Directory.Exists("./Oupput"))
				Directory.CreateDirectory("./Oupput");
			File.WriteAllText($"./Oupput/{_modelName}Service.cs", text, Encoding.UTF8);

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
			text = text.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-ss HH:mm:ss"));
			if (!Directory.Exists("./Oupput"))
				Directory.CreateDirectory("./Oupput");
			File.WriteAllText($"./Oupput/{_modelName}Controller.cs", text, Encoding.UTF8);

			return this;
		}
		#endregion
	}
}

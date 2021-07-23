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

		/// <summary>
		/// 业务主键
		/// </summary>
		private string _primarykey;

		/// <summary>
		/// 查询字段
		/// </summary>
		private List<DbTableInfo> _searchlist;

		/// <summary>
		/// 数据库表
		/// </summary>
		private string _dbtable;
		public CodeBuilder(List<DbTableInfo> ls, string rootnamespace, string modelName,string database,string tbname,string buildpath,string projtype,string outputtype,string primarykey, List<DbTableInfo> searchls)
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
			_dbtable = tbname;
			_buildpath = buildpath;
			_projtype = projtype;
			_outputtype = outputtype;
			_primarykey = primarykey;
			_searchlist = searchls;
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
					stringBuilder.AppendLine($"        /// {item.columnDescription}")
					.AppendLine("        /// </summary>")
					.AppendLine($"        [Display(Name = \"{item.columnDescription}\")]")
					//stringBuilder.AppendLine("        [TableColumn(\"" + item.columnName + "\")]");
					.AppendLine(string.Concat(new string[]
					{
						"        public ",
						FiledUtil.GetFiledType(item.type),
						" ",
						item.columnName,
						" { get; set; }"
					}))
					.AppendLine("");
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
			var list=new List<DbTableInfo>();
            if (_searchlist.Any())
            {
				list=_list.Intersect(_searchlist).ToList();
            }
            else
            {
				list = _list;
            }
			if(!list.Exists(x => x.columnName == _primarykey))
            {
				list.Add(_list.Find(x=> x.columnName == _primarykey));
            }
			foreach (DbTableInfo item in list)
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
					stringBuilder.AppendLine($"        /// {item.columnDescription}")
					.AppendLine("        /// </summary>")
					.AppendLine($"        [Display(Name = \"{item.columnDescription}\")]")
					//stringBuilder.AppendLine("        [TableColumn(\"" + item.columnName + "\")]");
					.AppendLine(string.Concat(new string[]
					{
						"        public ",
						FiledUtil.GetFiledType(item.type),
						" ",
						item.columnName,
						" { get; set; }"
					}))
					.AppendLine("");
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
			var list = new List<DbTableInfo>();
			if (_searchlist.Any())
			{
				list = _list.Intersect(_searchlist).ToList();
			}
			else
			{
				list = _list;
			}
			foreach (DbTableInfo item in list)
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
			text = text.Replace("$PK$", _primarykey);
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
			var pktype=_list.Find(x => x.columnName == _primarykey).type;
			text = text.Replace("$Rootnamespace$", _rootnamespace);
			text = text.Replace("$ModelName$", _modelName);
			text = text.Replace("$PK$", _primarykey);
			text = text.Replace("$PKType$", pktype);
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

		#region build Db info for api_wechat
		/// <summary>
		/// BuildDbinfo
		/// </summary>
		/// <returns></returns>
		public CodeBuilder BuildDbinfo()
		{
			var arr = _buildpath.Split('/');
			string path = $"{ string.Join("/", arr.Take(arr.Length-2)) }/Models/CoreDbTables.cs";
			StringBuilder stringBuilder = new StringBuilder()
			.AppendLine("#region Tables")
			.AppendLine("		/// <summary>")
			.AppendLine($"		/// {_tbname}")
			.AppendLine("		/// </summary>")
			.AppendLine($"		public const string {_tbname} = \"{_dbtable}\";")
			.AppendLine("");
			string text = null;
			using FileStream filestream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
			using StreamReader streamReader = new StreamReader(filestream);
			text = streamReader.ReadToEnd();
			text = text.Replace("#region Tables", stringBuilder.ToString());
			var data=Encoding.UTF8.GetBytes(text);
			filestream.Seek(0, SeekOrigin.Begin);
			filestream.Write(data,0, data.Length);
			return this;
		}
		#endregion
	}
}

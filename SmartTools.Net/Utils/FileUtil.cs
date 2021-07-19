using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Net.Utils
{
    public static class FileUtil
    {
        #region initialize
        public static string ResourcesPath = $"SmartTools.Net.Resources.";
		#endregion

		#region GetTmpContent
		/// <summary>
		/// GetTplContent
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static string GetTplContent(string path)
		{
			string result;
			//using StreamReader streamReader = new StreamReader(path);
			//result = streamReader.ReadToEnd();

			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{ResourcesPath}{path}"))
			{
				using (StreamReader streamReader = new StreamReader(manifestResourceStream))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}
		#endregion
	}
}

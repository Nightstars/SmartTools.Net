using SmartTools_CS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartTools_CS.ViewModel
{
    public class CodeLessVM : INotifyPropertyChanged
    {
        #region initiazlize
        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify([CallerMemberName] string obj = "")
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(obj));
            }
        }
        #endregion

        #region 数据库列表
        /// <summary>
        /// 数据库列表
        /// </summary>
        public List<DataBaseInfo> databaselist { get; set; }
        #endregion

        #region 数据库连接字符串
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        //public String connectString= "Database=CMS;Server=127.0.0.1,6666;User ID = sa; Password = Ihavenoidea@0;";
        public String connectString= "Database=CMS;Server=192.168.0.151,61440;User ID = sa; Password = Dbwork2021;";

        public string ConnectionString
        {
            get{ return connectString; }
            set{ connectString = value; Notify(); }
        }
        #endregion

        #region 数据库
        /// <summary>
        /// 数据库
        /// </summary>
        public String database = "";

        public string Database
        {
            get { return database; }
            set { database = value; Notify(); }
        }
        #endregion

        #region 数据库
        /// <summary>
        /// 数据库
        /// </summary>
        public String _rootnamespace = "";

        public string Rootnamespace
        {
            get { return _rootnamespace; }
            set { _rootnamespace = value; Notify(); }
        }
        #endregion

        #region 数据库表
        /// <summary>
        /// 数据库表
        /// </summary>
        public String dbTable = "";

        public string DbTable
        {
            get { return dbTable; }
            set { dbTable = value; Notify(); }
        }
        #endregion

    }
}

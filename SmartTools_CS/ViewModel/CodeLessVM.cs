using SmartTools_CS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SmartTools_CS.ViewModel
{
    public class CodeLessVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
        public String _connectString= "Database=CMS;Server=192.168.0.151,61440;User ID = sa; Password = Dbwork2021;";

        public string ConnectionString
        {
            get
            {
                return _connectString;
            }
            set
            {
                _connectString = value;
                if(PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ConnectionString"));
                }
            }
        }
        #endregion

        #region 数据库
        /// <summary>
        /// 数据库
        /// </summary>
        public String _database = "";

        public string Database
        {
            get
            {
                return _database;
            }
            set
            {
                _database = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Database"));
                }
            }
        }
        #endregion

        #region 数据库表
        /// <summary>
        /// 数据库表
        /// </summary>
        public String _dbTable = "";

        public string DbTable
        {
            get
            {
                return _dbTable;
            }
            set
            {
                _dbTable = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DbTable"));
                }
            }
        }
        #endregion

    }
}

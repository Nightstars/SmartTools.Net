using AduSkin.Controls.Metro;
using HandyControl.Controls;
using SmartTools.Net.Services;
using SmartTools_CS.Db;
using SmartTools_CS.Models;
using SmartTools_CS.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SmartTools_CS.Views
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class CodeLessControl : UserControl
    {
        #region initialize
        CodeLessVM codeLessVM = new CodeLessVM();
        private string _currentLan = string.Empty;
        public CodeLessControl()
        {
            InitializeComponent();
            _currentLan = "zh-cn";
        }
        #endregion

        #region UserControl_Loaded
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = codeLessVM;
        }
        #endregion

        #region connect database
        private void connect_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(codeLessVM.connectString))
            {
                HandyControl.Controls.MessageBox.Error("连接字符串不能为空!");
                return;
            }

            try
            {
                codeLessVM.databaselist= new CodeLessService(codeLessVM.connectString).GetDatabase();

                databases.ItemsSource = codeLessVM.databaselist;
                databases.SelectedValuePath = "name";
                databases.DisplayMemberPath = "name";
                HandyControl.Controls.MessageBox.Success("数据库连接成功");
            }
            catch (Exception ex)
            {
                HandyControl.Controls.MessageBox.Error(ex.Message);
            }
        }
        #endregion

        #region databases_Selected
        private void databases_Selected(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(codeLessVM.connectString))
            {
                HandyControl.Controls.MessageBox.Error("连接字符串不能为空!");
                return;
            }

            try
            {
                if (!string.IsNullOrWhiteSpace(codeLessVM.database))
                {
                    dbtables.ItemsSource = new CodeLessService(codeLessVM.connectString).GetDbTable(codeLessVM.database);
                    dbtables.SelectedValuePath = "name";
                    dbtables.DisplayMemberPath = "name";
                }
                else
                {
                    dbtables.SelectedIndex = -1;
                    dbtables.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                HandyControl.Controls.MessageBox.Error(ex.Message);
            }
        }
        #endregion

        #region GenCode_Click
        private void GenCode_Click(object sender, RoutedEventArgs e)
        {
            //using var Db = DbUtil.GetInstance(codeLessVM._connectString);
            //string sql = $@"use {codeLessVM._database}
            //                select
            //                A.name as tableName, --表名
            //                B.name as columnName, --列名
            //                C.value as columnDescription, --列名备注
            //                T.type,--类型
            //                T.length--长度
            //                from sys.tables A
            //                inner join sys.columns B on B.object_id = A.object_id
            //                left join sys.extended_properties C on C.major_id = B.object_id and C.minor_id = B.column_id
            //                inner join (SELECT syscolumns.name AS name,systypes.name AS type,syscolumns.length AS length 
            //                 FROM syscolumns INNER JOIN systypes ON systypes.xtype=syscolumns.xtype
            //                 WHERE id=(SELECT id FROM sysobjects WHERE  name='{codeLessVM._dbTable}' and systypes.name<> 'sysname')
            //                )T on T.name=B.name
            //                where A.name = '{codeLessVM._dbTable}' order by b.name ";
            //var t = Db.Ado.SqlQuery<DbTableInfo>(sql);

            //ResourceDictionary dict = new ResourceDictionary();
            //if (_currentLan == "zh-cn")
            //{
            //    dict.Source = new Uri(@"Resources\Language\en-us.xaml", UriKind.Relative);
            //    _currentLan = "en-us";
            //}
            //else
            //{
            //    dict.Source = new Uri(@"Resources\Language\zh-cn.xaml", UriKind.Relative);
            //    _currentLan = "zh-cn";
            //}
            //Application.Current.Resources.MergedDictionaries[1] = dict;
            codeLessVM.Database = "";
        }
        #endregion

    }
}

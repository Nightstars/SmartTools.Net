using SmartTools_CS.Db;
using SmartTools_CS.Models;
using SmartTools_CS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartTools_CS.Views
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class CodeLessControl : UserControl
    {
        #region initialize
        CodeLessVM codeLessVM = new CodeLessVM();
        public CodeLessControl()
        {
            InitializeComponent();
        }
        #endregion

        #region UserControl_Loaded
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Container.DataContext = codeLessVM;
        }
        #endregion

        #region Test ConnectString
        private void testConnect_Click(object sender, RoutedEventArgs e)
        {
            if (codeLessVM._connectString == null|| codeLessVM._connectString == "") MessageBox.Show("连接字符串不能为空");
            try
            {
                using var Db=DbUtil.GetInstance(codeLessVM._connectString);
                codeLessVM.databaselist= Db.Queryable<DataBaseInfo>().AS("MASTER.dbo.SYSDATABASES").Where(x=>x.dbid>4).OrderBy(x=>x.name).ToList();
                databases.ItemsSource = codeLessVM.databaselist;
                databases.SelectedValuePath = "name";
                databases.DisplayMemberPath = "name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region databases_Selected
        private void databases_Selected(object sender, RoutedEventArgs e)
        {
            if (codeLessVM._connectString == null || codeLessVM._connectString == "") MessageBox.Show("连接字符串不能为空");
            try
            {
                using var Db = DbUtil.GetInstance(codeLessVM._connectString);
                var dbtableslist = Db.Queryable<DbTable>().AS($"{codeLessVM._database}.dbo.SysObjects").Where(x => x.xtype == "U").OrderBy(x => x.name).ToList();
                dbtables.ItemsSource = dbtableslist;
                dbtables.SelectedValuePath = "name";
                dbtables.DisplayMemberPath = "name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region GenCode_Click
        private void GenCode_Click(object sender, RoutedEventArgs e)
        {
            using var Db = DbUtil.GetInstance(codeLessVM._connectString);
            string sql = $@"use {codeLessVM._database}
                            select
                            A.name as tableName, --表名
                            B.name as columnName, --列名
                            C.value as columnDescription, --列名备注
                            T.type,--类型
                            T.length--长度
                            from sys.tables A
                            inner join sys.columns B on B.object_id = A.object_id
                            left join sys.extended_properties C on C.major_id = B.object_id and C.minor_id = B.column_id
                            inner join (SELECT syscolumns.name AS name,systypes.name AS type,syscolumns.length AS length 
                             FROM syscolumns INNER JOIN systypes ON systypes.xtype=syscolumns.xtype
                             WHERE id=(SELECT id FROM sysobjects WHERE  name='{codeLessVM._dbTable}' and systypes.name<> 'sysname')
                            )T on T.name=B.name
                            where A.name = '{codeLessVM._dbTable}' order by b.name ";
            var t = Db.Ado.SqlQuery<DbTableInfo>(sql);
        }
        #endregion
    }
}

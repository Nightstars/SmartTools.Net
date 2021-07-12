using SmartTools_CS.Db;
using SmartTools_CS.Models;
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
    public partial class UserControl1 : UserControl
    {
        List<DataBaseInfo> databaselist = new List<DataBaseInfo>();
        DbUtil dbUtil = new DbUtil();
        public UserControl1()
        {
            InitializeComponent();
        }

        #region Test ConnectString

        #endregion
        private void testConnect_Click(object sender, RoutedEventArgs e)
        {
            if (connectString.Text == null|| connectString.Text == "") MessageBox.Show("连接字符串不能为空");
            try
            {
                var Db=dbUtil.GetInstance(connectString.Text);
                databaselist=Db.Queryable<DataBaseInfo>().AS("MASTER.dbo.SYSDATABASES").Where(x=>x.dbid>4).ToList();
                databases.ItemsSource=databaselist;
                databases.SelectedValuePath = "name";
                databases.DisplayMemberPath = "name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void databases_Selected(object sender, RoutedEventArgs e)
        {
            if (connectString.Text == null || connectString.Text == "") MessageBox.Show("连接字符串不能为空");
            try
            {
                var Db = dbUtil.GetInstance(connectString.Text);
                var txt = databases.SelectedValue;
                var dbtableslist = Db.Queryable<DbTable>().AS($"{txt}.dbo.SysObjects").Where(x => x.xtype == "U").ToList();
                dbtables.ItemsSource = dbtableslist;
                dbtables.SelectedValuePath = "name";
                dbtables.DisplayMemberPath = "name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

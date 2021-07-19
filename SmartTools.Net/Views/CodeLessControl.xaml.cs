using AduSkin.Controls.Metro;
using HandyControl.Controls;
using SmartTools.Net.CustomControls;
using SmartTools.Net.Services;
using SmartTools.Net.Utils;
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
            new ControlUtil().HideBoundingBox(this);
        }
        #endregion

        #region connect database
        private void connect_Click(object sender, RoutedEventArgs e)
        {
            var logading = Dialog.Show(new Loading());
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
                //HandyControl.Controls.MessageBox.Success("数据库连接成功");

            }
            catch (Exception ex)
            {
                HandyControl.Controls.MessageBox.Error(ex.Message);
            }
            logading.Close();
        }
        #endregion

        #region databases_Selected
        private void databases_Selected(object sender, RoutedEventArgs e)
        {
            var logading = Dialog.Show(new Loading());
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
            logading.Close();
        }
        #endregion

        #region GenCode_Click
        private void GenCode_Click(object sender, RoutedEventArgs e)
        {
            var logading = Dialog.Show(new Loading());

            if (string.IsNullOrWhiteSpace(codeLessVM.connectString))
            {
                HandyControl.Controls.MessageBox.Error("连接字符串不能为空!");
                logading.Close();
                return;
            }

            if (string.IsNullOrWhiteSpace(codeLessVM.database))
            {
                HandyControl.Controls.MessageBox.Error("请选择数据库!");
                logading.Close();
                return;
            }

            if (string.IsNullOrWhiteSpace(codeLessVM.dbTable))
            {
                HandyControl.Controls.MessageBox.Error("请选择数据库表!");
                logading.Close();
                return;
            }

            if (string.IsNullOrWhiteSpace(codeLessVM._rootnamespace))
            {
                HandyControl.Controls.MessageBox.Error("请填写命名空间!");
                logading.Close();
                return;
            }

            try
            {
                var tbinfo = new CodeLessService(codeLessVM.connectString).GetDbTableInfo(codeLessVM.database,codeLessVM.dbTable);
                new CodeBuilder(tbinfo,
                    codeLessVM._rootnamespace,
                    $"Wechat{FiledUtil.GetModelName(codeLessVM.dbTable)}",
                    codeLessVM.database,
                    codeLessVM.dbTable)
                .BuildModel()
                .BuildSearchModel()
                .BuildService()
                .BuildController();
            }
            catch (Exception ex)
            {
                HandyControl.Controls.MessageBox.Error(ex.Message);
            }
            logading.Close();
        }
        #endregion

    }
}

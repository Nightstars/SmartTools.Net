using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SmartTools.Net.Views.mine;
using SmartTools.Net.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartTools.Net.ViewModel
{
   public class MainViewModel : ViewModelBase
   {
      public MainViewModel()
      {

      }


      private int _SelectedModularIndex;
      /// <summary>
      /// 属性.
      /// </summary>
      public int SelectedModularIndex
      {
         get { return _SelectedModularIndex; }
         set {
            Set(ref _SelectedModularIndex, value);
            if (value == 2)
               MainBackground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            else if (value == 3)
               MainBackground = new SolidColorBrush(Color.FromRgb(250, 251, 252));
            else
               MainBackground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
         }
      }

      private SolidColorBrush _MainBackground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
      /// <summary>
      /// 属性.
      /// </summary>
      public SolidColorBrush MainBackground
      {
         get { return _MainBackground; }
         set { Set(ref _MainBackground, value); }
      }
      /// <summary>
      /// 命令Command
      /// </summary>
      public ICommand OpenClick => new RelayCommand<string>((e) =>
      {
          switch (e)
          {
              case "AduSkinDemo":
                  //new UserControl1().Show();
                  return;
              default:
                  break;
          }
      });

        /// <summary>
        /// Mine
        /// </summary>
        private MineControl _CommonControlCase = new MineControl();
        public MineControl CommonControlCase
        {
            get { return _CommonControlCase; }
            set { Set(ref _CommonControlCase, value); }
        }
        /// <summary>
        /// CodeLess
        /// </summary>
        private UserControl _PracticalCase = new CodeLessControl();
        public UserControl PracticalCase
        {
            get { return _PracticalCase; }
            set { Set(ref _PracticalCase, value); }
        }
        /// <summary>
        /// 关于
        /// </summary>
        private UserControl _AduSkinAbout = new CodeLessControl();
        public UserControl AduSkinAbout
        {
            get { return _AduSkinAbout; }
            set { Set(ref _AduSkinAbout, value); }
        }
        /// <summary>
        /// 支持与赞助
        /// </summary>
        private UserControl _AduSkinSupport = new CodeLessControl();
        public UserControl AduSkinSupport
        {
            get { return _AduSkinSupport; }
            set { Set(ref _AduSkinSupport, value); }
        }

    }
}

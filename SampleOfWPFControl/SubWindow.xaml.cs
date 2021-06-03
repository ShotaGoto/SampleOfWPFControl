using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SampleOfWPFControl
{
    /// <summary>
    /// SubWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SubWindow : Window
    {
        private MainWindow MainWin { get; set; }

        public SubWindow(MainWindow main)
        {
            InitializeComponent();

            MainWin = main;

            DG4.DataContext = MainWin.DG4Data;
        }

        private void DG4_MouseMove(object sender, MouseEventArgs e)
        {
            //マウスクリック時以外は無視する
            if (e.LeftButton != MouseButtonState.Pressed) return;

            //クリックしたコントロールがDataGridで無い時は無視
            if (!(sender is DataGrid DG)) return;
            //行を選択していない時は無視
            if (DG.SelectedItems.Count == 0) return;

            DragDrop.DoDragDrop(DG, DG.SelectedItems[0], DragDropEffects.Move);
        }

        private void DG4_Drop(object sender, DragEventArgs e)
        {
            Customer _Customer = (Customer)e.Data.GetData(typeof(Customer));
            if (_Customer == null) return;

            //既に入っていたら無視
            if (MainWin.DG4Data.Where(x => x.ID == _Customer.ID).Count() > 0) return;

            MainWin.DG4Data.Add(_Customer);
            MainWin.DG3Data.Remove(_Customer);
        }
    }
}
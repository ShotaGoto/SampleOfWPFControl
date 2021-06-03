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

            DG2.DataContext = MainWin.DG2Data;
        }

        private void DG2_MouseMove(object sender, MouseEventArgs e)
        {
            //マウスクリック時以外は無視する
            if (e.LeftButton != MouseButtonState.Pressed) return;

            //クリックしたコントロールがDataGridで無い時は無視
            if (!(sender is DataGrid DG)) return;
            //行を選択していない時は無視
            if (DG.SelectedItems.Count == 0) return;

            DragDrop.DoDragDrop(DG, DG.SelectedItems[0], DragDropEffects.Move);
        }

        private void DG2_Drop(object sender, DragEventArgs e)
        {
            Customer _Customer = (Customer)e.Data.GetData(typeof(Customer));
            if (_Customer == null) return;

            //既に入っていたら無視
            if (MainWin.DG2Data.Where(x => x.ID == _Customer.ID).Count() > 0) return;

            MainWin.DG2Data.Add(_Customer);
            MainWin.DG1Data.Remove(_Customer);
        }
    }
}
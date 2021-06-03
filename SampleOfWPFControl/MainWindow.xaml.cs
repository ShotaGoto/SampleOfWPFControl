using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SampleOfWPFControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// テスト用初期データ生成
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<Customer> GetNewData()
        {
            var Customers = new List<Customer>();
            var Customer1 = new Customer { ID = 1, FirstName = "hoge", LastName = "foo" };
            var Customer2 = new Customer { ID = 2, FirstName = "tiger", LastName = "far" };

            Customers.Add(Customer1);
            Customers.Add(Customer2);

            return new ObservableCollection<Customer>(Customers);
        }

        private ObservableCollection<Customer> DG1Data { get; set; } = new ObservableCollection<Customer>();
        private ObservableCollection<Customer> DG2Data { get; set; } = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> DG3Data { get; set; } = new ObservableCollection<Customer>();

        public ObservableCollection<Customer> DG4Data { get; set; } = new ObservableCollection<Customer>();
        private SubWindow SubWin;

        public MainWindow()
        {
            InitializeComponent();

            //GetData() creates a collection of Customer data from a database
            DG1Data = GetNewData();
            DG3Data = GetNewData();

            //Bind the DataGrid to the customer data
            DG1.DataContext = DG1Data;
            DG2.DataContext = DG2Data;
            DG3.DataContext = DG3Data;

            SubWin = new SubWindow(this);
        }


        /// <summary>
        /// サブウィンドウ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SubWin.Show();
        }

        private void DG1_MouseMove(object sender, MouseEventArgs e)
        {
            //マウスクリック時以外は無視する
            if (e.LeftButton != MouseButtonState.Pressed) return;

            //クリックしたコントロールがDataGridで無い時は無視
            if (!(sender is DataGrid DG)) return;

            //行を選択していない時は無視
            if (DG.SelectedItems.Count == 0) return;

            DragDrop.DoDragDrop(DG, DG.SelectedItems[0], DragDropEffects.Move);
        }

        private void DG1_Drop(object sender, DragEventArgs e)
        {
            Customer _Customer = (Customer)e.Data.GetData(typeof(Customer));
            if (_Customer == null) return;

            //既に入っていたら無視
            if (DG1Data.Where(x => x.ID == _Customer.ID).Count() > 0) return;

            DG1Data.Add(_Customer);
            DG2Data.Remove(_Customer);
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
            if (DG2Data.Where(x => x.ID == _Customer.ID).Count() > 0) return;

            DG2Data.Add(_Customer);
            DG1Data.Remove(_Customer);
        }

        private void DG3_MouseMove(object sender, MouseEventArgs e)
        {
            //マウスクリック時以外は無視する
            if (e.LeftButton != MouseButtonState.Pressed) return;

            //クリックしたコントロールがDataGridで無い時は無視
            if (!(sender is DataGrid DG)) return;
            //行を選択していない時は無視
            if (DG.SelectedItems.Count == 0) return;

            DragDrop.DoDragDrop(DG, DG.SelectedItems[0], DragDropEffects.Move);
        }

        private void DG3_Drop(object sender, DragEventArgs e)
        {
            Customer _Customer = (Customer)e.Data.GetData(typeof(Customer));
            if (_Customer == null) return;

            //既に入っていたら無視
            if (DG3Data.Where(x => x.ID == _Customer.ID).Count() > 0) return;

            DG3Data.Add(_Customer);
            DG4Data.Remove(_Customer);
        }
    }
}
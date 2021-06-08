using SampleOfWPFControl.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SampleOfWPFControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<JobTimeLine> DG1Data { get; set; } = new ObservableCollection<JobTimeLine>();
        public ObservableCollection<Job> DG2Data { get; set; } = new ObservableCollection<Job>();

        private SubWindow SubWin { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            //GetData() creates a collection of Customer data from a database
            DG1Data = GetNewData();
            DG2Data = CreateJobs();

            //Bind the DataGrid to the customer data
            DG1.DataContext = DG1Data;

            SubWin = new SubWindow(this);
            cmb.ItemsSource = GetNewData().Select(x => x.Customer.FirstName);
            cmb.SelectedIndex = 0;
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
            var _Job = e.Data.GetData(typeof(Job)) as Job;
            if (_Job == null) return;

            //既に入っていたら無視
            if (DG1Data.Where(x => x.ID == _Job.ID).Count() > 0) return;

            //Dropされた場所を取得する
            var point = e.GetPosition((UIElement)sender);
            var hitResultTest = VisualTreeHelper.HitTest(DG1, point);

            if (hitResultTest == null) return;

            var visualHit = hitResultTest.VisualHit;

            while (visualHit != null)
            {
                if (visualHit is TextBlock)
                {
                    var dstTextBlock = visualHit as TextBlock;
                    dstTextBlock.Text = _Job.Details;
                    break;
                }
            }
            visualHit = VisualTreeHelper.GetParent(visualHit);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderValue.Content = ((Slider)sender).Value;
        }

        #region"テスト用初期データ生成"

        private ObservableCollection<JobTimeLine> GetNewData()
        {
            var JobTimeLines = new List<JobTimeLine>();
            var Customer1 = new Customer { ID = 1, FirstName = "hoge", LastName = "foo" };
            var JobTimeLine1 = new JobTimeLine() { Customer = Customer1 };
            JobTimeLines.Add(JobTimeLine1);

            var Customer2 = new Customer { ID = 2, FirstName = "tiger", LastName = "far" };
            var JobTimeLine2 = new JobTimeLine() { Customer = Customer2 };
            JobTimeLines.Add(JobTimeLine2);

            return new ObservableCollection<JobTimeLine>(JobTimeLines);
        }

        private ObservableCollection<Job> CreateJobs()
        {
            var Jobs = new List<Job>();
            var Job1 = new Job() { ID = 1, Details = "お仕事１" };
            var Job2 = new Job() { ID = 2, Details = "お仕事２" };
            var Job3 = new Job() { ID = 3, Details = "お仕事３" };
            var Job4 = new Job() { ID = 4, Details = "お仕事４" };
            var Job5 = new Job() { ID = 5, Details = "お仕事５" };

            Jobs.Add(Job1);
            Jobs.Add(Job2);
            Jobs.Add(Job3);
            Jobs.Add(Job1);
            Jobs.Add(Job4);
            Jobs.Add(Job5);

            return new ObservableCollection<Job>(Jobs);
        }

        #endregion

        private void Talking_Click(object sender, RoutedEventArgs e)
        {
            var txt = txtTalking.Text;
            Task.Run(() => {
                var synthesizer = new SpeechSynthesizer();
                synthesizer.SetOutputToDefaultAudioDevice();
                synthesizer.Speak(txt);
            });
            
        }
    }
}
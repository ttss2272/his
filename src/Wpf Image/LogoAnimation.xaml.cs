using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Uni
namespace Wpf_Image
{
    /// <summary>
    /// Interaction logic for LogoAnimation.xaml
    /// </summary>
    public partial class LogoAnimation : Window
    {
        public LogoAnimation()
        {
            InitializeComponent();
            StartTimer();
        }

        DispatcherTimer timer = null;
        void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += new EventHandler(timer_Elapsed);
            timer.Start();
        }

        void timer_Elapsed(object sender, EventArgs e)
        {
            timer.Stop();

            Welcome window = new Welcome();
            this.Close();
            window.Show();
        }
    }
}

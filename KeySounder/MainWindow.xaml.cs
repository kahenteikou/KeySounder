using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace KeySounder
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Media.SoundPlayer player = null;

        //WAVEファイルを再生する
        private void PlaySound()
        {
            //再生されているときは止める
            //if (player != null)
                //StopSound();

            //読み込む
            //非同期再生する
            player.Play();

            //次のようにすると、ループ再生される
            //player.PlayLooping();

            //次のようにすると、最後まで再生し終えるまで待機する
            //player.PlaySync();
        }
        private void StopSound()
        {
            if (player != null)
            {
                player.Stop();
                player.Dispose();
                player = null;
            }
        }

        KeyEventer.KeyEvent keyEvent;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            keyEvent.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            keyEvent.Stop();

        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            keyEvent = new KeyEventer.KeyEvent();
            keyEvent.Push += Push_Event;
            player = new System.Media.SoundPlayer(System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\takumi.wav");


        }
        private void Push_Event(object sender,EventArgs e)
        {
            PlaySound();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            keyEvent.Stop();
        }
    }
}

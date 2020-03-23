using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeyEventer
{
    public class KeyEvent
    {
        public event EventHandler Push;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int GetAsyncKeyState(int vKey);
        private bool run_flag = false;
        private CancellationTokenSource tokenSource;
        private void LoopyMethod(CancellationToken token)       //実際の処理
         
        {
            while (true)
            {
                //処理
                for (int i = 1; i <= 256; i++)
                {
                    int rtn = GetAsyncKeyState(i);
                    if ((rtn & 1) != 0)
                    {
                        //押されたら
                        Push(this, EventArgs.Empty);
                    }
                }
                if (token.IsCancellationRequested)
                {
                    run_flag = false;
                    break;   //終了処理
                }
            }

        }
        public void Start()
        {
            if (run_flag == true) return;
            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            Task.Run(() => LoopyMethod(token));
            run_flag = true;

        }
        public void Stop()
        {
            if (run_flag == false) return;
            tokenSource.Cancel();
        }

    }
}

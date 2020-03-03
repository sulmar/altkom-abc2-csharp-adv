using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfClient.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }


   


    public class CalculatorViewModel : BaseViewModel
    {
        private decimal cost;

        private int ThreadId => Thread.CurrentThread.ManagedThreadId;

        public ICommand SendCommand { get; }
        public ICommand DownloadCommand { get; }
        public ICommand CalculateCommand { get; }
        public ICommand CancelCalculateCommand { get; }

        public decimal Cost
        {
            get => cost;
            set
            {
                cost = value;
                OnPropertyChanged(nameof(Cost));
            }
        }

        private int step;

        public int Step
        {
            get { return step; }
            set 
            { 
                step = value;
                OnPropertyChanged(nameof(Step));
            }
        }


        public CalculatorViewModel()
        {
            SendCommand = new RelayCommand(SendTask2);
            DownloadCommand = new RelayCommand(() => DownloadTask("http://www.google.com"));
            CalculateCommand = new RelayCommand(CalculateAsyncAwait);
            CancelCalculateCommand = new RelayCommand(CancelCalculate);


            Queue<int> queue = new Queue<int>();

            // queue.Enqueue()

            ConcurrentQueue<int> queue2 = new System.Collections.Concurrent.ConcurrentQueue<int>();

            int item;
            while (queue2.TryDequeue(out item))
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

          

        }

        private void CancelCalculate()
        {
            cancellationTokenSource.Cancel();
        }

        private void ThreadSend()
        {
            Thread thread = new Thread(Send);
            thread.Start();
        }

        private void SendTask()
        {
            Task task = new Task(Send);


            // ...

            task.Start();
        }

        private void SendTask2()
        {
            // Task t = Task.Factory.StartNew(Send);

            Task task = Task.Run(Send);
        }

        private void Send()
        {
            Debug.WriteLine($"#{ThreadId} Sending...");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            Debug.WriteLine($"#{ThreadId} Sent.");
        }

        private void ThreadDownload(string url)
        {
            Thread thread = new Thread(Download);
            thread.Start(url);
        }

        private void ThreadDownload2(string url)
        {
            Thread thread = new Thread(() => Download(url));
            thread.Start();
        }

        private void DownloadTask(string url)
        {
            Task.Run(() => Download(url));
        }

        private void Download(object url)
        {
            Download((string)url);
        }

        private void Download(string url)
        {
            Debug.WriteLine($"#{ThreadId} Downloading {url}...");

            var client = new WebClient();
            string content = client.DownloadString(url);

            Thread.Sleep(TimeSpan.FromSeconds(5));

            Debug.WriteLine($"#{ThreadId} Downloaded {url} {content.Length}.");
        }


        private void ThreadCalculate()
        {
            Thread thread = new Thread(Calculate);
            thread.Start();
        }

        private void CalculateTask()
        {
            //Task<decimal> task = Task.Run(()=>CalculateCost(100, 10));

            //task
            //    .ContinueWith(t => Cost = t.Result);


            //Task.Run(() => CalculateCost(100, 10))
            //        .ContinueWith(t => Cost = t.Result);

            CalculateCostAsync(100, 10)
                    .ContinueWith(t => Cost = t.Result);

            // Cost = task.Result;
        }

        private CancellationTokenSource cancellationTokenSource;
        
        private async void CalculateAsyncAwait()
        {
            cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));

             // cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(3));

             CancellationToken token = cancellationTokenSource.Token;

            IProgress<int> progress = new Progress<int>(value => this.Step = value);

            try
            {
                Cost = await CalculateCostAsync(100, 10, token, progress);
            }
            catch(OperationCanceledException e)
            {
                Cost = -1;
                Step = 0;
            }

            //Task<decimal> t1 = CalculateCostAsync(100, 10);
            //Task<decimal> t2 = CalculateCostAsync(1, 10);

            //await Task.WhenAll(t1, t2);

            //decimal total = t1.Result + t2.Result;


        }
        private void Calculate()
        {
            Cost = CalculateCost(100, 10);

            Send();
        }


        private Task<decimal> CalculateCostAsync(int length, int copies,
            CancellationToken cancellationToken = default,
             IProgress<int> progress = default
            )
        {
            return Task.Run(() => CalculateCost(length, copies, cancellationToken, progress));
        }

        private decimal CalculateCost(int length, int copies,
            CancellationToken cancellationToken = default,
            IProgress<int> progress = default
            )
        {
            Debug.WriteLine($"#{ThreadId} Calculating...");

            for (int i = 0; i < copies; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                //if (cancellationToken.IsCancellationRequested)
                //{
                //    throw new OperationCanceledException();
                //}

                // Debug.Write(".");

                progress?.Report(i);

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            

            decimal result = length * copies;

            Debug.WriteLine($"#{ThreadId} Calculated.");

            return result;
        }


    }
}
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Delegates
{
    class Program
    {
        public static void LogConsole(string message)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void LogFile(string message)
        {
            System.IO.File.AppendAllText("log.txt", message);
        }

        static decimal CalculateStandardPrice(int length)
        {
            decimal unitPrice = 1m;

            return length * unitPrice;
        }

        static void Main(string[] args)
        {
            try
            {
                using (Printer printer = new Printer())
                {
                    printer.CalculateCost += CalculateStandardPrice;

                    printer.CalculateCost += delegate (int copies)
                    {
                        return copies * 0.99m;
                    };

                    printer.CalculateCost += copies => copies * 0.99m;


                    printer.Log = LogConsole;
                    printer.Log += LogFile;
                    // printer.Log += LogFacebook;

                    printer.Log += Console.WriteLine;

                    // Metoda animowa
                    printer.Log += delegate (string content)
                    {
                        Console.WriteLine($"Send sms {content}");
                    };

                    printer.Log += (content) => Console.WriteLine($"Send email {content}");

                    // printer.Log = null;

                    printer.Print("Hello World!");

                    printer.Log -= LogFile;

                    Delegate[] delegates = printer.Log?.GetInvocationList();

                    printer.Log -= (Action<string>) delegates[3];

                    Delegate[] delegates2 = printer.Log?.GetInvocationList();

                    printer.Print("Hello C#!");

                    printer.Log("Printing Java!");
                    printer.Completed += Printer_Completed;

                    printer.Error += (sender, e) => Console.WriteLine($"Error {e.ErrorId} {e.Description}");

                }
            }
            catch (Exception e)
            {

            }

           
        }

        private static void Printer_Completed() => Console.WriteLine("Completed!");

        static void LogFacebook(string message, string account)
        {

        }
    }

    class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs(int errorId, string description)
        {
            ErrorId = errorId;
            Description = description;
        }

        public int ErrorId { get; set; }
        public string Description { get; set; }

    }

    class Printer : IDisposable
    {
        //public delegate void LogDelegate(string message);
        //public LogDelegate Log;

        public Action<string> Log;

        //public delegate decimal CalculateCostDelegate(int length);
        //public CalculateCostDelegate CalculateCost;

        public Func<int, decimal> CalculateCost;

        public delegate void CompletedDelegate();
        public event CompletedDelegate Completed;

        //public delegate void ErrorHandler(object sender, ErrorEventArgs args);
        //public event ErrorHandler Error;

        public event EventHandler<ErrorEventArgs> Error;

        public void Print(string content, byte copies = 1)
        {
            if (content.Length==0)
            {
                Error?.Invoke(this, new ErrorEventArgs(1, "pusty tekst"));
            }

            Delegate[] delegates = Log?.GetInvocationList();

            Log?.Invoke($"{DateTime.Now} Printing {content}...");

            // ...
            if (Log != null) 
                Log.Invoke($"{DateTime.Now} Printed.");

            decimal? cost = CalculateCost?.Invoke(content.Length * copies);

            if (cost.HasValue)
            {
                Display(cost.Value);
            }

            Completed?.Invoke();

        }

        private void Display(decimal cost)
        {
            Console.WriteLine($"LCD: Cost: {cost}");
        }


        public void Release()
        {

        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}

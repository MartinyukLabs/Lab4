using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            First();
        }
        public static void First()
        {
            CustomEventBus eventBus = new CustomEventBus(1000);
            eventBus.RegisterHandler<string>(s => Console.WriteLine("Received string event: " + s));
            eventBus.RegisterHandler<int>(i => Console.WriteLine("Received int event: " + i));

            eventBus.Dispatch("hello");
            eventBus.Dispatch(42);
            Thread.Sleep(1000);
            eventBus.Dispatch("world");
            eventBus.Dispatch(123);
            Thread.Sleep(1000);
            eventBus.Dispatch("foo");
            eventBus.Dispatch(99);

            eventBus.UnregisterHandler<int>(i => Console.WriteLine("Received int event: " + i));
        }
        public static void Second()
        {

        }
    }
}

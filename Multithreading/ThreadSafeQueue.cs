using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading
{
    public class ThreadSafeQueue<T>
    {
        private readonly Queue<T> queue = new Queue<T>();
        private readonly object syncRoot = new object();

        public void Enqueue(T item)
        {
            lock (syncRoot)
            {
                queue.Enqueue(item);
                Monitor.Pulse(syncRoot);
            }
        }

        public T Dequeue()
        {
            lock (syncRoot)
            {
                while (queue.Count == 0)
                {
                    Monitor.Wait(syncRoot);
                }

                return queue.Dequeue();
            }
        }
    }

}

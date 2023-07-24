using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading
{
    public class CommandExecutor
    {
        private readonly Thread thread;
        private readonly ThreadSafeQueue<Command> commandQueue = new ThreadSafeQueue<Command>();
        private volatile bool isRunning = true;

        public CommandExecutor()
        {
            thread = new Thread(ExecuteCommands);
            thread.Start();
        }

        public void EnqueueCommand(Command command)
        {
            if (isRunning)
            {
                commandQueue.Enqueue(command);
            }
        }

        public void HardStop()
        {
            isRunning = false;
            thread.Interrupt(); // Прерываем поток, чтобы быстро остановить его.
        }

        public void SoftStop()
        {
            isRunning = false;
            thread.Join(); // Дожидаемся завершения потока.
        }

        private void ExecuteCommands()
        {
            while (isRunning)
            {
                try
                {
                    var command = commandQueue.Dequeue();
                    command.Execute();
                }
                catch (ThreadInterruptedException)
                {
                    // Обрабатываем прерывание потока, вызванное методом HardStop().
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred while executing command: {ex.Message}");
                }
            }
        }
    }


}

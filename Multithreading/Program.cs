using Multithreading;
using System;
using System.Threading;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Main thread started.");

        // Создаем экземпляр CommandExecutor.
        var commandExecutor = new CommandExecutor();

        // Запускаем команды в отдельном потоке.

        // Пример команды 1.
        Command command1 = new Command(() =>
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Command 1, iteration {i}");
                Thread.Sleep(1000);
            }
        });

        // Пример команды 2.
        Command command2 = new Command(() =>
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Command 2, iteration {i}");
                Thread.Sleep(1500);
            }
        });

        // Добавляем команды в очередь на выполнение.
        commandExecutor.EnqueueCommand(command1);
        commandExecutor.EnqueueCommand(command2);

        // Даем потоку время для выполнения команд.
        Thread.Sleep(6000);

        // Останавливаем поток сразу (hard stop).
        commandExecutor.HardStop();

        Console.WriteLine("Main thread continues.");

        // Даем время на завершение команд, если они еще выполняются.
        Thread.Sleep(4000);

        // Запускаем команды заново.

        // Пример команды 3.
        Command command3 = new Command(() =>
        {
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"Command 3, iteration {i}");
                Thread.Sleep(1200);
            }
        });

        // Добавляем команду в очередь на выполнение.
        commandExecutor.EnqueueCommand(command3);

        // Даем время для выполнения команд.
        Thread.Sleep(5000);

        // Полная остановка потока (soft stop).
        commandExecutor.SoftStop();

        Console.WriteLine("Main thread finished.");
    }
}

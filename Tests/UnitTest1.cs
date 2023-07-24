using Multithreading;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CommandExecutor_ExecuteSingleCommand_Success()
        {
            // Arrange
            var commandExecutor = new CommandExecutor();

            bool commandExecuted = false;

            // Act
            Command command = new Command(() =>
            {
                commandExecuted = true;
            });

            commandExecutor.EnqueueCommand(command);
            Thread.Sleep(1000); // Даем команде время на выполнение.

            // Assert
            Assert.True(commandExecuted);
        }

        [Fact]
        public void CommandExecutor_ExecuteMultipleCommands_Success()
        {
            // Arrange
            var commandExecutor = new CommandExecutor();

            int counter = 0;

            // Act
            Command command1 = new Command(() =>
            {
                for (int i = 0; i < 3; i++)
                {
                    counter++;
                    Thread.Sleep(500);
                }
            });

            Command command2 = new Command(() =>
            {
                for (int i = 0; i < 2; i++)
                {
                    counter++;
                    Thread.Sleep(700);
                }
            });

            commandExecutor.EnqueueCommand(command1);
            commandExecutor.EnqueueCommand(command2);
            Thread.Sleep(3000); // Даем командам время на выполнение.

            // Assert
            Assert.Equal(5, counter);
        }

        [Fact]
        public void CommandExecutor_HardStop_NoCommandExecutionAfterStop()
        {
            // Arrange
            var commandExecutor = new CommandExecutor();

            int counter = 0;

            // Act
            Command command = new Command(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    counter++;
                    Thread.Sleep(500);
                }
            });

            commandExecutor.EnqueueCommand(command);
            Thread.Sleep(1000); // Даем команде время на выполнение.

            commandExecutor.HardStop();
            int counterBeforeStop = counter;

            Thread.Sleep(3000); // Даем время на выполнение оставшихся команд.

            // Assert
            Assert.Equal(counterBeforeStop, counter);
        }

        [Fact]
        public void CommandExecutor_SoftStop_AllCommandsCompletedBeforeStop()
        {
            // Arrange
            var commandExecutor = new CommandExecutor();

            int counter = 0;

            // Act
            Command command1 = new Command(() =>
            {
                for (int i = 0; i < 3; i++)
                {
                    counter++;
                    Thread.Sleep(600);
                }
            });

            Command command2 = new Command(() =>
            {
                for (int i = 0; i < 2; i++)
                {
                    counter++;
                    Thread.Sleep(800);
                }
            });

            commandExecutor.EnqueueCommand(command1);
            commandExecutor.EnqueueCommand(command2);
            Thread.Sleep(1000); // Даем командам время на выполнение.

            commandExecutor.SoftStop();
            int counterBeforeStop = counter;

            Thread.Sleep(3000); // Даем время на выполнение оставшихся команд.

            // Assert
            Assert.Equal(counter, counterBeforeStop);
        }
    }
}
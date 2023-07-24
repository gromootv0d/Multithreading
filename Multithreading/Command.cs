using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading
{
    public class Command
    {
        public Action Execute { get; }

        public Command(Action execute)
        {
            Execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }
    }
}

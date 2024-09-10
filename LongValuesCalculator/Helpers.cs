using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongValuesCalculator
{
    internal class Helpers
    {
    }

    internal class CStack : Stack<char>
    {
        public override string ToString() => this.Count == 0 
            ? string.Empty 
            : this.Pop() + ToString();
    }

    internal class CQueue : Queue<char> {
        public override string ToString() => this.Count == 0
            ? string.Empty
            : this.Dequeue() + ToString();
    }
}

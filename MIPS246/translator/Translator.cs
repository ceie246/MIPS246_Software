using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIPS246.Core.DataStructure;


namespace MIPS246.Core.Translator
{
    class Translator
    {
        private Queue<FourExp> fourExpQueue;
        private Queue<Instruction> instructionQueue;

        public Translator(Queue<FourExp> fourExpQueue) 
        {
            this.fourExpQueue = fourExpQueue;
            this.instructionQueue=new Queue<Instruction>();
        }

        public Queue<Instruction> Process()
        {
            if (fourExpQueue.Count != 0)
            {
                FourExp fourExp = fourExpQueue.Dequeue();   
            }
            return instructionQueue;
        }
    }

    
}

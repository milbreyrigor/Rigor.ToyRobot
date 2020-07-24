using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Console.EventArgs
{
    public class HandlerEventArgs
    {

        
        public object Sender { get; set; }
        public string Message { get; set; }

        public bool WriteToSameLine { get; set; }

        public HandlerEventArgs()
        {
        }

        public HandlerEventArgs(object o, string message, bool sameLine)
        {
            Sender = o;
            Message = message;
            WriteToSameLine = sameLine;

        }
    }
}

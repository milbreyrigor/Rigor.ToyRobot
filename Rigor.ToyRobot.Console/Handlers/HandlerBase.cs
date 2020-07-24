using Rigor.ToyRobot.Console.EventArgs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Console.Handlers
{
    public abstract class HandlerBase : IDisposable
    {
        public virtual event EventHandler<HandlerEventArgs> ReportMessageSet;

        public abstract string CommandArgs { get; }

        public virtual string[] CommandParams { get; set; }

        public abstract string CommandArgsDescription { get; }

        public abstract void ExecuteCommand();

        public abstract Guid HandlerGuid { get; }

        public abstract string GetErrorMessage { get; }

        public HandlerBase()
        {

        }

        protected void SetMessage(object sender, HandlerEventArgs args)
        {
            ReportMessageSet?.Invoke(sender, args);
        }

        public static Dictionary<string, HandlerBase> GetAvailableCommands()
        {
            List<HandlerBase> cmds = new List<HandlerBase>();

            Dictionary<string, HandlerBase> result = new Dictionary<string, HandlerBase>();

            try
            {
                cmds = HandlerBaseFactory.GetAllHandlerBases();

                foreach (HandlerBase c in cmds)
                {
                    result.Add(c.CommandArgs, c);
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }


        public override string ToString()
        {
            return String.Format("{0}\n{1}", CommandArgs, CommandArgsDescription);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

        }

        ~HandlerBase()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
    }
}

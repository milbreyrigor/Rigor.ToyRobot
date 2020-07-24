using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Console.Handlers
{
    public static class HandlerBaseFactory
    {

        static HandlerBaseFactory()
        {
        }


        public static HandlerBase CreateHandlerBase(Guid handlerGuid)
        {
            HandlerBase handlerBase = null;

            handlerBase = GetAllHandlerBases().FirstOrDefault(x => x.HandlerGuid == handlerGuid);

            return handlerBase;
        }

        public static List<HandlerBase> GetAllHandlerBases()
        {
            List<HandlerBase> result = new List<HandlerBase>();
            try
            {
                foreach (HandlerBase driver in typeof(HandlerBase).Assembly.GetTypes().Where(c => c.IsSubclassOf(typeof(HandlerBase)) && !c.IsAbstract).Select(c => Activator.CreateInstance(c)))
                {
                    result.Add(driver);
                }

                result = result.ToList();
            }

            catch (Exception e)
            {

            }

            return result;
        }
    }
}

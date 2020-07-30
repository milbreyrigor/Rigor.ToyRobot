using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Services
{
    public static class ServicesFactory
    {
        static ServicesFactory()
        {
        }

        /// <summary>
        /// Create all services.
        /// </summary>
        /// <returns></returns>
        public static List<ServicesBase> CreateAllServices()
        {
            List<ServicesBase> result = new List<ServicesBase>();
            try
            {

                foreach (ServicesBase vw in typeof(ServicesFactory).Assembly.GetTypes().Where(c => c.IsSubclassOf(typeof(ServicesBase)) && !c.IsAbstract).Select(c => Activator.CreateInstance(c)))
                {
                    result.Add(vw);
                }
            }

            catch (Exception e)
            {

            }

            return result;
        }
    }
}

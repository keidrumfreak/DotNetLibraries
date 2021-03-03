using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Diagnostics
{
    public interface IProcessFactory
    {
        IProcess Create(ProcessStartInfo startInfo);
    }

    public class ProcessFactory : IProcessFactory
    {
        static IProcessFactory instance;
        public static IProcessFactory Instance
        {
            get { return instance ?? (instance = new ProcessFactory()); }
            set { instance = value; }
        }

        private ProcessFactory() { }

        public IProcess Create(ProcessStartInfo startInfo)
        {
            return new ProcessWrapper{ StartInfo = startInfo };
        }
    }
}

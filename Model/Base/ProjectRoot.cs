using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FomodModel.Base
{
    public class ProjectRoot
    {
        public ModuleInformation ModuleInformation { get; set; }
        public ModuleConfiguration.ModuleConfiguration ModuleConfiguration { get; set; }
    }
}

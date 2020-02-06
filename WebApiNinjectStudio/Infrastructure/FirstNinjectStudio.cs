using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNinjectStudio.Infrastructure
{
    public class FirstNinjectStudio : INinjectStudio
    {
        public string MyMessage { get; set; }

        public FirstNinjectStudio()
        {
            this.MyMessage = "Hello World!";
        }
    }
}

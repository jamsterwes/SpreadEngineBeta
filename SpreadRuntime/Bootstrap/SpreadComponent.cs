using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadRuntime.Bootstrap
{
    public interface SpreadComponent
    {
        void Initialize();
        void Update();
    }
}

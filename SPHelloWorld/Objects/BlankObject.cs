using SpreadRuntime.Bootstrap;
using SPHelloWorld.Components;
using System.Windows.Forms;

namespace SPHelloWorld.Objects
{
    class BlankObject : SpreadObject
    {
        public BlankObject()
        {
            var blank = AddComponent<BlankComponent>();
            MessageBox.Show(blank.f.ToString());
        }
    }
}

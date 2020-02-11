using SpreadRuntime;
using SPHelloWorld.Components;
using System.Windows.Forms;

namespace SPHelloWorld.Objects
{
    [UseComponent(typeof(BlankComponent))]
    public class BlankObject : SpreadObject
    {
        public BlankObject() : base(typeof(BlankObject))
        {
            var x = GetComponent<BlankComponent>()?.f;
            MessageBox.Show(x.ToString());
        }
    }
}

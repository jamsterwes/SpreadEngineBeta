using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadRuntime
{
    // TODO: Add components from decorator
    public class SpreadObject
    {
        List<SpreadComponent> components = new List<SpreadComponent>();

        public SpreadObject(Type superclass)
        {
            // Add Components
            foreach (UseComponentAttribute attr in superclass.GetCustomAttributes(typeof(UseComponentAttribute), true))
            {
                if (attr == null) continue;
                AddComponent(attr.ComponentType);
                MessageBox.Show(attr.ToString());
            }
        }

        public void AddComponent(Type T)
        {
            var constructor = T.GetConstructor(new Type[] { });
            dynamic d = Convert.ChangeType(constructor.Invoke(new object[] { }), T);
            components.Add(d);
        }

        public T AddComponent<T>() where T : SpreadComponent
        {
            var constructor = typeof(T).GetConstructor(new Type[] { });
            T component = (T)constructor.Invoke(new object[] { });
            components.Add(component);
            return component;
        }

        public T GetComponent<T>() where T : SpreadComponent
        {
            foreach (SpreadComponent component in components)
            {
                MessageBox.Show(component.GetType().ToString());
                if (typeof(T).Equals(component))
                {
                    return (T)component;
                }
            }

            return default;
        }
    }
}

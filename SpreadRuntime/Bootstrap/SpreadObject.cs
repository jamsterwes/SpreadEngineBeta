using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadRuntime.Bootstrap
{
    // TODO: Add components from decorator
    public class SpreadObject
    {
        List<SpreadComponent> components = new List<SpreadComponent>();

        public T AddComponent<T>(params object[] parameters) where T : SpreadComponent
        {
            var constructor = typeof(T).GetConstructor(parameters.Select(obj => obj.GetType()).ToArray());
            T component = (T)constructor.Invoke(parameters);
            components.Add(component);
            return component;
        }

        public T GetComponent<T>() where T : SpreadComponent
        {
            foreach (SpreadComponent component in components)
            {
                if (typeof(T).Equals(component))
                {
                    return (T)component;
                }
            }

            return default;
        }
    }
}

using UnityEngine;

namespace Packages.Estenis.ComponentFilter_
{
    public class ComponentFilter : MonoBehaviour
    {
        public enum FilterType
        {
            COMPONENT,
            INSTANCE_ID,
            SCRIPT_TYPE
        }

        public FilterType _filterType;
        public string _componentType;
        public Component _component;
        public int _instanceId;
        

        
    }
}

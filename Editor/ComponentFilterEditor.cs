using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using Component = UnityEngine.Component;
using Packages.Estenis.ComponentFilter_;

namespace Packages.Estenis.ComponentFilterEditor_
{
    [CustomEditor(typeof(ComponentFilter))]
    public class ComponentFilterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            ComponentFilter filter = (ComponentFilter)target;

            filter._filterType = (ComponentFilter.FilterType)EditorGUILayout.EnumPopup("Filter By: ", filter._filterType);

            UnityEngine.Object component = null;
            Action filterAction = () => { };
            switch (filter._filterType)
            {
                case ComponentFilter.FilterType.COMPONENT:
                    filter._component = EditorGUILayout.ObjectField("Component", component, typeof(MonoBehaviour), false) as Component;
                    filterAction = () => FilterByComponent(filter._component);
                    break;
                case ComponentFilter.FilterType.INSTANCE_ID:
                    filter._instanceId = EditorGUILayout.IntField(filter._instanceId);
                    filterAction = () => FilterByInstanceId(filter._instanceId);
                    break;
                case ComponentFilter.FilterType.SCRIPT_TYPE:
                    filter._componentType = EditorGUILayout.TextField("Script Type", filter._componentType);
                    filterAction = () => FilterByComponentType(filter._componentType);
                    break;
                default:
                    break;
            }

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Filter"))
                {
                    filterAction();
                    EditorUtility.SetDirty(target);
                }
                if (GUILayout.Button("Clear Filters"))
                {
                    ClearFilters();
                    EditorUtility.SetDirty(target);
                }
            }
            GUILayout.EndHorizontal();
        }

        private void FilterByComponent(Component component)
        {

        }

        private void FilterByInstanceId(int instanceId)
        {
            ComponentFilter filter = (ComponentFilter)target;
            var components = filter.gameObject.GetComponents<Component>();
            var componentsToHide =
                components
                    .Where(c => c.GetInstanceID() != instanceId
                                && c.GetType().Name != nameof(ComponentFilter));
            foreach (var component in componentsToHide)
            {
                component.hideFlags |= HideFlags.HideInInspector;
            }
        }

        private void FilterByComponentType(string type)
        {
            ComponentFilter filter = (ComponentFilter)target;
            //string type = filter._componentType;
            var components = filter.gameObject.GetComponents<Component>();
            var componentsToHide = 
                components
                    .Where(c => !c.GetType().Name.ToLower().Contains(type.ToLower())
                                && c.GetType().Name != nameof(ComponentFilter));
            foreach (var component in componentsToHide)
            {
                component.hideFlags |= HideFlags.HideInInspector;
            }
        }

        private void ClearFilters()
        {
            ComponentFilter filter = (ComponentFilter)target;
            //string type = filter._componentType;
            var components = filter.gameObject.GetComponents<Component>();
            foreach (var co in components)
            {
                //co.hideFlags &= ~HideFlags.HideInInspector;
                co.hideFlags = HideFlags.None;
            }
        }
    }
}

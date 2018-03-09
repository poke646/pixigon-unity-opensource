using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Pixigon.OpenSource.Debug
{
    public class DebugMonoBehaviour : MonoBehaviour
    {
        private bool isInitialized;

        private List<FieldInfo> fieldList = new List<FieldInfo>();
        private List<PropertyInfo> propList = new List<PropertyInfo>();

        private BindingFlags flags = BindingFlags.NonPublic |
                             BindingFlags.Instance | BindingFlags.Public;

        private void OnGUI()
        {
            Draw();
        }

        private void Draw()
        {
            if (!isInitialized)
                Initialize();

            GUI.color = Color.white;
            float y = 0;

            foreach (var prop in fieldList)
            {
                DrawValue(ref y, prop.Name, prop.GetValue(this));
            }

            foreach (var prop in propList)
            {
                DrawValue(ref y, prop.Name, prop.GetValue(this, null));
            }
        }

        private void Initialize()
        {
            var fields = GetType().GetFields(flags);
            foreach (var field in fields)
            {
                object[] attrs = field.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    DebugDrawAttribute authAttr = attr as DebugDrawAttribute;
                    if (authAttr != null)
                    {
                        FieldInfo fi = GetType().GetField(field.Name, flags);
                        fieldList.Add(fi);
                    }
                }
            }

            var props = GetType().GetProperties(flags);
            foreach (var prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    DebugDrawAttribute authAttr = attr as DebugDrawAttribute;
                    if (authAttr != null)
                    {
                        var fi = GetType().GetProperty(prop.Name, flags);
                        propList.Add(fi);
                    }
                }
            }

            isInitialized = true;
        }

        private void DrawValue(ref float y, string name, object value)
        {
            string str = string.Empty;

            if (value is float)
                str = ((float)value).ToString("N3");
            else
                str = value.ToString();

            GUI.Label(new Rect(10, y, 300, 20), string.Format("{0}: {1}", name, str));
            y += 20;
        }
    }

    public class DebugDrawAttribute : Attribute
    {
    }
}

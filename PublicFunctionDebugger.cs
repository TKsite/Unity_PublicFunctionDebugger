using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(MonoBehaviour), true)]
public class PublicFunctionDebugger : Editor
{
    private Dictionary<string, object[]> methodArgs = new Dictionary<string, object[]>();
    private Dictionary<string, object> methodReturnValues = new Dictionary<string, object>();

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MonoBehaviour monoBehaviour = (MonoBehaviour)target;
        if (monoBehaviour == null) return;

        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
        MethodInfo[] methods = monoBehaviour.GetType().GetMethods(bindingFlags).Where(m => !m.IsSpecialName).ToArray();

        EditorGUILayout.LabelField("Public Functions", EditorStyles.boldLabel);

        foreach (MethodInfo method in methods)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(method.Name))
            {
                object[] args;
                methodArgs.TryGetValue(method.Name, out args);
                object returnValue = method.Invoke(monoBehaviour, args);
                if (method.ReturnType != typeof(void))
                {
                    methodReturnValues[method.Name] = returnValue;
                }
            }
            DrawArgumentFields(method);
            EditorGUILayout.EndHorizontal();

            if (method.ReturnType != typeof(void) && methodReturnValues.ContainsKey(method.Name))
            {
                EditorGUILayout.LabelField("Return Value:", $"{methodReturnValues[method.Name]}");
            }
        }
    }


    private void DrawArgumentFields(MethodInfo method)
    {
        ParameterInfo[] parameters = method.GetParameters();
        if (parameters.Length > 0)
        {
            if (!methodArgs.ContainsKey(method.Name))
            {
                methodArgs[method.Name] = new object[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    ParameterInfo param = parameters[i];
                    Type paramType = param.ParameterType;
                    object defaultValue = null;

                    if (paramType.IsValueType)
                    {
                        defaultValue = Activator.CreateInstance(paramType);
                    }
                    else if (paramType.IsEnum)
                    {
                        defaultValue = Enum.GetValues(paramType).GetValue(0);
                    }

                    methodArgs[method.Name][i] = defaultValue;
                }
            }

            EditorGUILayout.BeginVertical(GUI.skin.box);
            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo param = parameters[i];
                object value = methodArgs[method.Name][i];

                EditorGUI.BeginChangeCheck();
                value = DrawArgumentField(param, value);
                if (EditorGUI.EndChangeCheck())
                {
                    methodArgs[method.Name][i] = value;
                }
            }
            EditorGUILayout.EndVertical();
        }
    }



    private object DrawArgumentField(ParameterInfo param, object value)
    {
        Type paramType = param.ParameterType;

        if (paramType == typeof(int))
        {
            return EditorGUILayout.IntField(param.Name, (int)value);
        }
        else if (paramType == typeof(float))
        {
            return EditorGUILayout.FloatField(param.Name, (float)value);
        }
        else if (paramType == typeof(bool))
        {
            return EditorGUILayout.Toggle(param.Name, (bool)value);
        }
        else if (paramType == typeof(string))
        {
            return EditorGUILayout.TextField(param.Name, (string)value);
        }
        else if (paramType == typeof(Vector2))
        {
            return EditorGUILayout.Vector2Field(param.Name, (Vector2)value);
        }
        else if (paramType == typeof(Vector3))
        {
            return EditorGUILayout.Vector3Field(param.Name, (Vector3)value);
        }
        else if (paramType == typeof(Color))
        {
            return EditorGUILayout.ColorField(param.Name, (Color)value);
        }
        else if (paramType.IsEnum)
        {
            return EditorGUILayout.EnumPopup(param.Name, (Enum)value);
        }
        else if (typeof(UnityEngine.Object).IsAssignableFrom(paramType))
        {
            return EditorGUILayout.ObjectField(param.Name, (UnityEngine.Object)value, paramType, true);
        }
        else
        {
            EditorGUILayout.LabelField($"{param.Name}: Unsupported parameter type");
            return value;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Utilities.General
{
    /// <summary>
    /// Experimental class.
    /// </summary>
    [Serializable] public class ClassConstructor
    {
        [Serializable] public class TypeInfo
        {
            [SerializeField] private string _assemblFullName = string.Empty;
            public string AssemblFullName { get { return _assemblFullName; } }

            [SerializeField] private string _typeName = string.Empty;
            public string FullName { get { return _typeName; } }

            [SerializeField] private string _name = string.Empty;
            public string Name { get { return _name; } }

            private Type _type = null;

            public TypeInfo(Type type)
            {
                _assemblFullName = type.Assembly.GetName().FullName;
                _typeName = type.FullName;
                _name = type.Name;
            }

            private bool AssemblyQuiry(Assembly assembly)
            {
                return assembly.GetName().FullName == _assemblFullName;
            }

            public static implicit operator Type(TypeInfo stateInfo)
            {
                if (stateInfo == null || string.IsNullOrEmpty(stateInfo._assemblFullName) || string.IsNullOrEmpty(stateInfo._typeName))
                    return null;

                if (stateInfo._type == null)
                {
                    Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(stateInfo.AssemblyQuiry);
                    stateInfo._type = assembly.GetType(stateInfo._typeName);
                }

                return stateInfo._type;
            }
        }

        [Serializable] public class ConstructorParameter
        {
            [SerializeField] private string _parameterName = string.Empty;
            public string ParameterName { get { return _parameterName; } }

            [SerializeField] private TypeInfo _type = null;
            public TypeInfo Type
            {
                get { return _type; }
                set { _type = value; }
            }

            public UnityEngine.Object ObjectValue;
            public int IntValue = 0;
            public float FloatValue = 0f;
            public bool BoolValue = false;
            public string StringValue = string.Empty;
            public Color ColorValue = Color.white;

            public ConstructorParameter(ParameterInfo parameterInfo)
            {
                Type = new TypeInfo(parameterInfo.ParameterType);
                _parameterName = parameterInfo.Name;
            }

            public override int GetHashCode()
            {
                int value = 0;
                for (int i = 0; i < _parameterName.Length; i++)
                    value += _parameterName[i];
                return value;
            }

            public object GetObject()
            {
                Type type = Type;
                switch (type.Name)
                {
                    case "Int32":
                        return IntValue;
                    case "Single":
                        return FloatValue;
                    case "Boolean":
                        return BoolValue;
                    case "String":
                        return StringValue;
                    case "Color":
                        return ColorValue;
                    default:
                        return ObjectValue;
                }
            }
        }

        [SerializeField] private TypeInfo _baseType = null;
        public TypeInfo BaseType
        {
            get { return _baseType; }
            set { _baseType = value; }
        }

        [SerializeField] private TypeInfo _type = null;
        public TypeInfo Type { get { return _type; } }

        [SerializeField] private ConstructorParameter[] _parameters = null;
        public ConstructorParameter[] Parameters { get { return _parameters; } }

        [SerializeField] private string _name = string.Empty;
        public string Name { get { return _name; } }

        public ClassConstructor() { }

        public ClassConstructor(Type type)
        {
            _baseType = new TypeInfo(type);
        }

        public ClassConstructor(ConstructorInfo info, Type type) : this(type)
        {
            _type = new TypeInfo(info.DeclaringType);
            _name = string.Format("{0} (", info.DeclaringType.Name);

            var parameters = info.GetParameters();
            _parameters = new ConstructorParameter[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                _parameters[i] = new ConstructorParameter(parameters[i]);
                _name += string.Format("{0} {1}{2}", _parameters[i].Type.Name, _parameters[i].ParameterName, i < parameters.Length - 1 ? ", " : "");
            }
            _name += ")";
        }


        public object CreateInstance()
        {
            object[] data = new object[_parameters.Length];

            for (int i = 0; i < _parameters.Length; i++)
                data[i] = _parameters[i].GetObject();

            return Activator.CreateInstance(_type, data);
        }

        public override int GetHashCode()
        {
            int value = 0;
            for (int i = 0; i < _type.FullName.Length; i++)
                value += _type.FullName[i];

            for (int i = 0; i < _parameters.Length; i++)
                value += _parameters[i].GetHashCode();

            return value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return GetHashCode() == obj.GetHashCode();
        }

        public static bool operator != (ClassConstructor a, ClassConstructor b)
        {
            return !(a == b);
        }

        public static bool operator == (ClassConstructor a, ClassConstructor b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            else if ((ReferenceEquals(a, null) && !ReferenceEquals(b, null)) ||
                (!ReferenceEquals(a, null) && ReferenceEquals(b, null)))
                return false;

            return a.Equals(b);
        }
    }
}
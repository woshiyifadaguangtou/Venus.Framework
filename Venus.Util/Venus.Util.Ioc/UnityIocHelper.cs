﻿using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;


namespace Venus.Util.Ioc
{
    public class UnityIocHelper : IServiceProvider
    {
        private readonly IUnityContainer _container;
        private static readonly UnityIocHelper dbinstance = new UnityIocHelper("DBcontainer");
        private UnityIocHelper(string containerName)
        {
            try
            {
                var section1 = ConfigurationManager.GetSection("unity");
                UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
                _container = new UnityContainer();
                section.Configure(_container, containerName);
                //section.Configure(_container);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static string GetmapToByName(string containerName, string itype, string name = "")
        {
            try
            {
                UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
                var _Containers = section.Containers;
                foreach (var _Container in _Containers)
                {
                    if (_Container.Name == containerName)
                    {
                        var _Registrations = _Container.Registrations;
                        foreach (var _Registration in _Registrations)
                        {
                            if (name == "" && string.IsNullOrEmpty(_Registration.Name) && _Registration.TypeName == itype)
                            {
                                return _Registration.MapToName;
                            }
                        }
                        break;
                    }
                }
                return "";
            }
            catch
            {
                throw;
            }

        }

        public static UnityIocHelper DBInstance
        {
            get { return dbinstance; }
        }
        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }
        public T GetService<T>()
        {
            return _container.Resolve<T>();
        }
        public T GetService<T>(params ParameterOverride[] obj)
        {
            return _container.Resolve<T>(obj);
        }
        public T GetService<T>(string name, params ParameterOverride[] obj)
        {
            return _container.Resolve<T>(name, obj);
        }
    }
}

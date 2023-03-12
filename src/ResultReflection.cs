using System;
using System.Linq;
using System.Reflection;

namespace PowerUtils.Results
{
    internal static class ResultReflection
    {
        internal static Type TryGetErrorType(string fullName)
        {
            var type = Type.GetType(fullName, false, true);

            type ??= AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .SingleOrDefault(s => s.FullName.Equals(fullName, StringComparison.InvariantCultureIgnoreCase));

            if(type?.GetInterfaces().Contains(typeof(IError)) == true)
            {
                return type;
            }

            return null;
        }

        internal static TError CreateError<TError>(Type type, string property, string code, string description)
            where TError : IError
        {
            if(type is null)
            {
                throw new TypeLoadException($"Could not load type '{typeof(TError).FullName}'.");
            }

            try
            {
                var instance = CreateInstance(type);

                PopulateInternalProperty(instance, nameof(IError.Property), property);
                PopulateInternalProperty(instance, nameof(IError.Code), code);
                PopulateInternalProperty(instance, nameof(IError.Description), description);

                return (TError)instance;
            }
            catch(Exception exception)
            {
                throw new TargetInvocationException($"Could not create new instance for '{type.FullName}'", exception);
            }


            static object CreateInstance(Type type)
            {
                // Find first constructor.
                //    If the constructor has parameters, pass in default for each parameter.
                var constructor = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)[0];
                return constructor.Invoke(new object[constructor.GetParameters().Length]);
            }

            static void PopulateInternalProperty(object instance, string setterName, string value)
            {
                var propertyInfo = instance.GetType().GetProperty(setterName);
                var setter = propertyInfo.GetSetMethod(true);
                setter.Invoke(instance, new object[] { value });
            }
        }
    }
}

using System;
using System.Diagnostics.CodeAnalysis;
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

            if(type?.GetInterfaces().Contains(typeof(IError)) is true)
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

            var instance = createInstance(type);

            populateInternalProperty(instance, nameof(IError.Property), property);
            populateInternalProperty(instance, nameof(IError.Code), code);
            populateInternalProperty(instance, nameof(IError.Description), description);

            return (TError)instance;


            [SuppressMessage("SonarQube", "S3011", Justification = "The use of the 'NonPublic' flag is necessary to be able to create an instance to help the deserialization of objects.")]
            static object createInstance(Type type)
            {
                // Find first constructor.
                //    If the constructor has parameters, pass default for each parameter.
                var constructor = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)[0];
                return constructor.Invoke(new object[constructor.GetParameters().Length]);
            }

            static void populateInternalProperty(object instance, string setterName, string value)
                => instance
                    .GetType()
                    .GetProperty(setterName)
                    .GetSetMethod()
                    .Invoke(instance, new object[] { value });
        }
    }
}

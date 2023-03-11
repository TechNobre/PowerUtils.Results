using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUtils.Results
{
    internal static class CommonUtils
    {
        internal const string ERROR_DEBUG_TEMPLATE = "{GetType().Name} > P: {Property}, C: {Code}, D: {Description}";


        internal static bool Equals<TError>(TError error, ref IError other) where TError : IError
        {
            if(other is not TError)
            {
                return false;
            }

            if(!error.Property.Equals(other.Property))
            {
                return false;
            }

            if(!error.Code.Equals(other.Code))
            {
                return false;
            }

            if(!error.Description.Equals(other.Description))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns true if contains error, false is not
        /// </summary>
        internal static bool RemoveNulls(ref List<IError> list)
        {
            list?.RemoveAll(r => r is null);

            return _hasError(ref list);
        }

        /// <summary>
        /// Returns true if contains error, false is not
        /// </summary>
        internal static bool AddError(ref List<IError> list, ref IError error)
        {
            list ??= new List<IError>();

            if(error is not null)
            {
                list.Add(error);
            }

            return _hasError(ref list);
        }

        /// <summary>
        /// Returns true if contains error, false is not
        /// </summary>
        internal static bool AddErrors(ref List<IError> list, ref IEnumerable<IError> errors)
        {
            list ??= new List<IError>();
            list.AddRange(errors);

            return RemoveNulls(ref list);
        }

        private static bool _hasError(ref List<IError> list)
        {
            if(list is null)
            {
                return false;
            }

            return list.Count is not 0;
        }

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
    }
}

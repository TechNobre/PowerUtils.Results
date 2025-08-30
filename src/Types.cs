using System;

namespace PowerUtils.Results
{
    public interface IType { }

#if NET6_0_OR_GREATER
    [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
    public readonly record struct Success : IType
#else
    [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
    public readonly struct Success : IType
#endif
    {
        public static Success Create() => new();
    }
}

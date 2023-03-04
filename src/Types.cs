namespace PowerUtils.Results
{
    public interface IType { }

#if NET6_0_OR_GREATER
    public readonly record struct Success : IType
#else
    public readonly struct Success : IType
#endif
    {
        public static Success Create() => new();
    }
}

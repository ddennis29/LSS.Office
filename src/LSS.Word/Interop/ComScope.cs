using System;
using System.Runtime.InteropServices;

namespace LSS.Word.Interop;

public sealed class ComScope<T> : IDisposable where T : class
{
    public ComScope(T value)
    {
        Value = value;
    }

    public T Value { get; }

    public void Dispose()
    {
        if (Marshal.IsComObject(Value))
        {
            Marshal.FinalReleaseComObject(Value);
        }
    }
}

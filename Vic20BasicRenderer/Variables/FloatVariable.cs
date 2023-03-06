using Vic20BasicRenderer.BaseTypes;

namespace Vic20BasicRenderer.Variables;

public class FloatVariable : Variable
{
    public FloatVariable(string underlyingName) : base(underlyingName)
    {
    }

    public override string ToString() =>
        UnderlyingName;
}
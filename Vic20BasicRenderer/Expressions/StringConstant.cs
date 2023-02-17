using Vic20BasicRenderer.BaseTypes;

namespace Vic20BasicRenderer.Expressions;

public class StringConstant : Expression
{
    private readonly string _value;

    public StringConstant(string value)
    {
        _value = value;
    }

    public override string GetCode(List<ProgramContent> currentProgramContent, BasicProgram currentProgram, int currentIndex) =>
    $@"""{_value}""";

    public static explicit operator StringConstant(string d) =>
        new(d);
}
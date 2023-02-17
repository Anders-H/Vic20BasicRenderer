using System.Text;
using Vic20BasicRenderer.BaseTypes;

namespace Vic20BasicRenderer.Commands;

public class Print : ProgramContent
{
    private List<Expression> Expressions { get; }

    public Print(params Expression[] expressions)
    {
        Expressions = new List<Expression>();

        foreach (var exp in expressions)
            Expressions.Add(exp);
    }

    public override string GetCode(List<ProgramContent> currentProgramContent, BasicProgram currentProgram, int currentIndex)
    {
        var s = new StringBuilder();
        s.Append("?");

        foreach (var exp in Expressions)
            s.Append(exp.GetCode(currentProgramContent, currentProgram, currentIndex));

        return s.ToString().Trim();
    }

    public override bool HasLineNumber =>
        true;

    public void Add(Expression exp) =>
        Expressions.Add(exp);
}
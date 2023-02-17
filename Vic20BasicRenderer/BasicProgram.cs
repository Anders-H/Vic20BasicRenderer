using System.Text;
using Vic20BasicRenderer.BaseTypes;
using Vic20BasicRenderer.Commands;

namespace Vic20BasicRenderer;

public class BasicProgram
{
    internal int LineNumber { get; set; }
    private List<ProgramContent> ProgramContent { get; }
    public Platform Platform { get; }

    public BasicProgram(Platform platform)
    {
        LineNumber = 0;
        ProgramContent = new List<ProgramContent>();
        Platform = platform;
    }

    public void NewProgram()
    {
        LineNumber = 0;
        ProgramContent.Clear();
    }

    public void Add(ProgramContent p)
    {
        p.Index = ProgramContent.Count;
        ProgramContent.Add(p);
    }

    public void AddRange(Expression[] p)
    {
        foreach (var x in p)
            Add(x);
    }

    public string GetCode()
    {
        CreateLineNumbers();

        var s = new StringBuilder();
        
        var index = 0;
        
        foreach (var programContent in ProgramContent)
        {
            var code = programContent.GetCode(ProgramContent, this, index);

            if (string.IsNullOrWhiteSpace(code))
                continue;

            s.AppendLine($@"{programContent.LineNumber}{code}");

            index++;
        }

        return s.ToString().Trim();
    }

    private void CreateLineNumbers()
    {
        var lineNumber = 0;

        foreach (var programContent in ProgramContent)
        {
            if (programContent.HasLineNumber)
            {
                programContent.LineNumber = lineNumber;
                lineNumber++;
            }
            else
            {
                programContent.LineNumber = null;
            }

            if (programContent is Label lbl)
                lbl.UnderlyingLineNumber = lineNumber;
        }

        foreach (var programContent in ProgramContent)
            if (programContent is Goto gto)
                gto.TargetLineNumber = GetLineNumberFor(gto.Label);
    }

    internal int GetLineNumberFor(Label label)
    {
        if (ProgramContent.Count <= 0)
            return 0;

        for (var i = 0; i < ProgramContent.Count - 1; i++)
            if (ProgramContent[i] == label)
                return ((Label)ProgramContent[i]).UnderlyingLineNumber;

        return 0;
    }

    public void Goto(Label label) => Add(new Goto(label));
    public void Print(params Expression[] expressions) => Add(new Print(expressions));
    public void CharacterCase(bool upperCase, bool lockedCase) => Add(new CharacterCase(upperCase, lockedCase));
    public void Color(Vic20BorderColor borderColor, Vic20Color backgroundColor) => Add(new Color(borderColor, backgroundColor));
    public void Color(C64Color borderColor, C64Color backgroundColor) => Add(new Color(borderColor, backgroundColor));
}
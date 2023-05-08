using System.Text;
using Vic20BasicRenderer.BaseTypes;
using Vic20BasicRenderer.Commands;
using Vic20BasicRenderer.Expressions;
using Vic20BasicRenderer.Variables;

namespace Vic20BasicRenderer;

public class BasicProgram
{
    private int _usedIntPointer = -1;
    private int _usedFloatPointer = -1;
    private int _usedStringPointer = -1;
    private readonly List<string> _variableNames;
    internal int LineNumber { get; set; }
    private List<ProgramContent> ProgramContent { get; }
    public Platform Platform { get; }

    public BasicProgram(Platform platform)
    {
        LineNumber = 0;
        ProgramContent = new List<ProgramContent>();
        Platform = platform;
        _variableNames = new List<string>();
        CreateVariableNames();
    }

    private void CreateVariableNames()
    {
        _variableNames.Clear();
        const string firstCharacter = "abcdefghijklmnopqrstuvwxyz";
        const string secondCharacter = "abcdefghijklmnopqrstuvwxyz0123456789";
        
        foreach (var c in firstCharacter)
            _variableNames.Add(c.ToString());
        
        foreach (var c in firstCharacter)
        {
            _variableNames.Add(c.ToString());

            foreach (var c2 in secondCharacter)
                _variableNames.Add(c2.ToString());
        }
    }

    public void NewProgram()
    {
        LineNumber = 0;
        ProgramContent.Clear();
        _usedIntPointer = -1;
        _usedFloatPointer = -1;
        _usedStringPointer = -1;
    }

    public IntVariable CreateInt()
    {
        _usedIntPointer++;
        return new IntVariable(_variableNames[_usedIntPointer]);
    }

    public FloatVariable CreateFloat()
    {
        _usedFloatPointer++;
        return new FloatVariable(_variableNames[_usedFloatPointer]);
    }

    public StringVariable CreateString()
    {
        _usedStringPointer++;
        return new StringVariable(_variableNames[_usedStringPointer]);
    }

    public int AddFree(string code)
    {
        var p = new FreeTextCode(code)
        {
            Index = ProgramContent.Count
        };
        ProgramContent.Add(p);
        return p.Index;
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
        RenderCustomLibraries();
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

    private void RenderCustomLibraries()
    {
        var renderedLibTypes = new List<Type>();

        bool again;
        do
        {
            again = false;

            foreach (var programContent in ProgramContent)
            {
                if (programContent is LibraryCall lib && !renderedLibTypes.Exists(x => x == (programContent as LibraryCall)!.Library.GetType()))
                {
                    if (renderedLibTypes.Count <= 0 && ProgramContent.Count > 0 && ProgramContent.Last().GetCode(ProgramContent, this, 0) != "end")
                        AddFree("end");

                    lib.Library.AddLibraryCode(this);
                    renderedLibTypes.Add((programContent as LibraryCall)!.Library.GetType());
                    again = true;
                    break;
                }
            }

        } while (again);
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
        {
            if (programContent is Goto gto)
                gto.TargetLineNumber = GetLineNumberFor(gto.Label);
            else if (programContent is Gosub gsb)
                gsb.TargetLineNumber = GetLineNumberFor(gsb.Label);
            else if (programContent is CustomLibrary lib) // This is CALL, not LIB
                lib.TargetLineNumber = GetLineNumberFor(lib.LibraryLabel);
        }
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

    public void CharacterCase(bool upperCase, bool lockedCase) => Add(new CharacterCase(upperCase, lockedCase));
    public void Color(Vic20BorderColor borderColor, Vic20Color backgroundColor, bool vic20InvertedMode) => Add(new Color(borderColor, backgroundColor, vic20InvertedMode));
    public void Color(C64Color borderColor, C64Color backgroundColor) => Add(new Color(borderColor, backgroundColor));
    public void Gosub(Label label) => Add(new Gosub(label));
    public void Goto(Label label) => Add(new Goto(label));
    public void LibraryCall(CustomLibrary library) => Add(new LibraryCall(library));
    public void Print(params Expression[] expressions) => Add(new Print(expressions));
    public void Print(string text) => Add(new Print(new StringConstant(text)));
    public void Return() => AddFree("return");
}
﻿using System.Text;
using Vic20BasicRenderer.BaseTypes;
using Vic20BasicRenderer.Commands;
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

    public IntVariable CreateFloat()
    {
        _usedFloatPointer++;
        return new IntVariable(_variableNames[_usedFloatPointer]);
    }

    public IntVariable CreateString()
    {
        _usedStringPointer++;
        return new IntVariable(_variableNames[_usedStringPointer]);
    }

    public int AddFree(string code)
    {
        
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
    public void Color(Vic20BorderColor borderColor, Vic20Color backgroundColor, bool vic20InvertedMode) => Add(new Color(borderColor, backgroundColor, vic20InvertedMode));
    public void Color(C64Color borderColor, C64Color backgroundColor) => Add(new Color(borderColor, backgroundColor));
}
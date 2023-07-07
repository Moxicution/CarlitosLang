using Antlr4.Runtime;
using LanguageParser;

namespace CarlitosLangParser;

public static class Program
{
    public static void Main()
    {
        const string fileName = "test.cl4";
        var fileContents = File.ReadAllText(fileName);
        var inputStream = new AntlrInputStream(fileContents);

        var speakLexer = new CarlitosLangLexer(inputStream);
        var commonTokenStream = new CommonTokenStream(speakLexer);
        var langParser = new LanguageParser.CarlitosLangParser(commonTokenStream);
        
        var assignmentContext = langParser.program();
        var visitor = new CarlitosLangVisitor();
        
        visitor.Visit(assignmentContext);
    }
}
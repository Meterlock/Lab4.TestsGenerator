using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestsGeneratorLibrary
{
    public class SourceCodeParcer
    {
        public ParcingInfo Parce(string srcCode)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(srcCode);
            CompilationUnitSyntax compilationUnit = tree.GetCompilationUnitRoot();
            return new ParcingInfo(GetClasses(compilationUnit));
        }

        private List<ClassInfo> GetClasses(CompilationUnitSyntax compilationUnit)
        {
            string classNamespace, className;
            var classes = new List<ClassInfo>();

            foreach (ClassDeclarationSyntax classDecl in compilationUnit.DescendantNodes().OfType<ClassDeclarationSyntax>())
            {
                classNamespace = ((NamespaceDeclarationSyntax)classDecl.Parent).Name.ToString();
                className = classDecl.Identifier.ValueText;
                classes.Add(new ClassInfo(className, classNamespace, GetMethods(classDecl)));
            }

            return classes;
        }

        private List<MethodInfo> GetMethods(ClassDeclarationSyntax classDecl)
        {
            string methodName;
            var classMethods = new List<MethodInfo>();
            
            foreach (MethodDeclarationSyntax methodDecl in classDecl.DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Where(methodDecl => methodDecl.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.PublicKeyword))))
            {
                methodName = methodDecl.Identifier.ValueText;
                classMethods.Add(new MethodInfo(methodName));
            }

            return classMethods;
        }
    }
}

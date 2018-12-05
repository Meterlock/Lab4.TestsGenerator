using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestsGeneratorLibrary
{
    public class SourceCodeParcer
    {
        public ParcingInfo Parce(string sourceCode)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
            return new ParcingInfo(GetClasses(root));
        }

        private List<ClassInfo> GetClasses(CompilationUnitSyntax root)
        {
            string className, classNamespace;
            List<ClassInfo> classes = new List<ClassInfo>();

            foreach (ClassDeclarationSyntax classDeclaration in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
            {
                classNamespace = ((NamespaceDeclarationSyntax)classDeclaration.Parent).Name.ToString();
                className = classDeclaration.Identifier.ValueText;
                classes.Add(new ClassInfo(className, classNamespace, GetMethods(classDeclaration)));
            }
            return classes;
        }

        private List<MethodInfo> GetMethods(ClassDeclarationSyntax classDeclaration)
        {
            string methodName;
            List<MethodInfo> classMethods = new List<MethodInfo>();

            // public only
            foreach (MethodDeclarationSyntax methodDeclaration in classDeclaration.DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Where((methodDeclaration) => methodDeclaration.Modifiers.Any((modifier) => modifier.IsKind(SyntaxKind.PublicKeyword))))
            {
                methodName = methodDeclaration.Identifier.ValueText;
                classMethods.Add(new MethodInfo(methodName));
            }
            return classMethods;
        }
    }
}

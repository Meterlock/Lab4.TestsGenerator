using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace TestsGeneratorLibrary
{
    public class TemplateGenerator
    {
        public List<TestInfo> MakeTemplates(ParcingInfo parsingInfo)
        {
            string fileName, content;
            List<TestInfo> res = new List<TestInfo>();

            foreach (ClassInfo classInfo in parsingInfo.Classes)
            {
                CompilationUnitSyntax cus = CompilationUnit()
                    .WithUsings(GetUsingDeclarations(classInfo))
                    .WithMembers(SingletonList<MemberDeclarationSyntax>(GetNamespaceDeclaration(classInfo)
                        .WithMembers(SingletonList<MemberDeclarationSyntax>(ClassDeclaration(classInfo.ClassName + "Tests")
                            .WithAttributeLists(
                                SingletonList<AttributeListSyntax>(
                                    AttributeList(
                                        SingletonSeparatedList<AttributeSyntax>(
                                            Attribute(
                                                IdentifierName("TestClass"))))))
                            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                            .WithMembers(GetMembersDeclarations(classInfo))))
                        )
                     );

                fileName = classInfo.ClassName + "Test.cs";
                content = cus.NormalizeWhitespace().ToFullString();
                res.Add(new TestInfo(fileName, content));
            }

            return res;
        }

        private NamespaceDeclarationSyntax GetNamespaceDeclaration(ClassInfo classInfo)
        {
            NamespaceDeclarationSyntax namespaceDecl = NamespaceDeclaration(QualifiedName(
                IdentifierName(classInfo.ClassNamespace), IdentifierName("Tests")));

            return namespaceDecl;
        }

        private SyntaxList<UsingDirectiveSyntax> GetUsingDeclarations(ClassInfo classInfo)
        {
            List<UsingDirectiveSyntax> usings = new List<UsingDirectiveSyntax>();

            usings.Add(UsingDirective(IdentifierName("System")));
            usings.Add(UsingDirective(IdentifierName("System.Collections.Generic")));
            usings.Add(UsingDirective(IdentifierName("System.Linq")));
            usings.Add(UsingDirective(IdentifierName("System.Text")));
            usings.Add(UsingDirective(IdentifierName("Microsoft.VisualStudio.TestTools.UnitTesting")));
            usings.Add(UsingDirective(IdentifierName(classInfo.ClassNamespace)));

            return new SyntaxList<UsingDirectiveSyntax>(usings);
        }

        private SyntaxList<MemberDeclarationSyntax> GetMembersDeclarations(ClassInfo classInfo)
        {
            List<MemberDeclarationSyntax> methods = new List<MemberDeclarationSyntax>();

            foreach (MethodInfo methodInfo in classInfo.ClassMethods)
            {
                methods.Add(GetMethodDeclaration(methodInfo));
            }

            return new SyntaxList<MemberDeclarationSyntax>(methods);
        }

        private MethodDeclarationSyntax GetMethodDeclaration(MethodInfo method)
        {
            MethodDeclarationSyntax methodDecl;
            List<StatementSyntax> bodyMembers = new List<StatementSyntax>();

            bodyMembers.Add(
                ExpressionStatement(
                    InvocationExpression(
                        GetAssertFail())
                    .WithArgumentList(GetMemberArgs())));

            methodDecl = MethodDeclaration(
                PredefinedType(
                    Token(SyntaxKind.VoidKeyword)),
                Identifier(method.MethodName + "Test"))
                .WithAttributeLists(
                    SingletonList<AttributeListSyntax>(
                        AttributeList(
                            SingletonSeparatedList<AttributeSyntax>(
                                Attribute(
                                    IdentifierName("TestMethod"))))))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBody(Block(bodyMembers));

            return methodDecl;
        }

        private MemberAccessExpressionSyntax GetAssertFail()
        {
            MemberAccessExpressionSyntax fail = MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                IdentifierName("Assert"),
                IdentifierName("Fail"));
            return fail;
        }

        private ArgumentListSyntax GetMemberArgs()
        {
            ArgumentListSyntax arguments = ArgumentList(
                SingletonSeparatedList(
                    Argument(
                        LiteralExpression(
                            SyntaxKind.StringLiteralExpression,
                            Literal("autogenerated")))));
            return arguments;
        }
    }
}

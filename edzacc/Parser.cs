using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace edzacc
{
    public class Parser : IParser
    {
        DocumentRoot IParser.Parse(string text)
        {
            DocumentRoot root = new DocumentRoot();

            var notEmpty = new Func<string, bool>(s => !string.IsNullOrEmpty(s));

            var words = text.Split(' ', '\t', '\r', '\n').Select(i => i.Trim()).Where(notEmpty);

            BuildTokenTree(root, new List<string>(words));

            return root;
        }

        private void BuildTokenTree(TokenNode parent, List<string> remainder)
        {
            while (remainder.Count > 0)
            {
                Token targetToken = DetermineTarget(remainder, parent);

                if (targetToken is EmptyToken)
                    return;

                var node = new TokenNode(targetToken) { Parent = parent };

                if (HasInnerToken(targetToken))
                {
                    CompleteUntilInner(targetToken, node, remainder);
                    BuildTokenTree(node, remainder);
                    CompleteAfterInner(targetToken, node, remainder);
                    parent.Children.Add(node);
                }
                else
                {
                    parent.Children.Add(node);
                }
            }
        }

        private void CompleteAfterInner(Token targetToken, TokenNode node, List<string> remainder)
        {
            if (targetToken is FunctionToken)
            {
                if (remainder[0] != "}")
                    throw new InvalidDataException();

                remainder.RemoveRange(0, 1);
            }
            else if (targetToken is DeclarationToken)
            {
                if (remainder[0] != ";")
                    throw new InvalidDataException();

                remainder.RemoveRange(0, 1);
            }
        }

        private void CompleteUntilInner(Token targetToken, TokenNode node, List<string> remainder)
        {
            if (targetToken is FunctionToken)
            {
                var token = (FunctionToken)targetToken;

                token.ReturnType = remainder[0];
                token.Name = new string(remainder[1].TakeWhile(c => c != '(').ToArray());
                remainder.RemoveRange(0, 3);
            }
            else if (targetToken is DeclarationToken)
            {
                var token = (DeclarationToken)targetToken;

                token.Type = remainder[0];
                token.Identifier = remainder[1];
                remainder.RemoveRange(0, 2);
            }
        }

        private bool HasInnerToken(Token targetToken)
        {
            if ((new[] {typeof(FunctionToken), typeof(DeclarationToken)}).Contains(targetToken.GetType()))
                return true;

            return false;
        }

        private Token DetermineTarget(List<string> remainder, TokenNode parent)
        {
            // is function
            if (IsDataType(remainder[0]) && IsParameterListOpening(remainder[1]))
                return new FunctionToken();

            if(IsDataType(remainder[0]) && IsIdentifierWithSemicolon(remainder[1] + remainder[2]))
                return new DeclarationToken();

            if (remainder[0] == "}" || remainder[0] == ";")
                return new EmptyToken();

            throw new InvalidDataException();
        }

        private bool IsIdentifierWithSemicolon(string s)
        {
            return Regex.IsMatch(s, "^[a-zA-Z]+;$");
        }

        private bool IsParameterListOpening(string s)
        {
            if (s.Count(c => c == '(') == 1 && s.Count(c => c == ')') <= 1)
                return true;

            return false;
        }

        private bool IsDataType(string s)
        {
            if (s == "int")
                return true;

            return false;
        }
    }
}

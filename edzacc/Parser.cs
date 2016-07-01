using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace edzacc
{
    public class Parser : IParser
    {
        DocumentRoot IParser.Parse(string text)
        {
            DocumentRoot root = new DocumentRoot();

            var words = text.Split(' ', '\t', '\r', '\n').Select(i => i.Trim());

            BuildTokenTree(root, new List<string>(words));

            return root;
        }

        private void BuildTokenTree(TokenNode parent, List<string> remainder)
        {
            while (remainder.Count > 0)
            {
                Token targetToken = DetermineTarget(remainder);
                var node = new TokenNode(targetToken) { Parent = parent };

                if (HasInnerToken(targetToken))
                {
                    CompleteUntilInner(targetToken, node, remainder);
                    BuildTokenTree(node, remainder);
                    CompleteAfterInner(targetToken, node, remainder);
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
                if(remainder[0] != "}")
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
                token.Name = remainder[1].TakeWhile(c => c != '(').ToString();
                remainder.RemoveRange(0, 2);
            }
        }

        private bool HasInnerToken(Token targetToken)
        {
            if (targetToken is FunctionToken)
                return true;

            return false;
        }

        private FunctionToken DetermineTarget(List<string> remainder)
        {
            // is function
            if (IsDataType(remainder[0]) && IsParameterListOpening(remainder[1]))
                return new FunctionToken();

            throw new InvalidDataException();
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

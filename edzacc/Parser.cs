using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace edzacc
{
    public class Parser : IParser
    {
        TokenTreeRoot IParser.Parse(string text)
        {
            var sr = new StringReader(text);
            var root = new TokenTreeRoot();

            using (sr)
            {
                var curr = new StringBuilder();
                var unresolvedTokens = new List<Token>();

                while (true)
                {
                    var res = sr.Read();

                    if (res == -1)
                        throw new InvalidDataException();

                    var c = (char) res;

                    if (c != ' ' || c != '\t' || c != '\r' || c != '\n')
                    {
                        curr.Append(c);
                    }
                    else
                    {
                        // Token complete
                        unresolvedTokens.Add(ResolveWord(curr.ToString()));

                        List<Token> resolvedTokens;
                        bool success = TryCompleteUnresolvedTokens(out resolvedTokens, unresolvedTokens);

                        if (success)
                        {
                            root.Children = resolvedTokens;
                            break;
                        }
                    }
                }
            }

            return root;
        }

        private bool TryCompleteUnresolvedTokens(out List<Token> resolvedTokens, List<Token> unresolvedTokens)
        {
            throw new NotImplementedException();
        }

        private Token ResolveWord(string word)
        {
            throw new NotImplementedException();

        }
    }
}

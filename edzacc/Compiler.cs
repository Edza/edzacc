using System.IO;
using System.Linq;

namespace edzacc
{
    public class Compiler
    {
        //.386 
        //.model flat, stdcall 
        //.data
        //i dw
        //.code
        //start:
        //mov eax, offset i
        //mov[eax], 10
        //end start

        public string Compile(DocumentRoot tokenTree)
        {
            var s = "";

            var node = tokenTree.Children[0];
            var f = tokenTree.Children[0].Token as FunctionToken;

            if (f != null && f.Name == "main")
            {
                s += @"
.386 
.model flat,stdcall 
.data ";

                foreach (var token in node.Children.Where(c => c.Token is DeclarationToken))
                {
                    var declarationToken = token.Token as DeclarationToken;
                    s += $"\r\n{declarationToken.Identifier} ";
                    string a;

                    if (declarationToken.Type == "int")
                        a = "dw";
                    else
                        throw new InvalidDataException();

                    s += a;
                }

                s += "\r\n.code\r\nstart:";

                foreach (var token in node.Children.Where(c => c.Token is AssignmetToken))
                {
                    var assignmetToken = token.Token as AssignmetToken;
                    s += $"\r\nmov eax, offset " + assignmetToken.Destination;
                    s += "\r\nmov [eax], " + assignmetToken.Immediate;
                }

                s += @"
end start
";
            }

            return s;
        }
    }
}
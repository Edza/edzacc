using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edzacc
{
    public class TokenNode
    {
        public TokenNode()
        {
            // parameterless contructor
        }
        
        public TokenNode(Token t)
        {
            Token = t;
        }

        public Token Token;

        public virtual TokenNode Parent
        {
            get;
            set;
        } = null;

        public List<TokenNode> Children = new List<TokenNode>();
    }

    public class DocumentRoot : TokenNode
    {
        public override TokenNode Parent
        {
            get
            {
                throw new InvalidOperationException();
            }
        }
    }
}

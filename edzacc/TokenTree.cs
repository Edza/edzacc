using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edzacc
{
    public class TokenNode
    {
        public virtual Token Parent
        {
            get;
            set;
        } = null;

        public List<Token> Children = new List<Token>();
    }

    public class TokenTreeRoot : TokenNode
    {
        public override Token Parent
        {
            get
            {
                throw new InvalidOperationException();
            }
        }
    }
}

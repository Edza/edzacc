using System.Collections.Generic;

namespace edzacc
{
    public abstract class Token
    {
        // empty class
    }

    public class DeclarationToken : Token
    {
        public string Identifier;
        public string Type;
    }

    public class AssignmetToken : Token
    {
        public string Destination;
        public int Immediate;
    }

    public class FunctionToken : Token
    {
        public List<Token> Body;
        public string Name;
        public string ReturnType;
    }

    public class EmptyToken : FunctionToken
    {
        // empty class
    }


    //public class DataTypeToken
    //{
    //    public string Name;
    //}
}
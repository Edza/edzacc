﻿using System.Collections.Generic;

namespace edzacc
{
    public abstract class Token
    {
    }

    public class FunctionToken : Token
    {
        public List<Token> Body;
        public string Name;
        public string ReturnType;
    }

    //public class DataTypeToken
    //{
    //    public string Name;
    //}
}
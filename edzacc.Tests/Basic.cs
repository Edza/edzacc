using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace edzacc.Tests
{
    [TestClass]
    public class Basic
    {
        private IParser _parser;

        [TestInitialize]
        public void Init()
        {
            _parser = new Parser();
        }

        [TestMethod]
        public void SingleEmptyEntryPoint()
        {
            string s = @"
int main() {

}
";
            DocumentRoot root = _parser.Parse(s);
        }

        [TestMethod]
        public void IntDeclaration()
        {
            string s = @"
int main() {
int i ;
}
";
            DocumentRoot root = _parser.Parse(s);
        }
    }
}

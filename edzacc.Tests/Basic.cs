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

            string r = @"
.386 
.model flat,stdcall 
.data 
.code
start:
end start
";

            Assert.AreEqual(r, s);
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

            string r = @"
.386 
.model flat,stdcall 
.data 
i dw
.code
start:
end start
";
            Assert.AreEqual(r, s);
        }

        [TestMethod]
        public void IntAssignment()
        {
            string s = @"
int main() {
int i ;
i = 10 ;
}
";
            DocumentRoot root = _parser.Parse(s);

            string r = @"
.386 
.model flat,stdcall 
.data 
i dw
.code
start:
mov eax, offset i
mov [eax], 10
end start
";
            Assert.AreEqual(r, s);
        }
    }
}

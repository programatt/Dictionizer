using System.Collections.Generic;
using NUnit.Framework;


namespace Dictionizer.Tests
{
    class Simple
    {
        public string Prop1 { get; set; }
        public long Prop2 { get; set; }
    }

    class Complex
    {
        public Simple Simple1 { get; set; }
        public string Prop1 { get; set; }
    }

    [TestFixture]
    public class ObjectConverterTests
    {
        [Test]
        public void SimpleClassConversionIsSuccessful()
        {
            var testObj = new Simple
                              {
                                  Prop1 = "Hello",
                                  Prop2 = 100
                              };
            var result = testObj.ToDictionary();
            Assert.IsInstanceOf<IDictionary<string, object>>(result);
            Assert.AreEqual("Hello",result["Prop1"]);
            Assert.AreEqual(100,result["Prop2"]);
        }

        [Test]
        public void ComplexClassConversionWithoutDeepCopy()
        {
            var testObj = new Complex
                              {
                                  Simple1 = new Simple
                                                {
                                                    Prop1 = "Hello",
                                                    Prop2 = 100   
                                                },
                                  Prop1 = "Hello"
                              };

            var result = testObj.ToDictionary();
            Assert.IsInstanceOf<IDictionary<string, object>>(result);
            Assert.AreEqual("Hello",result["Prop1"]);
            Assert.IsInstanceOf<Simple>(result["Simple1"]);
        }

        [Test]
        public void ComplexClassConversionWithDeepCopy()
        {
            var testObj = new Complex
            {
                Simple1 = new Simple
                {
                    Prop1 = "Hello",
                    Prop2 = 100
                },
                Prop1 = "Hello"
            };

            var result = testObj.ToDictionary(true);
            Assert.IsInstanceOf<IDictionary<string, object>>(result);
            Assert.IsInstanceOf<IDictionary<string,object>>(result["Simple1"]);
            Assert.AreEqual("Hello", result["Prop1"]);
            
        }
    }
}

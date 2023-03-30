using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class ListHelpersTests : MonoBehaviour
{
    public class TestClass
    {
        public int Foo;
    }

    private void Awake()
    {
        {
            var actual = ListHelpers.MinBy(new List<TestClass> {}, null);
            Assert.AreEqual(null, actual);
        }

        {
            var actual = ListHelpers.MinBy(new List<int> {}, null);
            Assert.AreEqual(0, actual);
        }
    }
}

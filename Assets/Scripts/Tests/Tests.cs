using System.Collections;
using UnityEngine.TestTools;
using NUnit.Framework;
using CakeApp;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [UnityTest]
        public IEnumerator _Return_Data_From_Request()
        {
            string response = "";
            yield return NetworkAPI.GetJson(output => response = output);
            Assert.IsNotEmpty(response);
        }

        [UnityTest]
        public IEnumerator _Return_Image_From_Request()
        {
            //todo: mke this
            yield return null;
        }
    }
}


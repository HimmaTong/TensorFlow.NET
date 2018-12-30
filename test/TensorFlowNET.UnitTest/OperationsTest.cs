﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Tensorflow;
using Buffer = Tensorflow.Buffer;

namespace TensorFlowNET.UnitTest
{
    [TestClass]
    public class OperationsTest
    {
        /// <summary>
        /// Port from tensorflow\c\c_api_test.cc
        /// `TEST(CAPI, GetAllOpList)`
        /// </summary>
        [TestMethod]
        public void c_api_GetAllOpList()
        {
            var handle = c_api.TF_GetAllOpList();
            var buffer = new Buffer(handle);
            Assert.IsTrue(buffer.Length == buffer.Data.Length);
        }

        [TestMethod]
        public void addInPlaceholder()
        {
            var a = tf.placeholder(tf.float32);
            var b = tf.placeholder(tf.float32);
            var c = tf.add(a, b);

            using(var sess = tf.Session())
            {
                var feed_dict = new Dictionary<Tensor, object>();
                feed_dict.Add(a, 3.0f);
                feed_dict.Add(b, 2.0f);

                var o = sess.run(c, feed_dict);
            }
        }

        [TestMethod]
        public void addInConstant()
        {
            var a = tf.constant(4.0f);
            var b = tf.placeholder(tf.float32);
            var c = tf.add(a, b);

            using (var sess = tf.Session())
            {
                var o = sess.run(c);
                Assert.AreEqual(o, 9.0f);
            }
        }
    }
}

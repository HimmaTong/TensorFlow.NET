﻿using NumSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Tensorflow;
using np = NumSharp.Core.NumPy;
using tensor_pb2 = Tensorflow;

namespace TensorFlowNET.Core
{
    public static class tensor_util
    {
        public static TensorProto make_tensor_proto(object values, Type dtype = null)
        {
            NDArray nparray;
            TensorProto tensor_proto = null;
            TensorShape tensor_shape = new TensorShape();

            switch (values)
            {
                case double val:
                    nparray = np.array(new double[] { val }, np.float64);
                    tensor_proto = new tensor_pb2.TensorProto
                    {
                        Dtype = DataType.DtDouble,
                        TensorShape = tensor_shape.as_shape().as_proto()
                    };
                    tensor_proto.DoubleVal.Add(val);
                    break;

                case string val:
                    nparray = np.array(new string[] { val }, np.chars);
                    tensor_proto = new tensor_pb2.TensorProto
                    {
                        Dtype = DataType.DtString,
                        TensorShape = tensor_shape.as_shape().as_proto()
                    };
                    tensor_proto.StringVal.Add(Google.Protobuf.ByteString.CopyFrom(val, Encoding.UTF8));
                    break;
            }

            return tensor_proto;
        }
    }
}

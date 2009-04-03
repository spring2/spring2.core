#if (NET_1_1)
#else
using System;

using NUnit.Framework;
using Spring2.Core.Types;
using System.IO;
using System.Runtime.Serialization;

namespace Spring2.Core.Test {

    /// <summary>
    /// Tests for BooleanType
    /// </summary>
    [TestFixture]
    public class DataTypeSerializationTest {

	[Test]
		public void TestBooleanTypeSerialization() {
		 //Needs 3.0 framework.
		 
		 /*
            NetDataContractSerializer serializer = new NetDataContractSerializer();
            
            Stream saveStream = File.Create("save.dat");

            //save code 
			BooleanSerializationClass before = new BooleanSerializationClass();
			before.BUnset = BooleanType.UNSET;
			before.BDefault = BooleanType.DEFAULT;
			before.BTrue = BooleanType.TRUE;
			before.BFalse = BooleanType.FALSE;
            serializer.Serialize(saveStream, before);
            saveStream.Close();

            //and my load is this 
            Stream loadStream = File.OpenRead("save.dat");
            //load code 
            BooleanSerializationClass after = (BooleanSerializationClass)(serializer.Deserialize(loadStream));
            loadStream.Close();

			Assert.AreEqual(BooleanType.UNSET, after.BUnset);
			Assert.AreEqual(BooleanType.DEFAULT, after.BDefault);
			Assert.AreEqual(BooleanType.TRUE, after.BTrue);
			Assert.AreEqual(BooleanType.FALSE, after.BFalse);
			*/
		}
		
    }
	
	[Serializable]
	public class BooleanSerializationClass {
		private BooleanType bUnset = BooleanType.DEFAULT;
		private BooleanType bDefault = BooleanType.DEFAULT;
		private BooleanType bTrue = BooleanType.TRUE;
		private BooleanType bFalse = BooleanType.FALSE;
		
		public BooleanType BUnset {
			get { return bUnset; }
			set { bUnset = value; }
		}
		
		public BooleanType BDefault {
			get { return bDefault; }
			set { bDefault = value; }
		}
		
		public BooleanType BTrue {
			get { return bTrue; }
			set { bTrue = value; }
		}
		
		public BooleanType BFalse {
			get { return bFalse; }
			set { bFalse = value; }
		}
	}
}
#endif

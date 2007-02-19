using System;
using System.Collections.Specialized;

using NUnit.Framework;

using Spring2.Core.Message;
using Spring2.Core.PropertyPopulator;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    [TestFixture]
    public class PropertyPopulatorTest {

	[Test]
	public void ShouldBeAbleToPopulateSomeProperties() {
	    //set up NameValueCollection
	    NameValueCollection collection = new NameValueCollection();
	    collection.Add("systemString", "value1");
	    collection.Add("systemBool", "true");
	    collection.Add("systemDecimal", "1.1");
	    collection.Add("systemSByte", "1");
	    collection.Add("systemByte", "1");
	    collection.Add("systemShort", "1");
	    collection.Add("systemUShort", "1");
	    collection.Add("systemInt", "1");
	    collection.Add("systemUInt", "1");
	    collection.Add("systemLong", "1");
	    collection.Add("systemULong", "1");
	    collection.Add("systemChar", "a");
	    collection.Add("systemFloat", "1");
	    collection.Add("systemDouble", "1");
	    collection.Add("spring2BooleanType", "Y");
	    collection.Add("spring2CurrencyType", "1.01");
	    collection.Add("spring2CurrencyTypeList", "1.01,2.02");
	    collection.Add("spring2DateTimeType", "1/1/2005 1:01");
	    collection.Add("spring2DateType", "1/1/2005");
	    collection.Add("spring2DateTypeList", "1/1/2005,2/2/2005");
	    collection.Add("spring2DecimalType", "1.1");
	    collection.Add("spring2DecimalTypeList", "1.1,2.2");
	    collection.Add("spring2GenderType", "M");
	    collection.Add("spring2IdType", "1");
	    collection.Add("spring2IdTypeList", "1,2");
	    collection.Add("spring2IntegerType", "1");
	    collection.Add("spring2IntegerTypeList", "1,2");
	    collection.Add("spring2LongType", "1");
	    collection.Add("spring2PhoneNumberType", "(801)555-6666");
	    collection.Add("spring2QuantityType", "1");
	    collection.Add("spring2ShortType", "1");
	    collection.Add("spring2StringType", "value2");
	    collection.Add("spring2StringTypeList", "value2,value3");
	    collection.Add("spring2TimeType", "11:11");
	    collection.Add("spring2LanguageEnum", LanguageEnum.VIETNAMESE.Code);

	    //run populator
	    SampleDataForm data = new SampleDataForm();
	    Populator.Instance.Populate(data, collection);

	    //assert object was populated
	    Assert.AreEqual("value1", data.SystemString);
	    Assert.AreEqual(true, data.SystemBool);
	    Assert.AreEqual(1.1, data.SystemDecimal);
	    Assert.AreEqual(1, data.SystemSByte);
	    Assert.AreEqual(1, data.SystemByte);
	    Assert.AreEqual(1, data.SystemShort);
	    Assert.AreEqual(1, data.SystemUShort);
	    Assert.AreEqual(1, data.SystemInt);
	    Assert.AreEqual(1, data.SystemUInt);
	    Assert.AreEqual(1, data.SystemLong);
	    Assert.AreEqual(1, data.SystemULong);
	    Assert.AreEqual('a', data.SystemChar);
	    Assert.AreEqual(1, data.SystemFloat);
	    Assert.AreEqual(1, data.SystemDouble);
	    Assert.AreEqual(BooleanType.TRUE, data.Spring2BooleanType);
	    Assert.AreEqual(new CurrencyType(1.01), data.Spring2CurrencyType);
	    Assert.AreEqual(2, data.Spring2CurrencyTypeList.Count);
	    Assert.IsTrue(data.Spring2CurrencyTypeList.Contains(new CurrencyType(1.01)));
	    Assert.IsTrue(data.Spring2CurrencyTypeList.Contains(new CurrencyType(2.02)));
	    Assert.AreEqual(new DateTimeType(2005, 1, 1, 1, 1, 0), data.Spring2DateTimeType);
	    Assert.AreEqual(new DateType(2005,1,1), data.Spring2DateType);
	    Assert.AreEqual(2, data.Spring2DateTypeList.Count);
	    Assert.IsTrue(data.Spring2DateTypeList.Contains(new DateType(2005, 1, 1)));
	    Assert.IsTrue(data.Spring2DateTypeList.Contains(new DateType(2005, 2, 2)));
	    Assert.AreEqual(new DecimalType(1.1), data.Spring2DecimalType);
	    Assert.AreEqual(2, data.Spring2DecimalTypeList.Count);
	    Assert.IsTrue(data.Spring2DecimalTypeList.Contains(new DecimalType(1.1)));
	    Assert.IsTrue(data.Spring2DecimalTypeList.Contains(new DecimalType(1.1)));
	    Assert.AreEqual(GenderType.MALE, data.Spring2GenderType);
	    Assert.AreEqual(new IdType(1), data.Spring2IdType);
	    Assert.AreEqual(2, data.Spring2IdTypeList.Count);
	    Assert.IsTrue(data.Spring2IdTypeList.Contains(new IdType(1)));
	    Assert.IsTrue(data.Spring2IdTypeList.Contains(new IdType(2)));
	    Assert.AreEqual(new IntegerType(1), data.Spring2IntegerType);
	    Assert.AreEqual(2, data.Spring2IntegerTypeList.Count);
	    Assert.IsTrue(data.Spring2IntegerTypeList.Contains(new IntegerType(1)));
	    Assert.IsTrue(data.Spring2IntegerTypeList.Contains(new IntegerType(2)));
	    Assert.AreEqual(new LongType(1), data.Spring2LongType);
	    Assert.AreEqual(new PhoneNumberType("801", "555", "6666", String.Empty), data.Spring2PhoneNumberType);
	    Assert.AreEqual(new QuantityType(1), data.Spring2QuantityType);
	    Assert.AreEqual(new ShortType(1), data.Spring2ShortType);
	    Assert.AreEqual(new StringType("value2"), data.Spring2StringType);
	    Assert.AreEqual(2, data.Spring2StringTypeList.Count);
	    Assert.IsTrue(data.Spring2StringTypeList.Contains(new StringType("value2")));
	    Assert.IsTrue(data.Spring2StringTypeList.Contains(new StringType("value3")));
	    Assert.AreEqual(new TimeType(11,11,0), data.Spring2TimeType);
	    Assert.AreEqual(LanguageEnum.VIETNAMESE, data.Spring2LanguageEnum);
	}

	[Test()]
	public void ShouldNotDieIfPropertyNotInCollection() {
	    //set up NameValueCollection
	    NameValueCollection collection = new NameValueCollection();
	    collection.Add("systemString", "value1");
	    collection.Add("spring2LanguageEnum", LanguageEnum.VIETNAMESE.Code);

	    //run populator
	    SampleDataForm data = new SampleDataForm();
	    Populator.Instance.Populate(data, collection);

	    //assert object was populated
	    Assert.AreEqual("value1", data.SystemString);
	    Assert.AreEqual(LanguageEnum.VIETNAMESE, data.Spring2LanguageEnum);
	}

	[Test()]
	public void ShouldNotBlowUpIfNameValueCollectionIsNull() {
	    NameValueCollection collection = null;

	    SampleDataForm data = new SampleDataForm();
	    Populator.Instance.Populate(data, collection);
	}

	[Test()]
	public void ShouldBeAbleToPopulateObjectUsingPrefix() {
	    //set up NameValueCollection with prefix
	    String prefix = "prefix";
	    NameValueCollection collection = new NameValueCollection();
	    collection.Add(prefix + "systemString", "value1");
	    collection.Add(prefix + "spring2StringType", "value2");
	    collection.Add("spring2IdType", "1");

	    //run populator
	    SampleDataForm data = new SampleDataForm();
	    Populator.Instance.Populate(data, collection, prefix);

	    //assert object was populated correctly
	    Assert.AreEqual("value1", data.SystemString);
	    Assert.AreEqual(new StringType("value2"), data.Spring2StringType);
	    Assert.IsTrue(data.Spring2IdType.IsDefault);
	}

	[Test()]
	public void ShouldGetMessageListWhenParseException() {
	    //set up NameValueCollection
	    NameValueCollection collection = new NameValueCollection();
	    collection.Add("spring2IdType", "value1");

	    //run populator
	    SampleDataForm data = new SampleDataForm();
	    MessageList errors = Populator.Instance.Populate(data, collection);
	   
	    Assert.IsTrue(errors.Count.Equals(1));
	    Assert.IsTrue(errors[0].GetType().Name.Equals("InvalidTypeFormatError"));
	}

	[Test()]
	public void ShouldGetMessageListWhenMissingRequiredField() {
	    //set up NameValueCollection
	    NameValueCollection collection = new NameValueCollection();

	    //run populator
	    SampleClassWithRequiredField data = new SampleClassWithRequiredField();
	    MessageList errors = Populator.Instance.Populate(data, collection);
	   
	    Assert.IsTrue(errors.Count.Equals(5));
	    Assert.IsTrue(errors[0].GetType().Name.Equals("MissingRequiredFieldError"));
	    Assert.IsTrue(errors[1].GetType().Name.Equals("MissingRequiredFieldError"));
	    Assert.IsTrue(errors[2].GetType().Name.Equals("MissingRequiredFieldError"));
	    Assert.IsTrue(errors[3].GetType().Name.Equals("MissingRequiredFieldError"));
	    Assert.IsTrue(errors[4].GetType().Name.Equals("MissingRequiredFieldError"));
	}

	[Test()]
	public void ShouldBeAbleToPopulateRequiredProperties() {
	    //set up NameValueCollection
	    NameValueCollection collection = new NameValueCollection();
	    collection.Add("systemString", "value1");
	    collection.Add("systemInt", "1");
	    collection.Add("spring2IntegerType", "1");
	    collection.Add("spring2StringType", "value2");
	    collection.Add("spring2LanguageEnum", LanguageEnum.VIETNAMESE.Code);

	    //run populator
	    SampleClassWithRequiredField data = new SampleClassWithRequiredField();
	    MessageList errors = Populator.Instance.Populate(data, collection);
	   
	    Assert.IsTrue(errors.Count.Equals(0));
	    Assert.AreEqual("value1", data.SystemString);
	    Assert.AreEqual(1, data.SystemInt);
	    Assert.AreEqual(new IntegerType(1), data.Spring2IntegerType);
	    Assert.AreEqual(new StringType("value2"), data.Spring2StringType);
	    Assert.AreEqual(LanguageEnum.VIETNAMESE, data.Spring2LanguageEnum);
	}

	//if the value is an empty string and is not required then no problem
	[Test()]
	public void ShouldNotGetMessageListWhenPropertyIsNotRequiredAndTypeIsStringType() {
	    //set up NameValueCollection
	    NameValueCollection collection = new NameValueCollection();
	    collection.Add("systemString", "value1");
	    collection.Add("systemInt", "1");
	    collection.Add("spring2IntegerType", "1");
	    collection.Add("spring2StringType", "");
	    collection.Add("spring2LanguageEnum", LanguageEnum.VIETNAMESE.Code);

	    //run populator
	    SampleClassWithRequiredField data = new SampleClassWithRequiredField();
	    MessageList errors = Populator.Instance.Populate(data, collection);
	   
	    Assert.IsTrue(errors.Count.Equals(0));
	    Assert.AreEqual("value1", data.SystemString);
	    Assert.AreEqual(1, data.SystemInt);
	    Assert.AreEqual(new IntegerType(1), data.Spring2IntegerType);
	    Assert.AreEqual(new StringType(""), data.Spring2StringType);
	    Assert.AreEqual(LanguageEnum.VIETNAMESE, data.Spring2LanguageEnum);
	}
	
	[Test()]
	public void ShouldNotGetMessageListWhenPropertyIsNotRequiredAndTypeIsString() {
	    //set up NameValueCollection
	    NameValueCollection collection = new NameValueCollection();
	    collection.Add("systemString", "");
	    collection.Add("systemInt", "1");
	    collection.Add("spring2IntegerType", "1");
	    collection.Add("spring2StringType", "value2");
	    collection.Add("spring2LanguageEnum", LanguageEnum.VIETNAMESE.Code);

	    //run populator
	    SampleClassWithRequiredField data = new SampleClassWithRequiredField();
	    MessageList errors = Populator.Instance.Populate(data, collection);
	   
	    Assert.IsTrue(errors.Count.Equals(0));
	    Assert.AreEqual("", data.SystemString);
	    Assert.AreEqual(1, data.SystemInt);
	    Assert.AreEqual(new IntegerType(1), data.Spring2IntegerType);
	    Assert.AreEqual(new StringType("value2"), data.Spring2StringType);
	    Assert.AreEqual(LanguageEnum.VIETNAMESE, data.Spring2LanguageEnum);
	}

	[Test()]
	public void IfParseErrorOnRequiredFieldShouldGetMissingRequiredFieldError() {
	    //set up NameValueCollection
	    NameValueCollection collection = new NameValueCollection();
	    collection.Add("systemString", "value1");
	    collection.Add("systemInt", "1");
	    collection.Add("spring2IntegerType", "ONE");
	    collection.Add("spring2StringType", "value2");
	    collection.Add("spring2LanguageEnum", LanguageEnum.VIETNAMESE.Code);

	    //run populator
	    SampleClassWithRequiredField data = new SampleClassWithRequiredField();
	    MessageList errors = Populator.Instance.Populate(data, collection);
	   
	    Assert.IsTrue(errors.Count.Equals(1));
	    Assert.IsTrue(errors[0].GetType().Name.Equals("MissingRequiredFieldError"));
	    Assert.AreEqual("value1", data.SystemString);
	    Assert.AreEqual(1, data.SystemInt);
	    Assert.IsTrue(data.Spring2IntegerType.IsDefault);
	    Assert.AreEqual(new StringType("value2"), data.Spring2StringType);
	    Assert.AreEqual(LanguageEnum.VIETNAMESE, data.Spring2LanguageEnum);
	}
    	
	[Test()]
	public void ShouldNotAttemptToPopulateReadOnlyProperty() {
	    NameValueCollection collection = new NameValueCollection();
	    collection.Add("ReadOnlySystemString", "value1");
		
	    SampleDataForm data = new SampleDataForm();
	    Populator.Instance.Populate(data, collection);
	}


	public class SampleDataForm {
	    private string systemString;
	    private bool systemBool;
	    private decimal systemDecimal;
	    private sbyte systemSByte;
	    private byte systemByte;
	    private short systemShort;
	    private ushort systemUShort;
	    private int systemInt;
	    private uint systemUInt;
	    private long systemLong;
	    private ulong systemULong;
	    private char systemChar;
	    private float systemFloat;
	    private double systemDouble;
	    private BooleanType spring2BooleanType;
	    private CurrencyType spring2CurrencyType;
	    private CurrencyTypeList spring2CurrencyTypeList;
	    private DateTimeType spring2DateTimeType;
	    private DateType spring2DateType;
	    private DateTypeList spring2DateTypeList;
	    private DecimalType spring2DecimalType;
	    private DecimalTypeList spring2DecimalTypeList;
	    private GenderType spring2GenderType;
	    private IdType spring2IdType;
	    private IdTypeList spring2IdTypeList;
	    private IntegerType spring2IntegerType;
	    private IntegerTypeList spring2IntegerTypeList;
	    private LongType spring2LongType;
	    private PhoneNumberType spring2PhoneNumberType;
	    private QuantityType spring2QuantityType;
	    private ShortType spring2ShortType;
	    private StringType spring2StringType;
	    private StringTypeList spring2StringTypeList;
	    private TimeType spring2TimeType;
	    private LanguageEnum spring2LanguageEnum;

	    public string SystemString {
		get { return systemString; }
		set { systemString = value; }
	    }
	    public bool SystemBool {
		get { return systemBool; }
		set { systemBool = value; }
	    }
	    public decimal SystemDecimal {
		get { return systemDecimal; }
		set { systemDecimal = value; }
	    }
	    public sbyte SystemSByte {
		get { return systemSByte; }
		set { systemSByte = value; }
	    }
	    public byte SystemByte {
		get { return systemByte; }
		set { systemByte = value; }
	    }
	    public short SystemShort {
		get { return systemShort; }
		set { systemShort = value; }
	    }
	    public ushort SystemUShort {
		get { return systemUShort; }
		set { systemUShort = value; }
	    }
	    public int SystemInt {
		get { return systemInt; }
		set { systemInt = value; }
	    }
	    public uint SystemUInt {
		get { return systemUInt; }
		set { systemUInt = value; }
	    }
	    public long SystemLong {
		get { return systemLong; }
		set { systemLong = value; }
	    }
	    public ulong SystemULong {
		get { return systemULong; }
		set { systemULong = value; }
	    }
	    public char SystemChar {
		get { return systemChar; }
		set { systemChar = value; }
	    }
	    public float SystemFloat {
		get { return systemFloat; }
		set { systemFloat = value; }
	    }
	    public double SystemDouble {
		get { return systemDouble; }
		set { systemDouble = value; }
	    }
	    public BooleanType Spring2BooleanType {
		get { return spring2BooleanType; }
		set { spring2BooleanType = value; }
	    }
	    public CurrencyType Spring2CurrencyType {
		get { return spring2CurrencyType; }
		set { spring2CurrencyType = value; }
	    }
	    public CurrencyTypeList Spring2CurrencyTypeList {
		get { return spring2CurrencyTypeList; }
		set { spring2CurrencyTypeList = value; }
	    }
	    public DateTimeType Spring2DateTimeType {
		get { return spring2DateTimeType; }
		set { spring2DateTimeType = value; }
	    }
	    public DateType Spring2DateType {
		get { return spring2DateType; }
		set { spring2DateType = value; }
	    }
	    public DateTypeList Spring2DateTypeList {
		get { return spring2DateTypeList; }
		set { spring2DateTypeList = value; }
	    }
	    public DecimalType Spring2DecimalType {
		get { return spring2DecimalType; }
		set { spring2DecimalType = value; }
	    }
	    public DecimalTypeList Spring2DecimalTypeList {
		get { return spring2DecimalTypeList; }
		set { spring2DecimalTypeList = value; }
	    }
	    public GenderType Spring2GenderType {
		get { return spring2GenderType; }
		set { spring2GenderType = value; }
	    }
	    public IdType Spring2IdType {
		get { return spring2IdType; }
		set { spring2IdType = value; }
	    }
	    public IdTypeList Spring2IdTypeList {
		get { return spring2IdTypeList; }
		set { spring2IdTypeList = value; }
	    }
	    public IntegerType Spring2IntegerType {
		get { return spring2IntegerType; }
		set { spring2IntegerType = value; }
	    }
	    public IntegerTypeList Spring2IntegerTypeList {
		get { return spring2IntegerTypeList; }
		set { spring2IntegerTypeList = value; }
	    }
	    public LongType Spring2LongType {
		get { return spring2LongType; }
		set { spring2LongType = value; }
	    }
	    public PhoneNumberType Spring2PhoneNumberType {
		get { return spring2PhoneNumberType; }
		set { spring2PhoneNumberType = value; }
	    }
	    public QuantityType Spring2QuantityType {
		get { return spring2QuantityType; }
		set { spring2QuantityType = value; }
	    }
	    public ShortType Spring2ShortType {
		get { return spring2ShortType; }
		set { spring2ShortType = value; }
	    }
	    public StringType Spring2StringType {
		get { return spring2StringType; }
		set { spring2StringType = value; }
	    }
	    public StringTypeList Spring2StringTypeList {
		get { return spring2StringTypeList; }
		set { spring2StringTypeList = value; }
	    }
	    public TimeType Spring2TimeType {
		get { return spring2TimeType; }
		set { spring2TimeType = value; }
	    }
	    public LanguageEnum Spring2LanguageEnum {
		get { return spring2LanguageEnum; }
		set { spring2LanguageEnum = value; }
	    }
		
	    public String ReadOnlySystemString {
		get { return String.Empty; }
	    }

	}


	public class SampleClassWithRequiredField {
	    private string systemString;
	    private int systemInt;
	    private IntegerType spring2IntegerType;
	    private StringType spring2StringType;
	    private LanguageEnum spring2LanguageEnum;

	    [Required]
	    public string SystemString {
		get { return systemString; }
		set { systemString = value; }
	    }
	    [Required]
	    public int SystemInt {
		get { return systemInt; }
		set { systemInt = value; }
	    }
	    [Required]
	    public IntegerType Spring2IntegerType {
		get { return spring2IntegerType; }
		set { spring2IntegerType = value; }
	    }
	    [Required]
	    public StringType Spring2StringType {
		get { return spring2StringType; }
		set { spring2StringType = value; }
	    }
	    [Required]
	    public LanguageEnum Spring2LanguageEnum {
		get { return spring2LanguageEnum; }
		set { spring2LanguageEnum = value; }
	    }
	}
    }
}

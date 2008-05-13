using System;
using System.IO;

using NUnit.Framework;

using Spring2.Core.Util;

namespace Spring2.Core.Test {
    /// <summary>
    /// Test PGP encryption/decryption
    /// </summary>
    [TestFixture]
    public class TestGnuPGWrapper {
	
#if (NET_1_1 || NET_2_0)
	private DirectoryInfo gpgDirectory = new DirectoryInfo(@"..\\..\\src\\Test\\GnuPG");
	private DirectoryInfo testDirectory = new DirectoryInfo(@"..\\..\\src\\Test");
#else
	private DirectoryInfo gpgDirectory = new DirectoryInfo(@"..\\src\\Test\\GnuPG");
	private DirectoryInfo testDirectory = new DirectoryInfo(@"..\\src\\Test");
#endif
	private static readonly string TEST_STRING = "This is a test of the GnuPG wrapper.";

	/// <summary>
	/// Test of files with the wrapper.
	/// </summary>
	[Test]
	public void FilesTest() {

	    CleanFiles();

	    try {
		// Encrypt file
		GnuPGWrapper gpg = new GnuPGWrapper();
		gpg.command = Commands.Encrypt;

		gpg.homedirectory = gpgDirectory.FullName;
		gpg.recipient = "Spring2 Core Test";
		gpg.inputfile = testDirectory.FullName + "\\GnuPGTest.txt";
		gpg.outputfile = testDirectory.FullName + "\\GnuPGTest.pgp";
		gpg.keyringfile = gpgDirectory.FullName + "\\pubring.gpg";

		gpg.ExecuteCommand();

		// Decrypt file
		gpg = new GnuPGWrapper();
		gpg.command = Commands.Decrypt;

		gpg.homedirectory = gpgDirectory.FullName;
		gpg.passphrase = "Used to unit test GnuPG";
		gpg.inputfile = testDirectory.FullName + "\\GnuPGTest.pgp";
		gpg.outputfile = testDirectory.FullName + "\\NewGnuPGTest.txt";
		gpg.keyringfile = gpgDirectory.FullName + "\\pubring.gpg";

		gpg.ExecuteCommand();

		TestUtilities.CompareFiles(testDirectory.FullName + "\\GnuPGTest.txt", testDirectory.FullName + "\\NewGnuPGTest.txt");
	    }
	    finally {
		CleanFiles();
	    }
	}

	/// <summary>
	/// Test of strings with wrapper.
	/// </summary>
	[Test]
	public void StringTest() {
	    // Encrypt file
	    GnuPGWrapper gpg = new GnuPGWrapper();
	    gpg.command = Commands.Encrypt;

	    gpg.homedirectory = gpgDirectory.FullName;
	    gpg.recipient = "Spring2 Core Test";
	    gpg.keyringfile = gpgDirectory.FullName + "\\pubring.gpg";

	    string encryptedString = "";
	    gpg.ExecuteCommand(TEST_STRING, out encryptedString);

	    // Decrypt file
	    gpg = new GnuPGWrapper();
	    gpg.command = Commands.Decrypt;

	    gpg.homedirectory = gpgDirectory.FullName;
	    gpg.passphrase = "Used to unit test GnuPG";
	    gpg.keyringfile = gpgDirectory.FullName + "\\pubring.gpg";

	    string decryptedString = "";
	    gpg.ExecuteCommand(encryptedString, out decryptedString);

	    Assert.IsTrue(TEST_STRING.Equals(decryptedString), "Encryption or decryption messed something up");
	}

	/// <summary>
	/// Test of file decrypting to string with wrapper.
	/// </summary>
	[Test]
	public void FileToStringTest() {
	    // Encrypt to file
	    try {
		// Encrypt to String
		GnuPGWrapper gpg = new GnuPGWrapper();
		gpg.command = Commands.Encrypt;

		gpg.homedirectory = gpgDirectory.FullName;
		gpg.recipient = "Spring2 Core Test";
		gpg.inputfile = testDirectory.FullName + "\\GnuPGTest.txt";
		gpg.keyringfile = gpgDirectory.FullName + "\\pubring.gpg";

		string encryptString = "";
		gpg.ExecuteCommand(out encryptString);

		// Decrypt the strings.
		gpg = new GnuPGWrapper();
		gpg.command = Commands.Decrypt;

		gpg.homedirectory = gpgDirectory.FullName;
		gpg.passphrase = "Used to unit test GnuPG";
		gpg.keyringfile = gpgDirectory.FullName + "\\pubring.gpg";

		string decryptString = "";
		gpg.ExecuteCommand(encryptString, out decryptString);

		StreamReader r = new StreamReader(testDirectory.FullName + "\\GnuPGTest.txt");
		string origString = r.ReadToEnd();
		r.Close();

		Assert.IsTrue(decryptString.Equals(origString), "Encryption messed something up");
	    }
	    finally {
		CleanFiles();
	    }
	}

	/// <summary>
	/// Cleans up files for the file test.
	/// </summary>
	private void CleanFiles() {
	    File.Delete(testDirectory.FullName + "\\GnuPGTest.pgp");
	    File.Delete(testDirectory.FullName + "\\NewGnuPGTest.txt");
	}
    }
}

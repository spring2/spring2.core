using System;
using System.IO;

using NUnit.Framework;

using Spring2.Core.Util;

namespace Spring2.Core.Test.Test {
    /// <summary>
    /// Tests the FTP Client class
    /// </summary>
    [TestFixture]
    public class TestFTPClient {

	private static readonly string SERVER="bendesktop";
	private static readonly string USER="test";
	private static readonly string PASSWORD="test";
	private static readonly string STARTDIRECTORY="ftproot";
	private static readonly string DIRECTORY="ftptest";
	private static readonly string ORIGINALXMLFILE = "Spring2.Core.Test.xml";
	private static readonly string ORIGINALDLLFILE = "Spring2.Core.WebControl.dll";
	private static readonly string REMOTEXMLFILE = "Test.xml";
	private static readonly string REMOTEDLLFILE = "Test.dll";
	private static readonly string LOCALXMLFILE = "LocalTest.xml";
	private static readonly string LOCALDLLFILE = "LocalTest.dll";
	private static readonly string REMOTEXMLFILERENAMED = "Test.Newname.xml";
	private static readonly string NEWDIRECTORY = "NewDirectory";
	private static readonly string REMOTETEXTFILE = "Test.txt";
	private static readonly string STRINGTOUPLOAD = "some text\nand some more text";

	/// <summary>
	/// Test the ftp client class.
	/// </summary>
	[Test, Ignore("Ignored till we have a reliable FTP server to use.")]
	public void FTPClientTest() {

	    Cleanup();

	    FTPClient fc = new FTPClient(SERVER, STARTDIRECTORY, USER, PASSWORD);

	    Assertion.Assert("Unable to log in to server" + fc.MessageString, fc.Login());

	    Assertion.Assert("Unable to change directory to " + DIRECTORY + "." + fc.MessageString, fc.ChangeDirectory(DIRECTORY));

	    Assertion.Assert("Unable to create new directory." + fc.MessageString, fc.CreateDirectory(NEWDIRECTORY));

	    try {
		Assertion.Assert("Unable to change to the new directory." + fc.MessageString, fc.ChangeDirectory(NEWDIRECTORY));

		fc.SetBinaryMode(true);

		// Test upload
		fc.UploadFile(ORIGINALXMLFILE, REMOTEXMLFILE);
		fc.UploadFile(ORIGINALDLLFILE, REMOTEDLLFILE);

		// Test download
		fc.DownloadFile(REMOTEXMLFILE, LOCALXMLFILE);
		fc.DownloadFile(REMOTEDLLFILE, LOCALDLLFILE);

		TestUtilities.CompareFiles(ORIGINALXMLFILE, LOCALXMLFILE);

		TestUtilities.CompareFiles(ORIGINALDLLFILE, LOCALDLLFILE);

		Assertion.Assert("Unable to delete remote file" + REMOTEDLLFILE + "." + fc.MessageString, fc.DeleteFile(REMOTEDLLFILE));

		Assertion.Assert("Unable to rename remote file." + fc.MessageString, fc.RenameFile(REMOTEXMLFILE, REMOTEXMLFILERENAMED));

		string[] files = fc.GetFileList("");
		bool found = false;
		foreach(string file in files) {
		    Assertion.Assert("Unexpected file " + file + " found in file list.",
			file.Equals("") || file.StartsWith(".") || file.StartsWith(REMOTEXMLFILERENAMED));
		    found = found || file.StartsWith(REMOTEXMLFILERENAMED);
		}
		Assertion.Assert("File " + REMOTEXMLFILERENAMED + " not found in getlist.", found);

		fc.UploadFromString(REMOTETEXTFILE, STRINGTOUPLOAD);

		string txt = fc.DownloadToString(REMOTETEXTFILE);

		Assertion.Assert("Upload or download of string didn't work.", txt.Equals(STRINGTOUPLOAD));

		Assertion.Assert("Unable to delete remote file " + REMOTEXMLFILERENAMED + "." + fc.MessageString, fc.DeleteFile(REMOTEXMLFILERENAMED));

		Assertion.Assert("Unable to delete remote file " + REMOTETEXTFILE + "." + fc.MessageString,
		    fc.DeleteFile(REMOTETEXTFILE));

		Assertion.Assert("Unable to change back to parent directory" + fc.MessageString, fc.ChangeDirectory(".."));

		Assertion.Assert("Unable to delete directory." + fc.MessageString,  fc.RemoveDirectory(NEWDIRECTORY));
	    }
	    finally {
		fc.CloseConnection();
		Cleanup();
	    }
	}

	/// <summary>
	/// Cleans up files created during test.
	/// </summary>
	private void Cleanup() {
	    FTPClient fc = new FTPClient(SERVER, STARTDIRECTORY, USER, PASSWORD);
	    File.Delete(LOCALXMLFILE);
	    File.Delete(LOCALDLLFILE);
	    fc.ChangeDirectory(DIRECTORY);
	    if (fc.ChangeDirectory(NEWDIRECTORY)) {
		string[] files = fc.GetFileList("");
		foreach(string file in files) {
		    if (file.Length > 0 && !file.StartsWith(".")) {
			fc.DeleteFile(file.Trim(new char[] {'\r',' ','\n'}));
		    }
		}
		fc.ChangeDirectory("..");

		fc.RemoveDirectory(NEWDIRECTORY);
	    }

	    fc.CloseConnection();
	}
    }
}
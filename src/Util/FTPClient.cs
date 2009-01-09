using System;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;

namespace Spring2.Core.Util {
    /// <summary>
    /// This class permits you to perform direct connections to FTP sites
    /// The class supports the following FTP commands:
    ///   - Download a file
    ///   - Upload a file
    ///   - Download a file
    ///   - Create a directory
    ///   - Remove a directory
    ///   - Change directory
    ///   - Remove a file
    ///   - Rename a file
    ///   - Set the user name of the remote user
    ///   - Set the password of the remote user
    /// </summary>
    public class FTPClient {
	#region "Class Variable Declarations"

	private string remoteHost;
	private string remotePath;
	private string remoteUser;
	private string remotePassword;
	private int remotePort;
	private int bytes;
	private Socket clientSocket;

	private string message;
	private string reply;
	private int retValue;

	private bool loggedIn;

	private int receiveTimeout = DEFAULT_RECEIVE_TIMEOUT;

	//Set the size of the packet that is used to read and to write data to the FTP server 
	//to the following specified size.
	private static readonly int BLOCK_SIZE = 512;
	private byte[] buffer = new byte[BLOCK_SIZE];
	private static readonly Encoding ASCII = Encoding.ASCII;
	private static readonly int DEFAULT_RECEIVE_TIMEOUT = 1000; // in milliseconds.

	//General variable declaration
	private string messageString;
	#endregion

	#region "Class Constructors"

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="remoteHost">Host to connect to</param>
	/// <param name="remotePath">Path on remote host to start work in.</param>
	/// <param name="remoteUser">Username for the remote host.</param>
	/// <param name="remotePassword">Password for the remote host.</param>
	/// <param name="remotePort">Port to use.</param>
	public FTPClient(string remoteHost, string remotePath, string remoteUser, string remotePassword, int remotePort) {
	    Init(remoteHost, remotePath, remoteUser, remotePassword, remotePort);
	}

	/// <summary>
	/// Constructor.  Port defaults to 21.
	/// </summary>
	/// <param name="remoteHost">Host to connect to</param>
	/// <param name="remotePath">Path on remote host to start work in.</param>
	/// <param name="remoteUser">Username for the remote host.</param>
	/// <param name="remotePassword">Password for the remote host.</param>
	public FTPClient(string remoteHost, string remotePath, string remoteUser, string remotePassword) {
	    Init(remoteHost, remotePath, remoteUser, remotePassword, 21);
	}

	/// <summary>
	/// Constructor.  Port defaults to 21.
	/// </summary>
	/// <param name="remoteHost">Host to connect to</param>
	/// <param name="remoteUser">Username for the remote host.</param>
	/// <param name="remotePassword">Password for the remote host.</param>
	public FTPClient(string remoteHost, string remoteUser, string remotePassword) {
	    Init(remoteHost, ".", remoteUser, remotePassword, 21);
	}

	/// <summary>
	/// Used by constructors.
	/// </summary>
	/// <param name="remoteHost">Host to connect to</param>
	/// <param name="remotePath">Path on remote host to start work in.</param>
	/// <param name="remoteUser">Username for the remote host.</param>
	/// <param name="remotePassword">Password for the remote host.</param>
	/// <param name="remotePort">Port to use.</param>
	private void Init(string remoteHost, string remotePath, string remoteUser, string remotePassword, int remotePort) {
	    this.remoteHost = remoteHost;
	    this.remotePath = remotePath;
	    this.remoteUser = remoteUser;
	    this.remotePassword = remotePassword;
	    this.messageString = "";
	    this.remotePort = remotePort;
	    this.loggedIn = false;
	}

	#endregion

	#region "public Properties"

	/// <summary>
	/// Set or Get the name of the FTP server that you want to connect to.
	/// </summary>
	public string RemoteHostFTPServer {
	    get {return remoteHost; }
	    set { remoteHost = value; }
	}

	/// <summary>
	/// Set or Get the FTP port number of the FTP server that you want to connect to.
	/// </summary>
	public int RemotePort{
	    get {return remotePort; }
	    set {remotePort = value; }
	}

	/// <summary>
	/// Set or Get the remote path of the FTP server that you want to connect to.
	/// </summary>
	public string RemotePath {
	    get { return remotePath; }
	    set { remotePath = value; }
	}

	/// <summary>
	/// Set the remote password of the FTP server that you want to connect to.
	/// </summary>
	public string RemotePassword {
	    get {return remotePassword; }
	    set { remotePassword = value; }
	}

	/// <summary>
	/// Set or Get the remote user of the FTP server that you want to connect to. 
	/// </summary>
	public string RemoteUser {
	    get { return remoteUser; }
	    set { remoteUser = value; }
	}

	/// <summary>
	/// Get or Set the class messagestring.
	/// </summary>
	public string MessageString {
	    get { return messageString; }
	    set { messageString = value; }
	}

	/// <summary>
	/// Get or set the amount of time to wait for an ftp receive (in milliseconds).
	/// </summary>
	public int ReceiveTimeout {
	    get { return this.receiveTimeout; }
	    set { receiveTimeout = value; }
	}

	#endregion

	#region "public Subs and Functions"

	/// <summary>
	/// Get files from file system
	/// </summary>
	/// <param name="mask">Mask to limit files returned.</param>
	/// <returns>Array of file objects.</returns>
	public string[] GetFileList(string mask) {
	    Socket socket;
	    int bytes;
	    char seperator = '\n';
	    string[] mess;

	    message = "";
	    //Check to see if you are logged on to the FTP server.
	    if (! (loggedIn)) {
		Login();
	    }

	    socket = CreateDataSocket();
	    //Send an FTP command. 
	    SendCommand("NLST " + mask);

	    if (!(retValue == 150 || retValue == 125)) {
		MessageString = reply;
		throw new IOException(reply);
	    }

	    message = "";
	    while (true) {
		Array.Clear(buffer, 0, buffer.Length);
		bytes = socket.Receive(buffer, buffer.Length, 0);
		message += ASCII.GetString(buffer, 0, bytes);

		if (bytes < buffer.Length) {
		    break;
		}
	    }

	    mess = message.Split(seperator);
	    socket.Close();
	    ReadReply();

	    if (retValue != 226) {
		MessageString = reply;
		throw new IOException(reply);
	    }

	    return mess;
	}

	/// <summary>
	///  Get the size of the file on the FTP server.
	/// </summary>
	/// <param name="filename">Name of file</param>
	/// <returns>Size of file.</returns>
	public Int64 GetFileSize(string filename) {
	    long size;

	    if (!loggedIn) {
		Login();
	    }
	    //Send an FTP command. 
	    SendCommand("SIZE " + filename);
	    size = 0;

	    if (retValue == 213) {
		size = Int64.Parse(reply);
	    } else {
		MessageString = reply;
		throw new IOException(reply);
	    }

	    return size;
	}


	/// <summary>
	/// Log on to the FTP server.
	/// </summary>
	/// <returns>true if successful, false otherwise.</returns>
	public bool Login() {

	    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	    IPEndPoint ep = null;

	    try {
		ep = new IPEndPoint(Dns.GetHostEntry(remoteHost).AddressList[0], remotePort);
	    } catch(ArgumentOutOfRangeException) {
		MessageString = "Invalid port specified.";
		throw new IOException(MessageString);
	    }

	    try {
		clientSocket.Connect(ep);
	    }
	    catch(Exception) {
		MessageString = reply;
		throw new IOException("Cannot connect to the remote server");
	    }

	    ReadReply();
	    if (retValue != 220) {
		CloseConnection();
		MessageString = reply;
		throw new IOException(reply);
	    }
	    //Send an FTP command to send a user logon ID to the server.
	    SendCommand("USER " + remoteUser);
	    if (! (retValue == 331 || retValue == 230)) {
		Cleanup();
		MessageString = reply;
		throw new IOException(reply);
	    }

	    if (retValue != 230) {
		//Send an FTP command to send a user logon password to the server.
		SendCommand("PASS " + remotePassword);
		if (! (retValue == 230 || retValue == 202)) {
		    Cleanup();
		    MessageString = reply;
		    throw new IOException(reply);
		}
	    }

	    loggedIn = true;
	    //Call the ChangeDirectory user-defined function to change the directory to the 
	    //remote FTP folder that is mapped.
	    ChangeDirectory(remotePath);

	    //return the final result.
	    return loggedIn;
	}

	/// <summary>
	/// This is a function that is used to change the current working directory on the remote FTP server.
	/// </summary>
	/// <param name="sDirName">Nameof directory</param>
	/// <returns>True on success.</returns>
	public bool ChangeDirectory(string sDirName) {
	    bool bResult;

	    bResult = true;
	    //Check if you are in the root directory.
	    if (sDirName.Equals(".")) {
		return true;
	    }
	    // Check if logged on to the FTP server
	    if (!loggedIn) {
		Login();
	    }
	    //Send an FTP command to change directory on the FTP server.
	    SendCommand("CWD " + sDirName);
	    if (retValue != 250) {
		bResult = false;
		MessageString = reply;
	    }

	    this.remotePath = sDirName;

	    // Return the final result.
	    return bResult;
	}

	/// <summary>
	/// Close the FTP connection of the remote server.
	/// </summary>
	public void CloseConnection() {
	    if (clientSocket != null) {
		// Send an FTP command to end an FTP server system.
		SendCommand("QUIT");
	    }

	    Cleanup();
	}
		
	/// <summary>
	/// Set the binary mode.
	/// </summary>
	/// <param name="bMode">if the value of mode is true, set binary mode for downloads. Otherwise, set ASCII mode.</param>
	public void SetBinaryMode(bool bMode) {

	    if (bMode) {
		//Send the FTP command to set the binary mode.
		//(TYPE is an FTP command that is used to specify representation type.)
		SendCommand("TYPE I");
	    } else {
		//Send the FTP command to set ASCII mode. 
		//(TYPE is an FTP command that is used to specify representation type.)
		SendCommand("TYPE A");
	    }

	    if (retValue != 200) {
		MessageString = reply;
		throw new IOException(reply);
	    }
	}

	/// <summary>
	///  Download a file to the local directory of the assembly. Keep the same file name.
	/// </summary>
	/// <param name="fileName">Name of file to download.</param>
	public void DownloadFile(string fileName) {
	    DownloadFile(fileName, "", false);
	}

	/// <summary>
	/// Download a file.
	/// </summary>
	/// <param name="fileName">Name of file to download.</param>
	/// <param name="output">Stream to write the file to.</param>
	public void DownloadFile(string fileName, Stream output) {
	    DownloadFile(fileName, false, output);
	}

	/// <summary>
	///  Download a remote file to the local directory of the Assembly. Keep the same file name.
	/// </summary>
	/// <param name="filename">filename to download.</param>
	/// <param name="resume">Indicates if we a resuming a previously aborted download</param>
	public void DownloadFile(string filename, bool resume) {
	    DownloadFile(filename, "", resume);
	}

	/// <summary>
	/// Download a remote file to a local file name. You must include a path.
	/// The local file name will be created or will be overwritten, but the path must exist.
	/// </summary>
	/// <param name="filename">remote file name</param>
	/// <param name="localFileName">local file name</param>
	//
	public void DownloadFile(string filename, string localFileName) {
	    DownloadFile(filename, localFileName, false);
	}

	/// <summary>
	///  Download a remote file to a local file name. You must include a path. Set the 
	/// resume flag. The local file name will be created or will be overwritten, but the path must exist.
	/// </summary>
	/// <param name="filename">remote file name</param>
	/// <param name="localFileName">local file name</param>
	/// <param name="resume">Indicates if we a resuming a previously aborted download</param>
	public void DownloadFile(string filename, string localFileName, bool resume) {
	    Stream st;
	    if (localFileName.Equals("")) {
		localFileName = filename;
	    }

	    if (! (File.Exists(localFileName))) {
		st = File.Create(localFileName);
		st.Close();
	    }

	    FileStream output = new FileStream(localFileName, FileMode.Open);

	    DownloadFile(filename, resume, output);
	}

	/// <summary>
	/// Downloads a file to the stream.
	/// </summary>
	/// <param name="filename">Name of file to download.</param>
	/// <param name="resume">Indicates if we a resuming a previously aborted download</param>
	/// <param name="output">Stream to write the file to.</param>
	public void DownloadFile(string filename, bool resume, Stream output) {
	    Socket socket;
	    Int64 offset;
	    Int64 npos;

	    if (!loggedIn) {
		Login();
	    }

	    SetBinaryMode(true);

	    socket = CreateDataSocket();
	    offset = 0;

	    if (resume) {
		offset = output.Length;

		if (offset > 0) {
		    //Send an FTP command to restart.
		    SendCommand("REST " + offset.ToString());
		    if (retValue != 350) {
			offset = 0;
		    }
		}

		if (offset > 0) {
		    npos = output.Seek(offset, SeekOrigin.Begin);
		}
	    }

	    //Send an FTP command to retrieve a file.
	    SendCommand("RETR " + filename);

	    if (! (retValue == 150 || retValue == 125)) {
		MessageString = reply;
		throw new IOException(reply);
	    }

	    while (true) {
		Array.Clear(buffer, 0, buffer.Length);
		bytes = socket.Receive(buffer, buffer.Length, 0);
		output.Write(buffer, 0, bytes);

		if (bytes <= 0) {
		    break;
		}
	    }

	    output.Close();
	    if (socket.Connected) {
		socket.Close();
	    }

	    ReadReply();
	    if (! (retValue == 226 || retValue == 250)) {
		MessageString = reply;
		throw new IOException(reply);
	    }

	}

	/// <summary>
	/// Downloads a file to a string.
	/// </summary>
	/// <param name="filename">File to download.</param>
	/// <returns>Contents of file downloaded.</returns>
	public string DownloadToString(string filename) {
	    return DownloadToString(filename, false);
	}

	/// <summary>
	/// Downloads a file to a string.
	/// </summary>
	/// <param name="filename">File to download.</param>
	/// <param name="resume">Indicates if we a resuming a previously aborted download</param>
	/// <returns>Contents of file downloaded.</returns>
	public string DownloadToString(string filename, bool resume) {
	    MemoryStream stream = new MemoryStream();
	    DownloadFile(filename, resume, stream);
	    return ASCII.GetString(stream.ToArray());
	}

	/// <summary>
	/// This is a function that is used to upload a file from your local hard disk to your FTP site.
	/// </summary>
	/// <param name="filename">Name of file to upload.</param>
	public void UploadFile(string filename) {
	    UploadFile(filename, false);
	}

	/// <summary>
	/// This is a function that is used to upload a file from your local hard disk to your FTP site.
	/// </summary>
	/// <param name="filename">Name of file to upload.</param>
	/// <param name="remoteFileName">Name of file to create on remote system.</param>
	public void UploadFile(string filename, string remoteFileName) {
	    UploadFile(filename, remoteFileName, false);
	}

	/// <summary>
	/// This is a function that is used to upload a stream to your FTP site. 
	/// </summary>
	/// <param name="input">stream to upload.</param>
	/// <param name="remoteFileName">Name of file to create on remote system.</param>
	public void UploadFile(Stream input, string remoteFileName) {
	    UploadFile(input, remoteFileName, false);
	}

	/// <summary>
	/// This is a function that is used to upload a file from your local hard disk to your FTP site 
	/// and then set the resume flag.
	/// </summary>
	/// <param name="filename">name of file to upload.</param>
	/// <param name="resume">Indicates if we a resuming a previously aborted download</param>
	public void UploadFile(string filename, bool resume) {
	    UploadFile(filename, filename, resume);
	}

	/// <summary>
	/// This is a function that is used to upload a file from your local hard disk to your FTP site 
	/// and then set the resume flag.
	/// </summary>
	/// <param name="filename">name of file to upload.</param>
	/// <param name="remoteFileName">Name of file to create on remote system.</param>
	/// <param name="resume">Indicates if we a resuming a previously aborted download</param>
	public void UploadFile(string filename, string remoteFileName, bool resume) {
	    FileStream input;
	    bool bFileNotFound = false;

	    if (File.Exists(filename)) {
		// Open the input stream to read the source file.
		input = new FileStream(filename, FileMode.Open);
		try {
		    UploadFile(input, remoteFileName, resume);
		}
		finally {
		    input.Close();
		}
	    } else {
		bFileNotFound = true;
	    }

	    // Check the return value if the file was not found.
	    if (bFileNotFound) {
		MessageString = reply;
		throw new IOException("The file: " + filename + " was not found. "  + "Cannot upload the file to the FTP site");
	    }
	}

	/// <summary>
	/// This is a function that is used to upload a stream to your FTP site 
	/// and then set the resume flag.
	/// </summary>
	/// <param name="input">stream to upload.</param>
	/// <param name="remoteFileName">Name of file to create on remote system.</param>
	/// <param name="resume">Indicates if we a resuming a previously aborted download</param>
	public void UploadFile(Stream input, string remoteFileName, bool resume) {
	    Socket socket;
	    Int64 offset;

	    if (!loggedIn) {
		Login();
	    }

	    socket = CreateDataSocket();
	    offset = 0;

	    if (resume) {
		try {
		    SetBinaryMode(true);
		    offset = GetFileSize(remoteFileName);
		} catch(Exception) {
		    offset = 0;
		}
	    }

	    if (offset != 0) {
		input.Seek(offset, SeekOrigin.Begin);
	    }

	    if (offset > 0) {
		SendCommand("REST " + offset);
		if (retValue != 350) {

		    // The remote server may not support resuming.
		    offset = 0;
		}
	    }
	    //Send an FTP command to store a file.
	    SendCommand("STOR " + remoteFileName);
	    if (! (retValue == 125 || retValue == 150)) {
		MessageString = reply;
		throw new IOException(reply);
	    }

	    // Upload the file. 
	    bytes = input.Read(buffer, 0, buffer.Length);
	    while (bytes > 0) {
		socket.Send(buffer, bytes, 0);
		bytes = input.Read(buffer, 0, buffer.Length);
	    }

	    if (socket.Connected) {
		socket.Close();
	    }

	    ReadReply();
	    if (! (retValue == 226 || retValue == 250)) {
		MessageString = reply;
		throw new IOException(reply);
	    }
	}

	/// <summary>
	/// Uploads a file from a string.
	/// </summary>
	/// <param name="remoteFilename">Name of file to create on ftp server.</param>
	/// <param name="input">String containing contents of file to create.</param>
	public void UploadFromString(string remoteFilename, string input) {
	    UploadFromString(remoteFilename, input, false);
	}


	/// <summary>
	/// Uploads a file from a string.
	/// </summary>
	/// <param name="remoteFilename">Name of file to create on ftp server.</param>
	/// <param name="input">String containing contents of file to create.</param>
	/// <param name="resume">Indicates if we a resuming a previously aborted download</param>
	public void UploadFromString(string remoteFilename, string input, bool resume) {
	    MemoryStream stream = new MemoryStream(ASCII.GetBytes(input), false);
	    UploadFile(stream, remoteFilename, resume);
	}

	/// <summary>
	///  Delete a file from the remote FTP server.
	/// </summary>
	/// <param name="filename">Name of file to delete</param>
	/// <returns>result.</returns>
	public bool DeleteFile(string filename) {

	    bool result = true;
	    if (! (loggedIn)) {
		Login();
	    }
	    //Send an FTP command to delete a file.
	    SendCommand("DELE " + filename);
	    if (retValue != 250) {
		result = false;
		MessageString = reply;
	    }

	    // Return the final result.
	    return result;
	}
	/// <summary>
	///  Rename a file on the remote FTP server.
	/// </summary>
	/// <param name="sOldFileName">File name before rename</param>
	/// <param name="sNewFileName">File name after successful rename.</param>
	/// <returns>True if successful.</returns>
	public bool RenameFile(string sOldFileName, string sNewFileName) {
	    bool bResult;

	    bResult = true;
	    if (! (loggedIn)) {
		Login();
	    }
	    //Send an FTP command to rename from a file.
	    SendCommand("RNFR " + sOldFileName);
	    if (retValue != 350) {
		MessageString = reply;
		throw new IOException(reply);
	    }

	    // Send an FTP command to rename a file to a new file name.
	    // It will overwrite if newFileName exists. 
	    SendCommand("RNTO " + sNewFileName);
	    if (retValue != 250) {
		MessageString = reply;
		throw new IOException(reply);
	    }
	    // Return the final result.
	    return bResult;
	}

	/// <summary>
	/// This is a function that is used to create a directory on the remote FTP server.
	/// </summary>
	/// <param name="sDirName">Name of directory to create</param>
	/// <returns>true if successful</returns>
	public bool CreateDirectory(string sDirName) {
	    bool bResult;

	    bResult = true;
	    if (! (loggedIn)) {
		Login();
	    }
	    // Send an FTP command to make directory on the FTP server.
	    SendCommand("MKD " + sDirName);
	    if (retValue != 257) {
		bResult = false;
		MessageString = reply;
	    }

	    //  Return the final result.
	    return bResult;
	}
	/// <summary>
	/// This is a function that is used to delete a directory on the remote FTP server.
	/// </summary>
	/// <param name="sDirName">Name of directory to delete</param>
	/// <returns>True if successful</returns>
	public bool RemoveDirectory(string sDirName) {
	    bool bResult;

	    bResult = true;
	    // Check if logged on to the FTP server
	    if (! (loggedIn)) {
		Login();
	    }
	    // Send an FTP command to remove directory on the FTP server.
	    SendCommand("RMD " + sDirName);
	    if (retValue != 250) {
		bResult = false;
		MessageString = reply;
	    }

	    // Return the final result.
	    return bResult;
	}
	#endregion

	#region "private Subs and Functions"

	/// <summary>
	///  Read the reply from the FTP server.
	/// </summary>
	private void ReadReply() {
	    message = "";
	    reply = ReadLine();
	    if (reply.Length < 3) {
		retValue = 0;
	    } else {
		retValue = Int32.Parse(reply.Substring(0, 3));
	    }
	}

	/// <summary>
	///  Clean up some variables.
	/// </summary>
	private void Cleanup() {
	    if (clientSocket != null) {
		clientSocket.Close();
		clientSocket = null;
	    }

	    loggedIn = false;
	}


	/// <summary>
	/// Reads a line from the FTP server without clearring the message.
	/// </summary>
	/// <returns>Line read</returns>
	private string ReadLine() {
	    return ReadLine(false);
	}

	/// <summary>
	///  Read a line from the FTP server.
	/// </summary>
	/// <param name="bClearMes">Indicates if message is to be cleared.</param>
	/// <returns>Line reead.</returns>
	private string ReadLine(bool bClearMes) {
	    Char seperator = '\n';
	    string[] mess;

	    clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, receiveTimeout);

	    if (bClearMes) {
		message = "";
	    }
	    while (true) {
		Array.Clear(buffer, 0, BLOCK_SIZE);
		bytes = clientSocket.Receive(buffer, buffer.Length, 0);
		message += ASCII.GetString(buffer, 0, bytes);
		if (bytes < buffer.Length) {
		    break;
		}
	    }

	    mess = message.Split(seperator);
	    if (message.Length > 2) {
		message = mess[mess.Length - 2];
	    } else {
		message = mess[0];
	    }

	    if (message.Length < 4) {
		return message;
	    }

	    if (!message.Substring(3, 1).Equals(" ")) {
		return ReadLine(true);
	    }

	    return message;
	}

	/// <summary>
	/// This is a function that is used to send a command to the FTP server that you are connected to.
	/// </summary>
	/// <param name="sCommand">Command to send.</param>
	private void SendCommand(string sCommand) {
	    sCommand = sCommand + "\r\n";
	    byte[] cmdbytes = ASCII.GetBytes(sCommand);
	    clientSocket.Send(cmdbytes, cmdbytes.Length, 0);
	    ReadReply();
	}

	/// <summary>
	///  Create a data socket.
	/// </summary>
	/// <returns>Socket created.</returns>
	private Socket CreateDataSocket() {
	    int index1;
	    int index2;
	    int len;
	    int partCount;
	    int i;
	    int port;
	    string ipData;
	    string buf;
	    string ipAddress;
	    int[] parts = new int[6];
	    char ch;
	    Socket s;
	    IPEndPoint ep;

	    //Send an FTP command to use passive data connection. 
	    SendCommand("PASV");
	    if (retValue != 227) {
		MessageString = reply;
		throw new IOException(reply);
	    }

	    index1 = reply.IndexOf("(");
	    index2 = reply.IndexOf(")");
	    ipData = reply.Substring(index1 + 1, index2 - index1 - 1);

	    len = ipData.Length;
	    partCount = 0;
	    buf = "";

	    for(i = 0;i<=len-1 && partCount <=6;i++) {
		ch = Char.Parse(ipData.Substring(i, 1));
		if (Char.IsDigit(ch)) {
		    buf += ch;
		} else if (ch != ',') {
		    MessageString = reply;
		    throw new IOException("Malformed PASV reply: " + reply);
		}

		if ((ch == ',') || (i + 1 == len)) {
		    try {
			parts[partCount] = Int32.Parse(buf);
			partCount += 1;
			buf = "";
		    } catch(Exception) {
			MessageString = reply;
			throw new IOException("Malformed PASV reply: " + reply);
		    }
		}
	    }

	    ipAddress = parts[0] + "." + parts[1] + "." + parts[2] + "." + parts[3];

	    // Make this call and then comment out the previous line for Visual Basic .NET 2003.
	    port = parts[4] << 8;

	    // Determine the data port number.
	    port = port + parts[5];

	    s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	    ep = new IPEndPoint(Dns.GetHostEntry(ipAddress).AddressList[0], port);

	    try {
		s.Connect(ep);
	    } catch(Exception) {
		MessageString = reply;
		throw new IOException("Cannot connect to remote server.");
	    }

	    return s;
	}

	#endregion
    }
}
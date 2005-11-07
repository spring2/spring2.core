using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using Spring2.Core.Configuration;

namespace Spring2.Core.Cryptography {
    /// <summary>
    /// Represents a collection of cryptographics utility methods. This class cannot be inherited.
    /// </summary>
    public sealed class CryptoUtil {
	#region Variable Declaration

	//
	// For the delimiter we are using the Zero-width nonbreaking space.  
	//
	private const char TIMESTAMP_DELIMITER = '\uFEFF';
	private byte[] m_keyFromSecretSeed;
	private byte[] m_keyFromSecretIv;
	private byte[] m_defaultKey;
	private byte[] m_defaultInitializationVector;
	private TripleDESCryptoServiceProvider m_provider = new TripleDESCryptoServiceProvider();
	private byte[] m_initializedKey;
	private byte[] m_initializedIv;
	private string m_initializedSecret = string.Empty;

	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="Spring2.Core.Cryptography.CryptoUtil"/> class.
	/// </summary>
	public CryptoUtil() {
	    #region m_defaultInitializationVector initialization

	    m_defaultInitializationVector = new Byte[8];
	    m_defaultInitializationVector[0] = 232;
	    m_defaultInitializationVector[1] = 31;
	    m_defaultInitializationVector[2] = 15;
	    m_defaultInitializationVector[3] = 236;
	    m_defaultInitializationVector[4] = 22;
	    m_defaultInitializationVector[5] = 118;
	    m_defaultInitializationVector[6] = 121;
	    m_defaultInitializationVector[7] = 238;
		    
	    #endregion
		    
	    #region m_defaultKey initialization

	    m_defaultKey = new Byte[24];
	    m_defaultKey[0] = 68;
	    m_defaultKey[1] = 138;
	    m_defaultKey[2] = 174;
	    m_defaultKey[3] = 195;
	    m_defaultKey[4] = 77;
	    m_defaultKey[5] = 159;
	    m_defaultKey[6] = 220;
	    m_defaultKey[7] = 45;
	    m_defaultKey[8] = 82;
	    m_defaultKey[9] = 179;
	    m_defaultKey[10] = 219;
	    m_defaultKey[11] = 7;
	    m_defaultKey[12] = 194;
	    m_defaultKey[13] = 109;
	    m_defaultKey[14] = 71;
	    m_defaultKey[15] = 120;
	    m_defaultKey[16] = 63;
	    m_defaultKey[17] = 215;
	    m_defaultKey[18] = 114;
	    m_defaultKey[19] = 9;
	    m_defaultKey[20] = 159;
	    m_defaultKey[21] = 113;
	    m_defaultKey[22] = 94;
	    m_defaultKey[23] = 233;
		    
	    #endregion
		    
	    #region m_keyFromSecretSeed initialization

	    m_keyFromSecretSeed = new Byte[8];
	    m_keyFromSecretSeed[0] = 114;
	    m_keyFromSecretSeed[1] = 98;
	    m_keyFromSecretSeed[2] = 169;
	    m_keyFromSecretSeed[3] = 133;
	    m_keyFromSecretSeed[4] = 3;
	    m_keyFromSecretSeed[5] = 93;
	    m_keyFromSecretSeed[6] = 40;
	    m_keyFromSecretSeed[7] = 114;
		    
	    #endregion
		    
	    #region m_keyFromSecretIv initialization

	    m_keyFromSecretIv = new Byte[8];
	    m_keyFromSecretIv[0] = 58;
	    m_keyFromSecretIv[1] = 29;
	    m_keyFromSecretIv[2] = 158;
	    m_keyFromSecretIv[3] = 216;
	    m_keyFromSecretIv[4] = 19;
	    m_keyFromSecretIv[5] = 30;
	    m_keyFromSecretIv[6] = 196;
	    m_keyFromSecretIv[7] = 177;
		    
	    #endregion

	    GetSymmetricKey();
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Spring2.Core.Cryptography.CryptoUtil"/> class.
	/// </summary>
	public CryptoUtil(bool generateKey) : this() {
	    if(generateKey) {
		m_provider.GenerateKey();
		m_provider.GenerateIV();
		m_initializedKey = m_provider.Key;
		m_initializedIv = m_provider.IV;
	    }
	    else {
		GetSymmetricKey();
	    }
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Spring2.Core.Cryptography.CryptoUtil"/> class.
	/// </summary>
	public CryptoUtil(byte[] key) : this() {
	    m_initializedKey = key;
	    m_initializedIv = m_defaultInitializationVector;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Spring2.Core.Cryptography.CryptoUtil"/> class.
	/// </summary>
	public CryptoUtil(string secret) : this() {
	    m_initializedSecret = secret;
	    m_initializedKey = GetKeyFromSecret(secret);
	    m_initializedIv = m_keyFromSecretIv;
	}
    
	#endregion

	#region Public Properties

	/// <summary>
	/// Gets the key associated with this instance.
	/// </summary>
	public byte[] Key {
	    get {
		return m_initializedKey;
	    }
	}

	/// <summary>
	/// Gets the initialization vector associated with this instance.
	/// </summary>
	public byte[] Iv {
	    get {
		return m_initializedIv;
	    }
	}

	/// <summary>
	/// Gets the secret associated with this instance.
	/// </summary>
	public string Secret {
	    get {
		return m_initializedSecret;
	    }
	}
	#endregion

	#region Public Members

	public string Encrypt(string clearText) {
	    //Add the timeout.
	    StringBuilder encrypt = new StringBuilder(DateTime.Now.ToFileTimeUtc().ToString());
	    encrypt.Append(TIMESTAMP_DELIMITER).Append(clearText);

	    byte[] bytes = System.Text.UnicodeEncoding.Unicode.GetBytes(encrypt.ToString());
	    ICryptoTransform Encrypt = m_provider.CreateEncryptor(m_initializedKey, m_initializedIv);
	    byte[] result = Encrypt.TransformFinalBlock(bytes, 0, bytes.Length);
	    return Convert.ToBase64String(result);
	}

	public string EncryptNoTimestamp(string clearText) {
	    byte[] bytes = System.Text.UnicodeEncoding.Unicode.GetBytes(clearText);
	    ICryptoTransform Encrypt = m_provider.CreateEncryptor(m_initializedKey, m_initializedIv);
	    byte[] result = Encrypt.TransformFinalBlock(bytes, 0, bytes.Length);
	    return Convert.ToBase64String(result);
	}

	/// <summary>
	/// Decrypts a string with a timeout of TimeSpan.MaxValue.
	/// </summary>
	/// <param name="cipherText"></param>
	/// <returns></returns>
	public string Decrypt(string cipherText) {
	    return Decrypt(cipherText, TimeSpan.MaxValue);
	}

	/// <summary>
	/// THIS METHOD EXISTS FOR INTEROPERABILITY ONLY.
	/// </summary>
	/// <param name="cipherText"></param>
	/// <param name="timeout">Number of milliseconds allowed before a timeout occurs.</param>
	/// <returns></returns>
	public string DecryptWithTimeout(string cipherText, int timeout) {
	    return Decrypt(cipherText, new TimeSpan(0, 0, 0, 0, timeout));
	}

	/// <summary>
	/// Decrypts a string with a specified timeout.
	/// </summary>
	/// <param name="cipherText"></param>
	/// <param name="timeout"></param>
	/// <returns></returns>
	public string Decrypt(string cipherText, TimeSpan timeout) {
	    byte[] bytes = Convert.FromBase64String(cipherText);
	    ICryptoTransform Decrypt = m_provider.CreateDecryptor(m_initializedKey, m_initializedIv);
	    string clearText = System.Text.UnicodeEncoding.Unicode.GetString(Decrypt.TransformFinalBlock(bytes, 0, bytes.Length));

	    int index = clearText.IndexOf(TIMESTAMP_DELIMITER);
	    if(index > 0){
		try{
		    // 
		    // Get the sent time from the message.
		    //
		    DateTime utcTime = DateTime.FromFileTimeUtc(long.Parse(clearText.Substring(0, index)));
		    TimeSpan lifeTime = DateTime.Now.Subtract(utcTime);
		    if(lifeTime > timeout) {
			return string.Empty;
		    }

		    // 
		    // Return the clear text without the time component.
		    //
		    return clearText.Substring(index + 1);
		}catch{
		    //
		    // Return the clear text.  No Time component was found.
		    //
		    return clearText;
		}
	    }

	    //
	    // Return the clear text.  No Time component was found.
	    //
	    return clearText;
	}

	#endregion

	#region Public Static Members

	/// <summary>
	/// Hashes the provided byte array to produce a fingerprint, uses the specified hash algorithm.
	/// </summary>
	/// <param name="algorithm">The name of the hash algorithm to be used.</param>
	/// <param name="input">The bytes to be hashed.</param>
	/// <param name="offset">Where in the byte array to begin the hash computation.</param>
	/// <param name="count">The number of bytes from the offset to use for the computation.</param>
	/// <returns>The computed hash of the input. Note that the length varies by algorithm.</returns>
	public static byte[] ComputeHash(string algorithm, byte[] input, int offset, int count) {
	    if(null == algorithm || algorithm.Length < 1) {
		throw new ArgumentException("algorithm");
	    }
		    
	    if(null == input || input.Length == 0) {
		throw new ArgumentException("input");
	    }

	    if(offset < 0 || offset >= input.Length) {
		throw new ArgumentException("offset");
	    }

	    if(count < 1 || count > (input.Length - offset)) {
		throw new ArgumentException("count");
	    }

	    lock(typeof(CryptoUtil)) {
		HashAlgorithm hash = (HashAlgorithm)CryptoConfig.CreateFromName(algorithm);
		return hash.ComputeHash(input, offset, count);
	    }
	}

	/// <summary>
	/// Hashes the provided byte array to produce a fingerprint, uses the specified hash algorithm.
	/// </summary>
	/// <param name="algorithm">The name of the hash algorithm to be used.</param>
	/// <param name="input">The bytes to be hashed.</param>
	/// <returns>The computed hash of the input. Note that the length varies by algorithm.</returns>
	public static byte[] ComputeHash(string algorithm, byte[] input) {
	    return ComputeHash(algorithm, input, 0, input.Length);
	}

	/// <summary>
	/// Hashes the provided byte array to produce a fingerprint, uses the specified hash algorithm.
	/// </summary>
	/// <param name="algorithm">The name of the hash algorithm to be used.</param>
	/// <param name="input">The string to be hashed.</param>
	/// <returns>The computed hash of the input. Note that the length varies by algorithm.</returns>
	public static byte[] ComputeHash(string algorithm, string input) {
	    if(null == input) {
		throw new ArgumentNullException("input");
	    }

	    if(0 == input.Length) {
		throw new ArgumentOutOfRangeException("input");
	    }

	    UTF8Encoding encoding = new UTF8Encoding();
	    byte[] bytes = encoding.GetBytes(input);
	    return ComputeHash(algorithm, bytes, 0, bytes.Length);
	}

	/// <summary>
	/// Computes the SHA-1 hash of a given string.
	/// </summary>
	/// <param name="message"></param>
	/// <returns></returns>
	public static byte[] ComputeHash(string message) {
	    if(null == message) {
		throw new ArgumentNullException("message");
	    }

	    //
	    // Create a new instance of UnicodeEncoding to 
	    // convert the string into an array of Unicode bytes.
	    //
	    UnicodeEncoding encoding = new UnicodeEncoding();

	    //
	    // Convert the string into an array of bytes.
	    //
	    byte[] bytes = encoding.GetBytes(message);

	    lock(typeof(CryptoUtil)) {
		//
		// Create a new instance of SHA1Managed to create 
		// the hash value.
		//
		SHA1Managed hash = new SHA1Managed();

		//
		// Create the hash value from the array of bytes.
		//
		return hash.ComputeHash(bytes);
	    }
	}

	#endregion

	#region Private Members
	    
	private byte[] GetKeyFromSecret(string secret) {
	    PasswordDeriveBytes KeyDerive = new PasswordDeriveBytes(secret, m_keyFromSecretSeed);
	    return KeyDerive.CryptDeriveKey("RC2", "MD5", 128, m_keyFromSecretIv);
	}

	private void GetSymmetricKey() {
	    try {
		m_initializedSecret = ConfigurationProvider.Instance.Settings["EncryptionSecret"].ToString();
		m_initializedIv = Convert.FromBase64String(ConfigurationProvider.Instance.Settings["EncryptionInitializationVector"].ToString());
		m_initializedKey = GetKeyFromSecret(m_initializedSecret);
	    }
	    catch {
		m_initializedKey = m_defaultKey;
		m_initializedIv = m_defaultInitializationVector;
	    }
	}

	#endregion
    }
}

using System;
using Spring2.Core.Types;

namespace Spring2.Core.Geocode.WebService {
	/// <summary>
	/// Summary description for TeleAtlasAuthenticator.
	/// </summary>
	public class TeleAtlasAuthenticator : ITeleAtlasAuthenticator {

	    private TeleAtlasAuthenticator() {}

	    #region Members
	    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	    private com.teleatlas.na.ezlocate.authentication.Authentication authenticator;
	    private static ITeleAtlasAuthenticator instance = null;
	    private int credential;
	    private DateTime validUntil;
	    private string userName;
	    private string password;
	    private string host;

            	    
	    #endregion
	
	    #region Properties

	    public static ITeleAtlasAuthenticator Instance  {
		get {
		    if (instance == null) {
			instance = new TeleAtlasAuthenticator();
		    }
		    return instance;
		}
	    }

	    private com.teleatlas.na.ezlocate.authentication.Authentication Authenticator {
		get {
		    if (authenticator == null) {
			authenticator = new com.teleatlas.na.ezlocate.authentication.Authentication();
			authenticator.Url = "http://" + host + "/axis/services/Authentication";
		    }
		    return authenticator;
		}
	    }

	    #endregion

	    #region Methods

	    /// <summary>
	    /// authenticates the user against the Tele Atlas server
	    /// </summary>
	    /// <param name="userName"></param>
	    /// <param name="password"></param>
	    /// <param name="host"></param>
	    /// <returns>int</returns>
	    public int Authenticate(string userName, string password, string host) {
		this.userName = userName;
		this.password = password;
		this.host = host;


		return GetCredential();
	    }


	    /// <summary>
	    /// uses a cached credential unless the timeout window has expired
	    /// new credentials are valid for 8 minutes
	    /// this function expires them after 5 minutes thus giving any caller
	    /// a guaranteed safety window during which the credential can be used.
	    /// </summary>
	    /// <returns>int</returns>
	    private int GetCredential() {
		if (credential == 0 || DateTime.Now > validUntil) {
		    credential = CreateCredential(8);
		    validUntil = DateTime.Now.AddMinutes(5);
		}

		return credential;
	    }

	    /// <summary>
	    /// authenticates with the server and returns the created credential value
	    /// </summary>
	    /// <param name="duration"></param>
	    /// <returns>int</returns>
	    private int CreateCredential(int duration) {
		int challenge;
		int result = Authenticator.requestChallenge(userName, duration, out challenge);

		if (result == 0) {
		    int key = ElfHash(password);
		    int unencryptedChallenge = challenge ^ key;
		    int permutedChallenge = Permute(unencryptedChallenge);
		    int response = permutedChallenge ^ key;
		    result = Authenticator.answerChallenge(response, challenge, out credential);
		    if (result != 0) {
			throw new Exception("Failed to authenticate by answering the challenge question properly.");
		    }
		} 
		else {
		    throw new Exception("Failed to authenticate by requesting a challenge.");
		}

		return credential;
	    }

	    private int ElfHash(StringType stringToHash) {
		long result;
		result = 0L;

		CharEnumerator ce = stringToHash.GetEnumerator();
		
		while (ce.MoveNext()) {
		    result = (result << 4) + ce.Current;
		    long x = result & 0xf0000000;
		    if (x != 0) {
			result = result ^ (x >> 24);
			result = result & (~x);
		    }
		}
		return (int)result;
	    }
		
	    private int Permute(int inputValue) {
		long result = inputValue;
		result *= 39371;
		return (int)(result % 0x3fffffff);
	    }

	    private int MakeKey(string account) {
		return ElfHash(account) % 0x3fffffff;
	    }

	    private void DropCredential(int credential) {			
		int result = Authenticator.invalidateCredential(credential); 
		if (result != 0) {
		    throw new Exception("Failed to drop credential.");
		}
	    }

	    #endregion



	}
}

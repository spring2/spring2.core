using System;
using Spring2.Core.Types;

namespace Spring2.Core.Geocode.WebService
{
	/// <summary>
	/// Summary description for ITeleAtlasAuthenticator.
	/// </summary>
	public interface ITeleAtlasAuthenticator {
		int Authenticate(string userName, string password, string host);
	}
}

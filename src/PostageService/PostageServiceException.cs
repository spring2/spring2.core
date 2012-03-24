using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService {
    public class PostageServiceException : Spring2.Core.Message.Message {
	public PostageServiceException(String message)
	    : base(message) {
	}
    }
}

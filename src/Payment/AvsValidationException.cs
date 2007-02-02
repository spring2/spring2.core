using System;
using Spring2.Core.Types;

namespace Spring2.Dss.Payment {

    /// <summary>
    /// Exception for Avs validation failures
    /// </summary>
    public class AvsValidationException : PaymentFailureException {

    	private StringType transactionId;

	public StringType TransactionId {
	    get { return transactionId; }
	}

    	public AvsValidationException(PaymentResult result, StringType transactionId) : base(result, "Address verification failed. {0}:{1}.", result.AVSResponseCode, result.ResultMessage) {
	    this.result = result;
	    this.transactionId = transactionId;
	}
    }
}


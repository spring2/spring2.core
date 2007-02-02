using System;
using Spring2.Core.Types;

namespace Spring2.Dss.Payment {

    /// <summary>
    /// Exception for Cvv validation failures
    /// </summary>
    public class CvvValidationException : PaymentFailureException {

	private StringType transactionId;

	public StringType TransactionId {
	    get { return transactionId; }
	}

    	public CvvValidationException(PaymentResult result, StringType transactionId) : base(result, "CVV verification failed. {0}:{1}.", result.CVVResponseCode, result.ResultMessage) {
	    this.result = result;
	    this.transactionId = transactionId;
	}
    }
}


using System;
using Spring2.Core.Types;
using Spring2.Core.Payment;

namespace Spring2.Core.Payment {
    /// <summary>
    /// Exception to indicate that the payment request has failed and that no funds should be moved.  This would include things like declines and card validation issues.
    /// </summary>
    public class PaymentFailureException : PaymentException {

	protected PaymentResult result;

	public PaymentResult Result {
	    get { return result; }
	}
    	
    	public PaymentFailureException(PaymentResult result, String format, String code, String message) : base(String.Format(format, code, message)) {
    	    this.result = result;
    	}

	public PaymentFailureException(PaymentResult result) : base(string.Format("Failed to process the payment. {0}:{1}.", result.ResultCode, result.ResultMessage)){
	    this.result = result;
	}
    }
}

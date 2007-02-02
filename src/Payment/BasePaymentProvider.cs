using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Spring2.Core.Payment {

    /// <summary>
    /// Summary description for BasePaymentProvider.
    /// </summary>
    public abstract class BasePaymentProvider {

    	/*
	TEST CREDIT CARD NUMBERS:

	These international credit card numbers can be used to test the entire system. They are numbers that banks will reject as invalid without problem and are intended for system testing. 
	Credit Card  	Sample Number 
			    Visa  	4111 1111 1111 1111 (best test cc to use)
	4007 0000 0002 7
	4242 4242 4242 4242 (often will decline even in test mode)
	MasterCard  	5500 0000 0000 0004
	5424 0000 0000 0015 
	American Express  	3782 8224 6310 005
	3400 0000 0000 009
	3700 0000 0000 002 
	Diner's Club  	3000 0000 0000 04 
	Carte Blanche  	3000 0000 0000 04 
	Discover  	6011 1111 1111 1117
	6011 0000 0000 0004
	6011 0000 0000 0012 
	en Route  	2014 0000 0000 009 
	JCB  	3088 0000 0000 0009 
	Special SendSafe Test Visa number 	0111-1111-1111-1111
    	*/
    	
	private CardType _cardTypes;

	public BasePaymentProvider() {
	    _cardTypes = CardType.All | CardType.Unknown;	// Accept everything
	}

	public enum CardType {
	    MasterCard	= 0x0001,
	    VISA	= 0x0002,
	    Amex	= 0x0004,
	    DinersClub	= 0x0008,
	    enRoute	= 0x0010,
	    Discover	= 0x0020,
	    JCB		= 0x0040,
	    Unknown	= 0x0080,
	    All		= CardType.Amex | CardType.DinersClub | CardType.Discover | CardType.Discover | CardType.enRoute | CardType.JCB | CardType.MasterCard | CardType.VISA
	}

    	/// <summary>
	/// Will ensure that the card is a valid length for the card type. If the
	/// card type isn't recognised it will return true by default.
	/// 
	/// The CreditCardValidator control also includes a CardTypes property that determines
	/// what card types should be accepted. If the card isn't recognised, and the Unknown card
	/// type is in the AcceptedCardTypes property then the DefaultLengthTest value will be
	/// returned.
	/// </summary>
	public CardType GetCardType(string cardNumber) {
	    if ((Regex.IsMatch(cardNumber, "^(34|37)")) && ((_cardTypes & CardType.Amex) != 0)) {
		// AMEX -- 34 or 37 -- 15 length
		return CardType.Amex;
	    } else if ((Regex.IsMatch(cardNumber, "^(51|52|53|54|55)")) && ((_cardTypes & CardType.MasterCard) != 0)) {
		// MasterCard -- 51 through 55 -- 16 length
		return CardType.MasterCard;
	    } else if ((Regex.IsMatch(cardNumber, "^(4)")) && ((_cardTypes & CardType.VISA) != 0)) {
		// VISA -- 4 -- 13 and 16 length
		return CardType.VISA;
	    } else if ((Regex.IsMatch(cardNumber, "^(300|301|302|303|304|305|36|38)")) && ((_cardTypes & CardType.DinersClub) != 0)) {
		// Diners Club -- 300-305, 36 or 38 -- 14 length
		return CardType.DinersClub;
	    } else if ((Regex.IsMatch(cardNumber, "^(2014|2149)")) && ((_cardTypes & CardType.DinersClub) != 0)) {
		// enRoute -- 2014,2149 -- 15 length
		return CardType.enRoute;
	    } else if ((Regex.IsMatch(cardNumber, "^(6011)")) && ((_cardTypes & CardType.Discover) != 0)) {
		// Discover -- 6011 -- 16 length
		return CardType.Discover;
	    } else if ((Regex.IsMatch(cardNumber, "^(3)")) && ((_cardTypes & CardType.JCB) != 0)) {
		// JCB -- 3 -- 16 length
		return CardType.JCB;
	    } else if ((Regex.IsMatch(cardNumber, "^(2131|1800)")) && ((_cardTypes & CardType.JCB) != 0)) {
		// JCB -- 2131, 1800 -- 15 length
		return CardType.JCB;
	    } else {
		return CardType.Unknown;
	    }
	}
    	
	/// <summary>
	/// Will ensure that the card is a valid length for the card type. If the
	/// card type isn't recognised it will return true by default.
	/// 
	/// The CreditCardValidator control also includes a CardTypes property that determines
	/// what card types should be accepted. If the card isn't recognised, and the Unknown card
	/// type is in the AcceptedCardTypes property then the DefaultLengthTest value will be
	/// returned.
	/// </summary>
	public Boolean IsValidCardType( string cardNumber ) {	
	    CardType cardType = GetCardType(cardNumber);
	    // AMEX -- 34 or 37 -- 15 length
	    if (CardType.Amex == cardType)
		return (15==cardNumber.Length);
			
		// MasterCard -- 51 through 55 -- 16 length
	    else if (CardType.MasterCard == cardType)
		return (16==cardNumber.Length);

		// VISA -- 4 -- 13 and 16 length
	    else if (CardType.VISA == cardType)
		return (13==cardNumber.Length||16==cardNumber.Length);

		// Diners Club -- 300-305, 36 or 38 -- 14 length
	    else if (CardType.DinersClub == cardType)
		return (14==cardNumber.Length);

		// enRoute -- 2014,2149 -- 15 length
	    else if (CardType.DinersClub == cardType)
		return (15==cardNumber.Length);

		// Discover -- 6011 -- 16 length
	    else if ( (Regex.IsMatch(cardNumber,"^(6011)")) && ((_cardTypes & CardType.Discover)!=0) )
		return (16==cardNumber.Length);

		// JCB -- 3 -- 16 length
	    else if ( (Regex.IsMatch(cardNumber,"^(3)")) && ((_cardTypes & CardType.JCB)!=0) )
		return (16==cardNumber.Length);

		// JCB -- 2131, 1800 -- 15 length
	    else if ( (Regex.IsMatch(cardNumber,"^(2131|1800)")) && ((_cardTypes & CardType.JCB)!=0) )
		return (15==cardNumber.Length);
	    else {
		// Card type wasn't recognised, provided Unknown is in the CardTypes property, then
		// return true, otherwise return false.
		if ( (_cardTypes & CardType.Unknown)!=0 )
		    return true;
		else
		    return false;
	    }
	}


	/// <summary>
	/// Performs a validation using Luhn's Formula.
	/// </summary>
	protected Boolean ValidateCardNumber(string cardNumber) {
	    try {
		// Array to contain individual numbers
		System.Collections.ArrayList checkNumbers = new ArrayList();
		// So, get length of card
		int cardLength = cardNumber.Length;
    
		// Double the value of alternate digits, starting with the second digit
		// from the right, i.e. back to front.
		// Loop through starting at the end
		for (int i = cardLength-2; i >= 0; i = i - 2) {
		    // Now read the contents at each index, this
		    // can then be stored as an array of integers

		    // Double the number returned
		    checkNumbers.Add( Int32.Parse(cardNumber[i].ToString())*2 );
		}

		int checkSum = 0;    // Will hold the total sum of all checksum digits
            
		// Second stage, add separate digits of all products
		for (int iCount = 0; iCount <= checkNumbers.Count-1; iCount++) {
		    int _count = 0;    // will hold the sum of the digits

		    // determine if current number has more than one digit
		    if ((int)checkNumbers[iCount] > 9) {
			int _numLength = ((int)checkNumbers[iCount]).ToString().Length;
			// add count to each digit
			for (int x = 0; x < _numLength; x++) {
			    _count = _count + Int32.Parse( 
				((int)checkNumbers[iCount]).ToString()[x].ToString() );
			}
		    } else {
			// single digit, just add it by itself
			_count = (int)checkNumbers[iCount];   
		    }
		    checkSum = checkSum + _count;    // add sum to the total sum
		}
		// Stage 3, add the unaffected digits
		// Add all the digits that we didn't double still starting from the
		// right but this time we'll start from the rightmost number with 
		// alternating digits
		int originalSum = 0;
		for (int y = cardLength-1; y >= 0; y = y - 2) {
		    originalSum = originalSum + Int32.Parse(cardNumber[y].ToString());
		}

		// Perform the final calculation, if the sum Mod 10 results in 0 then
		// it's valid, otherwise its false.
		return (((originalSum+checkSum)%10)==0);
	    } catch {
		return false;
	    }
	}
	
    }
}

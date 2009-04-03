using System;
using System.Collections;
using Spring2.Core.Types;

namespace Spring2.Core.Mail.Types {

    public class MailMessageTypeEnum : Spring2.Core.Types.EnumDataType, IMailMessageTypeEnum {

	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	public static readonly new MailMessageTypeEnum DEFAULT = new MailMessageTypeEnum();
	public static readonly new MailMessageTypeEnum UNSET = new MailMessageTypeEnum();

	public static readonly MailMessageTypeEnum NEW_USER = new MailMessageTypeEnum("New User", "New User");
	public static readonly MailMessageTypeEnum UNIT_TEST = new MailMessageTypeEnum("Unit Test", "Unit Test");
	public static readonly MailMessageTypeEnum FUNDRAISER_APPROVED = new MailMessageTypeEnum("Fundraiser Approved", "Fundraiser Approved");
	public static readonly MailMessageTypeEnum FUNDRAISER_DENIED = new MailMessageTypeEnum("Fundraiser Denied", "Fundraiser Denied");
	public static readonly MailMessageTypeEnum CUSTOMER_EMAIL = new MailMessageTypeEnum("Customer E-Mail", "Customer E-Mail");
	public static readonly MailMessageTypeEnum CONTRACT_RENEWAL = new MailMessageTypeEnum("Contract Renewal", "Contract Renewal");
	public static readonly MailMessageTypeEnum TRANSITION_CONTACT = new MailMessageTypeEnum("Transition Contact", "Transition Contact");
	public static readonly MailMessageTypeEnum NEW_RECRUIT = new MailMessageTypeEnum("New Recruit", "New Recruit");
	public static readonly MailMessageTypeEnum RECRUIT_RENEWED = new MailMessageTypeEnum("Recruit Renewed", "Recruit Renewed");
	public static readonly MailMessageTypeEnum ERROR_DURING_ENROLLMENT = new MailMessageTypeEnum("Error During Enrollment", "Error During Enrollment");
	public static readonly MailMessageTypeEnum NEW_DEMONSTRATOR = new MailMessageTypeEnum("New Demonstrator", "New Demonstrator");
	public static readonly MailMessageTypeEnum ORDER_CONFIRMATION = new MailMessageTypeEnum("Order Confirmation", "Order Confirmation");
	public static readonly MailMessageTypeEnum USER_INFORMATION = new MailMessageTypeEnum("User Information", "User Information");
	public static readonly MailMessageTypeEnum REFERRAL_NOTIFICATION_EMAIL = new MailMessageTypeEnum("Referral Notification E-Mail", "Referral Notification E-Mail");
	public static readonly MailMessageTypeEnum REFERRAL_PROSPECT_EMAIL = new MailMessageTypeEnum("Referral Prospect E-Mail", "Referral Prospect E-Mail");
	public static readonly MailMessageTypeEnum CONTACT = new MailMessageTypeEnum("Contact", "Contact");
	public static readonly MailMessageTypeEnum DOWNLINEAGENT = new MailMessageTypeEnum("DownLine Agent", "DownLineAgent");
	public static readonly MailMessageTypeEnum REFERRAL_CORPORATE_EMAIL = new MailMessageTypeEnum("Referral Corporate E-Mail", "Referral Corporate E-Mail");
	public static readonly MailMessageTypeEnum SHIPMENT_NOTIFICATION = new MailMessageTypeEnum("Shipment Notification", "Shipment Notification");
	public static readonly MailMessageTypeEnum DEMONSTRATOR_SHOPPING_CART_EMAIL = new MailMessageTypeEnum("Demonstrator Shopping Cart Email", "Demonstrator Shopping Cart Email");
	public static readonly MailMessageTypeEnum ERROR_DURING_WEBSITE_SUBSCRIPTION_PAYMENT = new MailMessageTypeEnum("Error During Website Subscription Payment", "Error During Website Subscription Payment");
	public static readonly MailMessageTypeEnum QUICK_START_PERSONAL_SALES_LEVEL_1_CONGRATULATIONS = new MailMessageTypeEnum("Quick Start Personal Sales Level 1 Congratulations", "Quick Start Personal Sales Level 1 Congratulations");
	public static readonly MailMessageTypeEnum QUICK_START_PERSONAL_SALES_LEVEL_2_CONGRATULATIONS = new MailMessageTypeEnum("Quick Start Personal Sales Level 2 Congratulations", "Quick Start Personal Sales Level 2 Congratulations");
	public static readonly MailMessageTypeEnum QUICK_START_PERSONAL_SALES_LEVEL_3_CONGRATULATIONS = new MailMessageTypeEnum("Quick Start Personal Sales Level 3 Congratulations", "Quick Start Personal Sales Level 3 Congratulations");
	public static readonly MailMessageTypeEnum QUICK_START_PERSONAL_SALES_LEVEL_1_REGRETS = new MailMessageTypeEnum("Quick Start Personal Sales Level 1 Regrets", "Quick Start Personal Sales Level 1 Regrets");
	public static readonly MailMessageTypeEnum QUICK_START_PERSONAL_SALES_LEVEL_2_REGRETS = new MailMessageTypeEnum("Quick Start Personal Sales Level 2 Regrets", "Quick Start Personal Sales Level 2 Regrets");
	public static readonly MailMessageTypeEnum QUICK_START_PERSONAL_SALES_LEVEL_1_REMINDER = new MailMessageTypeEnum("Quick Start Personal Sales Level 1 Reminder", "Quick Start Personal Sales Level 1 Reminder");
	public static readonly MailMessageTypeEnum QUICK_START_PERSONAL_SALES_LEVEL_2_REMINDER = new MailMessageTypeEnum("Quick Start Personal Sales Level 2 Reminder", "Quick Start Personal Sales Level 2 Reminder");
	public static readonly MailMessageTypeEnum QUICK_START_PERSONAL_SALES_LEVEL_3_REMINDER = new MailMessageTypeEnum("Quick Start Personal Sales Level 3 Reminder", "Quick Start Personal Sales Level 3 Reminder");
	public static readonly MailMessageTypeEnum QUICK_START_OVER = new MailMessageTypeEnum("Quick Start Over", "Quick Start Over");
	public static readonly MailMessageTypeEnum QUICK_START_ENROLLMENT = new MailMessageTypeEnum("Quick Start Enrollment", "Quick Start Enrollment");

	public static MailMessageTypeEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (MailMessageTypeEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private MailMessageTypeEnum() {}

	private MailMessageTypeEnum(String code, String name) {
	    this.code = code;
	    this.name = name;
	    OPTIONS.Add(this);
	}

	public override Boolean IsDefault {
	    get { return Object.ReferenceEquals(this, DEFAULT); }
	}

	public override Boolean IsUnset {
	    get { return Object.ReferenceEquals(this, UNSET); }
	}

	public static EnumDataTypeList Options {
	    get { return OPTIONS; }
	}
    }
}

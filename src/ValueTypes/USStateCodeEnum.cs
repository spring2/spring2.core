using System;
using System.Collections;

namespace Spring2.Types {

    public class USStateCodeEnum : EnumDataType {

	private static readonly IList OPTIONS = new ArrayList();

	public static readonly new USStateCodeEnum DEFAULT = new USStateCodeEnum();
	public static readonly new USStateCodeEnum UNSET = new USStateCodeEnum();

	public static readonly USStateCodeEnum ALABAMA = new USStateCodeEnum("AL", "AL");
	public static readonly USStateCodeEnum ALASKA = new USStateCodeEnum("AK", "AK");
	public static readonly USStateCodeEnum AMERICAN_SAMOA = new USStateCodeEnum("AS", "AS");
	public static readonly USStateCodeEnum ARIZONA = new USStateCodeEnum("AZ", "AZ");
	public static readonly USStateCodeEnum ARKANSAS = new USStateCodeEnum("AR", "AR");
	public static readonly USStateCodeEnum CALIFORNIA = new USStateCodeEnum("CA", "CA");
	public static readonly USStateCodeEnum COLORADO = new USStateCodeEnum("CO", "CO");
	public static readonly USStateCodeEnum CONNECTICUT = new USStateCodeEnum("CT", "CT");
	public static readonly USStateCodeEnum DELAWARE = new USStateCodeEnum("DE", "DE");
	public static readonly USStateCodeEnum DISTRICT_OF_COLUMBIA = new USStateCodeEnum("DC", "DC");
	public static readonly USStateCodeEnum FLORIDA = new USStateCodeEnum("FL", "FL");
	public static readonly USStateCodeEnum GEORGIA = new USStateCodeEnum("GA", "GA");
	public static readonly USStateCodeEnum GUAM = new USStateCodeEnum("GU", "GU");
	public static readonly USStateCodeEnum HAWAII = new USStateCodeEnum("HI", "HI");
	public static readonly USStateCodeEnum IDAHO = new USStateCodeEnum("ID", "ID");
	public static readonly USStateCodeEnum ILLINOIS = new USStateCodeEnum("IL", "IL");
	public static readonly USStateCodeEnum INDIANA = new USStateCodeEnum("IN", "IN");
	public static readonly USStateCodeEnum IOWA = new USStateCodeEnum("IA", "IA");
	public static readonly USStateCodeEnum KANSAS = new USStateCodeEnum("KS", "KS");
	public static readonly USStateCodeEnum KENTUCKY = new USStateCodeEnum("KY", "KY");
	public static readonly USStateCodeEnum LOUISIANA = new USStateCodeEnum("LA", "LA");
	public static readonly USStateCodeEnum MAINE = new USStateCodeEnum("ME", "ME");
	public static readonly USStateCodeEnum MARYLAND = new USStateCodeEnum("MD", "MD");
	public static readonly USStateCodeEnum MASSACHUSETTS = new USStateCodeEnum("MA", "MA");
	public static readonly USStateCodeEnum MICHIGAN = new USStateCodeEnum("MI", "MI");
	public static readonly USStateCodeEnum MINNESOTA = new USStateCodeEnum("MN", "MN");
	public static readonly USStateCodeEnum MISSISSIPPI = new USStateCodeEnum("MS", "MS");
	public static readonly USStateCodeEnum MISSOURI = new USStateCodeEnum("MO", "MO");
	public static readonly USStateCodeEnum MONTANA = new USStateCodeEnum("MT", "MT");
	public static readonly USStateCodeEnum NEBRASKA = new USStateCodeEnum("NE", "NE");
	public static readonly USStateCodeEnum NEVADA = new USStateCodeEnum("NV", "NV");
	public static readonly USStateCodeEnum NEW_HAMPSHIRE = new USStateCodeEnum("NH", "NH");
	public static readonly USStateCodeEnum NEW_JERSEY = new USStateCodeEnum("NJ", "NJ");
	public static readonly USStateCodeEnum NEW_MEXICO = new USStateCodeEnum("NM", "NM");
	public static readonly USStateCodeEnum NEW_YORK = new USStateCodeEnum("NY", "NY");
	public static readonly USStateCodeEnum NORTH_CAROLINA = new USStateCodeEnum("NC", "NC");
	public static readonly USStateCodeEnum NORTH_DAKOTA = new USStateCodeEnum("ND", "ND");
	public static readonly USStateCodeEnum OHIO = new USStateCodeEnum("OH", "OH");
	public static readonly USStateCodeEnum OKLAHOMA = new USStateCodeEnum("OK", "OK");
	public static readonly USStateCodeEnum OREGON = new USStateCodeEnum("OR", "OR");
	public static readonly USStateCodeEnum PENNSYLVANIA = new USStateCodeEnum("PA", "PA");
	public static readonly USStateCodeEnum PUERTO_RICO = new USStateCodeEnum("PR", "PR");
	public static readonly USStateCodeEnum RHODE_ISLAND = new USStateCodeEnum("RI", "RI");
	public static readonly USStateCodeEnum SOUTH_CAROLINA = new USStateCodeEnum("SC", "SC");
	public static readonly USStateCodeEnum SOUTH_DAKOTA = new USStateCodeEnum("SD", "SD");
	public static readonly USStateCodeEnum TENNESSEE = new USStateCodeEnum("TN", "TN");
	public static readonly USStateCodeEnum TEXAS = new USStateCodeEnum("TX", "TX");
	public static readonly USStateCodeEnum UTAH = new USStateCodeEnum("UT", "UT");
	public static readonly USStateCodeEnum VERMONT = new USStateCodeEnum("VT", "VT");
	public static readonly USStateCodeEnum VIRGINIA = new USStateCodeEnum("VA", "VA");
	public static readonly USStateCodeEnum VIRGIN_ISLANDS = new USStateCodeEnum("VI", "VI");
	public static readonly USStateCodeEnum WASHINGTON = new USStateCodeEnum("WA", "WA");
	public static readonly USStateCodeEnum WEST_VIRGINIA = new USStateCodeEnum("WV", "WV");
	public static readonly USStateCodeEnum WISCONSIN = new USStateCodeEnum("WI", "WI");
	public static readonly USStateCodeEnum WYOMING = new USStateCodeEnum("WY", "WY");

	public static USStateCodeEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (USStateCodeEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private USStateCodeEnum() {}

	private USStateCodeEnum(String code, String name) {
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

	public static IList Options {
	    get { return OPTIONS; }
	}
    }
}

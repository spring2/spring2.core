/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Google.Api.Maps.Service.Geocoding;
using Newtonsoft.Json;

namespace Google.Api.Maps.Service {
    #region Private extensions

    internal static class JsonEnumTypeConverterExtensions {
	public static ServiceResponseStatus AsResponseStatus(this string s) {
	    var result = ServiceResponseStatus.Unknown;

	    switch (s) {
		case "OK":
		    result = ServiceResponseStatus.Ok;
		    break;
		case "ZERO_RESULTS":
		    result = ServiceResponseStatus.ZeroResults;
		    break;
		case "OVER_QUERY_LIMIT":
		    result = ServiceResponseStatus.OverQueryLimit;
		    break;
		case "REQUEST_DENIED":
		    result = ServiceResponseStatus.RequestDenied;
		    break;
		case "INVALID_REQUEST":
		    result = ServiceResponseStatus.InvalidRequest;
		    break;
	    }

	    return result;
	}

	public static AddressType AsAddressType(this string s) {
	    var result = AddressType.Unknown;

	    switch (s) {
		case "street_address":
		    result = AddressType.StreetAddress;
		    break;
		case "route":
		    result = AddressType.Route;
		    break;
		case "intersection":
		    result = AddressType.Intersection;
		    break;
		case "political":
		    result = AddressType.Political;
		    break;
		case "country":
		    result = AddressType.Country;
		    break;
		case "administrative_area_level_1":
		    result = AddressType.AdministrativeAreaLevel1;
		    break;
		case "administrative_area_level_2":
		    result = AddressType.AdministrativeAreaLevel2;
		    break;
		case "administrative_area_level_3":
		    result = AddressType.AdministrativeAreaLevel3;
		    break;
		case "colloquial_area":
		    result = AddressType.ColloquialArea;
		    break;
		case "locality":
		    result = AddressType.Locality;
		    break;
		case "sublocality":
		    result = AddressType.Sublocality;
		    break;
		case "neighborhood":
		    result = AddressType.Neighborhood;
		    break;
		case "premise":
		    result = AddressType.Premise;
		    break;
		case "subpremise":
		    result = AddressType.Subpremise;
		    break;
		case "postal_code":
		    result = AddressType.PostalCode;
		    break;
		case "natural_feature":
		    result = AddressType.NaturalFeature;
		    break;
		case "airport":
		    result = AddressType.Airport;
		    break;
		case "park":
		    result = AddressType.Park;
		    break;
		case "point_of_interest":
		    result = AddressType.PointOfInterest;
		    break;
		case "post_box":
		    result = AddressType.PostBox;
		    break;
		case "street_number":
		    result = AddressType.StreetNumber;
		    break;
		case "floor":
		    result = AddressType.Floor;
		    break;
		case "room":
		    result = AddressType.Room;
		    break;
	    }

	    return result;
	}

	public static LocationType AsLocationType(this string s) {
	    var result = LocationType.Unknown;

	    switch (s) {
		case "ROOFTOP":
		    result = LocationType.Rooftop;
		    break;
		case "RANGE_INTERPOLATED":
		    result = LocationType.RangeInterpolated;
		    break;
		case "GEOMETRIC_CENTER":
		    result = LocationType.GeometricCenter;
		    break;
		case "APPROXIMATE":
		    result = LocationType.Approximate;
		    break;
	    }

	    return result;
	}


	public static String FromResponseStatus(this ServiceResponseStatus s) {
	    var result = String.Empty;

	    switch (s) {
		case ServiceResponseStatus.Ok:
		    result = "OK";
		    break;
		case ServiceResponseStatus.ZeroResults:
		    result = "ZERO_RESULTS";
		    break;
		case ServiceResponseStatus.OverQueryLimit:
		    result = "OVER_QUERY_LIMIT";
		    break;
		case ServiceResponseStatus.RequestDenied:
		    result = "REQUEST_DENIED";
		    break;
		case ServiceResponseStatus.InvalidRequest:
		    result = "INVALID_REQUEST";
		    break;
	    }

	    return result;
	}

	public static String FromLocationType(this LocationType s) {
	    var result = String.Empty;

	    switch (s) {
		case LocationType.Rooftop:
		    result = "ROOFTOP";
		    break;
		case LocationType.RangeInterpolated:
		    result = "RANGE_INTERPOLATED";
		    break;
		case LocationType.GeometricCenter:
		    result = "GEOMETRIC_CENTER";
		    break;
		case LocationType.Approximate:
		    result = "APPROXIMATE";
		    break;
	    }

	    return result;
	}


	public static String FromAddressType(this AddressType s) {
	    var result = String.Empty;

	    switch (s) {
		case AddressType.StreetAddress:
		    result = "street_address";
		    break;
		case AddressType.Route:
		    result = "route";
		    break;
		case AddressType.Intersection:
		    result = "intersection";
		    break;
		case AddressType.Political:
		    result = "political";
		    break;
		case AddressType.Country:
		    result = "country";
		    break;
		case AddressType.AdministrativeAreaLevel1:
		    result = "administrative_area_level_1";
		    break;
		case AddressType.AdministrativeAreaLevel2:
		    result = "administrative_area_level_2";
		    break;
		case AddressType.AdministrativeAreaLevel3:
		    result = "administrative_area_level_3";
		    break;
		case AddressType.ColloquialArea:
		    result = "colloquial_area";
		    break;
		case AddressType.Locality:
		    result = "locality";
		    break;
		case AddressType.Sublocality:
		    result = "sublocality";
		    break;
		case AddressType.Neighborhood:
		    result = "neighborhood";
		    break;
		case AddressType.Premise:
		    result = "premise";
		    break;
		case AddressType.Subpremise:
		    result = "subpremise";
		    break;
		case AddressType.PostalCode:
		    result = "postal_code";
		    break;
		case AddressType.NaturalFeature:
		    result = "natural_feature";
		    break;
		case AddressType.Airport:
		    result = "airport";
		    break;
		case AddressType.Park:
		    result = "park";
		    break;
		case AddressType.PointOfInterest:
		    result = "point_of_interest";
		    break;
		case AddressType.PostBox:
		    result = "post_box";
		    break;
		case AddressType.StreetNumber:
		    result = "street_number";
		    break;
		case AddressType.Floor:
		    result = "floor";
		    break;
		case AddressType.Room:
		    result = "room";
		    break;
	    }

	    return result;
	}

    }

    #endregion

    public class JsonEnumTypeConverter : JsonConverter {
	public override bool CanConvert(Type objectType) {
	    return
		    objectType == typeof(ServiceResponseStatus)
		    || objectType == typeof(AddressType)
		    || objectType == typeof(LocationType);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
	    object result = null;

	    if (objectType == typeof(ServiceResponseStatus))
		result = reader.Value.ToString().AsResponseStatus();

	    if (objectType == typeof(AddressType))
		result = reader.Value.ToString().AsAddressType();

	    if (objectType == typeof(LocationType))
		result = reader.Value.ToString().AsLocationType();

	    return result;
	}


	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
	    if (value == typeof(ServiceResponseStatus))
		writer.WriteValue("\"" + ((ServiceResponseStatus)value).FromResponseStatus() + "\"");

	    if (value == typeof(AddressType))
		writer.WriteValue("\"" + ((AddressType)value).FromAddressType() + "\"");

	    if (value == typeof(LocationType))
		writer.WriteValue("\"" + ((LocationType)value).FromLocationType() + "\"");

	}
    }
}

using System;
using System.Collections;
using System.Data;
using Spring2.DataTierGenerator.Attribute;

namespace Spring2.Core.Tax {
	/// <summary>
	/// TaxRateInfoData generic collection
	/// </summary>
	public class TaxRateInfoList : CollectionBase {
		[Generate]
		public static readonly TaxRateInfoList UNSET = new TaxRateInfoList(true);

		[Generate]
		public static readonly TaxRateInfoList DEFAULT = new TaxRateInfoList(true);

		[Generate]
		private Boolean immutable = false;

		[Generate]
		private TaxRateInfoList(Boolean immutable) {
			this.immutable = immutable;
		}

		[Generate]
		public TaxRateInfoList() {
		}

		// Indexer implementation.
		[Generate]
		public TaxRateInfo this[int index] {
			get { return (TaxRateInfo) List[index]; }
			set {
				if (!immutable) {
					List[index] = value;
				} else {
					throw new ReadOnlyException();
				}
			}
		}

		[Generate]
		public void Add(TaxRateInfo value) {
			if (!immutable) {
				List.Add(value);
			} else {
				throw new ReadOnlyException();
			}
		}

		[Generate]
		public Boolean Contains(TaxRateInfo value) {
			return List.Contains(value);
		}

		[Generate]
		public Int32 IndexOf(TaxRateInfo value) {
			return List.IndexOf(value);
		}

		[Generate]
		public void Insert(Int32 index, TaxRateInfo value) {
			if (!immutable) {
				List.Insert(index, value);
			} else {
				throw new ReadOnlyException();
			}
		}

		[Generate]
		public void Remove(int index) {
			if (!immutable) {
				if (index > Count - 1 || index < 0) {
					throw new IndexOutOfRangeException();
				} else {
					List.RemoveAt(index);
				}
			} else {
				throw new ReadOnlyException();
			}
		}

		[Generate]
		public void Remove(TaxRateInfo value) {
			if (!immutable) {
				List.Remove(value);
			} else {
				throw new ReadOnlyException();
			}
		}

		[Generate]
		public void AddRange(IList list) {
			foreach (Object o in list) {
				if (o is TaxRateInfo) {
					Add((TaxRateInfo) o);
				} else {
					throw new InvalidCastException("object in list could not be cast to TaxRateInfoData");
				}
			}
		}

		[Generate]
		public Boolean IsDefault {
			get { return Object.ReferenceEquals(this, DEFAULT); }
		}

		[Generate]
		public Boolean IsUnset {
			get { return Object.ReferenceEquals(this, UNSET); }
		}

		[Generate]
		public Boolean IsValid {
			get { return !(IsDefault || IsUnset); }
		}


		/// <summary>
		/// Sort a list by a column
		/// </summary>
		[Generate]
		public void Sort(IComparer comparer) {
			this.InnerList.Sort(comparer);
		}

		/// <summary>
		/// Sort the list given the name of a comparer class.
		/// </summary>
		[Generate]
		public void Sort(String comparerName) {
			Type type = GetType().GetNestedType(comparerName);
			if (type == null) {
				throw new ArgumentException(String.Format("Comparer {0} not found in class {1}.", comparerName, GetType().Name));
			}

			IComparer comparer = Activator.CreateInstance(type) as IComparer;
			if (comparer == null) {
				throw new ArgumentException("compareName must be the name of class that implements IComparer.");
			}

			this.InnerList.Sort(comparer);
		}

		[Generate]
		public class CountryTaxRateSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxRateInfo o1 = (TaxRateInfo) a;
				TaxRateInfo o2 = (TaxRateInfo) b;

				if (o1 == null || o2 == null || !o1.CountryTaxRate.IsValid || !o2.CountryTaxRate.IsValid) {
					return 0;
				}
				return o1.CountryTaxRate.CompareTo(o2.CountryTaxRate);
			}
		}

		[Generate]
		public class RegionTaxRateSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxRateInfo o1 = (TaxRateInfo) a;
				TaxRateInfo o2 = (TaxRateInfo) b;

				if (o1 == null || o2 == null || !o1.RegionTaxRate.IsValid || !o2.RegionTaxRate.IsValid) {
					return 0;
				}
				return o1.RegionTaxRate.CompareTo(o2.RegionTaxRate);
			}
		}

		[Generate]
		public class CountyTaxRateSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxRateInfo o1 = (TaxRateInfo) a;
				TaxRateInfo o2 = (TaxRateInfo) b;

				if (o1 == null || o2 == null || !o1.CountyTaxRate.IsValid || !o2.CountyTaxRate.IsValid) {
					return 0;
				}
				return o1.CountyTaxRate.CompareTo(o2.CountyTaxRate);
			}
		}

		[Generate]
		public class CityTaxRateSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxRateInfo o1 = (TaxRateInfo) a;
				TaxRateInfo o2 = (TaxRateInfo) b;

				if (o1 == null || o2 == null || !o1.CityTaxRate.IsValid || !o2.CityTaxRate.IsValid) {
					return 0;
				}
				return o1.CityTaxRate.CompareTo(o2.CityTaxRate);
			}
		}

		[Generate]
		public class LocalDistrict1TaxRateSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxRateInfo o1 = (TaxRateInfo) a;
				TaxRateInfo o2 = (TaxRateInfo) b;

				if (o1 == null || o2 == null || !o1.LocalDistrict1TaxRate.IsValid || !o2.LocalDistrict1TaxRate.IsValid) {
					return 0;
				}
				return o1.LocalDistrict1TaxRate.CompareTo(o2.LocalDistrict1TaxRate);
			}
		}

		[Generate]
		public class LocalDistrict2TaxRateSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxRateInfo o1 = (TaxRateInfo) a;
				TaxRateInfo o2 = (TaxRateInfo) b;

				if (o1 == null || o2 == null || !o1.LocalDistrict2TaxRate.IsValid || !o2.LocalDistrict2TaxRate.IsValid) {
					return 0;
				}
				return o1.LocalDistrict2TaxRate.CompareTo(o2.LocalDistrict2TaxRate);
			}
		}

		[Generate]
		public class LocalDistrict3TaxRateSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxRateInfo o1 = (TaxRateInfo) a;
				TaxRateInfo o2 = (TaxRateInfo) b;

				if (o1 == null || o2 == null || !o1.LocalDistrict3TaxRate.IsValid || !o2.LocalDistrict3TaxRate.IsValid) {
					return 0;
				}
				return o1.LocalDistrict3TaxRate.CompareTo(o2.LocalDistrict3TaxRate);
			}
		}

		[Generate]
		public class TotalTaxRateSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxRateInfo o1 = (TaxRateInfo) a;
				TaxRateInfo o2 = (TaxRateInfo) b;

				if (o1 == null || o2 == null || !o1.TotalTaxRate.IsValid || !o2.TotalTaxRate.IsValid) {
					return 0;
				}
				return o1.TotalTaxRate.CompareTo(o2.TotalTaxRate);
			}
		}

	}
}
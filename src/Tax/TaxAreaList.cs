using System;
using System.Collections;
using System.Data;
using Spring2.DataTierGenerator.Attribute;

namespace Spring2.Core.Tax {
	/// <summary>
	/// TaxAreaData generic collection
	/// </summary>
	public class TaxAreaList : CollectionBase {
		[Generate]
		public static readonly TaxAreaList UNSET = new TaxAreaList(true);

		[Generate]
		public static readonly TaxAreaList DEFAULT = new TaxAreaList(true);

		[Generate]
		private Boolean immutable = false;

		[Generate]
		private TaxAreaList(Boolean immutable) {
			this.immutable = immutable;
		}

		[Generate]
		public TaxAreaList() {
		}

		// Indexer implementation.
		[Generate]
		public TaxAreaData this[int index] {
			get { return (TaxAreaData) List[index]; }
			set {
				if (!immutable) {
					List[index] = value;
				} else {
					throw new ReadOnlyException();
				}
			}
		}

		[Generate]
		public void Add(TaxAreaData value) {
			if (!immutable) {
				List.Add(value);
			} else {
				throw new ReadOnlyException();
			}
		}

		[Generate]
		public Boolean Contains(TaxAreaData value) {
			return List.Contains(value);
		}

		[Generate]
		public Int32 IndexOf(TaxAreaData value) {
			return List.IndexOf(value);
		}

		[Generate]
		public void Insert(Int32 index, TaxAreaData value) {
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
		public void Remove(TaxAreaData value) {
			if (!immutable) {
				List.Remove(value);
			} else {
				throw new ReadOnlyException();
			}
		}

		[Generate]
		public void AddRange(IList list) {
			foreach (Object o in list) {
				if (o is TaxAreaData) {
					Add((TaxAreaData) o);
				} else {
					throw new InvalidCastException("object in list could not be cast to TaxAreaData");
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
		public class CountrySorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxAreaData o1 = (TaxAreaData) a;
				TaxAreaData o2 = (TaxAreaData) b;

				if (o1 == null || o2 == null || !o1.Country.IsValid || !o2.Country.IsValid) {
					return 0;
				}
				return o1.Country.CompareTo(o2.Country);
			}
		}

		[Generate]
		public class RegionSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxAreaData o1 = (TaxAreaData) a;
				TaxAreaData o2 = (TaxAreaData) b;

				if (o1 == null || o2 == null || !o1.Region.IsValid || !o2.Region.IsValid) {
					return 0;
				}
				return o1.Region.CompareTo(o2.Region);
			}
		}

		[Generate]
		public class CountySorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxAreaData o1 = (TaxAreaData) a;
				TaxAreaData o2 = (TaxAreaData) b;

				if (o1 == null || o2 == null || !o1.County.IsValid || !o2.County.IsValid) {
					return 0;
				}
				return o1.County.CompareTo(o2.County);
			}
		}

		[Generate]
		public class CitySorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxAreaData o1 = (TaxAreaData) a;
				TaxAreaData o2 = (TaxAreaData) b;

				if (o1 == null || o2 == null || !o1.City.IsValid || !o2.City.IsValid) {
					return 0;
				}
				return o1.City.CompareTo(o2.City);
			}
		}

		[Generate]
		public class LocalDistrict1Sorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxAreaData o1 = (TaxAreaData) a;
				TaxAreaData o2 = (TaxAreaData) b;

				if (o1 == null || o2 == null || !o1.LocalDistrict1.IsValid || !o2.LocalDistrict1.IsValid) {
					return 0;
				}
				return o1.LocalDistrict1.CompareTo(o2.LocalDistrict1);
			}
		}

		[Generate]
		public class LocalDistrict2Sorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxAreaData o1 = (TaxAreaData) a;
				TaxAreaData o2 = (TaxAreaData) b;

				if (o1 == null || o2 == null || !o1.LocalDistrict2.IsValid || !o2.LocalDistrict2.IsValid) {
					return 0;
				}
				return o1.LocalDistrict2.CompareTo(o2.LocalDistrict2);
			}
		}

		[Generate]
		public class LocalDistrict3Sorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxAreaData o1 = (TaxAreaData) a;
				TaxAreaData o2 = (TaxAreaData) b;

				if (o1 == null || o2 == null || !o1.LocalDistrict3.IsValid || !o2.LocalDistrict3.IsValid) {
					return 0;
				}
				return o1.LocalDistrict3.CompareTo(o2.LocalDistrict3);
			}
		}

		[Generate]
		public class CountryTaxRateSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxAreaData o1 = (TaxAreaData) a;
				TaxAreaData o2 = (TaxAreaData) b;

				if (o1 == null || o2 == null || !o1.CountryTaxRate.IsValid || !o2.CountryTaxRate.IsValid) {
					return 0;
				}
				return o1.CountryTaxRate.CompareTo(o2.CountryTaxRate);
			}
		}

		[Generate]
		public class RegionTaxRateSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxAreaData o1 = (TaxAreaData) a;
				TaxAreaData o2 = (TaxAreaData) b;

				if (o1 == null || o2 == null || !o1.RegionTaxRate.IsValid || !o2.RegionTaxRate.IsValid) {
					return 0;
				}
				return o1.RegionTaxRate.CompareTo(o2.RegionTaxRate);
			}
		}

		[Generate]
		public class CountyTaxRateSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxAreaData o1 = (TaxAreaData) a;
				TaxAreaData o2 = (TaxAreaData) b;

				if (o1 == null || o2 == null || !o1.CountyTaxRate.IsValid || !o2.CountyTaxRate.IsValid) {
					return 0;
				}
				return o1.CountyTaxRate.CompareTo(o2.CountyTaxRate);
			}
		}

		[Generate]
		public class CityTaxRateSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxAreaData o1 = (TaxAreaData) a;
				TaxAreaData o2 = (TaxAreaData) b;

				if (o1 == null || o2 == null || !o1.CityTaxRate.IsValid || !o2.CityTaxRate.IsValid) {
					return 0;
				}
				return o1.CityTaxRate.CompareTo(o2.CityTaxRate);
			}
		}

		[Generate]
		public class LocalDistrict1TaxRateSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxAreaData o1 = (TaxAreaData) a;
				TaxAreaData o2 = (TaxAreaData) b;

				if (o1 == null || o2 == null || !o1.LocalDistrict1TaxRate.IsValid || !o2.LocalDistrict1TaxRate.IsValid) {
					return 0;
				}
				return o1.LocalDistrict1TaxRate.CompareTo(o2.LocalDistrict1TaxRate);
			}
		}

		[Generate]
		public class LocalDistrict2TaxRateSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxAreaData o1 = (TaxAreaData) a;
				TaxAreaData o2 = (TaxAreaData) b;

				if (o1 == null || o2 == null || !o1.LocalDistrict2TaxRate.IsValid || !o2.LocalDistrict2TaxRate.IsValid) {
					return 0;
				}
				return o1.LocalDistrict2TaxRate.CompareTo(o2.LocalDistrict2TaxRate);
			}
		}

		[Generate]
		public class LocalDistrict3TaxRateSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxAreaData o1 = (TaxAreaData) a;
				TaxAreaData o2 = (TaxAreaData) b;

				if (o1 == null || o2 == null || !o1.LocalDistrict3TaxRate.IsValid || !o2.LocalDistrict3TaxRate.IsValid) {
					return 0;
				}
				return o1.LocalDistrict3TaxRate.CompareTo(o2.LocalDistrict3TaxRate);
			}
		}

		[Generate]
		public class TotalTaxRateSorter : IComparer {
			public Int32 Compare(Object a, Object b) {
				TaxAreaData o1 = (TaxAreaData) a;
				TaxAreaData o2 = (TaxAreaData) b;

				if (o1 == null || o2 == null || !o1.TotalTaxRate.IsValid || !o2.TotalTaxRate.IsValid) {
					return 0;
				}
				return o1.TotalTaxRate.CompareTo(o2.TotalTaxRate);
			}
		}

	}
}
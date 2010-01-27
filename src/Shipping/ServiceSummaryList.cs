using System;
using System.Collections.Generic;
using System.Text;

namespace Spring2.Core.Shipping {
    public class ServiceSummaryList : System.Collections.CollectionBase {

        public static readonly ServiceSummaryList UNSET = new ServiceSummaryList(true);

        public static readonly ServiceSummaryList DEFAULT = new ServiceSummaryList(true);

        private Boolean immutable = false;

        private ServiceSummaryList(Boolean immutable) {
            this.immutable = immutable;
        }

        public ServiceSummaryList() {
        }

        public void Add(IServiceSummary value) {
            if (!immutable) {
                List.Add(value);
            } else {
                throw new System.Data.ReadOnlyException();
            }
        }

        public Boolean Contains(IServiceSummary value) {
            return List.Contains(value);
        }

        public Int32 IndexOf(IServiceSummary value) {
            return List.IndexOf(value);
        }

        public void Insert(Int32 index, IServiceSummary value) {
            if (!immutable) {
                List.Insert(index, value);
            } else {
                throw new System.Data.ReadOnlyException();
            }
        }

        public void Remove(int index) {
            if (!immutable) {
                if (index > Count - 1 || index < 0) {
                    throw new IndexOutOfRangeException();
                } else {
                    List.RemoveAt(index);
                }
            } else {
                throw new System.Data.ReadOnlyException();
            }
        }

        public void Remove(IServiceSummary value) {
            if (!immutable) {
                List.Remove(value);
            } else {
                throw new System.Data.ReadOnlyException();
            }
        }

        public void AddRange(System.Collections.IList list) {
            foreach (Object o in list) {
                if (o is IServiceSummary) {
                    Add((IServiceSummary)o);
                } else {
                    throw new System.InvalidCastException("object in list could not be cast to AddressData");
                }
            }
        }

        public Boolean IsDefault {
            get { return Object.ReferenceEquals(this, DEFAULT); }
        }

        public Boolean IsUnset {
            get { return Object.ReferenceEquals(this, UNSET); }
        }

        public Boolean IsValid {
            get {
                return !(IsDefault || IsUnset);
            }
        }

        public IServiceSummary this[int index] {
            get { return (IServiceSummary)List[index]; }
            set {
                if (!immutable) {
                    List[index] = value;
                } else {
                    throw new System.Data.ReadOnlyException();
                }
            }
        }
    }
}

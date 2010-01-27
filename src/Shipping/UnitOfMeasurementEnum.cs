using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Spring2.Core.Shipping {
    [Serializable]
    public class UnitOfMeasurementEnum : Spring2.Core.Types.EnumDataType, ISerializable {

        private static readonly ArrayList OPTIONS = new ArrayList();

        public static readonly new UnitOfMeasurementEnum DEFAULT = new UnitOfMeasurementEnum();
        public static readonly new UnitOfMeasurementEnum UNSET = new UnitOfMeasurementEnum();

        public static readonly UnitOfMeasurementEnum POUNDS = new UnitOfMeasurementEnum("LBS", "Pounds");
        public static readonly UnitOfMeasurementEnum KILOGRAMS = new UnitOfMeasurementEnum("KGS", "Kilograms");

        public static UnitOfMeasurementEnum GetInstance(Object value) {
            if (value is String) {
                foreach (UnitOfMeasurementEnum t in OPTIONS) {
                    if (t.Value.Equals(value)) {
                        return t;
                    }
                }
            }

            return UNSET;
        }

        private UnitOfMeasurementEnum() { }

        private UnitOfMeasurementEnum(String code, String name) {
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

        [SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) {
            if (this.Equals(DEFAULT)) {
                info.SetType(typeof(UnitOfMeasurementEnum_DEFAULT));
            } else if (this.Equals(UNSET)) {
                info.SetType(typeof(UnitOfMeasurementEnum_UNSET));
            } else if (this.Equals(POUNDS)) {
                info.SetType(typeof(UnitOfMeasurementEnum_POUNDS));
            } else if (this.Equals(KILOGRAMS)) {
                info.SetType(typeof(UnitOfMeasurementEnum_KILOGRAMS));
            }

        }

        [Serializable]
        public class UnitOfMeasurementEnum_DEFAULT : IObjectReference {
            public object GetRealObject(StreamingContext context) {
                return UnitOfMeasurementEnum.DEFAULT;
            }
        }

        [Serializable]
        public class UnitOfMeasurementEnum_UNSET : IObjectReference {
            public object GetRealObject(StreamingContext context) {
                return UnitOfMeasurementEnum.UNSET;
            }
        }

        [Serializable]
        public class UnitOfMeasurementEnum_POUNDS : IObjectReference {
            public object GetRealObject(StreamingContext context) {
                return UnitOfMeasurementEnum.POUNDS;
            }
        }
        [Serializable]
        public class UnitOfMeasurementEnum_KILOGRAMS : IObjectReference {
            public object GetRealObject(StreamingContext context) {
                return UnitOfMeasurementEnum.KILOGRAMS;
            }
        }
    }
}

	

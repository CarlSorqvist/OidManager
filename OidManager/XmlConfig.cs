using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OidManager
{
    internal static class XmlConfig
    {
        /// <summary>
        /// The encoding to be used in all reading/writing of XML and Base64.
        /// </summary>
        internal static readonly Encoding Encoder = Encoding.UTF8;

        /// <summary>
        /// XML namespace declaration attribute.
        /// </summary>
        internal const String Xmlns = "xmlns";

        /// <summary>
        /// XML resource strings for the application.
        /// </summary>
        internal static class Resource
        {
            /// <summary>
            /// XML resource strings for OID Repository.
            /// </summary>
            internal static class Repository
            {
                /// <summary>
                /// The XML schema for OID Repository. The original string is encoded as UTF8.
                /// </summary>
                internal const String Schema = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz4NCjx4czpzY2hlbWEgdGFyZ2V0TmFtZXNwYWNlPSJ1cm46b2lkOjEuMy42LjEuNC4xLjQ2OTc5LjIuMSINCiAgICBlbGVtZW50Rm9ybURlZmF1bHQ9InF1YWxpZmllZCINCiAgICBhdHRyaWJ1dGVGb3JtRGVmYXVsdD0idW5xdWFsaWZpZWQiDQogICAgeG1sbnM6b2lkcj0idXJuOm9pZDoxLjMuNi4xLjQuMS40Njk3OS4yLjEiDQogICAgeG1sbnM6eHM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvWE1MU2NoZW1hIg0KPg0KICA8eHM6c2ltcGxlVHlwZSBuYW1lPSJPaWRTaG9ydE5hbWUiPg0KICAgIDx4czpyZXN0cmljdGlvbiBiYXNlPSJ4czpzdHJpbmciPg0KICAgICAgPHhzOnBhdHRlcm4gdmFsdWU9Il5bYS16XVthLXowLTktXSokIiAvPg0KICAgIDwveHM6cmVzdHJpY3Rpb24+DQogIDwveHM6c2ltcGxlVHlwZT4NCiAgPHhzOnNpbXBsZVR5cGUgbmFtZT0iSWRlbnRpZmllciI+DQogICAgPHhzOnJlc3RyaWN0aW9uIGJhc2U9InhzOm5vbk5lZ2F0aXZlSW50ZWdlciI+DQogICAgICA8eHM6cGF0dGVybiB2YWx1ZT0iXigwfFsxLTldXGQqKSQiIC8+DQogICAgICA8eHM6bWF4SW5jbHVzaXZlIHZhbHVlPSIyMTQ3NDgzNjQ3IiAvPg0KICAgICAgPHhzOm1pbkluY2x1c2l2ZSB2YWx1ZT0iMCIgLz4NCiAgICA8L3hzOnJlc3RyaWN0aW9uPg0KICA8L3hzOnNpbXBsZVR5cGU+DQogIA0KICA8eHM6Y29tcGxleFR5cGUgbmFtZT0iTm9kZSI+DQogICAgPHhzOmFubm90YXRpb24+DQogICAgICA8eHM6ZG9jdW1lbnRhdGlvbj5UaGlzIHR5cGUgcmVwcmVzZW50cyBhbiBPSUQgaW4gdGhlIHRyZWUuPC94czpkb2N1bWVudGF0aW9uPg0KICAgIDwveHM6YW5ub3RhdGlvbj4NCiAgICA8eHM6c2VxdWVuY2U+DQogICAgICA8eHM6ZWxlbWVudCBuYW1lPSJQcml2YXRlRGF0YSIgdHlwZT0ieHM6YmFzZTY0QmluYXJ5IiBtaW5PY2N1cnM9IjAiIG1heE9jY3Vycz0iMSI+DQogICAgICAgIDx4czphbm5vdGF0aW9uPg0KICAgICAgICAgIDx4czpkb2N1bWVudGF0aW9uPkFuIG9wdGlvbmFsIGVsZW1lbnQgdGhhdCBzcGVjaWZpZXMgcHJpdmF0ZS9pbnRlcm5hbCBkYXRhIGZvciB0aGlzIE9JRC4gVGhlIHRleHQgaXMgZW5jb2RlZCBpbiBCYXNlNjQgdXNpbmcgVVRGOC48L3hzOmRvY3VtZW50YXRpb24+DQogICAgICAgIDwveHM6YW5ub3RhdGlvbj4NCiAgICAgIDwveHM6ZWxlbWVudD4NCiAgICAgIDx4czplbGVtZW50IG5hbWU9IlB1YmxpY0RhdGEiIHR5cGU9InhzOmJhc2U2NEJpbmFyeSIgbWluT2NjdXJzPSIwIiBtYXhPY2N1cnM9IjEiPg0KICAgICAgICA8eHM6YW5ub3RhdGlvbj4NCiAgICAgICAgICA8eHM6ZG9jdW1lbnRhdGlvbj5PcHRpb25hbGx5IGNvbnRhaW5zIHB1YmxpYyBkYXRhIGFib3V0IHRoZSBPSUQsIGxpa2UgYSBnZW5lcmFsIGRlc2NyaXB0aW9uLiBUaGUgdGV4dCBpcyBlbmNvZGVkIGluIEJhc2U2NCB1c2luZyBVVEY4LjwveHM6ZG9jdW1lbnRhdGlvbj4NCiAgICAgICAgPC94czphbm5vdGF0aW9uPg0KICAgICAgPC94czplbGVtZW50Pg0KICAgICAgPHhzOmVsZW1lbnQgbmFtZT0iQXV0aG9yaXR5IiB0eXBlPSJ4czpiYXNlNjRCaW5hcnkiIG1pbk9jY3Vycz0iMCIgbWF4T2NjdXJzPSIxIj4NCiAgICAgICAgPHhzOmFubm90YXRpb24+DQogICAgICAgICAgPHhzOmRvY3VtZW50YXRpb24+T3B0aW9uYWxseSBjb250YWlucyBpbmZvcm1hdGlvbiBhYm91dCB0aGUgYXV0aG9yaXR5IGZvciB0aGlzIG5vZGUgaW4gdGhlIE9JRCB0cmVlLCBmb3IgZXhhbXBsZSBuYW1lcywgZS1tYWlsIGFkZHJlc3NlcywgcGhvbmUgbnVtYmVycywgZXRjLiBUaGUgdGV4dCBpcyBlbmNvZGVkIGluIEJhc2U2NCB1c2luZyBVVEY4LjwveHM6ZG9jdW1lbnRhdGlvbj4NCiAgICAgICAgPC94czphbm5vdGF0aW9uPg0KICAgICAgPC94czplbGVtZW50Pg0KICAgICAgPHhzOmVsZW1lbnQgbmFtZT0iT2lkIiB0eXBlPSJvaWRyOk5vZGUiIG1pbk9jY3Vycz0iMCIgbWF4T2NjdXJzPSJ1bmJvdW5kZWQiPg0KICAgICAgICA8eHM6YW5ub3RhdGlvbj4NCiAgICAgICAgICA8eHM6ZG9jdW1lbnRhdGlvbj5aZXJvIG9yIG1vcmUgY2hpbGQgbm9kZXMuPC94czpkb2N1bWVudGF0aW9uPg0KICAgICAgICA8L3hzOmFubm90YXRpb24+DQogICAgICAgIDx4czp1bmlxdWUgbmFtZT0iT2lkQ2hpbGROb2RlVW5pcXVlSWRlbnRpZmllciI+DQogICAgICAgICAgPHhzOnNlbGVjdG9yIHhwYXRoPSJvaWRyOk9pZCIgLz4NCiAgICAgICAgICA8eHM6ZmllbGQgeHBhdGg9IkBpZGVudGlmaWVyIiAvPg0KICAgICAgICA8L3hzOnVuaXF1ZT4NCiAgICAgIDwveHM6ZWxlbWVudD4NCiAgICA8L3hzOnNlcXVlbmNlPg0KICAgIDx4czphdHRyaWJ1dGUgbmFtZT0iaWRlbnRpZmllciIgdHlwZT0ib2lkcjpJZGVudGlmaWVyIiB1c2U9InJlcXVpcmVkIj4NCiAgICAgIDx4czphbm5vdGF0aW9uPg0KICAgICAgICA8eHM6ZG9jdW1lbnRhdGlvbj5EZW5vdGVzIHRoZSBudW1lcmljIHJlbGF0aXZlIGlkZW50aWZpZXIgZm9yIHRoaXMgbm9kZS4gRm9yIGluc3RhbmNlLCBmb3IgdGhlIE9JRCAxLjIuODQwLCB0aGUgaWRlbnRpZmllciB3b3VsZCBiZSAiODQwIi48L3hzOmRvY3VtZW50YXRpb24+DQogICAgICA8L3hzOmFubm90YXRpb24+DQogICAgPC94czphdHRyaWJ1dGU+DQogICAgPHhzOmF0dHJpYnV0ZSBuYW1lPSJuYW1lIiB0eXBlPSJvaWRyOk9pZFNob3J0TmFtZSIgdXNlPSJvcHRpb25hbCI+DQogICAgICA8eHM6YW5ub3RhdGlvbj4NCiAgICAgICAgPHhzOmRvY3VtZW50YXRpb24+QW4gb3B0aW9uYWwgc2hvcnQgbmFtZSB0aGF0IGRlc2NyaWJlcyB0aGUgT0lEIG5vZGUsIGZvciBpbnN0YW5jZSwgInVzIi4gTXVzdCBiZWdpbiB3aXRoIGEgbG93ZXJjYXNlIEFTQ0lJIGxldHRlciwgb3B0aW9uYWxseSBmb2xsb3dlZCBieSBhbnkgY29tYmluYXRpb24gb2YgbG93ZXJjYXNlIEFTQ0lJIGxldHRlcnMsIGRpZ2l0cywgcGVyaW9kLCBkYXNoIGFuZCB1bmRlcnNjb3JlLjwveHM6ZG9jdW1lbnRhdGlvbj4NCiAgICAgIDwveHM6YW5ub3RhdGlvbj4gICAgICANCiAgICA8L3hzOmF0dHJpYnV0ZT4NCiAgPC94czpjb21wbGV4VHlwZT4NCg0KICA8eHM6ZWxlbWVudCBuYW1lPSJPaWRSZXBvc2l0b3J5Ij4NCiAgICA8eHM6YW5ub3RhdGlvbj4NCiAgICAgIDx4czpkb2N1bWVudGF0aW9uPlRoZSByb290IGVsZW1lbnQgaW4gdGhlIHRyZWUuIE11c3QgY29udGFpbiBhdCBsZWFzdCBvbmUgY2hpbGQgT0lEIG5vZGUuPC94czpkb2N1bWVudGF0aW9uPg0KICAgIDwveHM6YW5ub3RhdGlvbj4NCiAgICA8eHM6Y29tcGxleFR5cGU+DQogICAgICA8eHM6c2VxdWVuY2U+DQogICAgICAgIDx4czplbGVtZW50IG5hbWU9Ik9pZCIgdHlwZT0ib2lkcjpOb2RlIiBtaW5PY2N1cnM9IjEiIG1heE9jY3Vycz0idW5ib3VuZGVkIj4NCiAgICAgICAgICA8eHM6YW5ub3RhdGlvbj4NCiAgICAgICAgICAgIDx4czpkb2N1bWVudGF0aW9uPk9uZSBvciBtb3JlIG5vZGVzIGluIHRoZSB0cmVlLjwveHM6ZG9jdW1lbnRhdGlvbj4NCiAgICAgICAgICA8L3hzOmFubm90YXRpb24+DQogICAgICAgICAgPHhzOnVuaXF1ZSBuYW1lPSJPaWRGaXJzdE5vZGVVbmlxdWVJZGVudGlmaWVyIj4NCiAgICAgICAgICAgIDx4czpzZWxlY3RvciB4cGF0aD0ib2lkcjpPaWQiIC8+DQogICAgICAgICAgICA8eHM6ZmllbGQgeHBhdGg9IkBpZGVudGlmaWVyIiAvPg0KICAgICAgICAgIDwveHM6dW5pcXVlPg0KICAgICAgICA8L3hzOmVsZW1lbnQ+DQogICAgICA8L3hzOnNlcXVlbmNlPg0KICAgICAgPHhzOmF0dHJpYnV0ZSBuYW1lPSJ2ZXJzaW9uIiB0eXBlPSJ4czpub25OZWdhdGl2ZUludGVnZXIiIHVzZT0ib3B0aW9uYWwiIC8+DQogICAgPC94czpjb21wbGV4VHlwZT4NCiAgICA8eHM6dW5pcXVlIG5hbWU9Ik9pZFJlcG9zaXRvcnlVbmlxdWVJZGVudGlmaWVyIj4NCiAgICAgIDx4czpzZWxlY3RvciB4cGF0aD0ib2lkcjpPaWQiIC8+DQogICAgICA8eHM6ZmllbGQgeHBhdGg9IkBpZGVudGlmaWVyIiAvPg0KICAgIDwveHM6dW5pcXVlPg0KICA8L3hzOmVsZW1lbnQ+DQoNCjwveHM6c2NoZW1hPg0K";
                /// <summary>
                /// OID Repository namespace strings.
                /// </summary>
                internal static class Namespace
                {
                    internal const String Prefix = "oidr";
                    internal const String Uri = "urn:oid:1.3.6.1.4.1.46979.2.1";
                }
                /// <summary>
                /// OID Repository element names.
                /// </summary>
                internal static class Elements
                {
                    internal const String Root = "OidRepository";
                    internal const String RootQualified = Namespace.Prefix + ":" + Root;
                    internal const String Oid = "Oid";
                    internal const String OidQualified = Namespace.Prefix + ":" + Oid;
                    internal const String PrivateData = "PrivateData";
                    internal const String PrivateDataQualified = Namespace.Prefix + ":" + PrivateData;
                    internal const String PublicData = "PublicData";
                    internal const String PublicDataQualified = Namespace.Prefix + ":" + PublicData;
                    internal const String Authority = "Authority";
                    internal const String AuthorityQualified = Namespace.Prefix + ":" + Authority;

                }
                /// <summary>
                /// OID Repository attribute names.
                /// </summary>
                internal static class Attributes
                {
                    internal const String Identifier = "identifier";
                    internal const String Name = "name";
                }
            }
            /// <summary>
            /// XML resource strings for the OID Repository Export function.
            /// </summary>
            internal static class Export
            {
                /// <summary>
                /// OID Repository Export namespace strings.
                /// </summary>
                internal static class Namespace
                {
                    internal const String Prefix = "oide";
                    internal const String Uri = "urn:oid:1.3.6.1.4.1.46979.2.2";
                }
                /// <summary>
                /// OID Repository Export element names.
                /// </summary>
                internal static class Elements
                {
                    internal const String Root = "OidList";
                    internal const String RootQualified = Namespace.Prefix + ":" + Root;
                    internal const String Oid = "Oid";
                    internal const String OidQualified = Namespace.Prefix + ":" + Oid;
                    internal const String Identifier = "Identifier";
                    internal const String IdentifierQualified = Namespace.Prefix + ":" + Identifier;
                    internal const String Notes = "Notes";
                    internal const String NotesQualified = Namespace.Prefix + ":" + Notes;
                    internal const String Authority = "Authority";
                    internal const String AuthorityQualified = Namespace.Prefix + ":" + Authority;
                }
            }
        }
    }
}

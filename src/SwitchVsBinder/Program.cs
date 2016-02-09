using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace SwitchVsBinder
{
    public class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            Console.WriteLine("Running in DEBUG");
#else
            Console.WriteLine("Running in RELEASE");
#endif
            SwitchTest();
            BinderTest();
            SwitchTest();
            BinderTest();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void SwitchTest()
        {
            var c = new DbColumnSwitch { ColumnName = "test" };
            var sw = Stopwatch.StartNew();
            for (var i = 0; i < 10000000; i++)
            {
                var test = c["ColumnName"];
            }
            sw.Stop();
            Console.WriteLine("Switch: " + sw.ElapsedMilliseconds);
        }

        private static void BinderTest()
        {
            var c = new DbColumnBinder { ColumnName = "test" };
            var sw = Stopwatch.StartNew();
            for (var i = 0; i < 10000000; i++)
            {
                var test = c["ColumnName"];
            }
            sw.Stop();
            Console.WriteLine("Binder: " + sw.ElapsedMilliseconds);
        }

        public class DbColumnSwitch
        {
            public virtual bool AllowDBNull { get; set; }
            public virtual string BaseCatalogName { get; set; }
            public virtual string BaseColumnName { get; set; }
            public virtual string BaseSchemaName { get; set; }
            public virtual string BaseServerName { get; set; }
            public virtual string BaseTableName { get; set; }
            public virtual string ColumnName { get; set; }

            public virtual string GetColumnName() { return ColumnName; }

            public virtual int ColumnOrdinal { get; set; }
            public virtual int ColumnSize { get; set; }
            public virtual bool IsAliased { get; set; }
            public virtual bool IsAutoIncrement { get; set; }
            public virtual bool IsExpression { get; set; }
            public virtual bool IsHidden { get; set; }
            public virtual bool IsIdentity { get; set; }
            public virtual bool IsKey { get; set; }
            public virtual bool IsLong { get; set; }
            public virtual bool IsReadOnly { get; set; }
            public virtual bool IsUnique { get; set; }
            public virtual int NumericPrecision { get; set; }
            public virtual int NumericScale { get; set; }
            public virtual string UdtAssemblyQualifiedName { get; set; }
            public virtual Type DataType { get; set; }
            public virtual string DataTypeName { get; set; }

            public virtual object this[string property]
            {
                get
                {
                    switch (property)
                    {
                        case "AllowDBNull":
                            return AllowDBNull;
                        case "BaseCatalogName":
                            return BaseCatalogName;
                        case "BaseColumnName":
                            return BaseColumnName;
                        case "BaseSchemaName":
                            return BaseSchemaName;
                        case "BaseServerName":
                            return BaseServerName;
                        case "BaseTableName":
                            return BaseTableName;
                        case "ColumnName":
                            return ColumnName;
                        case "ColumnOrdinal":
                            return ColumnOrdinal;
                        case "ColumnSize":
                            return ColumnSize;
                        case "IsAliased":
                            return IsAliased;
                        case "IsAutoIncrement":
                            return IsAutoIncrement;
                        case "IsExpression":
                            return IsExpression;
                        case "IsHidden":
                            return IsHidden;
                        case "IsIdentity":
                            return IsIdentity;
                        case "IsKey":
                            return IsKey;
                        case "IsLong":
                            return IsLong;
                        case "IsReadOnly":
                            return IsReadOnly;
                        case "IsUnique":
                            return IsUnique;
                        case "NumericPrecision":
                            return NumericPrecision;
                        case "NumericScale":
                            return NumericScale;
                        case "UdtAssemblyQualifiedName":
                            return UdtAssemblyQualifiedName;
                        case "DataType":
                            return DataType;
                        case "DataTypeName":
                            return DataTypeName;
                        default:
                            return null;
                    }
                }
            }
        }

        public class DbColumnBinder
        {
            public virtual bool AllowDBNull { get; set; }
            public virtual string BaseCatalogName { get; set; }
            public virtual string BaseColumnName { get; set; }
            public virtual string BaseSchemaName { get; set; }
            public virtual string BaseServerName { get; set; }
            public virtual string BaseTableName { get; set; }
            public virtual string ColumnName { get; set; }

            public virtual string GetColumnName()
            {
                return ColumnName;
            }

            public virtual int ColumnOrdinal { get; set; }
            public virtual int ColumnSize { get; set; }
            public virtual bool IsAliased { get; set; }
            public virtual bool IsAutoIncrement { get; set; }
            public virtual bool IsExpression { get; set; }
            public virtual bool IsHidden { get; set; }
            public virtual bool IsIdentity { get; set; }
            public virtual bool IsKey { get; set; }
            public virtual bool IsLong { get; set; }
            public virtual bool IsReadOnly { get; set; }
            public virtual bool IsUnique { get; set; }
            public virtual int NumericPrecision { get; set; }
            public virtual int NumericScale { get; set; }
            public virtual string UdtAssemblyQualifiedName { get; set; }
            public virtual Type DataType { get; set; }
            public virtual string DataTypeName { get; set; }

            static Dictionary<string, CallSite<Func<CallSite, object, object>>> _dictionary =
                new Dictionary<string, CallSite<Func<CallSite, object, object>>>();

            static DbColumnBinder()
            {
                foreach (var prop in typeof (DbColumnBinder).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    var binder = Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, prop.Name,
                        typeof (DbColumnBinder),
                        new[] {CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)});

                    _dictionary.Add(prop.Name, CallSite<Func<CallSite, object, object>>.Create(binder));
                }

            }

            public virtual object this[string propertyName]
            {
                get
                {
                    var callsite = _dictionary[propertyName];
                    return callsite.Target(callsite, this);
                }
            }
        }
    }
}

using System;
using System.Data;
using System.Data.SqlTypes;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Metadata
{
	public static class DataTypeEnumeration
	{
		public static ParameterDirection GetSystemParameterDirection(SqlParameterDirection sqlParameterDirection)
		{
			if (sqlParameterDirection == SqlParameterDirection.Input)
				return ParameterDirection.Input;
			if (sqlParameterDirection == SqlParameterDirection.InputOutput)
				return ParameterDirection.InputOutput;
			if (sqlParameterDirection == SqlParameterDirection.Output)
				return ParameterDirection.Output;
			if (sqlParameterDirection == SqlParameterDirection.ReturnValue)
				return ParameterDirection.ReturnValue;
			throw new Exception("Unknown SqlParameterDirection enumeration value");
		}

		public static SqlParameterDirection GetParameterDirectionByName(string name)
		{
			switch (name)
			{
				case "Input":
					return SqlParameterDirection.Input;
				case "InputOutput":
					return SqlParameterDirection.InputOutput;
				case "Output":
					return SqlParameterDirection.Output;
				case "ReturnValue":
					return SqlParameterDirection.ReturnValue;
			}
			throw new ArgumentException("Unknown ParameterDirection name", "Name");
		}

		public static SqlDbType GetSqlDbTypeByName(string typeName)
		{
			switch (typeName.ToLower())
			{
				case "bigint":
					return SqlDbType.BigInt;
				case "binary":
					return SqlDbType.Binary;
				case "bit":
					return SqlDbType.Bit;
				case "char":
					return SqlDbType.Char;
				case "datetime":
					return SqlDbType.DateTime;
				case "decimal":
					return SqlDbType.Decimal;
				case "float":
					return SqlDbType.Float;
				case "image":
					return SqlDbType.Image;
				case "int":
					return SqlDbType.Int;
				case "money":
					return SqlDbType.Money;
				case "nchar":
					return SqlDbType.NChar;
				case "ntext":
					return SqlDbType.NText;
				case "nvarchar":
					return SqlDbType.NVarChar;
				case "real":
					return SqlDbType.Real;
				case "smalldatetime":
					return SqlDbType.SmallDateTime;
				case "smallint":
					return SqlDbType.SmallInt;
				case "smallmoney":
					return SqlDbType.SmallMoney;
				case "text":
					return SqlDbType.Text;
				case "timestamp":
					return SqlDbType.Timestamp;
				case "tinyint":
					return SqlDbType.TinyInt;
				case "uniqueidentifier":
					return SqlDbType.UniqueIdentifier;
				case "varbinary":
					return SqlDbType.VarBinary;
				case "varchar":
					return SqlDbType.VarChar;
				case "sql_variant":
					return SqlDbType.Variant;
				case "xml":
					return SqlDbType.Xml;
				default:
					return SqlDbType.Udt;
			}
		}

		public static Type GetSystemTypeByName(string typeName)
		{
			switch (typeName)
			{
				case "System.Int64":
					return typeof(Int64);
				case "System.binary":
					return null;
				case "System.Boolean":
					return typeof(Boolean);
				case "System.String":
					return typeof(String);
				case "System.DateTime":
					return typeof(DateTime);
				case "System.Decimal":
					return typeof(Decimal);
				case "System.Double":
					return typeof(Double);
				case "System.Int32":
					return typeof(Int32);
				case "System.Single":
					return typeof(Single);
				case "System.Int16":
					return typeof(Int16);
				case "System.Byte":
					return typeof(Byte);
				case "System.Guid":
					return typeof(Guid);
				case "System.varbinary":
					return null;
				case "System.Object":
					return typeof(object);
				case "System.SqlXml":
					return typeof(SqlXml);
				default:
					return null;
			}
		}

		public static Type GetTypeByName(string typeName)
		{
			switch (typeName)
			{
				case "bigint":
					return typeof(Int64);
				case "binary":
					return null;
				case "bit":
					return typeof(Boolean);
				case "char":
					return typeof(String);
				case "datetime":
					return typeof(DateTime);
				case "decimal":
					return typeof(Decimal);
				case "float":
					return typeof(Double);
				case "image":
					return null;
				case "int":
					return typeof(Int32);
				case "money":
					return typeof(Decimal);
				case "nchar":
					return typeof(String);
				case "ntext":
					return typeof(String);
				case "nvarchar":
					return typeof(String);
				case "real":
					return typeof(Single);
				case "smalldatetime":
					return typeof(DateTime);
				case "smallint":
					return typeof(Int16);
				case "smallmoney":
					return typeof(Decimal);
				case "text":
					return typeof(String);
				case "timestamp":
					return null;
				case "tinyint":
					return typeof(Byte);
				case "uniqueidentifier":
					return typeof(Guid);
				case "varbinary":
					return null;
				case "varchar":
					return typeof(String);
				case "sql_variant":
					return typeof(object);
				case "xml":
					return typeof(SqlXml);
				default:
					return null;
			}
		}
	}
}
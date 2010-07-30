using System;
using System.Collections.Generic;
using System.Data;
using JetBat.Client.Metadata;
using JetBat.Client.Metadata.Misc;
using JetBat.Client.Metadata.Simple;

namespace JetBat.Client.SqlServer.Concrete
{
	internal static class StaticHelper
	{
		public const string MetadataCacheDirectoryName = "Metadata";

		public static NamedObjectCollection<ObjectAttribute> getAttributesFormReader(IDataReader reader)
		{
			NamedObjectCollection<ObjectAttribute> attributes = new NamedObjectCollection<ObjectAttribute>();
			while (reader.Read())
			{
				ObjectAttribute definition = new ObjectAttribute
												{
													Name = Convert.ToString(reader["Name"]),
													FriendlyName = Convert.ToString(reader["FriendlyName"]),
													DataType = DataTypeEnumeration.GetTypeByName(Convert.ToString(reader["DataTypeName"])).ToString(),
													SqlDbType = Convert.ToString(reader["DataTypeName"]),
													DateTimeFormatID =
														Convert.ToInt16(reader["DateTimeFormatID"] != DBNull.Value
																			? reader["DateTimeFormatID"]
																			: -1),
													IsNullable = Convert.ToBoolean(reader["IsNullable"]),
													IsReadOnly = Convert.ToBoolean(reader["IsReadOnly"]),
													IsExternal = Convert.ToBoolean(reader["IsExternal"]),
													IsUserVisible = Convert.ToBoolean(reader["IsUserVisible"]),
													IsPrimaryKeyMember = Convert.ToBoolean(reader["IsPrimaryKeyMember"]),
													MaxLength = Convert.ToInt32(reader["MaxLength"]),
													Precision = Convert.ToInt32(reader["Precision"]),
													Scale = Convert.ToInt32(reader["Scale"]),
													UILabel = Convert.ToString(reader["UILabel"]),
													UIPreferredWidth = Convert.ToInt32(reader["UIPreferredWidth"]),
													UIPreferredIndex = Convert.ToInt32(reader["UIPreferredIndex"]),
													UIAllowMultilineText = Convert.ToBoolean(reader["UIAllowMultilineText"])
												};
				attributes.Add(definition);
			}
			return attributes;
		}

		public static NamedObjectCollection<ObjectAction> getActionsFormReader(IDataReader reader)
		{
			NamedObjectCollection<ObjectAction> actions = new NamedObjectCollection<ObjectAction>();
			while (reader.Read())
			{
				ObjectAction definition = new ObjectAction
											{
												Name = Convert.ToString(reader["Name"]),
												FriendlyName = Convert.ToString(reader["FriendlyName"]),
												Description = Convert.ToString(reader["Description"]),
												Enabled = Convert.ToBoolean(reader["Enabled"]),
												UIFullText = Convert.ToString(reader["UIFullText"]),
												UIBriefText = Convert.ToString(reader["UIBriefText"])
											};
				actions.Add(definition);
			}

			return actions;
		}

		public static NamedObjectCollection<ObjectComplexAttribute> getComplexAttributesFromReader(IDataReader reader)
		{
			NamedObjectCollection<ObjectComplexAttribute> complexAttributes = new NamedObjectCollection<ObjectComplexAttribute>();
			while (reader.Read())
			{
				ObjectComplexAttribute definition = new ObjectComplexAttribute
														{
															ID = Convert.ToInt32(reader["ID"]),
															Name = Convert.ToString(reader["Name"]),
															FriendlyName = Convert.ToString(reader["FriendlyName"]),
															Description = Convert.ToString(reader["Description"]),
															UILabel = Convert.ToString(reader["UILabel"]),
															UIPreferredIndex = Convert.ToInt32(reader["UIPreferredIndex"]),
															MemberColumns = new List<ComplexAttributeColumnPair>()
														};
				complexAttributes.Add(definition);
			}
			return complexAttributes;
		}

		public static void getComplexAttributeAttributePairsFromReader(IDataReader reader,
																	   NamedObjectCollection<ObjectComplexAttribute>
																		complexAttributes)
		{
			while (reader.Read())
			{
				foreach (ObjectComplexAttribute complexAttribute in complexAttributes)
				{
					string attributeName = reader["ComplexObjectAttributeName"].ToString();
					if (complexAttribute.Name == attributeName)
					{
						complexAttribute.MemberColumns.Add(new ComplexAttributeColumnPair
															{
																PrimaryKeyColumnName = reader["PrimaryKeyAttributeName"].ToString(),
																ForeignKeyColumnName = reader["ForeignKeyAttributeName"].ToString()
															});
						break;
					}
				}
			}
		}

		public static NamedObjectCollection<ObjectMethod> getMethodsFromReader(IDataReader reader)
		{
			NamedObjectCollection<ObjectMethod> methods = new NamedObjectCollection<ObjectMethod>();
			while (reader.Read())
			{
				ObjectMethod definition = new ObjectMethod
											{
												Name = Convert.ToString(reader["Name"]),
												FriendlyName = Convert.ToString(reader["FriendlyName"]),
												StoredProcedureName = Convert.ToString(reader["DBStoredProcedureName"]),
												ReturnsXmlErrorList = Convert.ToBoolean(reader["ReturnsXMLErrorList"]),
												ParameterDefinitions = new NamedObjectCollection<ObjectMethodParameter>()
											};
				methods.Add(definition);
			}
			return methods;
		}

		public static void getMethodParametersFromReader(IDataReader reader, NamedObjectCollection<ObjectMethod> methods)
		{
			while (reader.Read())
			{
				foreach (ObjectMethod objectMethod in methods)
				{
					string methodName = reader["ObjectMethodName"].ToString();
					if (objectMethod.Name == methodName)
					{
						ObjectMethodParameter parameter = new ObjectMethodParameter
															{
																Name = Convert.ToString(reader["Name"]),
																SqlDbType = Convert.ToString(reader["DataTypeName"]),
																Direction =
																	Convert.ToBoolean(reader["IsOutput"])
																		? SqlParameterDirection.InputOutput
																		: SqlParameterDirection.Input,
																MaxLength = Convert.ToInt32(reader["MaxLength"]),
																Precision = Convert.ToByte(reader["Precision"]),
																Scale = Convert.ToByte(reader["Scale"]),
																AttributeName = Convert.ToString(reader["ObjectAttributeName"]),
																AlternativeName = Convert.ToString(reader["AlternativeName"])
															};

						objectMethod.ParameterDefinitions.Add(parameter);
						break;
					}
				}
			}
		}
	}
}
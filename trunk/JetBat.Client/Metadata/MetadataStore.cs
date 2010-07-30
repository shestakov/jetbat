using System;
using System.Collections;
using System.Collections.Generic;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;

namespace JetBat.Client.Metadata
{
	public class MetadataStore
	{
		private readonly IMetadataProvider _metadataProvider;
		private readonly Hashtable _objectDefinitions = new Hashtable();

		public MetadataStore(IMetadataProvider metadataProvider)
		{
			if (metadataProvider == null) throw new ArgumentNullException("metadataProvider");
			_metadataProvider = metadataProvider;
		}

		public ObjectDefinition GetObjectDefinition(string obejctNamespace, string objectName)
		{
			QualifiedName qualifiedName = new QualifiedName(obejctNamespace, objectName);
			return (ObjectDefinition)_objectDefinitions[qualifiedName];

		}

		public T Get<T>(string objectNamespace, string objectName) where T : ObjectDefinition
		{
			ObjectDefinition definition = GetObjectDefinition(objectNamespace, objectName);
			return definition as T;
		}

		public void LoadMetadata()
		{
			Dictionary<QualifiedName, int> objectNameList = _metadataProvider.LoadNameList();
			Dictionary<QualifiedName, ObjectDefinition> loadedObjects = _metadataProvider.LoadObjectList(objectNameList);
			_objectDefinitions.Clear();
			foreach (QualifiedName qualifiedName in loadedObjects.Keys)
			{
				_objectDefinitions.Add(qualifiedName, loadedObjects[qualifiedName]);
			}
		}
	}
}
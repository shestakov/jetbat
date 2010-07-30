namespace JetBat.Client.Metadata.Misc
{
	public class QualifiedName
	{
		public readonly string Name;
		public readonly string Namespace;

		public QualifiedName(string objectNamespace, string objectName)
		{
			Namespace = objectNamespace;
			Name = objectName;
		}

		public override string ToString()
		{
			return string.Format("{0}.{1}", Namespace, Name);
		}

		public override bool Equals(object obj)
		{
			QualifiedName qualifiedName = obj as QualifiedName;
			if (qualifiedName == null) return false;
			return
				(Namespace.ToUpper() == qualifiedName.Namespace.ToUpper()) &&
				(Name.ToUpper() == qualifiedName.Name.ToUpper());
		}

		public override int GetHashCode()
		{
			if (Namespace == null || Name == null)
				return string.Empty.GetHashCode();
			return (Namespace.ToUpper() + Name.ToUpper()).GetHashCode();
		}
	}
}
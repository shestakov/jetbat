using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using JetBat.Client.Entities;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;

namespace JetBat.UI.InputControls.Basic
{
	public class DataObjectPickComboBox : UserControl, IDataObjectPickListControl
	{
		private IAccessAdapter accessAdapter;
		private ComboBox comboBox;
		private DataView dataView;
		private Label labelHeader;
		private TextBox textBox;

		#region Инициализация и освобождение ресурсов

		public DataObjectPickComboBox()
		{
			InitializeComponent();
		}

		#endregion

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dataView = new System.Data.DataView();
			this.comboBox = new System.Windows.Forms.ComboBox();
			this.labelHeader = new System.Windows.Forms.Label();
			this.textBox = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize) (this.dataView)).BeginInit();
			this.SuspendLayout();
			// 
			// dataView
			// 
			this.dataView.AllowDelete = false;
			this.dataView.AllowEdit = false;
			this.dataView.AllowNew = false;
			// 
			// comboBox
			// 
			this.comboBox.FormattingEnabled = true;
			this.comboBox.Location = new System.Drawing.Point(0, 16);
			this.comboBox.Name = "comboBox";
			this.comboBox.Size = new System.Drawing.Size(250, 21);
			this.comboBox.TabIndex = 7;
			this.comboBox.Leave += new System.EventHandler(this.comboBox_Leave);
			this.comboBox.Enter += new System.EventHandler(this.comboBox_Enter);
			this.comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
			this.comboBox.SizeChanged += new System.EventHandler(this.comboBox_SizeChanged);
			this.comboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
			// 
			// labelHeader
			// 
			this.labelHeader.BackColor = System.Drawing.Color.WhiteSmoke;
			this.labelHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelHeader.Location = new System.Drawing.Point(0, 0);
			this.labelHeader.Name = "labelHeader";
			this.labelHeader.Size = new System.Drawing.Size(250, 16);
			this.labelHeader.TabIndex = 8;
			this.labelHeader.Text = "Заголовок";
			this.labelHeader.VisibleChanged += new System.EventHandler(this.labelHeader_VisibleChanged);
			this.labelHeader.SizeChanged += new System.EventHandler(this.labelHeader_SizeChanged);
			// 
			// textBox
			// 
			this.textBox.Location = new System.Drawing.Point(0, 16);
			this.textBox.Name = "textBox";
			this.textBox.ReadOnly = true;
			this.textBox.Size = new System.Drawing.Size(250, 20);
			this.textBox.TabIndex = 9;
			// 
			// DataObjectPickComboBox
			// 
			this.Controls.Add(this.labelHeader);
			this.Controls.Add(this.comboBox);
			this.Controls.Add(this.textBox);
			this.Name = "DataObjectPickComboBox";
			this.Size = new System.Drawing.Size(250, 37);
			this.Enter += new System.EventHandler(this.DataObjectPickComboBox_Enter);
			this.TabIndexChanged += new System.EventHandler(this.DataObjectPickComboBox_TabIndexChanged);
			this.Leave += new System.EventHandler(this.DataObjectPickComboBox_Leave);
			this.SizeChanged += new System.EventHandler(this.DataObjectListComboBox_SizeChanged);
			((System.ComponentModel.ISupportInitialize) (this.dataView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		#region Атрибуты

		private readonly AttributeValueSet parameters = new AttributeValueSet();
		protected bool allowNull = true;
		protected bool changed;
		private string columnName = String.Empty;
		private string complexAttributeName = String.Empty;
		protected bool informativeLabel = true;
		protected bool initialIsNull = true;

		protected bool isNull = true;
		private string isNullText = "[на задано]";
		private string listName = String.Empty;
		private string listNamespace = String.Empty;
		private ObjectDefinition objectPlainObjectListViewDefinition;
		protected bool readOnly;
		protected bool showTabOrder = true;
		private DataTable table;

		#endregion

		#region Выбранный объект

		public AttributeValueSet SelectedObject
		{
			get
			{
				AttributeValueSet keyValue = new AttributeValueSet();

				if ((comboBox.SelectedItem == null) || !(comboBox.SelectedItem is DataObjectListItem))
					return keyValue;

				DataObjectListItem item = comboBox.SelectedItem as DataObjectListItem;

				ObjectDefinition plainObjectList =
					accessAdapter.MetadataStore.Get<PlainObjectListViewDefinition>(ListNamespace, ListName);

				foreach (ObjectAttributeDefinition attribute in plainObjectList.Attributes)
					if (attribute.IsPrimaryKeyMember)
						if (item != null) keyValue.Add(attribute.Name, item.DataRow[attribute.Name]);

				return keyValue;
			}
		}

		#endregion

		#region Загрузка списка

		public void Prepare()
		{
			objectPlainObjectListViewDefinition =
				accessAdapter.MetadataStore.Get<PlainObjectListViewDefinition>(listNamespace, ListName);
			table = accessAdapter.ObjectFactory.LoadObjectListView(listNamespace, listName, parameters);
			dataView.Table = table;
			fill();
		}

		protected virtual void fill()
		{
			comboBox.BeginUpdate();
			comboBox.Items.Clear();

			if (table == null)
			{
				comboBox.EndUpdate();
				return;
			}

			if (allowNull)
			{
				comboBox.Items.Add(isNullText);
			}

			try
			{
				foreach (DataRowView rowView in dataView)
				{
					DataObjectListItem item =
						new DataObjectListItem(rowView[columnName].ToString(), rowView.Row, objectPlainObjectListViewDefinition);
					comboBox.Items.Add(item);
				}
			}
			finally
			{
				comboBox.EndUpdate();
			}
		}

		#endregion

		#region Внешний вид

		private void realign()
		{
			SizeChanged -= DataObjectListComboBox_SizeChanged;
			labelHeader.SizeChanged -= labelHeader_SizeChanged;
			comboBox.SizeChanged -= comboBox_SizeChanged;

			comboBox.Top = labelHeader.Visible ? labelHeader.Height : 0;
			Height = (labelHeader.Visible ? labelHeader.Height : 0) + comboBox.Height;
			comboBox.Width = Width;

			textBox.Top = labelHeader.Visible ? labelHeader.Height : 0;
			textBox.Width = Width;

			SizeChanged += DataObjectListComboBox_SizeChanged;
			labelHeader.SizeChanged += labelHeader_SizeChanged;
			comboBox.SizeChanged += comboBox_SizeChanged;
		}

		private void labelHeader_SizeChanged(object sender, EventArgs e)
		{
			realign();
		}

		private void DataObjectListComboBox_SizeChanged(object sender, EventArgs e)
		{
			realign();
		}

		private void comboBox_SizeChanged(object sender, EventArgs e)
		{
			realign();
		}


		private void labelHeader_VisibleChanged(object sender, EventArgs e)
		{
			realign();
		}

		#endregion

		#region IDataObjectPickListControl Members

		public event EventHandler ValueChanged;

		public virtual void ResetToNull()
		{
			comboBox.SelectedIndexChanged -= comboBox_SelectedIndexChanged;
			bool found = false;
			foreach (object item in comboBox.Items)
				if (!(item is DataObjectListItem))
				{
					comboBox.SelectedItem = item;
					found = true;
				}
			if (!found) comboBox.SelectedIndex = -1;
			comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
			isNull = true;
			initialIsNull = true;
			renderLabel();
			textBox.Text = comboBox.Text;
			if ((isNull == false) && (ValueChanged != null))
				ValueChanged(this, EventArgs.Empty);
		}

		public void SelectObject(AttributeValueSet primaryKey)
		{
			foreach (object listItem in comboBox.Items)
			{
				DataObjectListItem item = listItem as DataObjectListItem;
				if (item == null)
					continue;
				bool match = true;
				foreach (string attribute in primaryKey.Keys)
				{
					if (item.DataRow[attribute].GetHashCode() != (primaryKey[attribute].GetHashCode()))
					{
						match = false;
						break;
					}
				}
				if (match)
				{
					comboBox.SelectedItem = item;
					break;
				}
			}
			textBox.Text = comboBox.Text;
		}

		#endregion

		protected virtual void renderLabel()
		{
			labelHeader.Text = (informativeLabel && !allowNull ? "*" : "") + base.Text +
			                   (showTabOrder ? string.Format(" [{0}]", TabIndex + 1) : "");
			labelHeader.ForeColor = (informativeLabel && !allowNull && IsNull) ? Color.Red : SystemColors.HotTrack;
		}

		protected virtual void setNotNull()
		{
			if ((isNull) && (ValueChanged != null))
				ValueChanged(this, EventArgs.Empty);

			isNull = false;
			initialIsNull = false;
			renderLabel();
		}

		private void comboBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && e.Shift && allowNull)
			{
				ResetToNull();
			}
		}

		private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((comboBox.SelectedItem == null) || !(comboBox.SelectedItem is DataObjectListItem))
				isNull = true;
			else
				isNull = false;

			textBox.Text = comboBox.Text;

			if (ValueChanged != null)
				ValueChanged(sender, e);
		}

		private void DataObjectPickComboBox_TabIndexChanged(object sender, EventArgs e)
		{
			renderLabel();
		}

		private void comboBox_Enter(object sender, EventArgs e)
		{
		}

		private void comboBox_Leave(object sender, EventArgs e)
		{
		}

		private void DataObjectPickComboBox_Enter(object sender, EventArgs e)
		{
			comboBox.BackColor = Color.FromArgb(255, 255, 150);
			textBox.BackColor = Color.FromArgb(255, 255, 150);
		}

		private void DataObjectPickComboBox_Leave(object sender, EventArgs e)
		{
			comboBox.BackColor = Color.White;
			textBox.BackColor = Color.White;
		}

		#region Свойства

		[
			Description("Отображение TabOrder в заголовке"),
			Category("Appearance"),
			Browsable(true),
			DefaultValue(true),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
		]
		public virtual bool ShowTabOrder
		{
			get { return showTabOrder; }
			set
			{
				showTabOrder = value;
				renderLabel();
			}
		}

		[
			Description("Информативная подпись"),
			Category("Appearance"),
			Browsable(true),
			DefaultValue(true),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
		]
		public virtual bool InformativeLabel
		{
			get { return informativeLabel; }
			set
			{
				informativeLabel = value;
				renderLabel();
			}
		}

		[DefaultValue(true)]
		public bool ShowLabel
		{
			get { return labelHeader.Visible; }
			set { labelHeader.Visible = value; }
		}

		[DefaultValue("[на задано]")]
		public string IsNullText
		{
			get { return isNullText; }
			set
			{
				isNullText = value;
				fill();
			}
		}

		[
			Browsable(true),
			DefaultValue(BorderStyle.None),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
		]
		public new BorderStyle BorderStyle
		{
			get { return base.BorderStyle; }
			set { base.BorderStyle = value; }
		}

		[
			Description("Заголовок"),
			Category("Appearance"),
			Browsable(true),
			DefaultValue("Заголовок"),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
		]
		public override string Text
		{
			get { return base.Text; }
			set
			{
				base.Text = value;
				renderLabel();
			}
		}

		public virtual bool IsNull
		{
			get { return isNull; }
		}

		public virtual bool AllowNull
		{
			get { return allowNull; }
			set
			{
				allowNull = value;
				fill();
				renderLabel();
			}
		}

		public virtual bool Changed
		{
			get { return changed; }
		}

		public bool ReadOnly
		{
			get { return readOnly; }
			set
			{
				readOnly = value;
				comboBox.Enabled = !readOnly;
				comboBox.Visible = !readOnly;
				textBox.Visible = readOnly;
			}
		}

		#endregion

		#region Свойства: загрузка списка

		public string ColumnName
		{
			get { return columnName; }
			set { columnName = value; }
		}

		public string ListName
		{
			get { return listName; }
			set
			{
				listName = value;
				ResetToNull();
				comboBox.Items.Clear();
			}
		}

		public string ListNamespace
		{
			get { return listNamespace; }
			set
			{
				listNamespace = value;
				ResetToNull();
				comboBox.Items.Clear();
			}
		}

		public AttributeValueSet Parameters
		{
			get { return parameters; }
		}

		public IAccessAdapter AccessAdapter
		{
			get { return accessAdapter; }
			set { accessAdapter = value; }
		}

		public string ComplexAttributeName
		{
			get { return complexAttributeName; }
			set { complexAttributeName = value; }
		}

		public PlainObjectDefinition PlainObjectDefinition
		{
			get
			{
				PlainObjectListViewDefinition listDescriptor =
					accessAdapter.MetadataStore.Get<PlainObjectListViewDefinition>(listNamespace, listName);
				return
					accessAdapter.MetadataStore.Get<PlainObjectDefinition>(listDescriptor.BasicObjectNamespace,
					                                                       listDescriptor.BasicObjectName);
			}
		}

		#endregion
	}
}
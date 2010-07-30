using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using JetBat.Client.Entities;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;
using JetBat.UI.DataControls.Grid.Basic;
using JetBat.UI.InputControls;
using JetBat.UI.InputControls.Basic;
using MessageBox = JetBat.UI.DataControls.Messages.MessageBox;

namespace JetBat.UI.DataControls.Forms
{
	public partial class DocumentForm : Form
	{
		#region Mode enum

		public enum Mode
		{
			Create,
			Update,
			View
		}

		#endregion

		protected IAccessAdapter accessAdapter;
		protected Mode currentMode;
		protected DocumentInstance documentInstance;
		protected AttributeValueSet parameters;
		protected SqlConnection sqlConnection;

		#region Открытые методы

		public virtual DialogResult Show(DocumentInstance documentInstance, Mode mode,
										 AttributeValueSet parameters)
		{
			currentMode = mode;
			this.documentInstance = documentInstance;
			this.parameters = parameters;
			if (currentMode == Mode.Create) setParametrizedObjectAttributes();
			setInputControls();
			setDataObjectListComboBoxes();
			loadChildEntryListViews();
			return ShowDialog();
		}

		#endregion

		#region Установка параметров списков дочерних объектов и их загрузка

		protected virtual void loadChildEntryListViews()
		{
			//loadDocumentDetailListViews(this);
			loadChildPlainObjectListViews(this);
		}

		protected virtual void loadChildPlainObjectListViews(Control containerControl)
		{
			foreach (Control control in containerControl.Controls)
			{
				PlainObjectGridView gridView = control as PlainObjectGridView;
				if (gridView != null)
				{
					gridView.CurrentMode = currentMode == Mode.View
												? GridViewMode.ReadOnly
												: GridViewMode.Management;
					gridView.AccessAdapter = accessAdapter;
					gridView.Parameters["DocumentVersionID"] = documentInstance.VersionID;
					gridView.LoadList();
				}

				if (control.Controls.Count > 0)
					loadChildPlainObjectListViews(control);
			}
		}

		//protected virtual void loadDocumentDetailListViews(Control ContainerControl)
		//{
		//    foreach (Control control in ContainerControl.Controls)
		//    {
		//        DocumentDetailGridView grid_view = control as DocumentDetailGridView;
		//        if (grid_view != null)
		//        {
		//            grid_view.CurrentMode = currentMode == Mode.View
		//                                        ? DocumentDetailGridViewMode.ReadOnly
		//                                        : DocumentDetailGridViewMode.Management;
		//            grid_view.AccessAdapter = sqlAccessProvider;
		//            grid_view.Parameters["DocumentVersionID"] = documentInstance.VersionID;
		//            grid_view.LoadList();
		//        }

		//        if (control.Controls.Count > 0)
		//            loadDocumentDetailListViews(control);
		//    }
		//}

		#endregion

		#region Загрузка атрибутов объекта

		protected virtual void setInputControls()
		{
			setInputControls(this);
		}

		private void setInputControls(Control containerControl)
		{
			foreach (Control control in containerControl.Controls)
			{
				IInputControl inputControl = control as IInputControl;

				if (inputControl != null)
				{
					DocumentDefinition definition = documentInstance.DocumentDefinition;
					ObjectAttributeDefinition attribute = definition.Attributes[inputControl.AttributeName];
					if (attribute != null)
					{
						inputControl.Text = attribute.UILabel;
						if (currentMode == Mode.Create)
						{
							inputControl.ResetToNull();
						}
						else
						{
							inputControl.Value = documentInstance[attribute.Name];
						}

						if (inputControl is TextInput)
						{
							(inputControl as TextInput).MaxLength = attribute.MaxLength;
						}

						inputControl.ReadOnly = (currentMode == Mode.View || attribute.IsReadOnly) ? true : false;
						inputControl.AllowNull = attribute.IsNullable;
						if (inputControl is TextInput)
						{
							(inputControl as TextInput).MaxLength = attribute.MaxLength;
						}
						else if (inputControl is DecimalInput)
						{
							(inputControl as DecimalInput).DecimalPrecision = attribute.Precision;
							(inputControl as DecimalInput).DecimalScale = attribute.Scale;
						}
					}
				}

				if (control.Controls.Count > 0)
					setInputControls(control);
			}
		}

		protected virtual void setDataObjectListComboBoxes()
		{
			setDataObjectListComboBoxes(this);
		}

		protected virtual void setDataObjectListComboBoxes(Control containerControl)
		{
			foreach (Control control in containerControl.Controls)
			{
				IDataObjectPickControl comboBox = control as IDataObjectPickControl;

				if (comboBox != null)
				{
					comboBox.AccessAdapter = accessAdapter;
					comboBox.Parameters.Clear();
					comboBox.Parameters.Add(parameters);
					comboBox.Prepare();
					comboBox.ReadOnly = currentMode == Mode.View ? true : false;
					if (
						documentInstance.DocumentDefinition.ComplexAttributes[comboBox.ComplexAttributeName] !=
						null)
						comboBox.Text =
							documentInstance.DocumentDefinition.ComplexAttributes[comboBox.ComplexAttributeName]
								.UILabel;

					if (currentMode == Mode.Create)
					{
						comboBox.ResetToNull();
					}
					else
					{
						ObjectComplexAttributeDefinition foreignKey =
							documentInstance.DocumentDefinition.ComplexAttributes[comboBox.ComplexAttributeName];
						if (foreignKey != null)
						{
							AttributeValueSet primaryKey = new AttributeValueSet();
							foreach (string keyAttribute in foreignKey.MemberColumns.Keys)
							{
								primaryKey[keyAttribute] = documentInstance[foreignKey.MemberColumns[keyAttribute]];
							}
							comboBox.SelectObject(primaryKey);
						}
					}
				}
				if (control.Controls.Count > 0)
					setDataObjectListComboBoxes(control);
			}
		}

		#endregion

		#region Присвоение значений атрибутам объекта

		protected virtual void setParametrizedObjectAttributes()
		{
			if (parameters != null)
			{
				NamedObjectReadOnlyCollection<ObjectAttributeDefinition> attributes = documentInstance.DocumentDefinition.Attributes;
				foreach (ObjectAttributeDefinition attribute in attributes)
				{
					if (parameters.ContainsKey(attribute.Name))
						documentInstance[attribute.Name] = parameters[attribute.Name];
				}
			}
		}

		protected virtual void setObjectAttributes()
		{
			setObjectAttributes(this);
		}

		private void setObjectAttributes(Control container)
		{
			foreach (Control control in container.Controls)
			{
				IInputControl inputControl = control as IInputControl;

				if (inputControl != null)
				{
					DocumentDefinition definition = documentInstance.DocumentDefinition;
					ObjectAttributeDefinition attribute = definition.Attributes[inputControl.AttributeName];
					if (attribute != null)
					{
						if ((currentMode != Mode.View) && (attribute.IsReadOnly != true))
						{
							documentInstance[attribute.Name] = inputControl.Value;
						}
					}
				}
			}

			foreach (Control control in container.Controls)
			{
				IDataObjectPickControl comboBox = control as IDataObjectPickControl;

				if (comboBox != null)
				{
					ObjectComplexAttributeDefinition foreignKey =
						documentInstance.DocumentDefinition.ComplexAttributes[comboBox.ComplexAttributeName];
					if (foreignKey != null)
					{
						AttributeValueSet attributes = comboBox.SelectedObject;
						foreach (string keyAttribute in foreignKey.MemberColumns.Keys)
						{
							documentInstance[foreignKey.MemberColumns[keyAttribute]] = comboBox.IsNull
																							? DBNull.Value
																							: attributes[keyAttribute];
						}
					}
				}
				if (control.Controls.Count > 0)
					setObjectAttributes(control);
			}
		}

		#endregion

		#region Закрытые методы

		protected virtual void ConfirmEdit()
		{
			if (documentInstance == null)
				return;

			setObjectAttributes();
			if (currentMode != Mode.View)
			{
				documentInstance.UpdateVersion();
				documentInstance.ConfirmEdit();
			}
		}

		protected virtual void CancelEdit()
		{
			if (documentInstance == null)
				return;
			if (currentMode != Mode.View)
				documentInstance.CancelEdit();
		}

		#endregion

		#region Свойства

		public virtual IAccessAdapter AccessAdapter
		{
			get { return accessAdapter; }
			set { accessAdapter = value; }
		}

		public virtual SqlConnection Connection
		{
			get { return sqlConnection; }
			set { sqlConnection = value; }
		}

		public Mode CurrentMode
		{
			get { return currentMode; }
		}

		#endregion

		#region События

		private void OKBtn_Click(object sender, EventArgs e)
		{
			ValidateChildren();
			if (!ValidateInputControls(this))
				return;
			DialogResult = DialogResult.OK;
		}

		private void MultiversionDocumentForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				try
				{
					if (currentMode != Mode.View)
					{
						ConfirmEdit();
					}
					else
					{
						CancelEdit();
					}
					documentInstance = null;
				}
				catch (Exception ex)
				{
					MessageBox.Show(
						"При внесении изменений в базу данных произошла ошибка: " + ex.Message + Environment.NewLine +
						"Изменения не были произведены.", "Ошибка при обращении к базе данных...", MessageBoxButtons.OK,
						MessageBoxIcon.Error);
					e.Cancel = true;
				}
			}
			else
			{
				try
				{
					CancelEdit();
					documentInstance = null;
				}
				catch (Exception ex)
				{
					MessageBox.Show(
						"При внесении изменений в базу данных произошла ошибка: " + ex.Message + Environment.NewLine +
						"Изменения не были произведены.", "Ошибка при обращении к базе данных...", MessageBoxButtons.OK,
						MessageBoxIcon.Error);
					e.Cancel = true;
				}
			}
		}

		#endregion

		#region Validation

		private static bool ValidateInputControls(Control containerControl)
		{
			bool result = true;

			foreach (Control control in containerControl.Controls)
			{
				IInputControl inputControl = control as IInputControl;

				if (inputControl != null)
				{
					result = result && inputControl.ValidateValue();
				}

				if (control.Controls.Count > 0)
					result = result && ValidateInputControls(control);
			}
			return result;
		}

		#endregion

		#region Конструктор

		public DocumentForm()
		{
			InitializeComponent();
		}

		#endregion
	}
}
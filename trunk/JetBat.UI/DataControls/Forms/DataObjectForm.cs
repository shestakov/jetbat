using System;
using System.Windows.Forms;
using JetBat.Client.Entities;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;
using JetBat.UI.DataControls.Messages;
using JetBat.UI.InputControls;
using MessageBox=JetBat.UI.DataControls.Messages.MessageBox;

namespace JetBat.UI.DataControls.Forms
{
	public partial class DataObjectForm : Form
	{
		#region Mode enum

		public enum Mode
		{
			Insert,
			Update,
			View
		}

		#endregion

		protected IAccessAdapter accessAdapter;
		protected Mode currentMode;
		protected AttributeValueSet parameters;
		protected PlainObjectInstance plainObjectInstance;

		#region Открытые методы

		public virtual DialogResult Show(PlainObjectInstance plainObjectInstance, Mode mode, AttributeValueSet parameters)
		{
			currentMode = mode;
			this.plainObjectInstance = plainObjectInstance;
			this.parameters = parameters;
			if (currentMode == Mode.Insert) setParametrizedObjectAttributes();
			setInputControls();
			setDataObjectListComboBoxes();
			return ShowDialog();
		}

		#endregion

		#region Закрытые методы

		protected virtual void setParametrizedObjectAttributes()
		{
			if (parameters != null)
			{
				NamedObjectReadOnlyCollection<ObjectAttributeDefinition> attributes =
					plainObjectInstance.PlainObjectDefinition.Attributes;
				foreach (ObjectAttributeDefinition attribute in attributes)
				{
					if (parameters.ContainsKey(attribute.Name))
						plainObjectInstance[attribute.Name] = parameters[attribute.Name];
				}
			}
		}

		protected virtual bool updateObject()
		{
			if (plainObjectInstance == null)
				return false;
			setObjectAttributes();

			ErrorMessageCollection messages = plainObjectInstance.Update();

			int maxSeverity = 0;
			foreach (ErrorMessage message in messages)
			{
				if (message.Severity > maxSeverity)
					maxSeverity = message.Severity;
			}

			if (messages.Count > 0)
			{
				MessageListWindowForm form = new MessageListWindowForm();
				form.ShowDialog(messages, "Ошибки при обращении к БД!", MessageBoxButtons.OK, MessageBoxDefaultButton.Button1);
			}

			return maxSeverity < 2;
		}

		protected virtual bool insertObject()
		{
			if (plainObjectInstance == null)
				return false;
			setObjectAttributes();

			ErrorMessageCollection messages = plainObjectInstance.Insert();

			int maxSeverity = 0;
			foreach (ErrorMessage message in messages)
			{
				if (message.Severity > maxSeverity)
					maxSeverity = message.Severity;
			}

			if (messages.Count > 0)
			{
				MessageListWindowForm form = new MessageListWindowForm();
				form.ShowDialog(messages, "Ошибки при обращении к БД!", MessageBoxButtons.OK, MessageBoxDefaultButton.Button1);
			}

			return maxSeverity < 2;
		}

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
					PlainObjectDefinition definition = plainObjectInstance.PlainObjectDefinition;
					ObjectAttributeDefinition attribute = definition.Attributes[inputControl.AttributeName];
					if (attribute != null)
					{
						inputControl.Text = attribute.UILabel;

						if (inputControl is ITextInput)
						{
							(inputControl as ITextInput).MaxLength = attribute.MaxLength;
						}

						inputControl.ReadOnly = (currentMode == Mode.View || attribute.IsReadOnly) ? true : false;
						inputControl.AllowNull = attribute.IsNullable;
						if (inputControl is ITextInput)
						{
							(inputControl as ITextInput).MaxLength = attribute.MaxLength;
						}
						else if (inputControl is IDecimalInput)
						{
							(inputControl as IDecimalInput).DecimalPrecision = attribute.Precision;
							(inputControl as IDecimalInput).DecimalScale = attribute.Scale;
						}

						if (currentMode == Mode.Insert)
						{
							inputControl.ResetToNull();
						}
						else
						{
							inputControl.Value = plainObjectInstance[attribute.Name];
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

		private void setDataObjectListComboBoxes(Control containerControl)
		{
			foreach (Control control in containerControl.Controls)
			{
				IDataObjectPickControl comboBox = control as IDataObjectPickControl;

				if (comboBox != null)
				{
					comboBox.AccessAdapter = accessAdapter;
					//combo_box.Parameters.Clear();
					comboBox.Parameters.Add(parameters);
					comboBox.Prepare();
					comboBox.ReadOnly = currentMode == Mode.View ? true : false;
					if (plainObjectInstance.PlainObjectDefinition.ComplexAttributes[comboBox.ComplexAttributeName] !=
					    null)
						comboBox.Text =
							plainObjectInstance.PlainObjectDefinition.ComplexAttributes[comboBox.ComplexAttributeName].
								UILabel;

					if (currentMode == Mode.Insert)
					{
						comboBox.ResetToNull();
					}
					else
					{
						ObjectComplexAttributeDefinition foreignKey =
							plainObjectInstance.PlainObjectDefinition.ComplexAttributes[comboBox.ComplexAttributeName];
						if ( /*plainObjectList != null && */foreignKey != null)
						{
							AttributeValueSet primaryKey = new AttributeValueSet();
							foreach (string keyAttribute in foreignKey.MemberColumns.Keys)
							{
								primaryKey[keyAttribute] = plainObjectInstance[foreignKey.MemberColumns[keyAttribute]];
							}
							comboBox.SelectObject(primaryKey);
						}
					}
				}
				if (control.Controls.Count > 0)
					setDataObjectListComboBoxes(control);
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
					PlainObjectDefinition definition = plainObjectInstance.PlainObjectDefinition;
					ObjectAttributeDefinition attribute = definition.Attributes[inputControl.AttributeName];
					if (attribute != null)
					{
						if ((currentMode != Mode.View) && (attribute.IsReadOnly != true))
						{
							plainObjectInstance[attribute.Name] = inputControl.Value;
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
						plainObjectInstance.PlainObjectDefinition.ComplexAttributes[comboBox.ComplexAttributeName];
					if ( /*plainObjectList != null && */foreignKey != null)
					{
						AttributeValueSet attributes = comboBox.SelectedObject;
						foreach (string keyAttribute in foreignKey.MemberColumns.Keys)
						{
							plainObjectInstance[foreignKey.MemberColumns[keyAttribute]] = comboBox.IsNull
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

		#region Свойства

		public virtual IAccessAdapter AccessAdapter
		{
			get { return accessAdapter; }
			set { accessAdapter = value; }
		}

		public Mode CurrentMode
		{
			get { return currentMode; }
		}

		#endregion

		#region События

		private void DataObjectForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				bool succeeded = false;

				try
				{
					switch (currentMode)
					{
						case Mode.Insert:
							succeeded = insertObject();
							break;
						case Mode.Update:
							succeeded = updateObject();
							break;
						case Mode.View:
							break;
					}
					//plainObjectInstance = null;
				}
				catch (Exception ex)
				{
					MessageBox.Show(
						"При внесении изменений в базу данных произошла ошибка: " + ex.Message + Environment.NewLine +
						"Изменения не были произведены.", "Ошибка при обращении к базе данных...", MessageBoxButtons.OK,
						MessageBoxIcon.Error);
					succeeded = false;
				}

				e.Cancel = !succeeded;
			}
		}

		#endregion

		#region Validation

		protected bool ValidateInputControls(Control containerControl)
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

		public DataObjectForm()
		{
			InitializeComponent();
		}

		#endregion
	}
}
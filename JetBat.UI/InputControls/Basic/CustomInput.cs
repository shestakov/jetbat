using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace JetBat.UI.InputControls.Basic
{
	public class CustomInput : UserControl, IInputControl
	{
		protected bool allowNull;
		protected string attributeName;
		protected ErrorProvider errorProvider;
		protected bool informativeLabel = true;

		protected object initialValue;

		protected bool initialValueIsNull = true;
		protected Label labelHeader;
		protected bool readOnly;
		protected bool showLabel = true;
		protected bool showTabOrder = true;
		protected bool userChanged;
		protected object userValue;
		protected bool userValueIsNull = true;

		public CustomInput()
		{
			InitializeComponent();
		}

		public event EventHandler ValueChanged;

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.labelHeader = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelHeader
			// 
			this.labelHeader.BackColor = System.Drawing.Color.WhiteSmoke;
			this.labelHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelHeader.Font =
				new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular,
				                        System.Drawing.GraphicsUnit.Point, ((byte) (204)));
			this.labelHeader.ForeColor = System.Drawing.Color.RoyalBlue;
			this.labelHeader.Location = new System.Drawing.Point(0, 0);
			this.labelHeader.Name = "labelHeader";
			this.labelHeader.Size = new System.Drawing.Size(150, 16);
			this.labelHeader.TabIndex = 0;
			this.labelHeader.Text = "���������";
			// 
			// CustomInput
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.labelHeader);
			this.Name = "CustomInput";
			this.Size = new System.Drawing.Size(150, 40);
			this.TabIndexChanged += new System.EventHandler(this.SimpleInput_TabIndexChanged);
			this.Validating += new System.ComponentModel.CancelEventHandler(this.InputControl_Validating);
			this.ResumeLayout(false);
		}

		#endregion

		#region �������� ������

		protected virtual void renderLabel()
		{
			labelHeader.Text = (informativeLabel && !allowNull ? "*" : "") + base.Text +
			                   (showTabOrder ? string.Format(" [{0}]", TabIndex + 1) : "");
			labelHeader.ForeColor = (informativeLabel && !allowNull && IsNull && !DesignMode) ? Color.Red : Color.RoyalBlue;
			labelHeader.Visible = showLabel;
		}

		protected virtual void renderUserValue()
		{
			renderLabel();
		}

		protected virtual void pickUserValue()
		{
			userChanged = (initialValueIsNull != userValueIsNull) || (initialValue != userValue);
			if (ValueChanged != null)
				ValueChanged(this, EventArgs.Empty);
//			resetError();
			renderLabel();
		}

		protected virtual void setUserValueNull()
		{
			userValue = null;
			userValueIsNull = true;
		}

		protected virtual void resetToNull()
		{
			setUserValueNull();
			resetError();
			renderUserValue();
		}

		#endregion

		#region �������� ������

		public virtual void ResetToNull()
		{
			resetToNull();
			if (!userValueIsNull && (ValueChanged != null))
				ValueChanged(this, EventArgs.Empty);
		}

		#endregion

		#region ��������

		public virtual bool IsNull
		{
			get { return userValueIsNull; }
		}

		[Browsable(false)]
		public virtual bool UserChanged
		{
			get { return userChanged; }
		}

		[Browsable(false)]
		public virtual object InitialValue
		{
			get { return initialValue; }
		}

		[Browsable(false)]
		public virtual object Value
		{
			get { return userValue; }
			set
			{
				initialValueIsNull = (value == DBNull.Value || value == null);
				initialValue = initialValueIsNull ? null : value;
				userValueIsNull = initialValueIsNull;
				userValue = InitialValue;
				resetError();
				renderUserValue();

				if (ValueChanged != null)
					ValueChanged(this, EventArgs.Empty);
			}
		}

		#endregion

		#region ��������

		public ErrorProvider ErrorProvider
		{
			get { return errorProvider; }
			set
			{
				errorProvider = value;
				resetError();
			}
		}

		public string AttributeName
		{
			get { return attributeName; }
			set { attributeName = value; }
		}

		[
			Description("����������� TabOrder � ���������"),
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
			Description("������������� �������"),
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

		[
			Description("����������� ���������� �������"),
			Category("Appearance"),
			Browsable(true),
			DefaultValue(true),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
		]
		public virtual bool ShowLabel
		{
			get { return showLabel; }
			set
			{
				showLabel = value;
				renderLabel();
			}
		}


		[
			Description("���������"),
			Category("Appearance"),
			Browsable(true),
			DefaultValue("���������"),
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

		[DefaultValue(false)]
		public virtual bool AllowNull
		{
			get { return allowNull; }
			set
			{
				allowNull = value;
				renderLabel();
			}
		}

		public virtual bool ReadOnly
		{
			get { return readOnly; }
			set { readOnly = value; }
		}

		private void SimpleInput_TabIndexChanged(object sender, EventArgs e)
		{
			renderLabel();
		}

		#endregion

		#region �������� ��������

		public bool ValidateValue()
		{
			bool result = !(!allowNull && userValueIsNull && Enabled && !ReadOnly);

			if (errorProvider != null)
			{
				if (!result)
					errorProvider.SetError(this, "��� ���� �� ����� ���� ������. ����������, ������� ��������.");
				else
					errorProvider.SetError(this, "");
			}
			return result;
		}

		private void resetError()
		{
			if (errorProvider != null)
				errorProvider.SetError(this, "");
		}

		private void InputControl_Validating(object sender, CancelEventArgs e)
		{
			ValidateValue();
		}

		#endregion

		#region ���������� ��� ������

		private Color focusedBackColor = Color.AliceBlue;

		public Color FocusedBackColor
		{
			get { return focusedBackColor; }
			set { focusedBackColor = value; }
		}

		#endregion
	}
}
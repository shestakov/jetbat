using System;
using System.ComponentModel;
using System.Windows.Forms;
using JetBat.Client.Metadata.Abstract;
using JetBat.Client.Metadata.Misc;
using JetBat.UI.DataControls.Grid.Basic;

namespace JetBat.UI.DataControls.Forms
{
	public partial class DataObjectPickForm : Form
	{
		protected IAccessAdapter accessAdapter;
		protected AttributeValueSet parameters;
		protected PlainObjectGridView pickGrid;
		protected AttributeValueSet[] selectedObjects;

		public DataObjectPickForm()
		{
			InitializeComponent();
		}

		public virtual IAccessAdapter AccessAdapter
		{
			get { return accessAdapter; }
			set { accessAdapter = value; }
		}

		public virtual AttributeValueSet Parameters
		{
			get { return parameters; }
			set { parameters = value; }
		}

		public virtual AttributeValueSet[] SelectedObjects
		{
			get { return selectedObjects; }
		}

		[Browsable(true)]
		public PlainObjectGridView PickGrid
		{
			get { return pickGrid; }
			set
			{
				if (pickGrid != null)
					pickGrid.ObjectPick -= pickGrid_OnObjectPick;
				pickGrid = value;
				if (pickGrid != null)
					pickGrid.ObjectPick += pickGrid_OnObjectPick;
			}
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			if (pickGrid != null)
			{
				selectedObjects = pickGrid.SelectedObjects;
				DialogResult = DialogResult.OK;
			}
		}

		protected void pickGrid_OnObjectPick(object sender, EventArgs e)
		{
			if (pickGrid != null)
			{
				selectedObjects = pickGrid.SelectedObjects;
				DialogResult = DialogResult.OK;
			}
		}

		public virtual void Prepare()
		{
		}
	}
}
// ReSharper disable SuggestUseVarKeywordEvident
// ReSharper disable UseObjectOrCollectionInitializer
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using JetBat.Client.Entities;
using JetBat.Client.Metadata.Definitions;
using JetBat.Client.Metadata.Misc;
using JetBat.UI.DataControls.Forms;
using JetBat.UI.UserActions;
using MessageBox = JetBat.UI.DataControls.Messages.MessageBox;

namespace JetBat.UI.DataControls.Grid.Basic
{
	[Serializable]
	public enum DocumentGridViewMode
	{
		Management,
		Pick,
		PickAndManagement,
		ReadOnly
	} ;

	public partial class DocumentGridView : DataObjectListViewGrid
	{
		#region �����������

		public DocumentGridView()
		{
			InitializeComponent();
			createUserActions();
		}

		#region �������� �������� ������������

		private void createUserActions()
		{
			#region UserActionPick #1

			//userActionPick.SquenceNumber = 0;
			userActionPick.Active = true;
			userActionPick.BriefText = "�������";
			userActionPick.Enabled = false;
			userActionPick.Hint = "������� ���������� ������";
			userActionPick.Image = Resource.Pick_16;
			userActionPick.ImageTransparentColor = Color.Magenta;
			userActionPick.Name = "userActionPick";
			userActionPick.Shortcut = Keys.Return;
			userActionPick.Text = "������� ������";
			userActionPick.ActionExecute += delegate { OnPick(); };

			#endregion

			#region UserActionCreate #2

			//userActionCreate.SquenceNumber = 1;
			userActionCreate.Active = true;
			userActionCreate.BriefText = "�������";
			userActionCreate.Enabled = false;
			userActionCreate.Hint = "������� ����� ��������";
			userActionCreate.Image = Resource.Add_16;
			userActionCreate.ImageTransparentColor = Color.Magenta;
			userActionCreate.Name = "userActionCreate";
			userActionCreate.Shortcut = Keys.Insert;
			userActionCreate.Text = "������� ��������";
			userActionCreate.ActionExecute += delegate { OnCreate(); };

			#endregion

			#region UserActionStartEdit #3

			//userActionEdit.SquenceNumber = 2;
			userActionEdit.Active = true;
			userActionEdit.BriefText = "�������������";
			userActionEdit.Enabled = false;
			userActionEdit.Hint = "������������� ��������� ��������";
			userActionEdit.Image = Resource.Edit_16;
			userActionEdit.ImageTransparentColor = Color.Magenta;
			userActionEdit.Name = "userActionEdit";
			userActionEdit.Shortcut = Keys.Control | Keys.Return;
			userActionEdit.Text = "������������� ��������";
			userActionEdit.ActionExecute += delegate { OnStartEdit(); };

			#endregion

			#region UserActionDelete #4

			//userActionDelete.SquenceNumber = 3;
			userActionDelete.Active = true;
			userActionDelete.BriefText = "�������";
			userActionDelete.Enabled = false;
			userActionDelete.Hint = "������� ���������� ���������";
			userActionDelete.Image = Resource.Delete_16;
			userActionDelete.ImageTransparentColor = Color.Magenta;
			userActionDelete.Name = "userActionDelete";
			userActionDelete.Shortcut = Keys.Delete;
			userActionDelete.Text = "������� ��������";
			userActionDelete.ActionExecute += delegate { OnDelete(); };

			#endregion

			#region UserActionView #5

			//userActionView.SquenceNumber = 4;
			userActionView.Active = true;
			userActionView.BriefText = "�����������";
			userActionView.Enabled = false;
			userActionView.Hint = "���������� ��������� ��������";
			userActionView.Image = Resource.View_16;
			userActionView.ImageTransparentColor = Color.Magenta;
			userActionView.Name = "userActionView";
			userActionView.Shortcut = Keys.Return;
			userActionView.Text = "����������� ��������";
			userActionView.ActionExecute += delegate { OnView(); };

			#endregion

			#region UserActionCommit #6

			//userActionCommit.SquenceNumber = 5;
			userActionCommit.Active = true;
			userActionCommit.BriefText = "��������";
			userActionCommit.Enabled = false;
			userActionCommit.Hint = "�������� ��������� ���������";
			userActionCommit.Image = Resource.Commit_16;
			userActionCommit.ImageTransparentColor = Color.Magenta;
			userActionCommit.Name = "Commit";
			userActionCommit.Shortcut = Keys.None;
			userActionCommit.Text = "�������� ��������";
			userActionCommit.ActionExecute += delegate { OnCommit(); };

			#endregion

			#region UserActionRollback #7

			//userActionRollback.SquenceNumber = 6;
			userActionRollback.Active = true;
			userActionRollback.BriefText = "�������� ����������";
			userActionRollback.Enabled = false;
			userActionRollback.Hint = "�������� ���������� ��������� ����������";
			userActionRollback.Image = Resource.Rollback_16;
			userActionRollback.ImageTransparentColor = Color.Magenta;
			userActionRollback.Name = "Rollback";
			userActionRollback.Shortcut = Keys.None;
			userActionRollback.Text = "�������� ����������";
			userActionRollback.ActionExecute += delegate { OnRollback(); };

			#endregion

			#region UserActionRefresh #8

			//userActionRefresh.SquenceNumber = 7;
			userActionRefresh.Active = true;
			userActionRefresh.BriefText = "��������";
			userActionRefresh.Enabled = true;
			userActionRefresh.Hint = "�������� ������ ����������";
			userActionRefresh.Image = Resource.Refresh_16;
			userActionRefresh.ImageTransparentColor = Color.Magenta;
			userActionRefresh.Name = "userActionRefresh";
			userActionRefresh.Shortcut = Keys.F5;
			userActionRefresh.Text = "�������� ������";
			userActionRefresh.ActionExecute += delegate { LoadList(); };

			#endregion

			#region UserActionAdjustGrid #9

			//userActionCustomize.SquenceNumber = 8;
			userActionCustomize.Active = true;
			userActionCustomize.BriefText = "�������������";
			userActionCustomize.Enabled = true;
			userActionCustomize.Hint = "��������� ������������� ������";
			userActionCustomize.Image = Resource.Customize_16;
			userActionCustomize.ImageTransparentColor = Color.Magenta;
			userActionCustomize.Name = "userActionCustomize";
			userActionCustomize.Shortcut = Keys.None;
			userActionCustomize.Text = "��������� �������������";
			userActionCustomize.ActionExecute += delegate { showSettingsForm(); };

			#endregion

			userActions.Clear();
			userActions.AddPersistent(userActionPick);
			userActions.AddPersistent(userActionCreate);
			userActions.AddPersistent(userActionEdit);
			userActions.AddPersistent(userActionDelete);
			userActions.AddPersistent(userActionView);
			userActions.AddPersistent(userActionCommit);
			userActions.AddPersistent(userActionRollback);
			userActions.AddPersistent(userActionRefresh);
			userActions.AddPersistent(userActionCustomize);
		}

		#endregion

		#endregion

		#region �������� ������������

		#region �������� ������������: ��������

		private readonly UserAction userActionCommit = new UserAction();
		private readonly UserAction userActionCreate = new UserAction();
		private readonly UserAction userActionCustomize = new UserAction();
		private readonly UserAction userActionDelete = new UserAction();
		private readonly UserAction userActionEdit = new UserAction();
		private readonly UserAction userActionPick = new UserAction();
		private readonly UserAction userActionRefresh = new UserAction();
		private readonly UserAction userActionRollback = new UserAction();
		private readonly UserAction userActionView = new UserAction();
		private bool allowCommit = true;
		private bool allowCreate = true;
		private bool allowCustomize = true;
		private bool allowDelete = true;
		private bool allowEdit = true;
		private bool allowPick = true;
		private bool allowRefresh = true;
		private bool allowRollback = true;
		private bool allowView = true;

		#endregion

		#region �������� ������������: ������

		protected override void setActionEnabledState()
		{
			switch (currentMode)
			{
				case DocumentGridViewMode.Management:
					userActionPick.Enabled = false;
					userActionCreate.Enabled = true;
					userActionEdit.Enabled = true;
					userActionView.Enabled = true;
					userActionDelete.Enabled = true;
					userActionCommit.Enabled = true;
					userActionRollback.Enabled = true;
					break;
				case DocumentGridViewMode.Pick:
					userActionPick.Enabled = true;
					userActionCreate.Enabled = false;
					userActionEdit.Enabled = false;
					userActionView.Enabled = true;
					userActionDelete.Enabled = false;
					userActionCommit.Enabled = false;
					userActionRollback.Enabled = false;
					break;
				case DocumentGridViewMode.PickAndManagement:
					userActionPick.Enabled = true;
					userActionCreate.Enabled = true;
					userActionEdit.Enabled = true;
					userActionView.Enabled = true;
					userActionDelete.Enabled = true;
					userActionCommit.Enabled = true;
					userActionRollback.Enabled = true;
					break;
				case DocumentGridViewMode.ReadOnly:
					userActionPick.Enabled = false;
					userActionCreate.Enabled = false;
					userActionEdit.Enabled = false;
					userActionView.Enabled = true;
					userActionDelete.Enabled = false;
					userActionCommit.Enabled = false;
					userActionRollback.Enabled = false;
					break;
				default:
					break;
			}

			userActionPick.Enabled &= allowPick;
			userActionCreate.Enabled &= allowCreate;
			userActionEdit.Enabled &= allowEdit;
			userActionView.Enabled &= allowView;
			userActionDelete.Enabled &= allowDelete;
			userActionCommit.Enabled &= allowCommit;
			userActionRollback.Enabled &= allowRollback;
		}

		protected override void setActionActiveState()
		{
			int dataGridViewRowCount = getDataGridViewSelectedRowCount();
			int index = dataGridViewRowCount == 1 ? getTheOnlySelectedRowIndex() : -1;
			if (dataGridViewRowCount == 0)
			{
				userActionCreate.Active = validateParameters() && allowCreate;
				userActionEdit.Active = false;
				userActionDelete.Active = false;
				userActionPick.Active = false;
				userActionView.Active = false;
				userActionCommit.Active = false;
				userActionRollback.Active = false;
				userActionRefresh.Active = validateParameters() && allowRefresh;
				userActionCustomize.Active = allowCustomize;
			}
			else if (dataGridViewRowCount == 1)
			{
				userActionCreate.Active = validateParameters() && allowCreate;
				userActionEdit.Active = allowEdit &&
										Convert.ToInt32(dataView[index]["DocumentStateID"]) != 2;
				userActionDelete.Active = allowDelete &&
										  Convert.ToInt32(dataView[index]["DocumentStateID"]) != 2;
				userActionPick.Active = allowPick;
				userActionView.Active = allowView;
				userActionCommit.Active = allowCommit &&
										  Convert.ToInt32(dataView[index]["DocumentStateID"]) == 1;
				userActionRollback.Active = allowRollback &&
											Convert.ToInt32(dataView[index]["DocumentStateID"]) == 2;
				userActionRefresh.Active = validateParameters() && AllowRefresh;
				userActionCustomize.Active = allowCustomize;
			}
			else if (dataGridViewRowCount > 1)
			{
				userActionCreate.Active = validateParameters() & allowCreate;
				userActionEdit.Active = false;
				userActionDelete.Active = multiSelect && allowDelete &&
										  Convert.ToInt32(dataView[index]["DocumentStateID"]) != 2;
				userActionPick.Active = multiSelect && allowPick;
				userActionView.Active = false;
				userActionCommit.Active = false;
				userActionRollback.Active = false;
				userActionRefresh.Active = validateParameters() && allowRefresh;
				userActionCustomize.Active = allowCustomize;
			}
		}

		#endregion

		#region �������� ������������: ��������

		public bool AllowCreate
		{
			get { return allowCreate; }
			set
			{
				if (allowCreate != value)
				{
					allowCreate = value;
					setActionActiveState();
				}
			}
		}

		public bool AllowEdit
		{
			get { return allowEdit; }
			set
			{
				if (allowEdit != value)
				{
					allowEdit = value;
					setActionActiveState();
				}
			}
		}

		public bool AllowDelete
		{
			get { return allowDelete; }
			set
			{
				if (allowDelete != value)
				{
					allowDelete = value;
					setActionActiveState();
				}
			}
		}

		public bool AllowView
		{
			get { return allowView; }
			set
			{
				if (allowView != value)
				{
					allowView = value;
					setActionActiveState();
				}
			}
		}

		public bool AllowCommit
		{
			get { return allowCommit; }
			set
			{
				if (allowCommit != value)
				{
					allowCommit = value;
					setActionActiveState();
				}
			}
		}

		public bool AllowRollback
		{
			get { return allowRollback; }
			set
			{
				if (allowRollback != value)
				{
					allowRollback = value;
					setActionActiveState();
				}
			}
		}

		public bool AllowPick
		{
			get { return allowPick; }
			set
			{
				if (allowPick != value)
				{
					allowPick = value;
					setActionActiveState();
				}
			}
		}

		public bool AllowRefresh
		{
			get { return allowRefresh; }
			set
			{
				if (allowRefresh != value)
				{
					allowRefresh = value;
					setActionActiveState();
				}
			}
		}

		public bool AllowCustomize
		{
			get { return allowCustomize; }
			set
			{
				if (allowCustomize != value)
				{
					allowCustomize = value;
					setActionActiveState();
				}
			}
		}

		#endregion

		#endregion

		#region �������

		public override event EventHandler ListLoad;

		public event EventHandler DocumentCreate;
		public event EventHandler DocumentEdit;
		public event EventHandler DocumentDelete;
		public event EventHandler DocumentView;
		public event EventHandler ObjectPick;
		public event EventHandler DocumentCommit;
		public event EventHandler DocumentRollback;

		#endregion

		#region �������� ������

		public override void ConfigureList()
		{
			if (accessAdapter == null) return;

			objectListViewDefinition = accessAdapter.MetadataStore.Get<DocumentListViewDefinition>(listNamespace, listName);

			if (objectListViewDefinition == null)
			{
				ClearList();
				setActionEnabledState();
				return;
			}

			DocumentDefinition documentDefinition =
				accessAdapter.MetadataStore.Get<DocumentDefinition>(objectListViewDefinition.BasicObjectNamespace,
																	objectListViewDefinition.BasicObjectName);
			if (documentDefinition == null)
				throw new NullReferenceException("�������� ������� [" + objectListViewDefinition.Namespace + "] " +
												 objectListViewDefinition.Name + " ��� ������������� ������ ���������� �� �������.");

			Text = objectListViewDefinition.UIListCaption;

			userActionCreate.Text = documentDefinition.Actions["Create"].UIFullText;
			userActionEdit.Text = documentDefinition.Actions["StartEdit"].UIFullText;
			userActionDelete.Text = documentDefinition.Actions["Delete"].UIFullText;
			userActionView.Text = "��������";
			userActionPick.Text = documentDefinition.Actions["Pick"].UIFullText;
			userActionRefresh.Text = objectListViewDefinition.ActionUIFullTextLoadList;

			userActionCreate.BriefText = documentDefinition.Actions["Create"].UIBriefText;
			userActionEdit.BriefText = documentDefinition.Actions["StartEdit"].UIBriefText;
			userActionDelete.BriefText = documentDefinition.Actions["Delete"].UIBriefText;
			userActionView.BriefText = "��������";
			userActionPick.BriefText = documentDefinition.Actions["Pick"].UIBriefText;
			userActionRefresh.BriefText = objectListViewDefinition.ActionUIBriefTextLoadList;

			userActionCreate.Enabled = (currentMode == DocumentGridViewMode.PickAndManagement ||
										currentMode == DocumentGridViewMode.Management);
			userActionEdit.Enabled = (currentMode == DocumentGridViewMode.PickAndManagement ||
									  currentMode == DocumentGridViewMode.Management);
			userActionDelete.Enabled = (currentMode == DocumentGridViewMode.PickAndManagement ||
										currentMode == DocumentGridViewMode.Management);
			userActionView.Enabled = true;

			setActionEnabledState();

			gridViewListSettingsCollection.Fix(accessAdapter, listNamespace, listName);
			currentGridViewListSettings = gridViewListSettingsCollection[listNamespace, listName];

			dataView.Table = null;
			initiateDataGridViewColumnSettings();
			foreach (ObjectAttributeDefinition attribute in objectListViewDefinition.Attributes)
			{
				addDataGridViewColumn(attribute);
			}

			setColumns();
			setSort();
			applyGridSettings();
			configured = true;
		}

		public override void LoadList()
		{
			if (!configured) ConfigureList();

			setActionActiveState();
			disableDataGridView();
			try
			{
				if ((objectListViewDefinition == null) && (accessAdapter != null))
					objectListViewDefinition =
						accessAdapter.MetadataStore.Get<DocumentListViewDefinition>(listNamespace, listName);
				if (objectListViewDefinition == null)
					return;

				if (!validateParameters())
					throw new Exception("�� ������ ��������� ��� �������� ������");

				clearDataGridViewDataSource();
				if (accessAdapter == null) throw new NullReferenceException("AccessAdapter is not set");
				dataTable = accessAdapter.ObjectFactory.LoadObjectListView(listNamespace, listName, prepareParameterSet());
				dataView.Table = dataTable;
				fill();
				enableDataGridView();
				setActionActiveState();
			}
			catch (Exception ex)
			{
				MessageBox.Show(Text + ":" + Environment.NewLine + ex.Message, "�������� ������", MessageBoxButtons.OK,
								MessageBoxIcon.Error);
			}

			if (ListLoad != null)
				ListLoad(this, EventArgs.Empty);
		}

		#endregion

		#region ����� ������

		private DocumentGridViewMode currentMode = DocumentGridViewMode.Management;

		[DefaultValue(DocumentGridViewMode.Management)]
		public DocumentGridViewMode CurrentMode
		{
			get { return currentMode; }
			set
			{
				if (currentMode != value)
				{
					currentMode = value;
					setActionEnabledState();
					setActionActiveState();
				}
			}
		}

		#endregion

		#region �������� � ���������

		#region �������� ������ ���������

		protected virtual void OnCreate()
		{
			if (!allowCreate)
				return;

			if (DocumentFromCreating == null)
				throw new Exception("�� ����� ���������� ������� �������� ����� �������");

			try
			{
				DocumentForm form;
				DocumentInstance instance =
					(DocumentInstance)accessAdapter.ObjectFactory.New<DocumentDefinition>(BasicObjectNamespace, BasicObjectName);
				DocumentFromCreating(instance.DocumentDefinition.Namespace,
									 instance.DocumentDefinition.Name, out form);
				form.AccessAdapter = accessAdapter;
				instance.Create();
				if (form.Show(instance, DocumentForm.Mode.Create, prepareParameterSet()) == DialogResult.OK)
					LoadList();
				setDataGridViewFocus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "�������� �������", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}


			if (DocumentCreate != null)
				DocumentCreate(this, EventArgs.Empty);
		}

		#endregion

		#region �������� ��������� ��� ��������������

		protected virtual void OnStartEdit()
		{
			if (!allowEdit)
				return;

			if (DocumentFromCreating == null)
				throw new Exception("�� ����� ���������� ������� �������� ����� �������");

			if ((currentMode != DocumentGridViewMode.Management) &&
				(currentMode != DocumentGridViewMode.PickAndManagement))
				return;

			try
			{
				DocumentForm form;
				DocumentInstance instance =
					(DocumentInstance)accessAdapter.ObjectFactory.New<DocumentDefinition>(BasicObjectNamespace, BasicObjectName);
				instance.Load(Convert.ToInt32(SelectedObjects[0]["ID"]));
				instance.StartEdit();
				DocumentFromCreating(instance.DocumentDefinition.Namespace, instance.DocumentDefinition.Name, out form);
				if (form == null) throw new Exception("�� ������� ������� ����� ��������������!");
				form.AccessAdapter = accessAdapter;
				if (form.Show(instance, DocumentForm.Mode.Update, prepareParameterSet()) == DialogResult.OK)
					LoadList();
				setDataGridViewFocus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "�������� ������ ��������� ��� ��������������", MessageBoxButtons.OK,
								MessageBoxIcon.Error);
			}


			if (DocumentEdit != null)
				DocumentEdit(this, EventArgs.Empty);
		}

		#endregion

		#region �������� �������

		protected virtual void OnDelete()
		{
			if (!allowDelete)
				return;

			if (SelectedItemsCount < 1)
				return;

			try
			{
				if (
					MessageBox.Show("�� �������?", "���������� ������� ����� �������!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
									MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				{
					foreach (AttributeValueSet selectedObject in SelectedObjects)
					{
						DocumentInstance instance =
							(DocumentInstance)accessAdapter.ObjectFactory.New<DocumentDefinition>(BasicObjectNamespace, BasicObjectName);
						instance.Load(Convert.ToInt32(selectedObject["ID"]));
						instance.Delete();
					}
					LoadList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "�������� �������", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			if (DocumentDelete != null)
				DocumentDelete(this, EventArgs.Empty);
		}

		#endregion

		#region �������� ��������� �������

		protected virtual void OnView()
		{
			if (!allowView)
				return;

			if (DocumentFromCreating == null)
				throw new Exception("�� ����� ���������� ������� �������� ����� �������");

			try
			{
				DocumentForm form;
				DocumentInstance instance =
					(DocumentInstance)accessAdapter.ObjectFactory.New<DocumentDefinition>(BasicObjectNamespace, BasicObjectName);
				instance.Load(Convert.ToInt32(SelectedObjects[0]["ID"]));
				DocumentFromCreating(instance.DocumentDefinition.Namespace,
									 instance.DocumentDefinition.Name, out form);
				form.AccessAdapter = accessAdapter;
				if (form.Show(instance, DocumentForm.Mode.View, prepareParameterSet()) == DialogResult.OK)
					LoadList();
				setDataGridViewFocus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "�������� ���������", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}


			if (DocumentView != null)
				DocumentView(this, EventArgs.Empty);
		}

		#endregion

		#region ����� �������

		protected virtual void OnPick()
		{
			if (ObjectPick != null)
				ObjectPick(this, EventArgs.Empty);
		}

		#endregion

		#region ���������� ���������

		protected virtual void OnCommit()
		{
			if (!allowCommit)
				return;

			if (SelectedItemsCount < 1)
				return;

			try
			{
				if (
					MessageBox.Show("�� �������?", "�������� ����� ��������!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
									MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				{
					foreach (AttributeValueSet selectedObject in SelectedObjects)
					{
						DocumentInstance instance =
							(DocumentInstance)accessAdapter.ObjectFactory.New<DocumentDefinition>(BasicObjectNamespace, BasicObjectName);
						instance.Load(Convert.ToInt32(selectedObject["ID"]));
						instance.Commit();
					}
					LoadList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "���������� ���������", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}


			if (DocumentCommit != null)
				DocumentCommit(this, EventArgs.Empty);
		}

		#endregion

		#region ������ ���������� ���������

		protected virtual void OnRollback()
		{
			if (!allowRollback)
				return;

			if (SelectedItemsCount < 1)
				return;

			try
			{
				if (
					MessageBox.Show("�� �������?", "���������� ���������� ����� ��������!", MessageBoxButtons.YesNo,
									MessageBoxIcon.Warning,
									MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				{
					foreach (AttributeValueSet selectedObject in SelectedObjects)
					{
						DocumentInstance instance =
							(DocumentInstance)accessAdapter.ObjectFactory.New<DocumentDefinition>(BasicObjectNamespace, BasicObjectName);
						instance.Load(Convert.ToInt32(selectedObject["ID"]));
						instance.Rollback();
					}
					LoadList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "������ ���������� ���������", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}


			if (DocumentRollback != null)
				DocumentRollback(this, EventArgs.Empty);
		}

		#endregion

		#endregion

		#region ������ �� �������� ���������� �����

		public delegate void DocumentFromCreatingDelegate(string documentTypeNamespace, string documentTypeName, out DocumentForm form);

		public event DocumentFromCreatingDelegate DocumentFromCreating;

		#endregion

		#region UI event handling

		private void MultiversionDocumentGridView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				e.Handled = true;
				if (currentMode == DocumentGridViewMode.Pick ||
					currentMode == DocumentGridViewMode.PickAndManagement)
				{
					userActionPick.Execute();
					return;
				}
				userActionView.Execute();
				return;
			}
			if ((e.KeyData == (Keys.Enter | Keys.Shift)) &&
				(currentMode == DocumentGridViewMode.Pick ||
				 currentMode == DocumentGridViewMode.PickAndManagement))
			{
				userActionView.Execute();
				return;
			}

			foreach (UserAction action in userActions)
			{
				if (action.Shortcut == e.KeyData)
				{
					action.Execute();
					e.SuppressKeyPress = false;
					return;
				}
			}
		}

		protected override void dataGridView_KeyDown(object sender, KeyEventArgs e)
		{
			MultiversionDocumentGridView_KeyDown(sender, e);
		}

		protected override void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;

			if (currentMode == DocumentGridViewMode.Pick ||
				currentMode == DocumentGridViewMode.PickAndManagement)
			{
				if (userActionPick.Enabled && userActionPick.Permitted && userActionPick.Active)
					userActionPick.Execute();
			}
			else if (currentMode == DocumentGridViewMode.Management ||
					 currentMode == DocumentGridViewMode.ReadOnly)
			{
				if (userActionView.Enabled && userActionView.Permitted && userActionView.Active)
					userActionView.Execute();
			}
		}

		#endregion
	}
}

// ReSharper restore UseObjectOrCollectionInitializer
// ReSharper restore SuggestUseVarKeywordEvident
// ReSharper disable SuggestUseVarKeywordEvident
// ReSharper disable UseObjectOrCollectionInitializer
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace JetBat.UI.DataControls.Grid
{
	internal class ToolbarRenderer : ToolStripProfessionalRenderer
	{
		private static readonly Bitmap BitmapOverflowButton;

		static ToolbarRenderer()
		{
			BitmapOverflowButton = Resource.Unfold_16;
			BitmapOverflowButton.MakeTransparent(Color.Magenta);
		}

		protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
		{
			Graphics g = e.Graphics;
			Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);

			Color gradientBegin = e.ToolStrip.BackColor; //Color.FromArgb(203, 225, 252);
			Color gradientEnd = e.ToolStrip.BackColor; //Color.FromArgb(125, 165, 224);

			ToolStripButton button = e.Item as ToolStripButton;
			if (button == null) return;

			if (button.Pressed || button.Checked)
			{
				gradientBegin = Color.FromArgb(254, 128, 62);
				gradientEnd = Color.FromArgb(255, 223, 154);
			}
			else if (button.Selected)
			{
				gradientBegin = Color.FromArgb(255, 255, 222);
				gradientEnd = Color.FromArgb(255, 203, 136);
			}

			using (Brush b = new LinearGradientBrush(bounds, gradientBegin, gradientEnd, LinearGradientMode.Vertical))
			{
				g.FillRectangle(b, bounds);
			}
		}

		protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			Graphics g = e.Graphics;
			Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);

			Color gradientBegin = e.ToolStrip.BackColor; //Color.FromArgb(203, 225, 252);
			Color gradientEnd = e.ToolStrip.BackColor; //Color.FromArgb(125, 165, 224);

			ToolStripDropDownItem button = e.Item as ToolStripDropDownItem;
			if (button == null) return;

			if (button.Pressed)
			{
				gradientBegin = Color.FromArgb(254, 128, 62);
				gradientEnd = Color.FromArgb(255, 223, 154);
			}
			else if (button.Selected)
			{
				gradientBegin = Color.FromArgb(255, 255, 222);
				gradientEnd = Color.FromArgb(255, 203, 136);
			}

			using (Brush b = new LinearGradientBrush(bounds, gradientBegin, gradientEnd, LinearGradientMode.Vertical))
			{
				g.FillRectangle(b, bounds);
			}

			g.DrawImageUnscaled(BitmapOverflowButton, 0, (e.Item.Height - 16)/2 - 1);
		}
	}
}

// ReSharper restore UseObjectOrCollectionInitializer
// ReSharper restore SuggestUseVarKeywordEvident
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace CollapsiblePanel
{
	[ComVisible(true)]
	[DefaultEvent("Paint")]
	[DefaultProperty("Collapsed")]
	[Designer(typeof(CollapsiblePanelDesigner))]
	[DesignerCategory("Containers")]
	public partial class CollapsiblePanel : UserControl
	{
		private const int TOTAL_COLLAPSED_HEIGHT = 34;
		private const int CONTENT_PANEL_COLLAPSED_HEIGHT = 17;

		private int initialHeight; // expanded height (same value as this.Height)
		private int initialContentPanelHeight;
		private Point initialLocation;

		[Category("Appearance")]
		[Browsable(true)]
		[DefaultValue(CollapseSymbol.PlusMinus)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public CollapseSymbol CollapseSymbol { get; set; }

		/// <summary>
		/// Occurs when user changes the type of collapse symbol of the panel.
		/// </summary>
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Description("Occurs when user changes the type of collapse symbol of the collapsible panel.")]
		public event EventHandler CollapseSymbolChanged;

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Panel ContentArea { get { return contentPanel; } }

		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Description("Gets or sets the text displayed in the panel's header")]
		public override string Text
		{
			get { return textLabel.Text; }
			set { textLabel.Text = value; }
		}

		protected override bool ShowFocusCues { get; }

		/// <summary>
		/// Occurs when user collapses or expands the panel.
		/// </summary>
		[Category("Behavior")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Description("Occurs when user collapses or expands the panel.")]
		public event EventHandler CollapsedChanged;

		/// <summary>
		/// Indicates whether the panel is collapsed or not.
		/// </summary>
		[Category("Behavior")]
		[Browsable(true)]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Description("Indicates whether the panel is in collapsed state.")]
		public bool Collapsed { get; set; }

		public CollapsiblePanel()
		{
			InitializeComponent();

			initialHeight = this.Height;
			initialContentPanelHeight = contentPanel.Height;
			Paint += CollapsiblePanel_Paint;
			Collapsed = false;
			CollapseSymbol = CollapseSymbol.PlusMinus;
			CollapseSymbolChanged += CollapsiblePanel_CollapseSymbolChanged;

			MinimumSize = new Size(textLabel.Width + 90, TOTAL_COLLAPSED_HEIGHT);

			textLabel.AutoSize = true;
			textLabel.TextAlign = ContentAlignment.MiddleLeft;
			TextChanged += CollapsiblePanel_TextChanged;

			Text = this.Name;
			//Text = "collapsiblePanel1";
			ResetTextLocation();

			textLabel.MaximumSize = new Size(header.Width - 20, textLabel.Height);

			Resize += CollapsiblePanel_Resize;
			CollapsedChanged += CollapsiblePanel_CollapsedChanged;

			ControlRemoved += CollapsiblePanel_ControlRemoved;
		}

		private void CollapsiblePanel_CollapseSymbolChanged(object sender, EventArgs e)
		{
			switch(CollapseSymbol)
			{
				case CollapseSymbol.PlusMinus:
					if(Collapsed)
						collapseIndicator.Text = "+";
					else
						collapseIndicator.Text = "−";
					break;
				case CollapseSymbol.Triangle:
					if(Collapsed)
						collapseIndicator.Text = "▼";
					else
						collapseIndicator.Text = "▲";
					break;
			}
			collapseIndicator.Invalidate();
		}

		private void CollapsiblePanel_ControlRemoved(object sender, ControlEventArgs e)
		{
			if(sender is ContainerControl)
			{
				ContainerControl containerControl = sender as ContainerControl;
				containerControl.Controls.Remove(e.Control);
			}
		}

		private void CollapsiblePanel_Paint(object sender, PaintEventArgs e)
		{
			CollapsiblePanel_CollapseSymbolChanged(this, e);
			ResetTextLocation();
		}

		private int GetHalfWidth(int width)
		{
			return Math.DivRem(width, 2, out _);
		}

		private void ResetTextLocation()
		{
			textLabel.Location = new Point(GetHalfWidth(Width) - GetHalfWidth(textLabel.Width), textLabel.Location.Y);
		}

		private void CollapsiblePanel_CollapsedChanged(object sender, EventArgs e)
		{
			this.Location = initialLocation;
			if(Collapsed)
				contentPanel.AutoScroll = false;
			else
				contentPanel.AutoScroll = true;
			CollapseSymbolChanged(this, e);
		}

		private void CollapsiblePanel_TextChanged(object sender, EventArgs e)
		{
			if(Text.Length > 24)
				Text = Text.Remove(23);
			textLabel.Text = Text;
			MinimumSize = new Size(textLabel.Width + 90, TOTAL_COLLAPSED_HEIGHT);
			ResetTextLocation();
		}

		private void CollapsiblePanel_Resize(object sender, EventArgs e)
		{
			initialLocation = this.Location;
			//header.Width = contentPanel.Width - 8;
			header.Width = Width - 14;
			MinimumSize = new Size(textLabel.Width + 90, TOTAL_COLLAPSED_HEIGHT);
			ResetTextLocation();
		}

		private void header_Click(object sender, EventArgs e)
		{
			int heightBefore = initialHeight;
			if(Collapsed == true)
			{
				this.Height = initialHeight;
				contentPanel.Height = initialContentPanelHeight;
				collapseIndicator.Text = CollapseSymbol == CollapseSymbol.PlusMinus ? "−" : "▼";
				Collapsed = false;
			}
			else
			{
				this.Height = TOTAL_COLLAPSED_HEIGHT;
				contentPanel.Height = CONTENT_PANEL_COLLAPSED_HEIGHT;
				collapseIndicator.Text = CollapseSymbol == CollapseSymbol.PlusMinus ? "+" : "▲";
				Collapsed = true;
			}
			CollapsiblePanel_CollapsedChanged(this, e);
		}
	}

	public enum CollapseSymbol
	{
		PlusMinus,
		Triangle
	}

	class CollapsiblePanelDesigner : ControlDesigner
	{
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			var uc = (CollapsiblePanel)component;
			EnableDesignMode(uc.Controls["contentPanel"], "ContentArea");
		}
	}
}

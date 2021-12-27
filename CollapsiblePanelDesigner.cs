using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Collections;

namespace CollapsiblePanel
{
	internal sealed class CollapsiblePanelDesigner : ParentControlDesigner
	{
		IDesignerHost designerHost;

		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			base.AutoResizeHandles = true;
			base.EnableDesignMode(((CollapsiblePanel)component).ContentPanel, "ContentPanel");
			designerHost = (IDesignerHost)component.Site.GetService(typeof(IDesignerHost));
		}

		public override bool CanParent(Control control)
		{
			return false;
		}

		public override ICollection AssociatedComponents
		{
			get
			{
				List<Control> list = new List<Control>();
				foreach(Control ctrl in ((CollapsiblePanel)Control).ContentPanel.Controls)
				{
					list.Add(ctrl);
				}
				return list;
			}
		}

		protected override Control GetParentForComponent(IComponent component)
		{
			return ((CollapsiblePanel)Control).ContentPanel;
		}

		public override int NumberOfInternalControlDesigners()
		{
			return 1;
		}

		public override ControlDesigner InternalControlDesigner(int internalControlIndex)
		{
			Control panel = ((CollapsiblePanel)Control).ContentPanel;
			switch(internalControlIndex)
			{
				case 0:
					return this.designerHost.GetDesigner(panel) as ControlDesigner;
				default:
					return null;
			}
		}

		protected override IComponent[] CreateToolCore(System.Drawing.Design.ToolboxItem tool, int x, int y, int width, int height, bool hasLocation, bool hasSize)
		{
			ParentControlDesigner panelDesigner = this.designerHost.GetDesigner(((CollapsiblePanel)Control).ContentPanel) as ParentControlDesigner;
			InvokeCreateTool(panelDesigner, tool);
			return null;
		}
	}
}

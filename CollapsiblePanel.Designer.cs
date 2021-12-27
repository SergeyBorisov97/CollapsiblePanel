using System.Windows.Forms;

namespace CollapsiblePanel
{
	partial class CollapsiblePanel : UserControl
	{
		/// <summary> 
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.contentPanel = new System.Windows.Forms.Panel();
			this.collapseIndicator = new System.Windows.Forms.Label();
			this.header = new System.Windows.Forms.Panel();
			this.textLabel = new System.Windows.Forms.Label();
			this.header.SuspendLayout();
			this.SuspendLayout();
			// 
			// contentPanel
			// 
			this.contentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.contentPanel.AutoScroll = true;
			this.contentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.contentPanel.Location = new System.Drawing.Point(3, 14);
			this.contentPanel.Name = "contentPanel";
			this.contentPanel.Padding = new System.Windows.Forms.Padding(3, 12, 3, 3);
			this.contentPanel.Size = new System.Drawing.Size(227, 227);
			this.contentPanel.TabIndex = 0;
			// 
			// collapseIndicator
			// 
			this.collapseIndicator.AutoSize = true;
			this.collapseIndicator.BackColor = System.Drawing.SystemColors.Control;
			this.collapseIndicator.Location = new System.Drawing.Point(3, 0);
			this.collapseIndicator.Name = "collapseIndicator";
			this.collapseIndicator.Size = new System.Drawing.Size(16, 17);
			this.collapseIndicator.TabIndex = 2;
			this.collapseIndicator.Text = "−";
			this.collapseIndicator.Click += new System.EventHandler(this.header_Click);
			// 
			// header
			//
			this.header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.header.Controls.Add(this.textLabel);
			this.header.Controls.Add(this.collapseIndicator);
			this.header.Location = new System.Drawing.Point(7, 5);
			this.header.Name = "header";
			this.header.Size = new System.Drawing.Size(219, 18);
			this.header.TabIndex = 3;
			this.header.Click += new System.EventHandler(this.header_Click);
			// 
			// textLabel
			// 
			this.textLabel.AutoSize = true;
			this.textLabel.Location = new System.Drawing.Point(76, -1);
			this.textLabel.Name = "textLabel";
			this.textLabel.Size = new System.Drawing.Size(65, 17);
			this.textLabel.TabIndex = 3;
			this.textLabel.Text = "textLabel";
			this.textLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.textLabel.Click += new System.EventHandler(this.header_Click);
			// 
			// CollapsiblePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.header);
			this.Controls.Add(this.contentPanel);
			this.Size = new System.Drawing.Size(233, 244);
			this.header.ResumeLayout(false);
			this.header.PerformLayout();
			this.ResumeLayout(false);

		}

		private System.Windows.Forms.Panel contentPanel;
		private System.Windows.Forms.Label collapseIndicator;
		private System.Windows.Forms.Panel header;
		private System.Windows.Forms.Label textLabel;

		#endregion
	}
}

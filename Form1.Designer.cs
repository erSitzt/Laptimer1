
namespace Laptimer1
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBox_laptimeService = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.objectListView2 = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.dataListView1 = new BrightIdeasSoftware.DataListView();
            this.rfidReaderTextBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rfidConnectButton = new System.Windows.Forms.Button();
            this.readerStatusLabel = new System.Windows.Forms.Label();
            this.readerInventoryActiveCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tagRegistierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataListView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 20;
            this.listBox2.Location = new System.Drawing.Point(12, 799);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(1532, 304);
            this.listBox2.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1236);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1556, 32);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(179, 25);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // textBox_laptimeService
            // 
            this.textBox_laptimeService.Location = new System.Drawing.Point(134, 31);
            this.textBox_laptimeService.Name = "textBox_laptimeService";
            this.textBox_laptimeService.Size = new System.Drawing.Size(326, 26);
            this.textBox_laptimeService.TabIndex = 5;
            this.textBox_laptimeService.Text = "http://192.168.178.43:3000";
            this.textBox_laptimeService.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Laptime Service";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(134, 66);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(78, 24);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Active";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // objectListView2
            // 
            this.objectListView2.AllColumns.Add(this.olvColumn4);
            this.objectListView2.AllColumns.Add(this.olvColumn5);
            this.objectListView2.AllColumns.Add(this.olvColumn6);
            this.objectListView2.AllColumns.Add(this.olvColumn7);
            this.objectListView2.CellEditUseWholeCell = false;
            this.objectListView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn4,
            this.olvColumn5,
            this.olvColumn6,
            this.olvColumn7});
            this.objectListView2.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView2.HideSelection = false;
            this.objectListView2.Location = new System.Drawing.Point(447, 12);
            this.objectListView2.Name = "objectListView2";
            this.objectListView2.Size = new System.Drawing.Size(1097, 781);
            this.objectListView2.TabIndex = 9;
            this.objectListView2.UseCompatibleStateImageBehavior = false;
            this.objectListView2.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "tagId";
            this.olvColumn4.Groupable = false;
            this.olvColumn4.Text = "TagID";
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "start";
            this.olvColumn5.Groupable = false;
            this.olvColumn5.Text = "Startzeit";
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "end";
            this.olvColumn6.Groupable = false;
            this.olvColumn6.Text = "Endzeit";
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "laptime";
            this.olvColumn7.Groupable = false;
            this.olvColumn7.Text = "Rundenzeit";
            // 
            // dataListView1
            // 
            this.dataListView1.CellEditUseWholeCell = false;
            this.dataListView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataListView1.DataSource = null;
            this.dataListView1.HasCollapsibleGroups = false;
            this.dataListView1.HideSelection = false;
            this.dataListView1.Location = new System.Drawing.Point(17, 13);
            this.dataListView1.Name = "dataListView1";
            this.dataListView1.ShowGroups = false;
            this.dataListView1.Size = new System.Drawing.Size(424, 780);
            this.dataListView1.TabIndex = 10;
            this.dataListView1.UseCompatibleStateImageBehavior = false;
            this.dataListView1.View = System.Windows.Forms.View.Details;
            this.dataListView1.SelectedIndexChanged += new System.EventHandler(this.dataListView1_SelectedIndexChanged);
            // 
            // rfidReaderTextBox1
            // 
            this.rfidReaderTextBox1.Location = new System.Drawing.Point(99, 31);
            this.rfidReaderTextBox1.Name = "rfidReaderTextBox1";
            this.rfidReaderTextBox1.Size = new System.Drawing.Size(149, 26);
            this.rfidReaderTextBox1.TabIndex = 11;
            this.rfidReaderTextBox1.Text = "192.168.178.100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "IP Address";
            // 
            // rfidConnectButton
            // 
            this.rfidConnectButton.Location = new System.Drawing.Point(254, 31);
            this.rfidConnectButton.Name = "rfidConnectButton";
            this.rfidConnectButton.Size = new System.Drawing.Size(109, 27);
            this.rfidConnectButton.TabIndex = 13;
            this.rfidConnectButton.Text = "Connect";
            this.rfidConnectButton.UseVisualStyleBackColor = true;
            this.rfidConnectButton.Click += new System.EventHandler(this.rfidConnectButton_Click);
            // 
            // readerStatusLabel
            // 
            this.readerStatusLabel.AutoSize = true;
            this.readerStatusLabel.Location = new System.Drawing.Point(6, 64);
            this.readerStatusLabel.Name = "readerStatusLabel";
            this.readerStatusLabel.Size = new System.Drawing.Size(48, 20);
            this.readerStatusLabel.TabIndex = 14;
            this.readerStatusLabel.Text = "Read";
            // 
            // readerInventoryActiveCheckBox
            // 
            this.readerInventoryActiveCheckBox.AutoSize = true;
            this.readerInventoryActiveCheckBox.Location = new System.Drawing.Point(99, 63);
            this.readerInventoryActiveCheckBox.Name = "readerInventoryActiveCheckBox";
            this.readerInventoryActiveCheckBox.Size = new System.Drawing.Size(78, 24);
            this.readerInventoryActiveCheckBox.TabIndex = 15;
            this.readerInventoryActiveCheckBox.Text = "Active";
            this.readerInventoryActiveCheckBox.UseVisualStyleBackColor = true;
            this.readerInventoryActiveCheckBox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.readerInventoryActiveCheckBox);
            this.groupBox1.Controls.Add(this.rfidReaderTextBox1);
            this.groupBox1.Controls.Add(this.readerStatusLabel);
            this.groupBox1.Controls.Add(this.rfidConnectButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 1118);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(698, 103);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RFID Reader";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBox_laptimeService);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Location = new System.Drawing.Point(817, 1118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(727, 100);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "OpenLapTime Service";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Send Data";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tagRegistierenToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(199, 36);
            // 
            // tagRegistierenToolStripMenuItem
            // 
            this.tagRegistierenToolStripMenuItem.Name = "tagRegistierenToolStripMenuItem";
            this.tagRegistierenToolStripMenuItem.Size = new System.Drawing.Size(198, 32);
            this.tagRegistierenToolStripMenuItem.Text = "Tag registieren";
            this.tagRegistierenToolStripMenuItem.Click += new System.EventHandler(this.tagRegistierenToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1556, 1268);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataListView1);
            this.Controls.Add(this.objectListView2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataListView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox textBox_laptimeService;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private BrightIdeasSoftware.ObjectListView objectListView2;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private BrightIdeasSoftware.DataListView dataListView1;
        private System.Windows.Forms.TextBox rfidReaderTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button rfidConnectButton;
        private System.Windows.Forms.Label readerStatusLabel;
        private System.Windows.Forms.CheckBox readerInventoryActiveCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tagRegistierenToolStripMenuItem;
    }
}


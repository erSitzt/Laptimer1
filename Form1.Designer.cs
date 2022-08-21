
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tagRegistierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rfidReaderTextBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rfidConnectButton = new System.Windows.Forms.Button();
            this.readerStatusLabel = new System.Windows.Forms.Label();
            this.readerInventoryActiveCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.tagid = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lastseentime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.resetDatabaseButton = new System.Windows.Forms.Button();
            this.fastObjectListView1 = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).BeginInit();
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 1276);
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
            this.objectListView2.Location = new System.Drawing.Point(631, 12);
            this.objectListView2.Name = "objectListView2";
            this.objectListView2.Size = new System.Drawing.Size(913, 665);
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
            this.olvColumn5.AspectName = "started";
            this.olvColumn5.Groupable = false;
            this.olvColumn5.Text = "Startzeit";
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "finished";
            this.olvColumn6.Groupable = false;
            this.olvColumn6.Text = "Endzeit";
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "laptime";
            this.olvColumn7.Groupable = false;
            this.olvColumn7.Text = "Rundenzeit";
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
            this.rfidConnectButton.Location = new System.Drawing.Point(254, 25);
            this.rfidConnectButton.Name = "rfidConnectButton";
            this.rfidConnectButton.Size = new System.Drawing.Size(122, 41);
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
            this.groupBox1.Size = new System.Drawing.Size(407, 155);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RFID Reader";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBox_laptimeService);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Location = new System.Drawing.Point(817, 1118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(727, 155);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "OpenLapTime Service";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(466, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 41);
            this.button1.TabIndex = 10;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(134, 97);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(326, 26);
            this.textBox1.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "API-Key";
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
            // objectListView1
            // 
            this.objectListView1.AllColumns.Add(this.tagid);
            this.objectListView1.AllColumns.Add(this.lastseentime);
            this.objectListView1.CellEditUseWholeCell = false;
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tagid,
            this.lastseentime});
            this.objectListView1.ContextMenuStrip = this.contextMenuStrip1;
            this.objectListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView1.HideSelection = false;
            this.objectListView1.Location = new System.Drawing.Point(22, 12);
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.ShowGroups = false;
            this.objectListView1.Size = new System.Drawing.Size(603, 781);
            this.objectListView1.TabIndex = 18;
            this.objectListView1.UseCellFormatEvents = true;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            this.objectListView1.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.objectListView1_FormatRow);
            this.objectListView1.ItemsChanged += new System.EventHandler<BrightIdeasSoftware.ItemsChangedEventArgs>(this.objectListView1_ItemsChanged);
            this.objectListView1.SelectedIndexChanged += new System.EventHandler(this.objectListView1_SelectedIndexChanged);
            // 
            // tagid
            // 
            this.tagid.AspectName = "TagId";
            this.tagid.Groupable = false;
            this.tagid.Text = "TagId";
            // 
            // lastseentime
            // 
            this.lastseentime.AspectName = "TagSeenTime";
            this.lastseentime.Groupable = false;
            this.lastseentime.Text = "Last seen";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.resetDatabaseButton);
            this.groupBox3.Location = new System.Drawing.Point(425, 1118);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(386, 155);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Database";
            // 
            // resetDatabaseButton
            // 
            this.resetDatabaseButton.Location = new System.Drawing.Point(6, 25);
            this.resetDatabaseButton.Name = "resetDatabaseButton";
            this.resetDatabaseButton.Size = new System.Drawing.Size(170, 41);
            this.resetDatabaseButton.TabIndex = 0;
            this.resetDatabaseButton.Text = "Reset Database";
            this.resetDatabaseButton.UseVisualStyleBackColor = true;
            this.resetDatabaseButton.Click += new System.EventHandler(this.resetDatabaseButton_Click);
            // 
            // fastObjectListView1
            // 
            this.fastObjectListView1.AllColumns.Add(this.olvColumn1);
            this.fastObjectListView1.AllColumns.Add(this.olvColumn2);
            this.fastObjectListView1.CellEditUseWholeCell = false;
            this.fastObjectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this.fastObjectListView1.HideSelection = false;
            this.fastObjectListView1.Location = new System.Drawing.Point(632, 683);
            this.fastObjectListView1.Name = "fastObjectListView1";
            this.fastObjectListView1.ShowGroups = false;
            this.fastObjectListView1.Size = new System.Drawing.Size(912, 110);
            this.fastObjectListView1.TabIndex = 20;
            this.fastObjectListView1.UseCompatibleStateImageBehavior = false;
            this.fastObjectListView1.View = System.Windows.Forms.View.Details;
            this.fastObjectListView1.VirtualMode = true;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Id";
            this.olvColumn1.Text = "TagID";
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "started";
            this.olvColumn2.Text = "Startzeit";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1556, 1308);
            this.Controls.Add(this.fastObjectListView1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.objectListView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.objectListView2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listBox2);
            this.Name = "Form1";
            this.Text = "OpenLapTime";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).EndInit();
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private BrightIdeasSoftware.ObjectListView objectListView1;
        private BrightIdeasSoftware.OLVColumn tagid;
        private BrightIdeasSoftware.OLVColumn lastseentime;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button resetDatabaseButton;
        private System.Windows.Forms.Button button1;
        private BrightIdeasSoftware.FastObjectListView fastObjectListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
    }
}


using System;

namespace OidManager
{
    partial class OidManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tv_oidTree = new System.Windows.Forms.TreeView();
            this.tb_oid = new System.Windows.Forms.TextBox();
            this.lbl_oid = new System.Windows.Forms.Label();
            this.tb_private = new System.Windows.Forms.TextBox();
            this.tb_public = new System.Windows.Forms.TextBox();
            this.tb_authority = new System.Windows.Forms.TextBox();
            this.lbl_privateData = new System.Windows.Forms.Label();
            this.lbl_publicData = new System.Windows.Forms.Label();
            this.lbl_authority = new System.Windows.Forms.Label();
            this.btn_openfile = new System.Windows.Forms.Button();
            this.tb_friendlyName = new System.Windows.Forms.TextBox();
            this.lbl_friendlyName = new System.Windows.Forms.Label();
            this.cms_nodeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lbl_relId = new System.Windows.Forms.Label();
            this.nud_relId = new System.Windows.Forms.NumericUpDown();
            this.btn_saveEntry = new System.Windows.Forms.Button();
            this.btn_saveXml = new System.Windows.Forms.Button();
            this.btn_newTree = new System.Windows.Forms.Button();
            this.btn_undoChanges = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nud_relId)).BeginInit();
            this.SuspendLayout();
            // 
            // tv_oidTree
            // 
            this.tv_oidTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tv_oidTree.Location = new System.Drawing.Point(13, 13);
            this.tv_oidTree.Name = "tv_oidTree";
            this.tv_oidTree.PathSeparator = " / ";
            this.tv_oidTree.Size = new System.Drawing.Size(524, 704);
            this.tv_oidTree.Sorted = true;
            this.tv_oidTree.TabIndex = 0;
            this.tv_oidTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tv_oidTree_BeforeSelect);
            this.tv_oidTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_oidTree_AfterSelect);
            this.tv_oidTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_oidTree_NodeMouseClick);
            // 
            // tb_oid
            // 
            this.tb_oid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_oid.Location = new System.Drawing.Point(543, 29);
            this.tb_oid.Name = "tb_oid";
            this.tb_oid.ReadOnly = true;
            this.tb_oid.Size = new System.Drawing.Size(346, 20);
            this.tb_oid.TabIndex = 1;
            // 
            // lbl_oid
            // 
            this.lbl_oid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_oid.AutoSize = true;
            this.lbl_oid.Location = new System.Drawing.Point(543, 13);
            this.lbl_oid.Name = "lbl_oid";
            this.lbl_oid.Size = new System.Drawing.Size(23, 13);
            this.lbl_oid.TabIndex = 2;
            this.lbl_oid.Text = "Oid";
            // 
            // tb_descr
            // 
            this.tb_private.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_private.Location = new System.Drawing.Point(543, 108);
            this.tb_private.Multiline = true;
            this.tb_private.Name = "tb_descr";
            this.tb_private.Size = new System.Drawing.Size(453, 61);
            this.tb_private.TabIndex = 3;
            this.tb_private.TextChanged += new System.EventHandler(this.tb_descr_TextChanged);
            // 
            // tb_info
            // 
            this.tb_public.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_public.Location = new System.Drawing.Point(543, 188);
            this.tb_public.Multiline = true;
            this.tb_public.Name = "tb_info";
            this.tb_public.Size = new System.Drawing.Size(453, 61);
            this.tb_public.TabIndex = 4;
            this.tb_public.TextChanged += new System.EventHandler(this.tb_info_TextChanged);
            // 
            // tb_authority
            // 
            this.tb_authority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_authority.Location = new System.Drawing.Point(543, 268);
            this.tb_authority.Multiline = true;
            this.tb_authority.Name = "tb_authority";
            this.tb_authority.Size = new System.Drawing.Size(453, 61);
            this.tb_authority.TabIndex = 5;
            this.tb_authority.TextChanged += new System.EventHandler(this.tb_authority_TextChanged);
            // 
            // lbl_privateData
            // 
            this.lbl_privateData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_privateData.AutoSize = true;
            this.lbl_privateData.Location = new System.Drawing.Point(543, 92);
            this.lbl_privateData.Name = "lbl_privateData";
            this.lbl_privateData.Size = new System.Drawing.Size(64, 13);
            this.lbl_privateData.TabIndex = 2;
            this.lbl_privateData.Text = "Private data";
            // 
            // lbl_publicData
            // 
            this.lbl_publicData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_publicData.AutoSize = true;
            this.lbl_publicData.Location = new System.Drawing.Point(543, 172);
            this.lbl_publicData.Name = "lbl_publicData";
            this.lbl_publicData.Size = new System.Drawing.Size(60, 13);
            this.lbl_publicData.TabIndex = 2;
            this.lbl_publicData.Text = "Public data";
            // 
            // lbl_authority
            // 
            this.lbl_authority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_authority.AutoSize = true;
            this.lbl_authority.Location = new System.Drawing.Point(543, 252);
            this.lbl_authority.Name = "lbl_authority";
            this.lbl_authority.Size = new System.Drawing.Size(48, 13);
            this.lbl_authority.TabIndex = 2;
            this.lbl_authority.Text = "Authority";
            // 
            // btn_openfile
            // 
            this.btn_openfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_openfile.Location = new System.Drawing.Point(920, 694);
            this.btn_openfile.Name = "btn_openfile";
            this.btn_openfile.Size = new System.Drawing.Size(75, 23);
            this.btn_openfile.TabIndex = 10;
            this.btn_openfile.Text = "Open file";
            this.btn_openfile.UseVisualStyleBackColor = true;
            this.btn_openfile.Click += new System.EventHandler(this.btn_openfile_Click);
            // 
            // tb_friendlyName
            // 
            this.tb_friendlyName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_friendlyName.Location = new System.Drawing.Point(543, 68);
            this.tb_friendlyName.Name = "tb_friendlyName";
            this.tb_friendlyName.Size = new System.Drawing.Size(453, 20);
            this.tb_friendlyName.TabIndex = 2;
            this.tb_friendlyName.TextChanged += new System.EventHandler(this.tb_friendlyName_TextChanged);
            // 
            // lbl_friendlyName
            // 
            this.lbl_friendlyName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_friendlyName.AutoSize = true;
            this.lbl_friendlyName.Location = new System.Drawing.Point(543, 52);
            this.lbl_friendlyName.Name = "lbl_friendlyName";
            this.lbl_friendlyName.Size = new System.Drawing.Size(72, 13);
            this.lbl_friendlyName.TabIndex = 2;
            this.lbl_friendlyName.Text = "Friendly name";
            // 
            // cms_nodeMenu
            // 
            this.cms_nodeMenu.Name = "cms_nodeMenu";
            this.cms_nodeMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // lbl_relId
            // 
            this.lbl_relId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_relId.AutoSize = true;
            this.lbl_relId.Location = new System.Drawing.Point(892, 13);
            this.lbl_relId.Name = "lbl_relId";
            this.lbl_relId.Size = new System.Drawing.Size(60, 13);
            this.lbl_relId.TabIndex = 2;
            this.lbl_relId.Text = "Relative ID";
            // 
            // nud_relId
            // 
            this.nud_relId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nud_relId.Location = new System.Drawing.Point(896, 29);
            this.nud_relId.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nud_relId.Name = "nud_relId";
            this.nud_relId.Size = new System.Drawing.Size(100, 20);
            this.nud_relId.TabIndex = 1;
            this.nud_relId.ValueChanged += new System.EventHandler(this.nud_relId_ValueChanged);
            // 
            // btn_saveEntry
            // 
            this.btn_saveEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_saveEntry.Enabled = false;
            this.btn_saveEntry.Location = new System.Drawing.Point(543, 336);
            this.btn_saveEntry.Name = "btn_saveEntry";
            this.btn_saveEntry.Size = new System.Drawing.Size(75, 23);
            this.btn_saveEntry.TabIndex = 6;
            this.btn_saveEntry.Text = "Save entry";
            this.btn_saveEntry.UseVisualStyleBackColor = true;
            this.btn_saveEntry.Click += new System.EventHandler(this.btn_saveEntry_Click);
            // 
            // btn_saveXml
            // 
            this.btn_saveXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_saveXml.Location = new System.Drawing.Point(839, 694);
            this.btn_saveXml.Name = "btn_saveXml";
            this.btn_saveXml.Size = new System.Drawing.Size(75, 23);
            this.btn_saveXml.TabIndex = 9;
            this.btn_saveXml.Text = "Save";
            this.btn_saveXml.UseVisualStyleBackColor = true;
            this.btn_saveXml.Click += new System.EventHandler(this.btn_saveXml_Click);
            // 
            // btn_newTree
            // 
            this.btn_newTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_newTree.Location = new System.Drawing.Point(546, 694);
            this.btn_newTree.Name = "btn_newTree";
            this.btn_newTree.Size = new System.Drawing.Size(75, 23);
            this.btn_newTree.TabIndex = 8;
            this.btn_newTree.Text = "New tree";
            this.btn_newTree.UseVisualStyleBackColor = true;
            this.btn_newTree.Click += new System.EventHandler(this.btn_newTree_Click);
            // 
            // btn_undoChanges
            // 
            this.btn_undoChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_undoChanges.AutoSize = true;
            this.btn_undoChanges.Location = new System.Drawing.Point(625, 336);
            this.btn_undoChanges.Name = "btn_undoChanges";
            this.btn_undoChanges.Size = new System.Drawing.Size(91, 23);
            this.btn_undoChanges.TabIndex = 7;
            this.btn_undoChanges.Text = "Undo changes";
            this.btn_undoChanges.UseVisualStyleBackColor = true;
            this.btn_undoChanges.Click += new System.EventHandler(this.btn_undoChanges_Click);
            // 
            // OidManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.btn_undoChanges);
            this.Controls.Add(this.btn_saveEntry);
            this.Controls.Add(this.nud_relId);
            this.Controls.Add(this.tb_friendlyName);
            this.Controls.Add(this.btn_newTree);
            this.Controls.Add(this.btn_saveXml);
            this.Controls.Add(this.btn_openfile);
            this.Controls.Add(this.tb_authority);
            this.Controls.Add(this.tb_public);
            this.Controls.Add(this.tb_private);
            this.Controls.Add(this.lbl_authority);
            this.Controls.Add(this.lbl_publicData);
            this.Controls.Add(this.lbl_privateData);
            this.Controls.Add(this.lbl_friendlyName);
            this.Controls.Add(this.lbl_relId);
            this.Controls.Add(this.lbl_oid);
            this.Controls.Add(this.tb_oid);
            this.Controls.Add(this.tv_oidTree);
            this.Name = "OidManagerForm";
            this.Text = "OID Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OidManagerForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nud_relId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tv_oidTree;
        private System.Windows.Forms.TextBox tb_oid;
        private System.Windows.Forms.Label lbl_oid;
        private System.Windows.Forms.TextBox tb_private;
        private System.Windows.Forms.TextBox tb_public;
        private System.Windows.Forms.TextBox tb_authority;
        private System.Windows.Forms.Label lbl_privateData;
        private System.Windows.Forms.Label lbl_publicData;
        private System.Windows.Forms.Label lbl_authority;
        private System.Windows.Forms.Button btn_openfile;
        private System.Windows.Forms.TextBox tb_friendlyName;
        private System.Windows.Forms.Label lbl_friendlyName;
        private System.Windows.Forms.ContextMenuStrip cms_nodeMenu;
        private System.Windows.Forms.Label lbl_relId;
        private System.Windows.Forms.NumericUpDown nud_relId;
        private System.Windows.Forms.Button btn_saveEntry;
        private System.Windows.Forms.Button btn_saveXml;
        private System.Windows.Forms.Button btn_newTree;
        private System.Windows.Forms.Button btn_undoChanges;
    }
}


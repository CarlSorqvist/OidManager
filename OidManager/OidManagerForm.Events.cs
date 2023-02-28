using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Linq;

namespace OidManager
{
    public partial class OidManagerForm
    {
        private void OidManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (MessageBox.Show("Are you sure you want to exit? Any changes made to the current tree will be lost.", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No);
        }
        private void btn_openfile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = FILE_EXTENSION_FILTER;
                dialog.Title = "Open repository file";
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    using (var stream = dialog.OpenFile())
                    {
                        ReadXml(stream);
                    }
                }
            }
        }
        private void tv_oidTree_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            var tv = sender as TreeView;
            var currentNode = tv.SelectedNode;
            var nodeData = GetNodeData(currentNode);
            if (nodeData == null || currentNode.Level == 0)
                return;
            if (nodeData.IsNewNode)
            {
                var result = MessageBox.Show("Leaving the new node before it is saved will delete it. Are you sure?", "Confirm node change", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                bool cancel = result == DialogResult.Cancel;
                e.Cancel = cancel;
                if (!cancel)
                {
                    currentNode.Remove();
                    btn_saveEntry.Enabled = false;
                }
                return;
            }
            if (!IsEqual(currentNode))
            {
                var result = MessageBox.Show("Your changes will be lost if you select another node. Are you sure?", "Confirm node change", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                e.Cancel = (result == DialogResult.Cancel);
                return;
            }
        }
        private void tv_oidTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var tv = sender as TreeView;
            if (e.Node.Level == 0)
            {
                ClearFields();
                btn_saveEntry.Enabled = false;
                return;
            }
            var data = GetNodeData(e.Node);
            m_nodeLoading = true;
            if (data != null && !data.IsNewNode)
            {
                SetFullOid(e.Node);

                nud_relId.Value = Int32.Parse(data.Identifier);
                tb_friendlyName.Text = data.Name;
                tb_private.Text = data.PrivateData;
                tb_authority.Text = data.Authority;
                tb_public.Text = data.PublicData;
            }
            else
            {
                ClearFields();
            }
            m_nodeLoading = false;
        }

        private void tv_oidTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var tv = sender as TreeView;
            if (e.Button == MouseButtons.Right)
            {
                tv.SelectedNode = e.Node;
                cms_nodeMenu.Show(tv, e.X, e.Y);
            }
        }
        private TreeView GetTreeViewFromMenuItem(object sender)
        {
            var menuItem = sender as ToolStripMenuItem;
            var menu = menuItem.Owner as ContextMenuStrip;
            return menu.SourceControl as TreeView;
        }
        private void Addchild_Click(object sender, EventArgs e)
        {
            var tv = GetTreeViewFromMenuItem(sender);
            var parent = tv.SelectedNode;
            var parentData = GetNodeData(parent);
            if (parentData.IsNewNode)
            {
                MessageBox.Show("Child nodes cannot be added to an unsaved node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var newNode = AddNode(parent);
            var nodeData = GetNodeData(newNode);
            tv.SelectedNode = newNode;
            var prevNodeData = GetNodeData(parent.LastNode);
            if (parent.LastNode != null && prevNodeData != null && !String.IsNullOrEmpty(prevNodeData.Identifier))
            {
                int prevNodeId = Int32.Parse(prevNodeData.Identifier);
                nud_relId.Value = prevNodeId + 1;
            }
            else
            {
                nud_relId.Value = 1;
            }
            btn_saveEntry.Enabled = true;
        }
        private void Remove_Click(object sender, EventArgs e)
        {
            var tv = GetTreeViewFromMenuItem(sender);
            var node = tv.SelectedNode;
            var parent = node.Parent;
            var nodeData = GetNodeData(node);
            if (node.Level == 0)
            {
                MessageBox.Show("Root element cannot be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var parentData = GetNodeData(parent);
                if (MessageBox.Show($"Are you sure you want to delete the node '{tb_oid.Text}' ({tb_friendlyName.Text})? All child nodes will be deleted.", "Confirm delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (parentData != null)
                    {
                        parentData.RemoveChild(nodeData.Identifier);
                    }
                    node.Remove();
                }
            }
        }

        private void Export_Click(object sender, EventArgs e)
        {
            var tv = GetTreeViewFromMenuItem(sender);
            var node = tv.SelectedNode;
            var parent = node.Parent;
            var nodeData = GetNodeData(node);
            if (node.Parent == null)
            {
                MessageBox.Show("Export is not supported from the root node. Please select a child node and try again.", "Not supported", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (nodeData.IsNewNode)
            {
                MessageBox.Show("Cannot export an unsaved node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (
                var dialog = new SaveFileDialog()
                {
                    AddExtension = true,
                    DefaultExt = "xml",
                    Filter = FILE_EXTENSION_FILTER,
                    Title = "Export node",
                    OverwritePrompt = true
                }
            )
            {
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ExportXml(dialog.FileName, node);
                }
            }
        }

        private void btn_saveEntry_Click(object sender, EventArgs e)
        {
            Button save = sender as Button;
            if (!save.Enabled)
                return;
            var node = tv_oidTree.SelectedNode;
            if (node == null)
            {
                MessageBox.Show("Unexpected error: Selected node is null", "Null value error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var parent = node.Parent;
            if (parent == null)
            {
                MessageBox.Show("Unexpected error: Parent node is null", "Null value error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var nodeData = GetNodeData(node);
            var parentData = GetNodeData(parent);

            // Verify that the node identifier is unique for its level in the tree
            var identifier = nud_relId.Value.ToString();
            var prevId = nodeData.Identifier; // Save previous identifier
            if (parentData.IsDuplicate(identifier))
            {
                if (nodeData.IsNewNode || prevId != identifier)
                {
                    MessageBox.Show("The identifier is not unique for its level. Select another identifier and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            // Validate friendly name
            if (!String.IsNullOrEmpty(tb_friendlyName.Text) && !m_friendlyName.IsMatch(tb_friendlyName.Text))
            {
                MessageBox.Show($"Friendly name must begin with a lowercase ASCII letter, followed by any combination of lowercase ASCII letters, digits (0-9), dash (-), period (.), and underscore (_).\n\nFor the tech savvy, here's the regular expression:\n{m_friendlyName.ToString()}", "Format error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tb_friendlyName.Focus();
                tb_friendlyName.SelectAll();
                return;
            }

            nodeData.Identifier = identifier;
            nodeData.Name = tb_friendlyName.Text;
            nodeData.PrivateData = tb_private.Text;
            nodeData.Authority = tb_authority.Text;
            nodeData.PublicData = tb_public.Text;

            parentData.AddChild(identifier);
            if (!String.IsNullOrEmpty(prevId))
            {
                parentData.RemoveChild(prevId); // If the identifier was changed, remove the old one
            }
            node.Text = FormatNodeName(identifier, tb_friendlyName.Text);

            SetFullOid(node);

            nodeData.IsNewNode = false;
            save.Enabled = false;
            tv_oidTree.Sort();
            node.EnsureVisible();
            System.Threading.Thread.Sleep(1);
            tv_oidTree.SelectedNode = node;
            tv_oidTree.Focus();
        }
        private void btn_undoChanges_Click(object sender, EventArgs e)
        {
            ResetFields(tv_oidTree.SelectedNode);
        }

        private void btn_saveXml_Click(object sender, EventArgs e)
        {
            if (tv_oidTree.TopNode == null || tv_oidTree.TopNode.Nodes.Count == 0)
            {
                MessageBox.Show("There is nothing to save.", "No data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var dialog = new SaveFileDialog()
            {
                AddExtension = true,
                DefaultExt = "xml",
                Filter = FILE_EXTENSION_FILTER,
                Title = "Save file",
                OverwritePrompt = true
            };
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SaveXml(dialog.FileName);
            }
            dialog.Dispose();
        }
        private void btn_newTree_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to create a new OID tree? Any changes made to the current tree will be lost.", "Confirm operation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                AddRootNode();
                m_nodeLoading = true;
                ClearFields();
                m_nodeLoading = false;
            }
        }

        private void nud_relId_ValueChanged(object sender, EventArgs e)
        {
            EntryDataChanged(sender, e);
        }

        private void tb_friendlyName_TextChanged(object sender, EventArgs e)
        {
            EntryDataChanged(sender, e);
        }

        private void tb_descr_TextChanged(object sender, EventArgs e)
        {
            EntryDataChanged(sender, e);
        }

        private void tb_info_TextChanged(object sender, EventArgs e)
        {
            EntryDataChanged(sender, e);
        }

        private void tb_authority_TextChanged(object sender, EventArgs e)
        {
            EntryDataChanged(sender, e);
        }

        private void EntryDataChanged(object sender, EventArgs e)
        {
            if (tv_oidTree.Nodes.Count == 0)
                return;
            if (m_nodeLoading)
                return;
            var node = tv_oidTree.SelectedNode;
            btn_saveEntry.Enabled = !IsEqual(node);
        }

    }
}

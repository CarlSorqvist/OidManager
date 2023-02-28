using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Linq;
using Repository = OidManager.XmlConfig.Resource.Repository;
using Export = OidManager.XmlConfig.Resource.Export;

namespace OidManager
{
    public partial class OidManagerForm : Form
    {
        public OidManagerForm(String file)
        {
            InitializeComponent();
            this.Show(); // Need to have this for the treeview to actually be there before we try to load data into it
            Initialize(file);
        }

        private XmlDocument m_document;
        private XmlReaderSettings m_readersettings;
        private XmlNamespaceManager m_nsmgr;
        private List<ValidationEventArgs> m_validationErrors;

        private const String FILE_EXTENSION_FILTER = "XML Files (*.xml)|*.xml";
        private bool m_nodeLoading = false;
        private Regex m_friendlyName = new Regex(@"^[a-z][a-z0-9-]*$", RegexOptions.Compiled);


        private void Initialize(String file)
        {
            tv_oidTree.TreeViewNodeSorter = new TreeNodeSorter();
            var nt = new NameTable();
            m_readersettings = new XmlReaderSettings()
            {
                NameTable = nt,
                ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings | XmlSchemaValidationFlags.ProcessIdentityConstraints,
                ValidationType = ValidationType.Schema
            };
            m_document = new XmlDocument(nt);
            m_nsmgr = new XmlNamespaceManager(nt);
            m_nsmgr.AddNamespace(Repository.Namespace.Prefix, Repository.Namespace.Uri);
            m_readersettings.Schemas.Add(Repository.Namespace.Uri, GetSchemaReader());
            m_readersettings.ValidationEventHandler += ValidationCallback;
            InitializeContextMenuStrip();
            if (!String.IsNullOrEmpty(file))
            {
                try
                {
                    if (File.Exists(file))
                    {
                        using (var stream = File.OpenRead(file))
                        {
                            ReadXml(stream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occured while opening '{file}': {ex.Message}\n\nStack trace: {ex.StackTrace}", "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void ValidationCallback(object sender, ValidationEventArgs e)
        {
            m_validationErrors.Add(e);
        }
        private XmlReader GetSchemaReader()
        {
            String schema = Repository.Schema.FromBase64();
            var reader = new StringReader(schema);
            return XmlReader.Create(reader);
        }
        private void ReadXml(Stream stream)
        {
            m_validationErrors = new List<ValidationEventArgs>();
            m_document = new XmlDocument();
            using (var reader = XmlReader.Create(stream, m_readersettings))
            {
                try
                {
                    m_document.Load(reader);
                }
                catch (XmlSchemaValidationException ex)
                {
                    MessageBox.Show(String.Format("A validation error occured during parsing of the selected file:\n\n{0}", ex.Message), "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (XmlException ex)
                {
                    MessageBox.Show(String.Format("An XML error occured during parsing of the selected file:\n\n{0}", ex.Message), "Unexpected XML error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("An error occured during parsing of the selected file:\n\n{0}", ex.Message), "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var errorCount = m_validationErrors.Count;
                if (errorCount > 0)
                {
                    // Add a message that details the errors
                    var maxPrefLen = 500;
                    var sb = new StringBuilder(maxPrefLen * 2);
                    sb.AppendFormat("{0} validation failures occured during parsing of the XML file. Please correct your input and try again.", errorCount);

                    for (int i = 0; i < errorCount; i++)
                    {
                        var entry = m_validationErrors[i];
                        if (sb.Length < maxPrefLen)
                        {
                            sb.AppendLine()
                                .AppendLine()
                                .AppendFormat("At line {0}, char {1}", entry.Exception.LineNumber, entry.Exception.LinePosition)
                                .AppendLine()
                                .Append(entry.Message);
                        }
                        else
                        {
                            break;
                        }
                    }
                    MessageBox.Show(sb.ToString(), "Validation failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            AddRootNode();
            PopulateTree();
            m_document = null;
        }
        private void SaveXml(String path)
        {
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Encoding = XmlConfig.Encoder,
                ConformanceLevel = ConformanceLevel.Document,
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\n",
                NamespaceHandling = NamespaceHandling.OmitDuplicates
            };
            try
            {
                using (XmlWriter writer = XmlWriter.Create(path, settings))
                {
                    writer.WriteRepositoryDocument();
                    var root = tv_oidTree.TopNode;
                    if (root != null)
                    {
                        foreach (TreeNode child in root.Nodes)
                        {
                            writer.WriteRepositoryNode(child).Flush();
                        }
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured: {ex.Message}", "Write error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show($"Data saved to:\n\n{path}", "Save completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportXml(String path, TreeNode node)
        {
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Encoding = XmlConfig.Encoder,
                ConformanceLevel = ConformanceLevel.Document,
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\n",
                NamespaceHandling = NamespaceHandling.OmitDuplicates
            };
            try
            {
                using (var writer = XmlWriter.Create(path, settings))
                {
                    writer.WriteExportDocument().WriteExportNode(node).WriteEndDocument();
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured: {ex.Message}", "Write error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show($"Data exported to:\n\n{path}", "Export completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PopulateTree()
        {
            var rootXPathExpression = String.Format("/{0}", Repository.Elements.RootQualified);
            var navigator = m_document.CreateNavigator();
            var root = navigator.SelectSingleNode(rootXPathExpression, m_nsmgr);
            AddChildNode(root, tv_oidTree.TopNode);
            tv_oidTree.TopNode.Expand();
        }
        private void AddRootNode()
        {
            tv_oidTree.Nodes.Clear();
            var data = NodeData.AddNew();
            data.IsNewNode = false;
            TreeNode root = new TreeNode("Root")
            {
                Tag = data
            };
            tv_oidTree.Nodes.Add(root);
        }
        public TreeNode AddNode(TreeNode parent)
        {
            TreeNode node = new TreeNode("<new>")
            {
                Tag = NodeData.AddNew()
            };
            parent.Nodes.Add(node);
            return node;
        }
        private bool AddChildNode(XPathNavigator navigator, TreeNode parent)
        {
            var iterator = navigator.Select(Repository.Elements.OidQualified, m_nsmgr);
            var parentData = GetNodeData(parent);
            foreach (XPathNavigator oid in iterator)
            {
                var id = GetAttributeValue(oid, Repository.Attributes.Identifier).Value;
                var name = GetAttributeValue(oid, Repository.Attributes.Name)?.Value;
                var nodeName = FormatNodeName(id, name);
                TreeNode node = new TreeNode(nodeName);
                try
                {
                    node.Tag = NodeData.FromExisting(oid, m_nsmgr);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error on loading tree.\nID: {id}\nParent level: {parent.Level}\n\nMessage: {ex.GetBaseException().Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                parent.Nodes.Add(node);
                parentData.AddChild(id);
                if (!AddChildNode(oid, node))
                    return false;
                node.Collapse();
            }
            return true;
        }
        private XPathNavigator GetAttributeValue(XPathNavigator navigator, String name)
        {
            return navigator.SelectSingleNode($"@{name}", m_nsmgr);
        }
        private String FormatNodeName(String identifier, String name)
        {
            return String.Format("{0} ({1})", identifier, String.IsNullOrEmpty(name) ? "<null>" : name);
        }
        private static NodeData GetNodeData(TreeNode node)
        {
            return node?.Tag as NodeData;
        }
        private void SetFullOid(TreeNode node)
        {
            var oid = new StringBuilder(node.Level * 5 + 1); //  Should be enough in most cases
            GetCompositeOid(node, oid, false);
            tb_oid.Text = oid.ToString();
        }
        
        // Must be internal and static for the XmlWriterExtensions to be able to use it
        internal static void GetCompositeOid(TreeNode currentNode, StringBuilder oid, bool appendPeriod = true)
        {
            if (currentNode.Parent == null)
                return;
            GetCompositeOid(currentNode.Parent, oid);
            var data = GetNodeData(currentNode);
            if (data != null)
            {
                oid.Append(data.Identifier);
                if (appendPeriod)
                    oid.Append('.');
            }
        }
        private void ResetFields(TreeNode node)
        {
            m_nodeLoading = true;
            var data = GetNodeData(node);
            if (data != null)
            {
                SetFullOid(node);
                nud_relId.Value = Int32.Parse(data.Identifier);
                tb_friendlyName.Text = data.Name;
                tb_private.Text = data.PrivateData;
                tb_public.Text = data.PublicData;
                tb_authority.Text = data.Authority;
            }
            else
            {
                ClearFields();
            }
            m_nodeLoading = false;
            btn_saveEntry.Enabled = false;
        }
        private void ClearFields()
        {
            nud_relId.Value = 0;
            tb_oid.Text = String.Empty;
            tb_friendlyName.Text = String.Empty;
            tb_private.Text = String.Empty;
            tb_public.Text = String.Empty;
            tb_authority.Text = String.Empty;
        }
        private bool IsEqual(TreeNode node)
        {
            var data = GetNodeData(node);
            if (data == null)
                return false;
            return (data.Identifier ?? String.Empty) == nud_relId.Value.ToString()
                && (data.Name ?? String.Empty) == tb_friendlyName.Text
                && (data.PrivateData ?? String.Empty) == tb_private.Text
                && (data.PublicData ?? String.Empty) == tb_public.Text
                && (data.Authority ?? String.Empty) == tb_authority.Text;
        }

        private void InitializeContextMenuStrip()
        {
            var addchild = cms_nodeMenu.Items.Add("Add child");
            addchild.Click += Addchild_Click;
            var remove = cms_nodeMenu.Items.Add("Remove");
            remove.Click += Remove_Click;
            var export = cms_nodeMenu.Items.Add("Export");
            export.Click += Export_Click;
        }
    }
}

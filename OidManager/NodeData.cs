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
using Repository = OidManager.XmlConfig.Resource.Repository;

namespace OidManager
{
    internal class NodeData
    {
        private String m_identifier;
        private String m_name;
        private String m_private;
        private String m_public;
        private String m_authority;
        private HashSet<String> m_childNodes = new HashSet<String>(StringComparer.InvariantCultureIgnoreCase);
        bool m_newNode = false;

        public String Identifier
        {
            get => m_identifier;
            set
            {
                m_identifier = value;
            }
        }
        public String Name
        {
            get => m_name;
            set
            {
                m_name = value;
            }
        }
        public String PrivateData
        {
            get => m_private;
            set
            {
                m_private = value;
            }
        }
        public String PublicData
        {
            get => m_public;
            set
            {
                m_public = value;
            }
        }
        public String Authority
        {
            get => m_authority;
            set
            {
                m_authority = value;
            }
        }

        public bool IsNewNode { get => m_newNode; set => m_newNode = value; }

        private NodeData()
        {

        }
        public bool AddChild(String identifier)
        {
            return m_childNodes.Add(identifier);
        }
        public bool RemoveChild(String identifier)
        {
            return m_childNodes.Remove(identifier);
        }
        public bool IsDuplicate(String identifier)
        {
            return m_childNodes.Contains(identifier);
        }
        private void Initialize(XPathNavigator node, IXmlNamespaceResolver resolver)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver));

            m_identifier = node.SelectSingleNode($"@{Repository.Attributes.Identifier}")?.Value;
            m_name = node.SelectSingleNode($"@{Repository.Attributes.Name}")?.Value;
            m_private = node.SelectSingleNode(Repository.Elements.PrivateDataQualified, resolver)?.Value.FromBase64();
            m_public = node.SelectSingleNode(Repository.Elements.PublicDataQualified, resolver)?.Value.FromBase64();
            m_authority = node.SelectSingleNode(Repository.Elements.AuthorityQualified, resolver)?.Value.FromBase64();

        }
        public static NodeData FromExisting(XPathNavigator node, IXmlNamespaceResolver resolver)
        {
            var data = new NodeData();
            data.Initialize(node, resolver);
            return data;
        }
        public static NodeData AddNew()
        {
            return new NodeData() { m_newNode = true };
        }
    }
}

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
    internal static class XmlWriterExtensions
    {
        internal static XmlWriter WriteRepositoryNode(this XmlWriter writer, TreeNode node)
        {
            var nodeData = node.Tag as NodeData;
            if (nodeData != null)
            {
                if (!String.IsNullOrEmpty(nodeData.Identifier))
                {
                    writer.WriteStartElement(Repository.Namespace.Prefix, Repository.Elements.Oid, Repository.Namespace.Uri);
                    writer.WriteAttributeString(Repository.Attributes.Identifier, nodeData.Identifier);
                    if (!String.IsNullOrEmpty(nodeData.Name))
                    {
                        writer.WriteAttributeString(Repository.Attributes.Name, nodeData.Name);
                    }
                    if (!String.IsNullOrEmpty(nodeData.PrivateData))
                    {
                        writer.WriteRepositoryChildElement(Repository.Elements.PrivateData, nodeData.PrivateData);
                    }
                    if (!String.IsNullOrEmpty(nodeData.PublicData))
                    {
                        writer.WriteRepositoryChildElement(Repository.Elements.PublicData, nodeData.PublicData);
                    }
                    if (!String.IsNullOrEmpty(nodeData.Authority))
                    {
                        writer.WriteRepositoryChildElement(Repository.Elements.Authority, nodeData.Authority);
                    }

                    foreach (TreeNode child in node.Nodes)
                    {
                        writer.WriteRepositoryNode(child);
                    }
                    writer.WriteEndElement();
                }
            }
            return writer;
        }
        internal static XmlWriter WriteRepositoryChildElement(this XmlWriter writer, String name, String value)
        {
            writer.WriteStartElement(Repository.Namespace.Prefix, name, null);
            writer.WriteString(value?.Trim().ToBase64());
            writer.WriteEndElement();
            return writer;
        }
        internal static XmlWriter WriteRepositoryDocument(this XmlWriter writer)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement(Repository.Namespace.Prefix, Repository.Elements.Root, Repository.Namespace.Uri);
            writer.WriteAttributeString(XmlConfig.Xmlns, Repository.Namespace.Prefix, null, Repository.Namespace.Uri);
            return writer;
        }
        internal static XmlWriter WriteExportDocument(this XmlWriter writer)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement(Export.Namespace.Prefix, Export.Elements.Root, Export.Namespace.Uri);
            writer.WriteAttributeString(XmlConfig.Xmlns, Export.Namespace.Prefix, null, Export.Namespace.Uri);
            return writer;
        }
        internal static XmlWriter WriteExportChildElement(this XmlWriter writer, String name, String value)
        {
            writer.WriteStartElement(Export.Namespace.Prefix, name, null);
            writer.WriteString(value?.Trim());
            writer.WriteEndElement();
            return writer;
        }

        internal static XmlWriter WriteExportNode(this XmlWriter writer, TreeNode node)
        {
            var nodeData = node.Tag as NodeData;
            if (nodeData != null)
            {
                if (!String.IsNullOrEmpty(nodeData.Identifier))
                {
                    writer.WriteStartElement(Export.Namespace.Prefix, Export.Elements.Oid, null);

                    // Write composite OID
                    StringBuilder oid = new StringBuilder(node.Level * 5 + 1);
                    OidManagerForm.GetCompositeOid(node, oid, false);
                    writer.WriteExportChildElement(Export.Elements.Identifier, oid.ToString());

                    if (!String.IsNullOrEmpty(nodeData.PublicData))
                    {
                        writer.WriteExportChildElement(Export.Elements.Notes, nodeData.PublicData);
                    }
                    if (!String.IsNullOrEmpty(nodeData.Authority))
                    {
                        writer.WriteExportChildElement(Export.Elements.Authority, nodeData.Authority);
                    }

                    writer.WriteEndElement();
                    writer.Flush();
                    foreach (TreeNode child in node.Nodes)
                    {
                        writer.WriteExportNode(child);
                    }
                }
            }
            return writer;
        }

        /// <summary>
        /// Converts the supplied string to Base64 using the encoding specified in the <c>XmlConfig.Encoder</c> property.
        /// </summary>
        /// <param name="value">A string to convert to Base64.</param>
        /// <returns></returns>
        internal static String ToBase64(this String value)
        {
            return value.ToBase64(null);
        }
        /// <summary>
        /// Converts the supplied string to Base64 using the specified encoding.
        /// </summary>
        /// <param name="value">A string to convert to Base64.</param>
        /// <param name="encoding">The encoding to use. If null, uses the default as specified by the <c>XmlConfig.Encoder</c> property.</param>
        /// <returns></returns>
        internal static String ToBase64(this String value, Encoding encoding)
        {
            if (value == null)
                return null;
            if (encoding == null)
                encoding = XmlConfig.Encoder;
            return Convert.ToBase64String(encoding.GetBytes(value));
        }
        /// <summary>
        /// Converts the supplied Base64 string to a readable string using the encoding specified in the <c>XmlConfig.Encoder</c> property.
        /// </summary>
        /// <param name="value">A Base64 string to convert.</param>
        /// <returns></returns>
        internal static String FromBase64(this String value)
        {
            return value.FromBase64(null);
        }
        /// <summary>
        /// Converts the supplied Base64 string to a readable string using the specified encoding.
        /// </summary>
        /// <param name="value">A Base64 string to convert.</param>
        /// <param name="encoding">The encoding to use. If null, uses the default as specified by the <c>XmlConfig.Encoder</c> property.</param>
        /// <returns></returns>
        internal static String FromBase64(this String value, Encoding encoding)
        {
            if (value == null)
                return null;
            if (encoding == null)
                encoding = XmlConfig.Encoder;
            return encoding.GetString(Convert.FromBase64String(value));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OidManager
{
    public class TreeNodeSorter : IComparer<TreeNode>, IComparer
    {
        public TreeNodeSorter()
        {
        }
        int IComparer.Compare(object x, object y)
        {
            return Compare(x as TreeNode, y as TreeNode);
        }
        /// <summary>
        /// Compares two TreeNode objects by their NodeData contents. Lots of null checks, just in case (I know it's ugly).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(TreeNode x, TreeNode y)
        {
            if (x == null && y == null)
                return 0;
            if (x == null && y != null)
                return -1;
            if (x != null && y == null)
                return 1;
            var xdata = x.Tag as NodeData;
            var ydata = y.Tag as NodeData;
            if (xdata == null && ydata != null)
                return -1;
            if (xdata != null && ydata == null)
                return 1;
            if (xdata == null && ydata == null)
                return 0;
            if (String.IsNullOrEmpty(xdata.Identifier) && String.IsNullOrEmpty(ydata.Identifier))
                return 0;
            if (String.IsNullOrEmpty(xdata.Identifier) && !String.IsNullOrEmpty(ydata.Identifier))
                return -1;
            if (!String.IsNullOrEmpty(xdata.Identifier) && String.IsNullOrEmpty(ydata.Identifier))
                return 1;
            return Int32.Parse(xdata.Identifier).CompareTo(Int32.Parse(ydata.Identifier));
        }
    }
}

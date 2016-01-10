using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExportData.Helper
{
    /// <summary>
    /// 树视图辅助类
    /// </summary>
    public static class TreeViewHelper
    {
        /// <summary>
        /// 审核节点状态
        /// </summary>
        /// <param name="e"></param>
        public static void CheckControl(TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                SetNodeCheckStatus(e.Node, e.Node.Checked);
                SetNodeStyle(e.Node);
            }
        }
        private static void SetNodeCheckStatus(TreeNode tn, bool Checked)
        {
            if (tn == null) return;
            foreach (TreeNode tnChild in tn.Nodes)
            {
                tnChild.Checked = Checked;
                SetNodeCheckStatus(tnChild, Checked);
            }
            TreeNode tnParent = tn;
        }
        private static void SetNodeStyle(TreeNode Node)
        {
            int nNodeCount = 0;
            if (Node.Nodes.Count != 0)
            {
                foreach (TreeNode tnTemp in Node.Nodes)
                {

                    if (tnTemp.Checked == true)

                        nNodeCount++;
                }

                if (nNodeCount == Node.Nodes.Count)
                {
                    Node.Checked = true;
                    Node.ExpandAll();
                    Node.ForeColor = Color.Black;
                }
                else if (nNodeCount == 0)
                {
                    Node.Checked = false;
                    Node.Collapse();
                    Node.ForeColor = Color.Black;
                }
                else
                {
                    Node.Checked = true;
                    Node.ForeColor = Color.Gray;
                }
            }
            //当前节点选择完后，判断父节点的状态，调用此方法递归。
            if (Node.Parent != null)
                SetNodeStyle(Node.Parent);
        }
    }
}

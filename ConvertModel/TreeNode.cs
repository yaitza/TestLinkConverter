using System.Collections.Generic;


namespace ConvertModel
{
    public class TreeNode
    {
        List<TreeNode> Children = new List<TreeNode>();

        TestSuite Node { get; set; }

        public TreeNode(TestSuite node)
        {
            Node = node;
        }

        public TreeNode AddChildNode(TestSuite item)
        {
            TreeNode nodeItem = new TreeNode(item);
            Children.Add(nodeItem);
            return nodeItem;
        }
    }
}

namespace Trees
{
    using System;
    using System.Collections.Generic;

    public sealed class TreeNode : IEquatable<TreeNode>
    {
	    public int Value { get; set; }

	    public TreeNode Left { get; set; }

	    public TreeNode Right { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is TreeNode other)
            {
                return this.Value == other.Value && this.Left == other.Left && this.Right == other.Right;
            }

            return false;
        }

        public bool Equals(TreeNode other)
        {
            return other != null &&
                    Value == other.Value &&
                    EqualityComparer<TreeNode>.Default.Equals(Left, other.Left) &&
                    EqualityComparer<TreeNode>.Default.Equals(Right, other.Right);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Left, Right);
        }
            
        public override string ToString()
        {
            return Serialize(this);
        }

        #region Operators

        public static bool operator ==(TreeNode node1, TreeNode node2)
        {
            return EqualityComparer<TreeNode>.Default.Equals(node1, node2);
        }

        public static bool operator !=(TreeNode node1, TreeNode node2)
        {
            return !(node1 == node2);
        }

        private static string Serialize(TreeNode root)
        {
            if (root == null)
                return "#";
        
            return $"{root.Value},{Serialize(root.Left)},{Serialize(root.Right)}";
        }

        #endregion
    }
}

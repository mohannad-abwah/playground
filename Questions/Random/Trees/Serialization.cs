namespace Trees
{
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Deserialization
    {
        [DataTestMethod]
        [DynamicData(nameof(TestData))]
        public void SerializeDeserialize(TreeNode root)
        {
	        string serializedTree = root.ToString();
            TreeNode deserializedTree = Deserialize(serializedTree);
	        Assert.AreEqual(root, deserializedTree);
        }

        public static TreeNode Deserialize(string serializedTree) {
            Queue queue = new Queue(serializedTree.Split(','));
            return Deserialize(queue);
        }

        private static TreeNode Deserialize(Queue q)
        {
            string item = (string) q.Dequeue();
            if (item == "#")
                return null;
        
            TreeNode node = new TreeNode{ Value = int.Parse(item) };
            node.Left = Deserialize(q);
            node.Right = Deserialize(q);
            return node;
        }

        public static IEnumerable<object[]> TestData =>
            new[]
            {
                new object[]
                {
                    new TreeNode
                    {
                        Value = 2,
                        Left = new TreeNode
                        {
                            Value = 1
                        },
                        Right = new TreeNode
                        {
                            Value = 3
                        }
                    }
                },
                new object[]
                {
                    new TreeNode
                    {
                        Value = 2,
                        Left = new TreeNode
                        {
                            Value = 1
                        }
                    }
                },
                new object[]
                {
                    new TreeNode
                    {
                        Value = 2,
                        Right = new TreeNode
                        {
                            Value = 3
                        }
                    }
                },
                new object[]
                {
                    new TreeNode
                    {
                        Value = 2,
                        Left = new TreeNode
                        {
                            Value = 1,
                            Left = new TreeNode
                            {
                                Value = 3
                            }
                        }
                    }
                },
                new object[]
                {
                    new TreeNode
                    {
                        Value = 2
                    }
                }
            };
    }
}

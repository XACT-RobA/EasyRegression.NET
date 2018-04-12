using System.Collections.Generic;
using System.Linq;

namespace EasyRegression.Core.Common.Models
{
    public class PolynomialTreeNode
    {
        public PolynomialTreeNode()
        {
            Nodes = new List<PolynomialTreeNode>();
            Data = 1.0;
        }

        public PolynomialTreeNode(PolynomialTreeNode parent, double value)
            : this()
        {
            Parent = parent;
            Data = value * parent.Data;
        }

        public double Data { get; set; }

        public PolynomialTreeNode Parent { get; set; }

        public List<PolynomialTreeNode> Nodes { get; set; }

        public void AddNode(double value)
        {
            Nodes.Add(new PolynomialTreeNode(this, value));
        }

        public List<PolynomialTreeNode> GetBottomNodes(List<PolynomialTreeNode> data)
        {
            if (Nodes.Any())
            {
                foreach (var node in Nodes)
                {
                    node.GetBottomNodes(data);
                }
            }
            else
            {
                data.Add(this);
            }

            return data;
        }
    }
}
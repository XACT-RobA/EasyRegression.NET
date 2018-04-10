using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyRegression.Core.Common
{
    public class PolynomialTree
    {
        private readonly int _order;
        private readonly double[] _values;
        private readonly PolynomialTreeNode _root;

        public PolynomialTree(int order, double[] values)
        {
            _order = order;
            _values = values;
            _root = new PolynomialTreeNode();
        }

        private void Expand()
        {
            List<PolynomialTreeNode> bottomNodes;
            for (int iv = 0; iv < _values.Length; iv++)
            {
                var value = _values[iv];
                bottomNodes = GetBottomNodes();
                foreach (var node in bottomNodes)
                {
                    for (int io = 0; io <= _order; io++)
                    {
                        double data = Math.Pow(value, io);
                        node.AddNode(data);
                    }
                }
            }
        }

        public double[] GetExpandedData()
        {
            Expand();
            return GetBottomValues();
        }

        private List<PolynomialTreeNode> GetBottomNodes()
        {
            return _root.GetBottomNodes(new List<PolynomialTreeNode>());
        }

        private double[] GetBottomValues()
        {
            return GetBottomNodes().Select(x => x.Data).ToArray();
        }
    }
}
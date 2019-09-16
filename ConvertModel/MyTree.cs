using System.Collections.Generic;

namespace ConvertModel
{
    public class MyTree<T>
    {
        private List<MyTree<T>> _nodes;

        public List<MyTree<T>> Nodes
        {
            get { return _nodes; }
        }

        private MyTree<T> _parent;

        public MyTree<T> Parent
        {
            get { return _parent; }
            set { this._parent = value; }
        }

        public T Data { get; set; }


        public MyTree()
        {
            _nodes = new List<MyTree<T>>();
        }

        public MyTree(T data)
        {
            this.Data = data;
            this._nodes = new List<MyTree<T>>();
        }


        public void AddNode(MyTree<T> node)
        {
            if (!this._nodes.Contains(node))
            {
                node.Parent = this;
                this._nodes.Add(node);
            }
        }

        public void AddNode(List<MyTree<T>> nodes)
        {
            foreach (var node in nodes)
            {
                if (!this._nodes.Contains(node))
                {
                    node.Parent = this;
                    this._nodes.Add(node);
                }
            }
        }

        public void RemoveNode(MyTree<T> node)
        {
            if (this._nodes.Contains(node))
            {
                this._nodes.Remove(node);
            }
        }

        public void RemoveAll()
        {
            if (this._nodes != null && this._nodes.Count != 0)
            {
                this._nodes.Clear();
            }
        }
    }
}

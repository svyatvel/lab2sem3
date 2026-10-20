using System;
using System.Collections.Generic;

namespace Lab2;

public class BinaryTree<T> where T : class
{
    private readonly IComparer<T> _comparer;
    private Node<T>? _root;

    public BinaryTree()
        : this(Comparer<T>.Default)
    {
    }

    public BinaryTree(IComparer<T> comparer)
    {
        _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
    }

    public void Add(T value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        _root = Add(_root, value);
    }

    private Node<T>? Add(Node<T>? node, T value)
    {
        if (node is null)
        {
            return new Node<T>(value);
        }

        if (_comparer.Compare(value, node.Value) < 0)
        {
            node.Left = Add(node.Left, value);
        }
        else if (_comparer.Compare(value, node.Value) > 0)
        {
            node.Right = Add(node.Right, value);
        }
        else
        {
            node.Value = value;
        }

        return node;
    }

    public bool Contains(T value)
    {
        return Find(_root, value) is not null;
    }

    public bool Remove(T value)
    {
        int initialCount = Count();
        _root = Remove(_root, value);
        return Count() < initialCount;
    }

    private Node<T>? Remove(Node<T>? node, T value)
    {
        if (node is null)
        {
            return null;
        }

        int comparison = _comparer.Compare(value, node.Value);

        if (comparison < 0)
        {
            node.Left = Remove(node.Left, value);
            return node;
        }

        if (comparison > 0)
        {
            node.Right = Remove(node.Right, value);
            return node;
        }

        if (node.Left is null)
        {
            return node.Right;
        }

        if (node.Right is null)
        {
            return node.Left;
        }

        Node<T> successor = FindMin(node.Right);
        node.Value = successor.Value;
        node.Right = Remove(node.Right, successor.Value);
        return node;
    }

    private Node<T>? Find(Node<T>? node, T value)
    {
        if (node is null)
        {
            return null;
        }

        int comparison = _comparer.Compare(value, node.Value);

        if (comparison < 0)
        {
            return Find(node.Left, value);
        }

        if (comparison > 0)
        {
            return Find(node.Right, value);
        }

        return node;
    }

    private Node<T> FindMin(Node<T> node)
    {
        while (node.Left is not null)
        {
            node = node.Left;
        }

        return node;
    }

    public int Count()
    {
        return Count(_root);
    }

    private int Count(Node<T>? node)
    {
        if (node is null)
        {
            return 0;
        }

        return 1 + Count(node.Left) + Count(node.Right);
    }

    public void InOrder(Action<T> action)
    {
        if (action is null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        InOrder(_root, action);
    }

    private void InOrder(Node<T>? node, Action<T> action)
    {
        if (node is null)
        {
            return;
        }

        InOrder(node.Left, action);
        action(node.Value);
        InOrder(node.Right, action);
    }

    private sealed class Node<TNode>
    {
        public TNode Value { get; set; }
        public Node<TNode>? Left { get; set; }
        public Node<TNode>? Right { get; set; }

        public Node(TNode value)
        {
            Value = value;
        }
    }
}

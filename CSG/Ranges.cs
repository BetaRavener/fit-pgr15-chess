using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayMath;
using System.Collections.Concurrent;

namespace CSG
{
    //public class Ranges<T> where T : class
    //{
    //    protected List<Range<T>> _data;

    //    public Ranges()
    //    {
    //        _data = new List<Range<T>>();
    //    }

    //    public Ranges(Range<T> r)
    //    {
    //        _data = new List<Range<T>>();
    //        Add(r);
    //    }

    //    public void Add(Range<T> range)
    //    {
    //        int pos = 0;
    //        for (; pos < _data.Count && _data[pos].Right < range.Left; pos++) ;
    //        if (pos != _data.Count)
    //        {
    //            var current = _data[pos];
    //            var next = ((pos + 1) < _data.Count ? _data[pos + 1] : null);

    //            if (current.Left > range.Right)
    //            {
    //                _data.Insert(pos, range);
    //            }
    //            else if (current.Left <= range.Left)
    //            {
    //                if (current.Right >= range.Right) return;
    //                current.SetRight(range.Right, range.RightNode);
    //                while (next != null && next.Left <= current.Right)
    //                {
    //                    if (next.Right > current.Right)
    //                        current.SetRight(next.Right, next.RightNode);

    //                    _data.RemoveAt(pos + 1);
    //                    next = ((pos + 1) < _data.Count ? _data[pos + 1] : null);
    //                }
    //            }
    //            else if (current.Right >= range.Right)
    //            {
    //                current.SetLeft(range.Left, range.LeftNode);
    //            }
    //            else
    //            {
    //                // pozor na zakryte intervaly!!
    //                current.SetLeft(range.Left, range.LeftNode);
    //                current.SetRight(range.Right, range.RightNode);
    //                while (next != null && next.Left <= current.Right)
    //                {
    //                    if (next.Right > current.Right)
    //                        current.SetRight(next.Right, next.RightNode);

    //                    _data.RemoveAt(pos + 1);
    //                    next = ((pos + 1) < _data.Count ? _data[pos + 1] : null);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            _data.Add(range);
    //        }
    //    }

    //    public void Union(Ranges<T> other)
    //    {
    //        for (var i = 0; i < other._data.Count; i++)
    //            Add(other._data[i]);
    //    }

    //    public void Intersection(Ranges<T> other)
    //    {
    //        var result = new List<Range<T>>();
    //        var pos = 0;
    //        for (var i = 0; i < _data.Count; i++)
    //        {
    //            while (pos < other._data.Count && other._data[pos].Right < _data[i].Left)
    //                pos++;

    //            while (pos < other._data.Count && other._data[pos].Left < _data[i].Right)
    //            {
    //                result.Add(other._data[pos] * _data[i]);
    //                if (other._data[pos].Right < _data[i].Right)
    //                    pos++;
    //                else
    //                    break;
    //            }
    //        }

    //        _data = result;
    //    }

    //    public void Inverse()
    //    {
    //        var result = new List<Range<T>>();
    //        var pos = 0;

    //        if (pos == _data.Count)
    //        {
    //            Add(new Range<T>(-Range<T>.Inf, Range<T>.Inf));
    //            return;
    //        }

    //        if (_data[pos].Left > -Range<T>.Inf)
    //            result.Add(new Range<T>(-Range<T>.Inf, _data[pos].Left, null, _data[pos].LeftNode));

    //        while (pos < _data.Count)
    //        {
    //            if (pos + 1 < _data.Count)
    //            {
    //                result.Add(new Range<T>(_data[pos].Right, _data[pos].Left, _data[pos].RightNode, _data[pos].LeftNode));
    //            }
    //            else
    //            {
    //                result.Add(new Range<T>(_data[pos].Right, Range<T>.Inf, _data[pos].RightNode));
    //            }
    //            pos++;
    //        }
    //        _data = result;
    //    }

    //    public bool Empty()
    //    {
    //        return _data.Count == 0;
    //    }

    //    public virtual RangeEdge<T> FirstEdgeGreater(double t = 0.0)
    //    {
    //        foreach (var x in _data)
    //        {
    //            if (x.Left > t)
    //                return new RangeEdge<T>(x.Left, x.LeftNode, CSG.Intersection.IntersectionKind.Into);

    //            if (x.Right > t)
    //                return new RangeEdge<T>(x.Right, x.RightNode, CSG.Intersection.IntersectionKind.Outfrom);
    //        }

    //        return new RangeEdge<T>(t, null, CSG.Intersection.IntersectionKind.None);
    //    }
    //}

    //public class RangesShape : Ranges<Shapes.Shape> {
    //    public RangesShape()
    //    {
    //        _data = new List<Range<Shapes.Shape>>();
    //    }

    //    public RangesShape(RangeShape r)
    //    {
    //        _data = new List<Range<Shapes.Shape>>();
    //        Add(r);
    //    }

    //    public override RangeEdge<Shapes.Shape> FirstEdgeGreater(double t = 0.0)
    //    {
    //        for (var i = 0; i < _data.Count; i++)
    //        {
    //            var x = _data[i];
    //            if (x.Left > t)
    //                return new RangeEdgeShape(x.Left, x.LeftNode, CSG.Intersection.IntersectionKind.Into);

    //            if (x.Right > t)
    //                return new RangeEdgeShape(x.Right, x.RightNode, CSG.Intersection.IntersectionKind.Outfrom);
    //        }

    //        return new RangeEdgeShape(t, null, CSG.Intersection.IntersectionKind.None);
    //    }
    //}

    /// <summary>
    /// Represents a set of spans for CSG raytracing.
    /// </summary>
    public class RangesShape
    {
        /// <summary>
        /// Holds individual spans.
        /// </summary>
        private List<RangeShape> _data;

        public RangesShape()
        {
            _data = new List<RangeShape>();
        }

        public RangesShape(RangeShape r)
        {
            _data = new List<RangeShape>();
            Add(r);
        }

        /// <summary>
        /// Add another span.
        /// </summary>
        /// <param name="range">Span to be added.</param>
        public void Add(RangeShape range)
        {
            int pos = 0;
            for (; pos < _data.Count && _data[pos].Right < range.Left; pos++) ;
            if (pos != _data.Count)
            {
                var current = _data[pos];
                RangeShape next = current;
                if ((pos + 1) < _data.Count)
                    next = _data[pos + 1];

                if (current.Left > range.Right)
                {
                    _data.Insert(pos, range);
                }
                else if (current.Left <= range.Left)
                {
                    if (current.Right >= range.Right) return;
                    current.SetRight(range.Right, range.RightNode);
                    while ((pos + 1) < _data.Count && next.Left <= current.Right)
                    {
                        if (next.Right > current.Right)
                            current.SetRight(next.Right, next.RightNode);

                        _data.RemoveAt(pos + 1);
                        if ((pos + 1) < _data.Count)
                            next = _data[pos + 1];
                    }
                }
                else if (current.Right >= range.Right)
                {
                    current.SetLeft(range.Left, range.LeftNode);
                }
                else
                {
                    // pozor na zakryte intervaly!!
                    current.SetLeft(range.Left, range.LeftNode);
                    current.SetRight(range.Right, range.RightNode);
                    while ((pos + 1) < _data.Count && next.Left <= current.Right)
                    {
                        if (next.Right > current.Right)
                            current.SetRight(next.Right, next.RightNode);

                        _data.RemoveAt(pos + 1);
                        if ((pos + 1) < _data.Count)
                            next = _data[pos + 1];
                    }
                }
            }
            else
            {
                _data.Add(range);
            }
        }

        /// <summary>
        /// Does union operation between two span sets. The result is saved to this set.
        /// </summary>
        /// <param name="other"></param>
        public void Union(RangesShape other)
        {
            for (var i = 0; i < other._data.Count; i++)
                Add(other._data[i]);
        }

        /// <summary>
        /// Does intersection operation between two span sets. The result is saved to this set.
        /// </summary>
        /// <param name="other"></param>
        public void Intersection(RangesShape other)
        {
            var result = new List<RangeShape>();
            var pos = 0;
            for (var i = 0; i < _data.Count; i++)
            {
                while (pos < other._data.Count && other._data[pos].Right < _data[i].Left)
                    pos++;

                while (pos < other._data.Count && other._data[pos].Left < _data[i].Right)
                {
                    result.Add(other._data[pos] * _data[i]);
                    if (other._data[pos].Right < _data[i].Right)
                        pos++;
                    else
                        break;
                }
            }

            _data = result;
        }

        /// <summary>
        /// Does inverse of this span set. The result is saved to this set.
        /// </summary>
        public void Inverse()
        {
            var result = new List<RangeShape>();
            var pos = 0;

            if (pos == _data.Count)
            {
                Add(new RangeShape(-RangeShape.Inf, RangeShape.Inf));
                return;
            }

            if (_data[pos].Left > -RangeShape.Inf)
                result.Add(new RangeShape(-RangeShape.Inf, _data[pos].Left, null, _data[pos].LeftNode));

            while (pos < _data.Count)
            {
                if (pos + 1 < _data.Count)
                {
                    result.Add(new RangeShape(_data[pos].Right, _data[pos].Left, _data[pos].RightNode, _data[pos].LeftNode));
                }
                else
                {
                    result.Add(new RangeShape(_data[pos].Right, RangeShape.Inf, _data[pos].RightNode));
                }
                pos++;
            }
            _data = result;
        }

        /// <summary>
        /// Tells if set is empty.
        /// </summary>
        /// <returns>True if empty.</returns>
        public bool Empty()
        {
            return _data.Count == 0;
        }

        /// <summary>
        /// Finds first edge in set that begins after specified parameter.
        /// </summary>
        /// <param name="t">Distance from origin of ray.</param>
        /// <returns></returns>
        public RangeEdgeShape FirstEdgeGreater(double t = 0.0)
        {
            for (var i = 0; i < _data.Count; i++)
            {
                var x = _data[i];
                if (x.Left > t)
                    return new RangeEdgeShape(x.Left, x.LeftNode, CSG.Intersection.IntersectionKind.Into);

                if (x.Right > t)
                    return new RangeEdgeShape(x.Right, x.RightNode, CSG.Intersection.IntersectionKind.Outfrom);
            }

            return new RangeEdgeShape(t, null, CSG.Intersection.IntersectionKind.None);
        }
    }
}

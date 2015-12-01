using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayMath;
using System.Collections.Concurrent;

namespace CSG
{
    /// <summary>
    /// Represents a set of spans for CSG raytracing.
    /// </summary>
    public class RangesShape
    {
        /// <summary>
        /// Holds individual spans.
        /// </summary>
        public List<RangeShape> _data;

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
                    current.SetRight(range.Right, range.RightNode, range.RightSide);
                    while ((pos + 1) < _data.Count && next.Left <= current.Right)
                    {
                        if (next.Right > current.Right)
                            current.SetRight(next.Right, next.RightNode, next.RightSide);

                        _data.RemoveAt(pos + 1);
                        if ((pos + 1) < _data.Count)
                            next = _data[pos + 1];
                    }
                }
                else if (current.Right >= range.Right)
                {
                    current.SetLeft(range.Left, range.LeftNode, range.LeftSide);
                }
                else
                {
                    // pozor na zakryte intervaly!!
                    current.SetLeft(range.Left, range.LeftNode, range.LeftSide);
                    current.SetRight(range.Right, range.RightNode, range.RightSide);
                    while ((pos + 1) < _data.Count && next.Left <= current.Right)
                    {
                        if (next.Right > current.Right)
                            current.SetRight(next.Right, next.RightNode, next.RightSide);

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
                Add(new RangeShape(-RangeShape.Inf, RangeShape.Inf, null, null, RangeShape.Sides.Exterior, RangeShape.Sides.Interior));
                return;
            }

            if (_data[pos].Left > -RangeShape.Inf)
                result.Add(new RangeShape(-RangeShape.Inf, _data[pos].Left, null, _data[pos].LeftNode, RangeShape.Sides.Exterior, _data[pos].LeftSide));

            while (pos < _data.Count)
            {
                if (pos + 1 < _data.Count)
                {
                    result.Add(new RangeShape(_data[pos].Right, _data[pos+1].Left, _data[pos].RightNode, _data[pos+1].LeftNode, _data[pos].RightSide, _data[pos+1].LeftSide));
                }
                else if (_data[pos].Right < RangeShape.Inf)
                {
                    result.Add(new RangeShape(_data[pos].Right, RangeShape.Inf, _data[pos].RightNode, null, _data[pos].RightSide, RangeShape.Sides.Interior));
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
                {
                    if (x.LeftSide == RangeShape.Sides.Exterior)
                        return new RangeEdgeShape(x.Left, x.LeftNode, IntersectionKind.Into);
                    if (x.LeftSide == RangeShape.Sides.Interior)
                        return new RangeEdgeShape(x.Left, x.LeftNode, IntersectionKind.Outfrom);
                }
                if (x.Right > t)
                {
                    if (x.RightSide == RangeShape.Sides.Exterior)
                        return new RangeEdgeShape(x.Right, x.RightNode, IntersectionKind.Into);
                    if (x.RightSide == RangeShape.Sides.Interior)
                        return new RangeEdgeShape(x.Right, x.RightNode, IntersectionKind.Outfrom);
                }
            }

            return new RangeEdgeShape(t);
        }
    }
}

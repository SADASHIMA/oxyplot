﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OxyPlot.Axes.ComposableAxis
{
    /// <summary>
    /// Providers method to interact with <see cref="System.Double"/>.
    /// </summary>
    public struct DoubleProvider : IDataProvider<double>
    {
        /// <inheritdoc/>
        public bool IsDiscrete => false;

        /// <inheritdoc/>
        public int Compare(double x, double y)
        {
            return x.CompareTo(y);
        }

        /// <inheritdoc/>
        public double Deinterpolate(double v0, double v1, double v)
        {
            return (v - v0) / (v1 - v0);
        }

        /// <inheritdoc/>
        public bool Equals(double x, double y)
        {
            return x.Equals(y);
        }

        /// <inheritdoc/>
        public int GetHashCode(double obj)
        {
            return obj.GetHashCode();
        }

        /// <inheritdoc/>
        public double Interpolate(double v0, double v1, double c)
        {
            return v0 * (1 - c) + v1 * c;
        }

        /// <inheritdoc/>
        public bool Intersects(Discontenuity<double> discontenuity, double a, double b)
        {
            var min = Math.Min(a, b);
            var max = Math.Max(a, b);
            return min > discontenuity.End || max < discontenuity.Start;
        }
    }

    /// <summary>
    /// A linear data transformation over <see cref="System.Double"/>.
    /// </summary>
    public struct Linear : IDataTransformation<double, DoubleProvider>
    {
        /// <inheritdoc/>
        public bool IsNonDiscontinuous => true;

        /// <inheritdoc/>
        public bool IsLinear => true;

        /// <inheritdoc/>
        public bool IsDiscrete => false;

        /// <inheritdoc/>
        public DoubleProvider Provider => default;

        /// <inheritdoc/>
        public double InverseTransform(InteractionReal x)
        {
            return Math.Exp(x.Value);
        }

        /// <inheritdoc/>
        public void LocateDiscontenuities(double min, double max, IList<Discontenuity<double>> discontenuities)
        {
            // anything <= 0 is invalid... not sure if that counts as a Discontenuity: should probably just throw here and in the transforms
            // NOTE: we don't want to provide a double-wrapper, because that will mean we can't use DataPoint without much suffering (or does it?)
            if (min <= 0 || max <= 0)
            {
                discontenuities.Add(new Discontenuity<double>(double.NegativeInfinity, +0));
            }
        }

        /// <inheritdoc/>
        public InteractionReal Transform(double data)
        {
            return new InteractionReal(Math.Log(data));
        }

        /// <inheritdoc/>
        public bool IsDiscontinuous(double a, double b)
        {
            return false;
        }
    }

    /// <summary>
    /// A logarithmic data projection over <see cref="System.Double"/>.
    /// </summary>
    public struct Logarithmic : IDataTransformation<double, DoubleProvider>
    {
        /// <inheritdoc/>
        public bool IsNonDiscontinuous => false;

        /// <inheritdoc/>
        public bool IsLinear => false;

        /// <inheritdoc/>
        public bool IsDiscrete => false;

        /// <inheritdoc/>
        public bool AreEqual(double l, double r)
        {
            return l == r;
        }

        /// <inheritdoc/>
        public DoubleProvider Provider => default;

        /// <inheritdoc/>
        public double InverseTransform(InteractionReal x)
        {
            return x.Value;
        }

        /// <inheritdoc/>
        public InteractionReal Transform(double data)
        {
            return new InteractionReal(data);
        }

        /// <inheritdoc/>
        public bool IsDiscontinuous(double a, double b)
        {
            // not sure it makes sense to not throw in this case
            return a <= 0 || b <= 0;
        }
    }

    /// <summary>
    /// This needs a better name
    /// </summary>
    public struct ViewInfo
    {
        /// <summary>
        /// Initialises a <see cref="ViewInfo"/>.
        /// </summary>
        /// <param name="screenOffset"></param>
        /// <param name="screenScale"></param>
        public ViewInfo(ScreenReal screenOffset, double screenScale)
        {
            ScreenOffset = screenOffset;
            ScreenScale = screenScale;
        }

        /// <summary>
        /// Gets the Screen space offset, that is, the Screen space value to which the Interaction space zero maps.
        /// </summary>
        public ScreenReal ScreenOffset { get; }

        /// <summary>
        /// Gets the Screen space offset, that is, the scaling between Screen space and Interaction space.
        /// </summary>
        public double ScreenScale { get; }

        /// <summary>
        /// Transforms a value in Interaction space to Screen space.
        /// </summary>
        /// <param name="i">A value in Interaction space.</param>
        /// <returns>The resulting value in Screen space.</returns>
        public ScreenReal Transform(InteractionReal i)
        {
            return ScreenOffset + new ScreenReal(i.Value * ScreenScale);
        }

        /// <summary>
        /// Transforms a value in Screen space to Interaction space.
        /// </summary>
        /// <param name="s">A value in Screen space.</param>
        /// <returns>The resulting value in Interaction space.</returns>
        public InteractionReal InverseTransform(ScreenReal s)
        {
            return new InteractionReal((s.Value - ScreenOffset.Value) / ScreenScale);
        }
    }

    /// <summary>
    /// Wraps a <typeparamref name="TDataTransformation"/> and <see cref="ViewInfo"/> to provide an <see cref="IAxisScreenTransformation{TData, TDataProvider}"/>.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TDataProvider"></typeparam>
    /// <typeparam name="TDataTransformation"></typeparam>
    public struct AxisScreenTransformation<TData, TDataProvider, TDataTransformation> : IAxisScreenTransformation<TData, TDataProvider>
        where TDataProvider : IDataProvider<TData>
        where TDataTransformation : IDataTransformation<TData, TDataProvider>
    {
        /// <summary>
        /// Initialises the <see cref="AxisScreenTransformation{TData, TDataProvider, TDataTransformation}"/>.
        /// </summary>
        /// <param name="dataTransformation"></param>
        /// <param name="viewInfo"></param>
        public AxisScreenTransformation(TDataTransformation dataTransformation, ViewInfo viewInfo)
        {
            DataTransformation = dataTransformation;
            ViewInfo = viewInfo;
        }

        /// <summary>
        /// Gets the <typeparamref name="TDataProvider"/>.
        /// </summary>
        private TDataTransformation DataTransformation { get; }

        /// <summary>
        /// Gets the <see cref="ViewInfo"/>.
        /// </summary>
        private ViewInfo ViewInfo { get; }

        /// <inheritdoc/>
        public TDataProvider Provider => DataTransformation.Provider;

        /// <inheritdoc/>
        public bool IsNonDiscontinuous => DataTransformation.IsNonDiscontinuous;

        /// <inheritdoc/>
        public bool IsLinear => DataTransformation.IsLinear;

        /// <inheritdoc/>
        public TData InverseTransform(ScreenReal s)
        {
            return DataTransformation.InverseTransform(ViewInfo.InverseTransform(s));
        }

        /// <inheritdoc/>
        public bool IsDiscontinuous(TData a, TData b)
        {
            return DataTransformation.IsDiscontinuous(a, b);
        }

        /// <inheritdoc/>
        public ScreenReal Transform(TData data)
        {
            return ViewInfo.Transform(DataTransformation.Transform(data));
        }
    }

    /// <summary>
    /// Provides <see cref="System.Double"/> as option when <see cref="double.NaN"/>.
    /// </summary>
    public struct DoubleAsNaNOptional : IOptionalProvider<double, double>
    {
        /// <inheritdoc/>
        public bool HasValue(double optional)
        {
            return !double.IsNaN(optional);
        }

        /// <inheritdoc/>
        public bool TryGetValue(double optional, out double value)
        {
            value = optional;
            return !double.IsNaN(optional);
        }

        /// <inheritdoc/>
        public double None => double.NaN;

        /// <inheritdoc/>
        public double Some(double value)
        {
            if (double.IsNaN(value))
                throw new ArgumentException("Cannot represent NaN as a non-none value.");

            return value;
        }
    }
}

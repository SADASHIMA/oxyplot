﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OxyPlot.Axes.ComposableAxis
{
    /// <summary>
    /// Band position
    /// </summary>
    public enum BandPosition
    {
        /// <summary>
        /// Inline band, positioned along the axis.
        /// </summary>
        Inline,

        /// <summary>
        /// Side band, positioned off the plot area.
        /// </summary>
        Side,

        /// <summary>
        /// Side-Near band, positioned off the plot area, above any Side bands.
        /// </summary>
        SideNear,

        /// <summary>
        /// Side-Far band, positioned off the plot area, below any Side bands.
        /// </summary>
        SideFar,

        /// <summary>
        /// Inline-Near band, positioned off the plot area, above any Inline bands.
        /// </summary>
        InlineNear,

        /// <summary>
        /// Inline-Far band, positioned off the plot area, below any Inline bands.
        /// </summary>
        InlineFar,

        /// <summary>
        /// A non-positions band, which won't constribute to margins.
        /// </summary>
        None,
    }

    /// <summary>
    /// Represents a tick or label at a position on an axis
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public readonly struct Tick<TData>
    {
        /// <summary>
        /// Initalises a <see cref="Tick{TData}"/> with value and label;
        /// </summary>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public Tick(TData value, string label)
        {
            Value = value;
            Label = label ?? throw new ArgumentNullException(nameof(label));
        }

        /// <summary>
        /// Initalises a <see cref="Tick{TData}"/> with value only;
        /// </summary>
        /// <param name="value"></param>
        public Tick(TData value)
        {
            Value = value;
            Label = null;
        }

        /// <summary>
        /// The value representated by this tick.
        /// </summary>
        public TData Value { get; }

        /// <summary>
        /// The label - if any - to attach to this tick.
        /// </summary>
        public string Label { get; }
    }

    /// <summary>
    /// Represents a label over a range of values
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public readonly struct RangeTick<TData>
    {
        /// <summary>
        /// Initalises a <see cref="Range{TData}"/> with min, max, and label.
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <param name="label"></param>
        public RangeTick(TData minimum, TData maximum, string label)
        {
            Minimum = minimum;
            Maximum = maximum;
            Label = label ?? throw new ArgumentNullException(nameof(label));
        }

        /// <summary>
        /// Initalises a <see cref="Tick{TData}"/> with min and max only.
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        public RangeTick(TData minimum, TData maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
            Label = null;
        }

        /// <summary>
        /// The minimum value of this range.
        /// </summary>
        public TData Minimum { get; }

        /// <summary>
        /// The value representated by this tick.
        /// </summary>
        public TData Maximum { get; }

        /// <summary>
        /// The label - if any - to attach to this tick.
        /// </summary>
        public string Label { get; }
    }

    /// <summary>
    /// Represents a label over a range of values
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public readonly struct ColorRangeTick<TData>
    {
        /// <summary>
        /// Initalises a <see cref="Range{TData}"/> with min, max, and color.
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <param name="color"></param>
        public ColorRangeTick(TData minimum, TData maximum, OxyColor color)
        {
            Minimum = minimum;
            Maximum = maximum;
            Color = color;
        }

        /// <summary>
        /// The minimum value of this range.
        /// </summary>
        public TData Minimum { get; }

        /// <summary>
        /// The value representated by this tick.
        /// </summary>
        public TData Maximum { get; }

        /// <summary>
        /// The color of this range.
        /// </summary>
        public OxyColor Color { get; }
    }

    /// <summary>
    /// The location of a band.
    /// </summary>
    public class BandLocation
    {
        /// <summary>
        /// Initialises an instance of the <see cref="BandLocation"/> struct.
        /// </summary>
        public BandLocation(ScreenPoint reference, ScreenVector parallel, ScreenVector normal)
        {
            Reference = reference;
            Parallel = parallel;
            Normal = normal;
        }

        /// <summary>
        /// The reference point on the left of the band.
        /// </summary>
        public ScreenPoint Reference { get; }

        /// <summary>
        /// The vector which, along with the <see cref="Reference"/>, describes the ray along which the band exists.
        /// </summary>
        public ScreenVector Parallel { get; }

        /// <summary>
        /// A unit vector normal to the <see cref="Parallel"/>.
        /// </summary>
        public ScreenVector Normal { get; }

        /// <summary>
        /// Gets the nominal rotation of the <see cref="BandLocation"/> in radians.
        /// </summary>
        public double Rotation => Math.Atan2(Parallel.Y, Parallel.X);

        /// <summary>
        /// Gets the nominal rotation of the <see cref="BandLocation"/> in degrees.
        /// </summary>
        public double RotationDegrees => Rotation * 180 / Math.PI;

        /// <summary>
        /// Gets the nominal width of the <see cref="BandLocation"/>.
        /// </summary>
        public double Width => Parallel.Length;

        /// <summary>
        /// Gets a value indicating whether the BandLocation is axis aligned.
        /// </summary>
        public bool IsAxisAligned => Normal.X == 0 || Normal.Y == 0;
    }

    /// <summary>
    /// The excesses of a band.
    /// </summary>
    public readonly struct BandExcesses
    {
        /// <summary>
        /// Initialises the <see cref="BandExcesses"/>.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        public BandExcesses(double left, double top, double right, double bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// The left excess.
        /// </summary>
        public double Left { get; }

        /// <summary>
        /// The top excess.
        /// </summary>
        public double Top { get; }

        /// <summary>
        /// The right excess.
        /// </summary>
        public double Right { get; }

        /// <summary>
        /// The bottom excess.
        /// </summary>
        public double Bottom { get; }

        /// <summary>
        /// Returns <see cref="BandExcesses"/> which bounds two other <see cref="BandExcesses"/>.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static BandExcesses Max(BandExcesses a, BandExcesses b)
        {
            return new BandExcesses(Math.Max(a.Left, b.Left), Math.Max(a.Top, b.Top), Math.Max(a.Right, b.Right), Math.Max(a.Bottom, b.Bottom));
        }

        /// <summary>
        /// Clamps each value to a minimum of zero.
        /// </summary>
        /// <returns></returns>
        public BandExcesses ClampToZero()
        {
            return new BandExcesses(Math.Max(this.Left, 0), Math.Max(this.Top, 0), Math.Max(this.Right, 0), Math.Max(this.Bottom, 0));
        }
    }

    /// <summary>
    /// Represents a collection of major ticks
    /// </summary>
    public class DoubleMajorTicks : ITicks<double>
    {
        private List<Tick<double>> _ticks = new List<Tick<double>>();

        private bool _invalidated = true;

        /// <inheritdoc/>
        public IList<Tick<double>> ActualTicks => _ticks;

        /// <summary>
        /// Gets or sets the <see cref="ITickLocator{TData}"/> for this instance.
        /// </summary>
        public ITickLocator<double> TickLocator { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SpacingOptions"/> for this instance.
        /// </summary>
        public SpacingOptions SpacingOptions { get; set; }

        /// <inheritdoc/>
        public void Invalidate()
        {
            _invalidated = true;
        }

        /// <inheritdoc/>
        public void Refresh(IViewInformation<double> viewInformation)
        {
            if (!_invalidated)
                return;

            // TODO: not sure if I want the ITicks abstraction at the moment...
            this._ticks.Clear();
            this.TickLocator.GetTicks(viewInformation.ClipMinimum, viewInformation.ClipMaximum, 100, this.SpacingOptions, this._ticks, null);
        }
    }

    /// <summary>
    /// A basic logarithmic tick locator for doubles.
    /// </summary>
    public class LogarithmicDoubleTickLocator : ITickLocator<double>
    {
        /// <summary>
        /// Gets or sets the Tick Offset.
        /// </summary>
        public double Offset { get; set; }

        /// <summary>
        /// Gets or sets the Formatter.
        /// </summary>
        public Func<double, string> Formatter { get; set; }

        /// <summary>
        /// Gets or sets the Format String. Used if <see cref="Formatter"/> is <c>null</c>. Default is <c>"G5"</c>.
        /// </summary>
        public string FormatString { get; set; } = "G5";

        /// <summary>
        /// Gets or sets the Base of the logarithm.
        /// </summary>
        public double Base { get; set; } = 10;

        /// <summary>
        /// Formats a tick value.
        /// </summary>
        /// <param name="value">The tick value to format.</param>
        /// <returns></returns>
        public string Format(double value)
        {
            if (Formatter != null)
                return Formatter(value);
            else
                return value.ToString(FormatString);
        }

        /// <inheritdoc/>
        public void GetTicks(double minimum, double maximum, double availableWidth, SpacingOptions spacingOptions, IList<Tick<double>> majorTicks, IList<Tick<double>> minorTicks)
        {
            var helper = new LogarithmicAxisTickHelper(this.Base, minimum, maximum);
            helper.GetTickValues(availableWidth, spacingOptions.MaximumIntervalSize, out var _, out var majorTickValues, out var minorTickValues);

            foreach (var t in majorTickValues)
            {
                majorTicks.Add(new Tick<double>(t, this.Format(t)));
            }

            foreach (var t in minorTickValues)
            {
                minorTicks.Add(new Tick<double>(t));
            }
        }
    }

    /// <summary>
    /// A basic Linear tick locator for doubles.
    /// </summary>
    public class LinearDoubleTickLocator : ITickLocator<double>
    {
        /// <summary>
        /// Gets or sets the Tick Offset.
        /// </summary>
        public double Offset { get; set; }

        /// <summary>
        /// Gets or sets the Formatter.
        /// </summary>
        public Func<double, string> Formatter { get; set; }

        /// <summary>
        /// Gets or sets the Format String. Used if <see cref="Formatter"/> is <c>null</c>. Default is <c>"G5"</c>.
        /// </summary>
        public string FormatString { get; set; } = "G5";

        /// <summary>
        /// Formats a tick value.
        /// </summary>
        /// <param name="value">The tick value to format.</param>
        /// <returns></returns>
        public string Format(double value)
        {
            if (Formatter != null)
                return Formatter(value);
            else
                return value.ToString(FormatString);
        }

        /// <inheritdoc/>
        public void GetTicks(double minimum, double maximum, double availableWidth, SpacingOptions spacingOptions, IList<Tick<double>> majorTicks, IList<Tick<double>> minorTicks)
        {
            var majorInterval = AxisUtilities.CalculateActualIntervalLinear(availableWidth, spacingOptions.MaximumIntervalSize, maximum - minimum);
            var minorInterval = AxisUtilities.CalculateMinorInterval(majorInterval);

            var majorTickValues = AxisUtilities.CreateTickValues(minimum, maximum, majorInterval, spacingOptions.MaximumTickCount);
            var minorTickValues = AxisUtilities.CreateTickValues(minimum, maximum, minorInterval, spacingOptions.MaximumTickCount);
            minorTickValues = AxisUtilities.FilterRedundantMinorTicks(majorTickValues, minorTickValues);

            foreach (var t in majorTickValues)
                majorTicks.Add(new Tick<double>(t, Format(t)));
            foreach (var t in minorTickValues)
                minorTicks.Add(new Tick<double>(t));
        }
    }

    /// <summary>
    /// A basic Linear tick locator for doubles.
    /// </summary>
    public class LinearDoubleRangeTickLocator : IRangeTickLocator<double>
    {
        /// <summary>
        /// Gets or sets the Tick Offset.
        /// </summary>
        public double Offset { get; set; }

        /// <summary>
        /// Gets or sets the Formatter.
        /// </summary>
        public Func<double, string> Formatter { get; set; }

        /// <summary>
        /// Gets or sets the Format String. Used if <see cref="Formatter"/> is <c>null</c>. Default is <c>"G5"</c>.
        /// </summary>
        public string FormatString { get; set; } = "G5";

        /// <summary>
        /// Formats a tick value.
        /// </summary>
        /// <param name="value">The tick value to format.</param>
        /// <returns></returns>
        public string Format(double value)
        {
            if (Formatter != null)
                return Formatter(value);
            else
                return value.ToString(FormatString);
        }

        /// <inheritdoc/>
        public void GetTicks(double minium, double maximum, double availableWidth, SpacingOptions spacingOptions, IList<RangeTick<double>> ticks)
        {
            var majorInterval = AxisUtilities.CalculateActualIntervalLinear(availableWidth, spacingOptions.MaximumIntervalSize, maximum - minium);

            var tickValues = AxisUtilities.CreateTickValues(minium - majorInterval, maximum, majorInterval, spacingOptions.MaximumTickCount);

            foreach (var t in tickValues)
                ticks.Add(new RangeTick<double>(t, t + majorInterval, Format(t)));
        }
    }

    /// <summary>
    /// A basic Linear tick locator for doubles.
    /// </summary>
    public class LinearDateTimeTickLocator : ITickLocator<DateTime>
    {
        /// <summary>
        /// Gets or sets the Formatter.
        /// </summary>
        public Func<DateTime, string> Formatter { get; set; }

        /// <summary>
        /// Gets or sets the Format String. Used if <see cref="Formatter"/> is <c>null</c>. Default is <c>"G5"</c>.
        /// </summary>
        public string FormatString { get; set; } = null;

        /// <inheritdoc/>
        public void GetTicks(DateTime minimum, DateTime maximum, double availableWidth, SpacingOptions spacingOptions, IList<Tick<DateTime>> majorTicks, IList<Tick<DateTime>> minorTicks)
        {
            // TODO: proper implementation

            var diff = 2 * (maximum - minimum).TotalDays * spacingOptions.MaximumIntervalSize * spacingOptions.MinimumTickCount / availableWidth;

            DateTime x0;
            DateTime x1;

            Func<DateTime, string> formatter = Formatter ?? (FormatString == null ? null : new Func<DateTime, string>(dt => dt.ToString(FormatString)));
            Func<DateTime, DateTime> inc;

            if (diff > 2000)
            {
                // years
                x0 = new DateTime(minimum.Year + 1, 1, 1);
                x1 = new DateTime(maximum.Year, 1, 1);
                inc = dt => dt.AddYears(1);
                formatter ??= dt => dt.ToString("yyyy");
            }
            else if (diff > 100)
            {
                // months
                x0 = new DateTime(minimum.Year, (minimum.Month + 1) % 12, 1);
                x1 = new DateTime(maximum.Year, maximum.Month, 1);
                inc = dt => dt.AddYears(1);
                formatter ??= dt => dt.ToString("yyyy-MMM");
            }
            else if (diff > 5)
            {
                // days
                x0 = new DateTime(minimum.Year, minimum.Month, minimum.Day);
                x1 = new DateTime(maximum.Year, maximum.Month, maximum.Day);
                inc = dt => dt.AddDays(1);
                formatter ??= dt => dt.ToString("yyyy-MMM-dd");
            }
            else if (diff > 2)
            {
                // 8 hours
                x0 = new DateTime(minimum.Year, minimum.Month, minimum.Day, ((minimum.Hour / 8 + 1) * 8) % 24, 0, 0);
                x1 = new DateTime(maximum.Year, maximum.Month, maximum.Day, maximum.Hour, 0, 0);
                inc = dt => dt.AddHours(8);
                formatter ??= dt => dt.ToString("yyyy-MM-ddTHH");
            }
            else if (diff > 1)
            {
                // 4 hours
                x0 = new DateTime(minimum.Year, minimum.Month, minimum.Day, ((minimum.Hour / 4 + 1) * 4) % 24, 0, 0);
                x1 = new DateTime(maximum.Year, maximum.Month, maximum.Day, maximum.Hour, 0, 0);
                inc = dt => dt.AddHours(4);
                formatter ??= dt => dt.ToString("yyyy-MM-ddTHH");
            }
            else if (diff > 0.2)
            {
                // hours
                x0 = new DateTime(minimum.Year, minimum.Month, minimum.Day, (minimum.Hour + 1) % 24, 0, 0);
                x1 = new DateTime(maximum.Year, maximum.Month, maximum.Day, maximum.Hour, 0, 0);
                inc = dt => dt.AddHours(1);
                formatter ??= dt => dt.ToString("yyyy-MM-ddTHH");
            }
            else if (diff > 0.02)
            {
                // 15 minutes
                x0 = new DateTime(minimum.Year, minimum.Month, minimum.Day, minimum.Hour, ((minimum.Minute / 15 + 1) * 15) % 60, 0);
                x1 = new DateTime(maximum.Year, maximum.Month, maximum.Day, maximum.Hour, minimum.Minute, 0);
                inc = dt => dt.AddMinutes(15);
                formatter ??= dt => dt.ToString("yyyy-MM-ddTHH:mm");
            }
            else if (diff > 0.002)
            {
                // minutes
                x0 = new DateTime(minimum.Year, minimum.Month, minimum.Day, minimum.Hour, (minimum.Minute + 1) % 60, 0);
                x1 = new DateTime(maximum.Year, maximum.Month, maximum.Day, maximum.Hour, minimum.Minute, 0);
                inc = dt => dt.AddMinutes(1);
                formatter ??= dt => dt.ToString("yyyy-MM-ddTHH:mm");
            }
            else if (diff > 0.0001)
            {
                // 15 seconds
                x0 = new DateTime(minimum.Year, minimum.Month, minimum.Day, minimum.Hour, minimum.Minute, ((minimum.Second / 15 + 1) * 15) % 60);
                x1 = new DateTime(maximum.Year, maximum.Month, maximum.Day, maximum.Hour, maximum.Minute, maximum.Second);
                inc = dt => dt.AddSeconds(15);
                formatter ??= dt => dt.ToString("yyyy-MM-ddTHH:mm:ss");
            }
            else if (diff > 0.00001)
            {
                // seconds
                x0 = new DateTime(minimum.Year, minimum.Month, minimum.Day, minimum.Hour, minimum.Minute, (minimum.Second + 1) % 60);
                x1 = new DateTime(maximum.Year, maximum.Month, maximum.Day, maximum.Hour, maximum.Minute, maximum.Second);
                inc = dt => dt.AddSeconds(1);
                formatter ??= dt => dt.ToString("yyyy-MM-ddTHH:mm:ss");
            }
            else if (diff > 0.00000001)
            {
                // milliseconds
                x0 = new DateTime(minimum.Year, minimum.Month, minimum.Day, minimum.Hour, minimum.Minute, minimum.Second, (minimum.Millisecond + 1) % 1000);
                x1 = new DateTime(maximum.Year, maximum.Month, maximum.Day, maximum.Hour, maximum.Minute, maximum.Second, maximum.Millisecond);
                inc = dt => dt.AddMinutes(1);
                formatter ??= dt => dt.ToString("yyyy-MM-ddTHH:mm:ss.fff");
            }
            else
            {
                var t = (maximum - minimum).Ticks / 10;
                // ticks
                x0 = minimum;
                x1 = maximum;
                inc = dt => dt.AddTicks(t);
                formatter ??= dt => dt.ToString("O");
            }

            var x = x0;
            do
            {
                majorTicks.Add(new Tick<DateTime>(x, formatter(x)));

                x = inc(x);
            }
            while (x <= x1);
        }
    }

    /// <summary>
    /// Contains formatting options for ticks.
    /// </summary>
    public class TickFormatting
    {
        /// <summary>
        /// Initialises an instance of the <see cref="TickFormatting"/> class.
        /// </summary>
        public TickFormatting()
        {
            this.TickLength = 5;
            this.TickStyle = TickStyle.Outside;
            this.TickColor = OxyColors.Automatic;
            this.TickLineStyle = LineStyle.Solid;
        }

        /// <summary>
        /// Gets or sets the length of ticks.
        /// </summary>
        public double TickLength { get; set; }

        /// <summary>
        /// Gets or sets the style of ticks.
        /// </summary>
        public TickStyle TickStyle { get; set; }

        /// <summary>
        /// Gets or sets the color of ticks.
        /// </summary>
        public OxyColor TickColor { get; set; }

        /// <summary>
        /// Gets or sets the line style of ticks.
        /// </summary>
        public LineStyle TickLineStyle { get; set; }
    }

    /// <summary>
    /// A band that provides rendering capabilities to an axis
    /// </summary>
    public interface IBand
    {
        /// <summary>
        /// Gets a value indicating whether the band should be rendered.
        /// </summary>
        /// <remarks>
        /// May be updated by <see cref="Update"/>
        /// </remarks>
        bool IsBandVisible { get; }

        /// <summary>
        /// Gets the position of the band.
        /// </summary>
        BandPosition BandPosition { get; }

        /// <summary>
        /// Gets the tier of the band.
        /// </summary>
        int BandTier { get; }

        /// <summary>
        /// Gets the Band Excessess
        /// </summary>
        /// <remarks>
        /// Updated by <see cref="Measure(IRenderContext, BandLocation)"/>.
        /// </remarks>
        BandExcesses Excesses { get; }

        /// <summary>
        /// Associates the given axis with this band.
        /// Will be called before <see cref="Update"/>.
        /// </summary>
        /// <param name="axis"></param>
        void AssociateAxis(AxisBase axis);

        /// <summary>
        /// Updates the visibility and other state of the band.
        /// Called once per frame, before all calls to <see cref="Measure(IRenderContext, BandLocation)"/>.
        /// </summary>
        void Update();

        /// <summary>
        /// Measures the band, setting the <see cref="Excesses"/> accordingly.
        /// May be called many times each frame.
        /// </summary>
        /// <param name="renderContext">The render context.</param>
        /// <param name="zeroReferenceLocation">A band location with a meaningless <see cref="BandLocation.Reference"/> decribing the size and orientation of the band.</param>
        void Measure(IRenderContext renderContext, BandLocation zeroReferenceLocation);

        /// <summary>
        /// Renders the band.
        /// Called one per frame, after all calls to <see cref="Measure(IRenderContext, BandLocation)"/>.
        /// </summary>
        /// <param name="renderContext">The render context.</param>
        /// <param name="location">The location where the band should be rendered</param>
        void Render(IRenderContext renderContext, BandLocation location);
    }

    /// <summary>
    /// Represents a band of axis annotations.
    /// </summary>
    public abstract class BandBase : IBand
    {
        /// <summary>
        /// Gets a value indicating whether the band should be rendered.
        /// </summary>
        public bool IsBandVisible { get; set; }

        /// <summary>
        /// Gets or sets the position of the band.
        /// </summary>
        public BandPosition BandPosition { get; set; }

        /// <summary>
        /// Gets or sets the tier of the band.
        /// </summary>
        public int BandTier { get; set; }

        /// <summary>
        /// Gets the Band Excessess
        /// </summary>
        public BandExcesses Excesses { get; protected set; }

        /// <summary>
        /// The axis associated with this band.
        /// </summary>
        /// <remarks>
        /// Updated by <see cref="AssociateAxis(AxisBase)"/>
        /// </remarks>
        protected AxisBase Axis { get; private set; }

        /// <inheritdoc/>
        public virtual void AssociateAxis(AxisBase axis)
        {
            Axis = axis;
        }

        /// <inheritdoc/>
        public abstract void Update();

        /// <inheritdoc/>
        public abstract void Measure(IRenderContext renderContext, BandLocation zeroReferenceLocation);

        /// <inheritdoc/>
        public abstract void Render(IRenderContext renderContext, BandLocation location);
    }

    /// <summary>
    /// Represents a typed band of axis annotations.
    /// </summary>
    public abstract class BandBase<TData> : BandBase
    {
        /// <summary>
        /// The axis associated with this band.
        /// </summary>
        /// <remarks>
        /// Updated by <see cref="AssociateAxis(AxisBase)"/>
        /// </remarks>
        new protected IAxis<TData> Axis { get; private set; }

        /// <inheritdoc/>
        public override void AssociateAxis(AxisBase axis)
        {
            base.AssociateAxis(axis);

            if (axis is IAxis<TData> typed)
            {
                Axis = typed;
            }
            else
            {
                throw new InvalidOperationException($"{this.GetType().Name} cannot be associated with an axis of type {axis.GetType().Name}. The axis must implement IAxis<{typeof(TData).Name}>.");
            }
        }
    }

    /// <summary>
    /// A band that renders the axis title.
    /// </summary>
    public class LabelBand : BandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LabelBand" /> class.
        /// </summary>
        public LabelBand()
        {
            this.BandPosition = BandPosition.Inline;
            this.BandTier = 1;
            this.IsBandVisible = true;
        }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        public string Font { get; set; }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        public double FontSize { get; set; }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        public double FontWeight { get; set; }

        /// <summary>
        /// Gets or sets the label padding.
        /// </summary>
        public OxyThickness Padding { get; set; }

        /// <summary>
        /// Gets or sets the label position.
        /// A value in the range [0, 1].
        /// </summary>
        public double LabelPosition { get; set; }

        /// <summary>
        /// The text color.
        /// </summary>
        public OxyColor TextColor { get; set; }

        /// <inheritdoc/>
        public override void Update()
        {
        }

        /// <inheritdoc/>
        public override void Measure(IRenderContext renderContext, BandLocation zeroReferenceLocation)
        {
            var titleTextSize = renderContext.MeasureText(Text, Font, FontSize, FontWeight);

            if (titleTextSize.Height > 0)
            {
                var top = Padding.Bottom + titleTextSize.Height;
                double middle = zeroReferenceLocation.Width * this.LabelPosition;

                var left = middle - titleTextSize.Width / 2;
                var right = middle - titleTextSize.Width / 2;

                Excesses = new BandExcesses(-left, top + Padding.Top - Padding.Bottom, right - zeroReferenceLocation.Width, Padding.Bottom);
            }
            else
            {
                Excesses = new BandExcesses(0.0, 0.0, 0.0, 0.0);
            }
        }

        /// <inheritdoc/>
        public override void Render(IRenderContext renderContext, BandLocation location)
        {
            var titleTextSize = renderContext.MeasureText(Text, Font, FontSize, FontWeight);

            if (titleTextSize.Height > 0)
            {
                var offset = location.Reference
                    + location.Parallel * Axis.TitlePosition
                    + location.Normal * (Padding.Bottom + titleTextSize.Height / 2);

                var rot = location.RotationDegrees;
                if (rot < -90 || rot > 90)
                    rot = 0;

                renderContext.DrawText(offset, Text, TextColor, Font, FontSize, FontWeight, rot, HorizontalAlignment.Center, VerticalAlignment.Middle, null);
            }
        }
    }

    /// <summary>
    /// A band that renders the axis title.
    /// </summary>
    public class TitleBand : LabelBand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TitleBand" /> class.
        /// </summary>
        public TitleBand()
        {
        }

        /// <inheritdoc/>
        public override void Update()
        {
            this.Text = Axis.ActualTitle;
            this.Font = Axis.ActualFont;
            this.FontSize = Axis.ActualFontSize;
            this.FontWeight = Axis.ActualFontWeight;
            this.Padding = new OxyThickness(0, 0, 0, Axis.AxisTitleDistance);
            this.LabelPosition = Axis.TitlePosition;
            this.TextColor = Axis.ActualTextColor;
        }
    }

    /// <summary>
    /// A band that renders axis ticks.
    /// </summary>
    public class AxisLineBand : BandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TitleBand" /> class.
        /// </summary>
        public AxisLineBand()
        {
            this.BandPosition = BandPosition.Inline;
            this.BandTier = 0;
            this.IsBandVisible = true;
        }

        /// <inheritdoc/>
        public override void Measure(IRenderContext renderContext, BandLocation location)
        {
            Excesses = new BandExcesses(0, 0, 0, 0);
        }

        /// <inheritdoc/>
        public override void Render(IRenderContext renderContext, BandLocation location)
        {
            // TODO: decide whether to render the axis line
            var s0 = location.Reference;
            var s1 = location.Reference + location.Parallel;

            var pen = new OxyPen(Axis.AxislineColor, Axis.AxislineThickness, Axis.AxislineStyle);
            renderContext.DrawLine(s0.X, s0.Y, s1.X, s1.Y, pen, EdgeRenderingMode.Automatic);
        }

        /// <inheritdoc/>
        public override void Update()
        {
            // nix: we need to know how big we are before we can do anything
        }
    }

    /// <summary>
    /// A band that renders axis ticks.
    /// </summary>
    public class TickBand<TData> : BandBase<TData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TitleBand" /> class.
        /// </summary>
        public TickBand(ITickLocator<TData> tickLocator, SpacingOptions spacingOptions)
        {
            this.TickLocator = tickLocator;
            this.SpacingOptions = spacingOptions;

            this.BandPosition = BandPosition.Inline;
            this.BandTier = 0;
            this.IsBandVisible = true;

            this.MajorTicks = new List<Tick<TData>>();
            this.MinorTicks = new List<Tick<TData>>();
        }

        /// <summary>
        /// Gets or sets the <see cref="ITickLocator{TData}"/> for this band.
        /// </summary>
        public ITickLocator<TData> TickLocator { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SpacingOptions"/>  for this band.
        /// </summary>
        public SpacingOptions SpacingOptions { get; set; }

        /// <summary>
        /// Gets the ticks rendered by this band.
        /// </summary>
        /// <remarks>
        /// Updated by <see cref="Measure"/>
        /// </remarks>
        public List<Tick<TData>> MajorTicks { get; }

        /// <summary>
        /// Gets the ticks rendered by this band.
        /// </summary>
        /// <remarks>
        /// Updated by <see cref="Measure"/>
        /// </remarks>
        public List<Tick<TData>> MinorTicks { get; }

        /// <summary>
        /// Updates the <see cref="MajorTicks"/> and <see cref="MinorTicks"/>.
        /// </summary>
        /// <param name="availableWidth"></param>
        protected virtual void UpdateTicks(double availableWidth)
        {
            MajorTicks.Clear();
            MinorTicks.Clear();

            TickLocator.GetTicks(Axis.ClipMinimum, Axis.ClipMaximum, availableWidth, SpacingOptions, MajorTicks, MinorTicks);
        }

        /// <inheritdoc/>
        public override void Measure(IRenderContext renderContext, BandLocation location)
        {
            UpdateTicks(location.Width);

            var renderHelper = TickRenderHelperPreparer<TData>.PrepareHorizontalVertial(Axis);
            var majorExcesses = renderHelper.MeasureTicks(renderContext, location, MajorTicks.AsReadOnlyList(), TickStyle.Outside, Axis.MajorTickSize, Axis.MajorGridlineThickness, Axis.TicklineColor, ((AxisBase)Axis).ActualFont, ((AxisBase)Axis).ActualFontSize, ((AxisBase)Axis).ActualFontWeight, ((AxisBase)Axis).ActualTextColor, 0, Axis.AxisTickToLabelDistance);
            var minorExcesses = renderHelper.MeasureTicks(renderContext, location, MajorTicks.AsReadOnlyList(), TickStyle.Outside, Axis.MinorTickSize, Axis.MinorGridlineThickness, Axis.TicklineColor, ((AxisBase)Axis).ActualFont, ((AxisBase)Axis).ActualFontSize, ((AxisBase)Axis).ActualFontWeight, ((AxisBase)Axis).ActualTextColor, 0, Axis.AxisTickToLabelDistance);
            Excesses = BandExcesses.Max(majorExcesses, minorExcesses);
        }

        /// <inheritdoc/>
        public override void Render(IRenderContext renderContext, BandLocation location)
        {
            var renderHelper = TickRenderHelperPreparer<TData>.PrepareHorizontalVertial(Axis);
            renderHelper.RenderTicks(renderContext, location, MajorTicks.AsReadOnlyList(), TickStyle.Outside, Axis.MajorTickSize, Axis.MajorGridlineThickness, Axis.TicklineColor, ((AxisBase)Axis).ActualFont, ((AxisBase)Axis).ActualFontSize, ((AxisBase)Axis).ActualFontWeight, ((AxisBase)Axis).ActualTextColor, 0, Axis.AxisTickToLabelDistance);
            renderHelper.RenderTicks(renderContext, location, MinorTicks.AsReadOnlyList(), TickStyle.Outside, Axis.MinorTickSize, Axis.MinorGridlineThickness, Axis.TicklineColor, ((AxisBase)Axis).ActualFont, ((AxisBase)Axis).ActualFontSize, ((AxisBase)Axis).ActualFontWeight, ((AxisBase)Axis).ActualTextColor, 0, Axis.AxisTickToLabelDistance);
        }

        /// <inheritdoc/>
        public override void Update()
        {
            // nix: we need to know how big we are before we can do anything
        }
    }

    /// <summary>
    /// A band that renders axis ticks for color axes.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class ColorTickBand<TData> : BandBase<TData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorTickBand{TData}" /> class.
        /// </summary>
        public ColorTickBand()
        {
            this.BandPosition = BandPosition.Inline;
            this.BandTier = 0;
            this.IsBandVisible = true;

            this.RangeTicks = new List<RangeTick<TData>>();
            this.ColorRangeTicks = new List<ColorRangeTick<TData>>();
        }

        /// <summary>
        /// Gets the ticks rendered by this band.
        /// </summary>
        /// <remarks>
        /// Updated by <see cref="Measure"/>
        /// </remarks>
        private List<RangeTick<TData>> RangeTicks { get; }

        /// <summary>
        /// Gets the ticks rendered by this band.
        /// </summary>
        /// <remarks>
        /// Updated by <see cref="Measure"/>
        /// </remarks>
        public List<ColorRangeTick<TData>> ColorRangeTicks { get; }

        /// <summary>
        /// The axis associated with this band.
        /// </summary>
        /// <remarks>
        /// Updated by <see cref="AssociateAxis(AxisBase)"/>
        /// </remarks>
        protected IColorAxis<TData> ColorAxis { get; private set; }

        /// <inheritdoc/>
        public override void AssociateAxis(AxisBase axis)
        {
            base.AssociateAxis(axis);

            if (axis is IColorAxis<TData> typed)
            {
                ColorAxis = typed;
            }
            else
            {
                throw new InvalidOperationException($"{this.GetType().Name} cannot be associated with an axis of type {axis.GetType().Name}. The axis must implement IColorAxis<{typeof(TData).Name}>.");
            }
        }

        /// <summary>
        /// Updates the <see cref="ColorRangeTicks"/>.
        /// </summary>
        protected virtual void UpdateTicks()
        {
            ColorRangeTicks.Clear();
            var colorHelper = ColorHelperPreparer<TData>.Prepare(ColorAxis);
            colorHelper.ColorTransformation.GetColorRanges(Axis.ClipMinimum, Axis.ClipMaximum, ColorRangeTicks);
        }

        /// <inheritdoc/>
        public override void Measure(IRenderContext renderContext, BandLocation location)
        {
            UpdateTicks();

            var renderHelper = TickRenderHelperPreparer<TData>.PrepareHorizontalVertial(Axis);

            var barWidth = Axis.MajorTickSize;

            Excesses = renderHelper.MeasureColorRangeTicks(renderContext, location, ColorRangeTicks.AsReadOnlyList(), Axis.TickStyle, Axis.MajorTickSize, ColorAxis.LowColor, ColorAxis.HighColor, barWidth);
        }

        /// <inheritdoc/>
        public override void Render(IRenderContext renderContext, BandLocation location)
        {
            var renderHelper = TickRenderHelperPreparer<TData>.PrepareHorizontalVertial(Axis);

            var barWidth = Axis.MajorTickSize;

            renderHelper.RenderColorRangeTicks(renderContext, location, ColorRangeTicks.AsReadOnlyList(), Axis.TickStyle, Axis.MajorTickSize, ColorAxis.LowColor, ColorAxis.HighColor, barWidth);
        }

        /// <inheritdoc/>
        public override void Update()
        {
            // nix
        }
    }

    /// <summary>
    /// Represents a sample.
    /// </summary>
    /// <typeparam name="XData"></typeparam>
    /// <typeparam name="YData"></typeparam>
    public readonly struct DataSample<XData, YData>
    {
        /// <summary>
        /// Creates a sample from values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public DataSample(XData x, YData y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// The X position.
        /// </summary>
        public readonly XData X;

        /// <summary>
        /// The Y position.
        /// </summary>
        public readonly YData Y;
    }

    /// <summary>
    /// Represents a sample.
    /// </summary>
    /// <typeparam name="XData"></typeparam>
    /// <typeparam name="YData"></typeparam>
    /// <typeparam name="VData"></typeparam>
    public readonly struct DataSample<XData, YData, VData>
    {
        /// <summary>
        /// Creates a sample from values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public DataSample(XData x, YData y, VData value)
        {
            X = x;
            Y = y;
            Value = value;
        }

        /// <summary>
        /// Gets the X position.
        /// </summary>
        public XData X { get; }

        /// <summary>
        /// Gets the Y position.
        /// </summary>
        public YData Y { get; }

        /// <summary>
        /// Gets the Value.
        /// </summary>
        public VData Value { get; }
    }

    /// <summary>
    /// Represents a value in Interaction space.
    /// </summary>
    public readonly struct InteractionReal : IComparable<InteractionReal>
    {
        /// <summary>
        /// Initialises a <see cref="InteractionReal"/> with the given value.
        /// </summary>
        /// <param name="value"></param>
        public InteractionReal(double value)
        {
            // it is critical that we don't do any work in the release build, because this whole struct should be erased by the JIT
            //System.Diagnostics.Debug.Assert(!(double.IsNaN(value) || double.IsInfinity(value)), "Value must be finite");

            Value = value;
        }

        /// <summary>
        /// Zero.
        /// </summary>
        public static readonly InteractionReal Zero = new InteractionReal(0);

        /// <summary>
        /// The double value of the <see cref="InteractionReal"/>.
        /// </summary>
        public double Value { get; }

        /// <inheritdoc/>
        public int CompareTo(InteractionReal other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Adds two <see cref="InteractionReal"/> values.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static InteractionReal operator +(InteractionReal l, InteractionReal r)
        {
            return new InteractionReal(l.Value + r.Value);
        }

        /// <summary>
        /// Computes the difference between two <see cref="InteractionReal"/> values.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static InteractionReal operator -(InteractionReal l, InteractionReal r)
        {
            return new InteractionReal(l.Value - r.Value);
        }

        /// <summary>
        /// Scales a <see cref="InteractionReal"/> by a factor.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static InteractionReal operator *(InteractionReal l, double r)
        {
            return new InteractionReal(l.Value * r);
        }

        /// <summary>
        /// Scales a <see cref="InteractionReal"/> by a factor.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static InteractionReal operator /(InteractionReal l, double r)
        {
            return new InteractionReal(l.Value / r);
        }

        /// <summary>
        /// Computes the radio of two <see cref="InteractionReal"/> values.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static double operator /(InteractionReal l, InteractionReal r)
        {
            return l.Value / r.Value;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is InteractionReal other && other.Value == Value;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Compares two <see cref="InteractionReal"/> values for equality.
        /// </summary>
        public static bool operator ==(InteractionReal left, InteractionReal right)
        {
            return left.Value == right.Value;
        }

        /// <summary>
        /// Compares two <see cref="InteractionReal"/> values for inequality.
        /// </summary>
        public static bool operator !=(InteractionReal left, InteractionReal right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Compares two <see cref="InteractionReal"/> values.
        /// </summary>
        public static bool operator >(InteractionReal left, InteractionReal right)
        {
            return left.Value > right.Value;
        }

        /// <summary>
        /// Compares two <see cref="InteractionReal"/> values.
        /// </summary>
        public static bool operator <(InteractionReal left, InteractionReal right)
        {
            return left.Value < right.Value;
        }

        /// <summary>
        /// Compares two <see cref="InteractionReal"/> values.
        /// </summary>
        public static bool operator >=(InteractionReal left, InteractionReal right)
        {
            return left.Value >= right.Value;
        }

        /// <summary>
        /// Compares two <see cref="InteractionReal"/> values.
        /// </summary>
        public static bool operator <=(InteractionReal left, InteractionReal right)
        {
            return left.Value <= right.Value;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Value.ToString();
        }
    }

    /// <summary>
    /// Represents a value in Screen space.
    /// </summary>
    public readonly struct ScreenReal : IComparable<ScreenReal>
    {
        /// <summary>
        /// Initialises a <see cref="ScreenReal"/> with the given value.
        /// </summary>
        /// <param name="value"></param>
        public ScreenReal(double value)
        {
            // it is critical that we don't do any work in the release build, because this whole struct should be erased by the JIT
            //System.Diagnostics.Debug.Assert(!(double.IsNaN(value) || double.IsInfinity(value)), "Value must be finite");

            Value = value;
        }

        /// <summary>
        /// Zero.
        /// </summary>
        public static readonly ScreenReal Zero = new ScreenReal(0);

        /// <summary>
        /// The double value of the <see cref="ScreenReal"/>.
        /// </summary>
        public double Value { get; }

        /// <inheritdoc/>
        public int CompareTo(ScreenReal other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Adds two <see cref="ScreenReal"/> values.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static ScreenReal operator +(ScreenReal l, ScreenReal r)
        {
            return new ScreenReal(l.Value + r.Value);
        }

        /// <summary>
        /// Computes the difference between two <see cref="ScreenReal"/> values.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static ScreenReal operator -(ScreenReal l, ScreenReal r)
        {
            return new ScreenReal(l.Value - r.Value);
        }

        /// <summary>
        /// Scales a <see cref="ScreenReal"/> by a factor.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static ScreenReal operator *(ScreenReal l, double r)
        {
            return new ScreenReal(l.Value * r);
        }

        /// <summary>
        /// Computes the radio of two <see cref="ScreenReal"/> values.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static double operator /(ScreenReal l, ScreenReal r)
        {
            return l.Value / r.Value;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is ScreenReal other && other.Value == Value;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Compares two <see cref="ScreenReal"/> values for equality.
        /// </summary>
        public static bool operator ==(ScreenReal left, ScreenReal right)
        {
            return left.Value == right.Value;
        }

        /// <summary>
        /// Compares two <see cref="ScreenReal"/> values for inequality.
        /// </summary>
        public static bool operator !=(ScreenReal left, ScreenReal right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Compares two <see cref="ScreenReal"/> values.
        /// </summary>
        public static bool operator >(ScreenReal left, ScreenReal right)
        {
            return left.Value > right.Value;
        }

        /// <summary>
        /// Compares two <see cref="ScreenReal"/> values.
        /// </summary>
        public static bool operator <(ScreenReal left, ScreenReal right)
        {
            return left.Value < right.Value;
        }

        /// <summary>
        /// Compares two <see cref="ScreenReal"/> values.
        /// </summary>
        public static bool operator >=(ScreenReal left, ScreenReal right)
        {
            return left.Value >= right.Value;
        }

        /// <summary>
        /// Compares two <see cref="ScreenReal"/> values.
        /// </summary>
        public static bool operator <=(ScreenReal left, ScreenReal right)
        {
            return left.Value <= right.Value;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Value.ToString();
        }
    }

    /// <summary>
    /// Represents a range that may be empty.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Range<T>
    {
        private bool _isNonEmpty;
        private T _min;
        private T _max;

        /// <summary>
        /// Gets the empty range.
        /// </summary>
        public static readonly Range<T> Empty = default;

        /// <summary>
        /// Initialises a range.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public Range(T min, T max) : this()
        {
            _isNonEmpty = true;
            _min = min;
            _max = max;
        }

        /// <summary>
        /// Initialises a zero-width range.
        /// </summary>
        /// <param name="value"></param>
        public Range(T value)
            : this(value, value)
        {
        }

        /// <summary>
        /// Gets a value indiciating whether the range is empty.
        /// </summary>
        public bool IsEmpty => !_isNonEmpty;
        
        /// <summary>
        /// Tries to get the min and max values.
        /// </summary>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns><c>true</c> if the range is not empty.</returns>
        public bool TryGetMinMax(out T min, out T max)
        {
            max = _max;
            min = _min;
            return _isNonEmpty;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[{_min}, {_max}]";
        }
    }
}

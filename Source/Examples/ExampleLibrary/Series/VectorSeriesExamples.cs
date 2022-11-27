﻿namespace ExampleLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;

    [Examples("VectorSeries"), Tags("Series")]
    public static class VectorSeriesExamples
    {
        [Example("VectorSeries")]
        [DocumentationExample("Series/VectorSeries")]
        public static PlotModel FromItems()
        {
            var model = GetModel(true, out _);
            return model;
        }

        [Example("VectorSeries (Veeness = 2)")]
        [DocumentationExample("Series/VectorSeries")]
        public static PlotModel FromItemsVeeness()
        {
            var model = GetModel(true, out var series);
            series.ArrowVeeness = 2;
            return model;
        }

        [Example("VectorSeries (Vector Origin and Label position)")]
        [DocumentationExample("Series/VectorSeries")]
        public static PlotModel FromItemsVectorOriginAndLabelPosition()
        {
            var model = GetModel(true, out var series);
            series.ArrowLabelPosition = 0.25;
            series.ArrowStartPosition = 0.5;
            return model;
        }

        [Example("VectorSeries (without ColorAxis)")]
        [DocumentationExample("Series/VectorSeries")]
        public static PlotModel FromItemsOhneColorAxis()
        {
            var model = GetModel(false, out _);
            return model;
        }

        private static PlotModel GetModel(bool includeColorAxis, out VectorSeries series)
        {
            const int NumberOfItems = 100;
            var model = new PlotModel { Title = "VectorSeries (Veeness = 2)" };

            var rand = new Random(1);
            var w = 100.0;
            var h = 100.0;
            var max = 10.0;

            if (includeColorAxis)
            {
                model.Axes.Add(new LinearColorAxis
                {
                    Position = AxisPosition.Right,
                    Palette = new OxyPalette(OxyPalettes.Cool(10).Colors.Select(c => OxyColor.FromAColor(100, c))),
                    Minimum = 0.0,
                    Maximum = max,
                });
            }

            model.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = -max,
                Maximum = w + max,
            });

            model.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = -max,
                Maximum = h + max,
            });

            series = new VectorSeries() { LabelFontSize = 12 };
            for (int i = NumberOfItems - 1; i >= 0; i--)
            {
                var ang = rand.NextDouble() * Math.PI * 2.0;
                var mag = rand.NextDouble() * max;

                var origin = new DataPoint(rand.NextDouble() * w, rand.NextDouble() * h);
                var direction = new DataVector(Math.Cos(ang) * mag, Math.Sin(ang) * mag);
                series.Items.Add(new VectorItem(origin, direction, mag));
            }

            model.Series.Add(series);

            return model;
        }

        [Example("VectorSeries on Log Axis")]
        [DocumentationExample("Series/VectorSeries")]
        public static PlotModel LogarithmicYAxis()
        {
            const int NumberOfItems = 100;
            var model = new PlotModel { Title = "VectorSeries" };

            var rand = new Random(1);
            var w = 100.0;
            var h = 100.0;
            var max = 50.0;

            model.Axes.Add(new LinearColorAxis
            {
                Position = AxisPosition.Right,
                Palette = OxyPalettes.Cool(10),
                Minimum = 0.0,
                Maximum = max,
            });

            model.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = -max,
                Maximum = w + max,
            });
            model.Axes.Add(new LogarithmicAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 1,
                Maximum = h + max,
            });

            var s = new VectorSeries() { LabelFontSize = 12 };
            for (int i = NumberOfItems - 1; i >= 0; i--)
            {
                var ang = rand.NextDouble() * Math.PI * 2.0;
                var mag = rand.NextDouble() * max;

                var origin = new DataPoint(rand.NextDouble() * w, rand.NextDouble() * h + 1);
                var direction = new DataVector(Math.Cos(ang) * mag, Math.Sin(ang) * mag);
                s.Items.Add(new VectorItem(origin, direction, mag));
            }

            model.Series.Add(s);

            return model;
        }
    }
}

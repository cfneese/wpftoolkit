﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Windows.Controls.Primitives;
using Microsoft.Windows.Controls.Core.Utilities;

namespace Microsoft.Windows.Controls
{
    public class ColorCanvas : Control
    {
        #region Private Members

        private TranslateTransform _colorShadeSelectorTransform = new TranslateTransform();
        private Canvas _colorShadingCanvas;
        private Canvas _colorShadeSelector;
        private ColorSpectrumSlider _spectrumSlider;
        private Point? _currentColorPosition;

        #endregion //Private Members

        #region Properties

        #region SelectedColor

        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorCanvas), new UIPropertyMetadata(Colors.White, OnSelectedColorChanged));
        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        private static void OnSelectedColorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnSelectedColorChanged((Color)e.OldValue, (Color)e.NewValue);
        }

        protected virtual void OnSelectedColorChanged(Color oldValue, Color newValue)
        {
            UpdateRGBValues(newValue);
        }

        #endregion //SelectedColor

        #region RGB

        #region A

        public static readonly DependencyProperty AProperty = DependencyProperty.Register("A", typeof(byte), typeof(ColorCanvas), new UIPropertyMetadata((byte)255, new PropertyChangedCallback(OnAChanged), new CoerceValueCallback(OnCoerceA)));

        private static object OnCoerceA(DependencyObject o, object value)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                return colorCanvas.OnCoerceA((byte)value);
            else
                return value;
        }

        private static void OnAChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnAChanged((byte)e.OldValue, (byte)e.NewValue);
        }

        protected virtual byte OnCoerceA(byte value)
        {
            // TODO: Keep the proposed value within the desired range.
            return value;
        }

        protected virtual void OnAChanged(byte oldValue, byte newValue)
        {
            UpdateRGBColor();
        }

        public byte A
        {
            // IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
            get
            {
                return (byte)GetValue(AProperty);
            }
            set
            {
                SetValue(AProperty, value);
            }
        }

        #endregion //A

        #region R

        public static readonly DependencyProperty RProperty = DependencyProperty.Register("R", typeof(byte), typeof(ColorCanvas), new UIPropertyMetadata((byte)255, new PropertyChangedCallback(OnRChanged), new CoerceValueCallback(OnCoerceR)));

        private static object OnCoerceR(DependencyObject o, object value)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                return colorCanvas.OnCoerceR((byte)value);
            else
                return value;
        }

        private static void OnRChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnRChanged((byte)e.OldValue, (byte)e.NewValue);
        }

        protected virtual byte OnCoerceR(byte value)
        {
            // TODO: Keep the proposed value within the desired range.
            return value;
        }

        protected virtual void OnRChanged(byte oldValue, byte newValue)
        {
            UpdateRGBColor();
        }

        public byte R
        {
            // IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
            get
            {
                return (byte)GetValue(RProperty);
            }
            set
            {
                SetValue(RProperty, value);
            }
        }


        #endregion //R

        #region G

        public static readonly DependencyProperty GProperty = DependencyProperty.Register("G", typeof(byte), typeof(ColorCanvas), new UIPropertyMetadata((byte)255, new PropertyChangedCallback(OnGChanged), new CoerceValueCallback(OnCoerceG)));

        private static object OnCoerceG(DependencyObject o, object value)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                return colorCanvas.OnCoerceG((byte)value);
            else
                return value;
        }

        private static void OnGChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnGChanged((byte)e.OldValue, (byte)e.NewValue);
        }

        protected virtual byte OnCoerceG(byte value)
        {
            // TODO: Keep the proposed value within the desired range.
            return value;
        }

        protected virtual void OnGChanged(byte oldValue, byte newValue)
        {
            UpdateRGBColor();
        }

        public byte G
        {
            // IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
            get
            {
                return (byte)GetValue(GProperty);
            }
            set
            {
                SetValue(GProperty, value);
            }
        }


        #endregion //G

        #region B

        public static readonly DependencyProperty BProperty = DependencyProperty.Register("B", typeof(byte), typeof(ColorCanvas), new UIPropertyMetadata((byte)255, new PropertyChangedCallback(OnBChanged), new CoerceValueCallback(OnCoerceB)));

        private static object OnCoerceB(DependencyObject o, object value)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                return colorCanvas.OnCoerceB((byte)value);
            else
                return value;
        }

        private static void OnBChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnBChanged((byte)e.OldValue, (byte)e.NewValue);
        }

        protected virtual byte OnCoerceB(byte value)
        {
            // TODO: Keep the proposed value within the desired range.
            return value;
        }

        protected virtual void OnBChanged(byte oldValue, byte newValue)
        {
            UpdateRGBColor();
        }

        public byte B
        {
            // IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
            get
            {
                return (byte)GetValue(BProperty);
            }
            set
            {
                SetValue(BProperty, value);
            }
        }


        #endregion //B

        #endregion //RGB

        #region HexadecimalString

        public static readonly DependencyProperty HexadecimalStringProperty = DependencyProperty.Register("HexadecimalString", typeof(string), typeof(ColorCanvas), new PropertyMetadata("#FFFFFFFF", new PropertyChangedCallback(OnHexadecimalStringPropertyChanged)));
        public string HexadecimalString
        {
            get { return (string)GetValue(HexadecimalStringProperty); }
            set { SetValue(HexadecimalStringProperty, value); }
        }

        private static void OnHexadecimalStringPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion //HexadecimalString

        #endregion //Properties

        #region Constructors

        static ColorCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorCanvas), new FrameworkPropertyMetadata(typeof(ColorCanvas)));
        }

        #endregion //Constructors

        #region Base Class Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _colorShadingCanvas = (Canvas)GetTemplateChild("PART_ColorShadingCanvas");
            _colorShadingCanvas.MouseLeftButtonDown += ColorShadingCanvas_MouseLeftButtonDown;
            _colorShadingCanvas.MouseMove += ColorShadingCanvas_MouseMove;
            _colorShadingCanvas.SizeChanged += ColorShadingCanvas_SizeChanged;

            _colorShadeSelector = (Canvas)GetTemplateChild("PART_ColorShadeSelector");
            _colorShadeSelector.RenderTransform = _colorShadeSelectorTransform;

            _spectrumSlider = (ColorSpectrumSlider)GetTemplateChild("PART_SpectrumSlider");
            _spectrumSlider.ValueChanged += SpectrumSlider_ValueChanged;

            SetSelectedColorAndPositionSelector(SelectedColor);
        }

        #endregion //Base Class Overrides

        #region Event Handlers

        void ColorShadingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(_colorShadingCanvas);
            UpdateColorShadeSelectorPositionAndCalculateColor(p, true);
        }

        void ColorShadingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(_colorShadingCanvas);
                UpdateColorShadeSelectorPositionAndCalculateColor(p, true);
                Mouse.Synchronize();
            }
        }

        void ColorShadingCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_currentColorPosition != null)
            {
                Point _newPoint = new Point
                {
                    X = ((Point)_currentColorPosition).X * e.NewSize.Width,
                    Y = ((Point)_currentColorPosition).Y * e.NewSize.Height
                };

                UpdateColorShadeSelectorPositionAndCalculateColor(_newPoint, false);
            }
        }

        void SpectrumSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_currentColorPosition != null)
            {
                CalculateColor((Point)_currentColorPosition);
            }
        }

        #endregion //Event Handlers

        #region Methods

        private void UpdateRGBColor()
        {
            SelectedColor = Color.FromArgb(A, R, G, B);
            UpdateColorShadeSelectorPosition(SelectedColor);
        }

        private void UpdateSelectedColor(Color color)
        {
            SelectedColor = Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        private void UpdateRGBValues(Color color)
        {
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
        }

        private void SetSelectedColorAndPositionSelector(Color color)
        {
            UpdateSelectedColor(color);
            UpdateRGBValues(color);
            UpdateColorShadeSelectorPosition(color);
        }

        private void UpdateColorShadeSelectorPositionAndCalculateColor(Point p, bool calculateColor)
        {
            if (p.Y < 0)
                p.Y = 0;

            if (p.X < 0)
                p.X = 0;

            if (p.X > _colorShadingCanvas.ActualWidth)
                p.X = _colorShadingCanvas.ActualWidth;

            if (p.Y > _colorShadingCanvas.ActualHeight)
                p.Y = _colorShadingCanvas.ActualHeight;

            _colorShadeSelectorTransform.X = p.X - (_colorShadeSelector.Width / 2);
            _colorShadeSelectorTransform.Y = p.Y - (_colorShadeSelector.Height / 2);

            p.X = p.X / _colorShadingCanvas.ActualWidth;
            p.Y = p.Y / _colorShadingCanvas.ActualHeight;

            _currentColorPosition = p;

            if (calculateColor)
                CalculateColor(p);
        }

        private void UpdateColorShadeSelectorPosition(Color color)
        {
            if (_spectrumSlider == null || _colorShadingCanvas == null)
                return;

            _currentColorPosition = null;

            HsvColor hsv = ColorUtilities.ConvertRgbToHsv(color.R, color.G, color.B);
            _spectrumSlider.Value = hsv.H;

            Point p = new Point(hsv.S, 1 - hsv.V);

            _currentColorPosition = p;

            _colorShadeSelectorTransform.X = (p.X * _colorShadingCanvas.Width) - 5;
            _colorShadeSelectorTransform.Y = (p.Y * _colorShadingCanvas.Height) - 5;
        }

        private void CalculateColor(Point p)
        {
            HsvColor hsv = new HsvColor(360 - _spectrumSlider.Value, 1, 1) { S = p.X, V = 1 - p.Y };
            var currentColor = ColorUtilities.ConvertHsvToRgb(hsv.H, hsv.S, hsv.V);
            currentColor.A = A;
            SelectedColor = currentColor;
            HexadecimalString = SelectedColor.ToString();
        }

        #endregion //Methods
    }
}

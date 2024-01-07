using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EyKettlesControlLibrary
{
    public class ButtonControl : ItemsControl
    {
        public enum ImageScaleMode
        {
            Fill,
            Fit,
            Stretch,
            Center
        }
        public enum AlignmentMode
        {
            Center,
            Left,
            Right,
            Top,
            Bottom,
            BottomLeft,
            BottomRight,
            TopLeft,
            TopRight
        }
        public enum ScaleAlignment
        {
            Center,
            Left,
            Right,
            Top,
            Bottom,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        // 整体属性
        public static readonly DependencyProperty CanActProperty =
            DependencyProperty.Register("CanAct", typeof(bool), typeof(ButtonControl), new PropertyMetadata(true));

        private static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(double), typeof(ButtonControl), new PropertyMetadata(1d, OnSpaceChanged));

        public static readonly DependencyProperty SizeAlignmentProperty =
            DependencyProperty.Register("SizeAlignment", typeof(ScaleAlignment), typeof(ButtonControl), new PropertyMetadata(ScaleAlignment.Center));

        private static readonly DependencyProperty SpaceHeightProperty =
            DependencyProperty.Register("SpaceHeight", typeof(double), typeof(ButtonControl), new PropertyMetadata(0d, OnSpaceChanged));

        private static readonly DependencyProperty DisabledSpaceHeightProperty =
            DependencyProperty.Register("DisabledSpaceHeight", typeof(double?), typeof(ButtonControl), new PropertyMetadata(null, OnPropertyChanged));

        private static readonly DependencyProperty ShadowColorProperty =
            DependencyProperty.Register("ShadowColor", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(Brushes.Transparent, OnPropertyChanged));

        private static readonly DependencyProperty DisabledShadowColorProperty =
            DependencyProperty.Register("DisabledShadowColor", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(Brushes.Transparent, OnPropertyChanged));

        // 缓动属性
        public static readonly DependencyProperty EnterDurationProperty =
            DependencyProperty.Register("EnterDuration", typeof(TimeSpan), typeof(ButtonControl), new PropertyMetadata(TimeSpan.FromSeconds(0.12)));

        public static readonly DependencyProperty LeaveDurationProperty =
            DependencyProperty.Register("LeaveDuration", typeof(TimeSpan), typeof(ButtonControl), new PropertyMetadata(TimeSpan.FromSeconds(0.3)));

        public static readonly DependencyProperty DownDurationProperty =
            DependencyProperty.Register("DownDuration", typeof(TimeSpan), typeof(ButtonControl), new PropertyMetadata(TimeSpan.FromSeconds(0.12)));

        public static readonly DependencyProperty UpDurationProperty =
            DependencyProperty.Register("UpDuration", typeof(TimeSpan), typeof(ButtonControl), new PropertyMetadata(TimeSpan.FromSeconds(0.16)));

        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(ButtonControl), new PropertyMetadata(new CubicEase { EasingMode = EasingMode.EaseOut }));

        // 整体动画
        public static readonly DependencyProperty DefaultSizeProperty =
            DependencyProperty.Register("DefaultSize", typeof(double), typeof(ButtonControl), new PropertyMetadata(1d));

        public static readonly DependencyProperty EnterSizeProperty =
            DependencyProperty.Register("EnterSize", typeof(double?), typeof(ButtonControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DownSizeProperty =
            DependencyProperty.Register("DownSize", typeof(double?), typeof(ButtonControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DefaultSpaceHeightProperty =
            DependencyProperty.Register("DefaultSpaceHeight", typeof(double), typeof(ButtonControl), new PropertyMetadata(0d, OnDefaultSpaceHeightChanged));

        public static readonly DependencyProperty EnterSpaceHeightProperty =
            DependencyProperty.Register("EnterSpaceHeight", typeof(double?), typeof(ButtonControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DownSpaceHeightProperty =
            DependencyProperty.Register("DownSpaceHeight", typeof(double?), typeof(ButtonControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DefaultShadowColorProperty =
            DependencyProperty.Register("DefaultShadowColor", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(Brushes.Transparent, OnDefaultShadowColorChanged));

        public static readonly DependencyProperty EnterShadowColorProperty =
            DependencyProperty.Register("EnterShadowColor", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DownShadowColorProperty =
            DependencyProperty.Register("DownShadowColor", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(null));

        // 文本属性
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(ButtonControl), new PropertyMetadata(null, OnPropertyChanged));
        
        private new static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(Brushes.Black, OnPropertyChanged));

        public static readonly DependencyProperty ContentAlignmentModeProperty =
            DependencyProperty.Register("ContentAlignment", typeof(AlignmentMode), typeof(ButtonControl), new PropertyMetadata(AlignmentMode.Center, OnPropertyChanged));

        public static readonly DependencyProperty ContentPaddingProperty =
            DependencyProperty.Register("ContentPadding", typeof(Thickness), typeof(ButtonControl), new PropertyMetadata(new Thickness(12), OnPropertyChanged));

        public static readonly DependencyProperty DisabledForegroundProperty =
            DependencyProperty.Register("DisabledForeground", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(null, OnPropertyChanged));

        // 文本动画
        public static readonly DependencyProperty DefaultForegroundProperty =
            DependencyProperty.Register("DefaultForeground", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(Brushes.Black, OnDefaultForegroundChanged));

        public static readonly DependencyProperty EnterForegroundProperty =
            DependencyProperty.Register("EnterForeground", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DownForegroundProperty =
            DependencyProperty.Register("DownForeground", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(null));

        // 图像属性
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ButtonControl), new PropertyMetadata(OnPropertyChanged));

        public static readonly DependencyProperty ImageScaleModeProperty =
            DependencyProperty.Register("ImageScaleMode", typeof(ImageScaleMode), typeof(ButtonControl), new PropertyMetadata(ImageScaleMode.Fit, OnPropertyChanged));

        // 开关属性
        public static readonly DependencyProperty IsSwitchProperty =
            DependencyProperty.Register("IsSwitch", typeof(bool), typeof(ButtonControl), new PropertyMetadata(false));

        public static readonly DependencyProperty SwitchCanClickProperty =
            DependencyProperty.Register("SwitchCanClick", typeof(bool), typeof(ButtonControl), new PropertyMetadata(true));

        // 背景图形
        private new static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(Brushes.Transparent, OnPropertyChanged));

        public static readonly DependencyProperty DisabledBackgroundProperty =
            DependencyProperty.Register("DisabledBackground", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(null, OnPropertyChanged));

        private new static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(Brushes.Transparent, OnPropertyChanged));

        public static readonly DependencyProperty DisabledBorderBrushProperty =
            DependencyProperty.Register("DisabledBorderBrush", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(null, OnPropertyChanged));

        private new static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(ButtonControl), new PropertyMetadata(new Thickness(0), OnPropertyChanged));

        public static readonly DependencyProperty DisabledThicknessProperty =
            DependencyProperty.Register("DisabledThickness", typeof(double?), typeof(ButtonControl), new PropertyMetadata(null, OnPropertyChanged));

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ButtonControl), new PropertyMetadata(new CornerRadius(), OnPropertyChanged));

        public static readonly DependencyProperty BackgroundClippedProperty =
            DependencyProperty.Register("BackgroundClipped", typeof(bool), typeof(ButtonControl), new PropertyMetadata(false));

        public static readonly DependencyProperty BorderFixedProperty =
            DependencyProperty.Register("BorderFixed", typeof(bool), typeof(ButtonControl), new PropertyMetadata(true));

        // 背景动画
        public static readonly DependencyProperty DefaultBackgroundProperty =
            DependencyProperty.Register("DefaultBackground", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(Brushes.Transparent, OnDefaultBackgroundChanged));

        public static readonly DependencyProperty EnterBackgroundProperty =
            DependencyProperty.Register("EnterBackground", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DownBackgroundProperty =
            DependencyProperty.Register("DownBackground", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(null));

        // 描边动画
        public static readonly DependencyProperty DefaultBorderBrushProperty =
            DependencyProperty.Register("DefaultBorderBrush", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(Brushes.Transparent, OnDefaultBorderBrushChanged));

        public static readonly DependencyProperty EnterBorderBrushProperty =
            DependencyProperty.Register("EnterBorderBrush", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DownBorderBrushProperty =
            DependencyProperty.Register("DownBorderBrush", typeof(Brush), typeof(ButtonControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DefaultThicknessProperty =
            DependencyProperty.Register("DefaultThickness", typeof(double), typeof(ButtonControl), new PropertyMetadata(0d, OnDefaultThicknessChanged));

        public static readonly DependencyProperty EnterThicknessProperty =
            DependencyProperty.Register("EnterThickness", typeof(double?), typeof(ButtonControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DownThicknessProperty =
            DependencyProperty.Register("DownThickness", typeof(double?), typeof(ButtonControl), new PropertyMetadata(null));

        public event EventHandler? Click;
        public event EventHandler? IsOnChanged;
        private bool _isOn;

        // 装饰
        private readonly Adorner focusBox;

        // 颜色动画
        SolidColorBrush animatedBackground = Brushes.Transparent;
        SolidColorBrush animatedForeground = Brushes.Transparent;
        SolidColorBrush animatedBorderBrush = Brushes.Transparent;
        SolidColorBrush animatedShadowColor = Brushes.Transparent;

        // 整体属性
        public bool CanAct
        {
            get => (bool)GetValue(CanActProperty);
            set => SetValue(CanActProperty, value);
        }
        private double GetSize() => (double)GetValue(SizeProperty);
        private void SetSize(double value) => SetValue(SizeProperty, value);
        public ScaleAlignment SizeAlignment
        {
            get => (ScaleAlignment)GetValue(SizeAlignmentProperty);
            set => SetValue(SizeAlignmentProperty, value);
        }
        private double SpaceHeight
        {
            get => (double)GetValue(SpaceHeightProperty);
            set => SetValue(SpaceHeightProperty, value);
        }
        private double? DisabledSpaceHeight
        {
            get => (double?)GetValue(DisabledSpaceHeightProperty);
            set => SetValue(DisabledSpaceHeightProperty, value);
        }
        private Brush ShadowColor
        {
            get => (Brush)GetValue(ShadowColorProperty);
            set => SetValue(ShadowColorProperty, value);
        }
        private Brush DisabledShadowColor
        {
            get => (Brush)GetValue(DisabledShadowColorProperty);
            set => SetValue(DisabledShadowColorProperty, value);
        }

        // 缓动属性
        public TimeSpan EnterDuration
        {
            get => (TimeSpan)GetValue(EnterDurationProperty);
            set => SetValue(EnterDurationProperty, value);
        }
        public TimeSpan LeaveDuration
        {
            get => (TimeSpan)GetValue(LeaveDurationProperty);
            set => SetValue(LeaveDurationProperty, value);
        }
        public TimeSpan DownDuration
        {
            get => (TimeSpan)GetValue(DownDurationProperty);
            set => SetValue(DownDurationProperty, value);
        }
        public TimeSpan UpDuration
        {
            get => (TimeSpan)GetValue(UpDurationProperty);
            set => SetValue(UpDurationProperty, value);
        }
        public IEasingFunction EasingFunction
        {
            get => (IEasingFunction)GetValue(EasingFunctionProperty);
            set => SetValue(EasingFunctionProperty, value);
        }

        // 整体动画
        public double DefaultSize
        {
            get => (double)GetValue(DefaultSizeProperty);
            set
            {
                SetValue(DefaultSizeProperty, value);
                InvalidateVisual();
            }
        }
        public double? EnterSize
        {
            get => (double?)GetValue(EnterSizeProperty);
            set => SetValue(EnterSizeProperty, value);
        }
        public double? DownSize
        {
            get => (double?)GetValue(DownSizeProperty);
            set => SetValue(DownSizeProperty, value);
        }
        public double DefaultSpaceHeight
        {
            get => (double)GetValue(DefaultSpaceHeightProperty);
            set => SetValue(DefaultSpaceHeightProperty, value);
        }
        public double? EnterSpaceHeight
        {
            get => (double?)GetValue(EnterSpaceHeightProperty);
            set => SetValue(EnterSpaceHeightProperty, value);
        }
        public double? DownSpaceHeight
        {
            get => (double?)GetValue(DownSpaceHeightProperty);
            set => SetValue(DownSpaceHeightProperty, value);
        }
        public Brush DefaultShadowColor
        {
            get => (Brush)GetValue(DefaultShadowColorProperty);
            set => SetValue(DefaultShadowColorProperty, value);
        }
        public Brush EnterShadowColor
        {
            get => (Brush)GetValue(EnterShadowColorProperty);
            set => SetValue(EnterShadowColorProperty, value);
        }
        public Brush DownShadowColor
        {
            get => (Brush)GetValue(DownShadowColorProperty);
            set => SetValue(DownShadowColorProperty, value);
        }

        // 文本属性
        private new Brush Foreground
        {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }
        public string Content
        {
            get => (string)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }
        public AlignmentMode ContentAlignment
        {
            get => (AlignmentMode)GetValue(ContentAlignmentModeProperty);
            set => SetValue(ContentAlignmentModeProperty, value);
        }
        public Thickness ContentPadding
        {
            get => (Thickness)GetValue(ContentPaddingProperty);
            set => SetValue(ContentPaddingProperty, value);
        }
        public Brush DisabledForeground
        {
            get => (Brush)GetValue(DisabledForegroundProperty);
            set => SetValue(DisabledForegroundProperty, value);
        }

        // 文本动画
        public Brush DefaultForeground
        {
            get => (Brush)GetValue(DefaultForegroundProperty);
            set => SetValue(DefaultForegroundProperty, value);
        }
        public Brush EnterForeground
        {
            get => (Brush)GetValue(EnterForegroundProperty);
            set => SetValue(EnterForegroundProperty, value);
        }
        public Brush DownForeground
        {
            get => (Brush)GetValue(DownForegroundProperty);
            set => SetValue(DownForegroundProperty, value);
        }

        // 图像属性
        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set
            {
                SetValue(ImageSourceProperty, value);
                InvalidateVisual();
            }
        }
        public ImageScaleMode ImageScale
        {
            get => (ImageScaleMode)GetValue(ImageScaleModeProperty);
            set => SetValue(ImageScaleModeProperty, value);
        }

        // 开关属性
        public bool IsSwitch
        {
            get => (bool)GetValue(IsSwitchProperty);
            set => SetValue(IsSwitchProperty, value);
        }
        public bool SwitchCanClick
        {
            get => (bool)GetValue(SwitchCanClickProperty);
            set => SetValue(SwitchCanClickProperty, value);
        }

        // 背景图形
        private new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }
        public Brush DisabledBackground
        {
            get => (Brush)GetValue(DisabledBackgroundProperty);
            set => SetValue(DisabledBackgroundProperty, value);
        }
        private new Brush BorderBrush
        {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }
        public Brush DisabledBorderBrush
        {
            get => (Brush)GetValue(DisabledBorderBrushProperty);
            set => SetValue(DisabledBorderBrushProperty, value);
        }
        private new Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
        public double? DisabledThickness
        {
            get => (double?)GetValue(DisabledThicknessProperty);
            set => SetValue(DisabledThicknessProperty, value);
        }
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        public bool BackgroundClipped
        {
            get => (bool)GetValue(BackgroundClippedProperty);
            set => SetValue(BackgroundClippedProperty, value);
        }
        public bool BorderFixed
        {
            get => (bool)GetValue(BorderFixedProperty);
            set => SetValue(BorderFixedProperty, value);
        }

        // 背景动画
        public Brush DefaultBackground
        {
            get => (Brush)GetValue(DefaultBackgroundProperty);
            set => SetValue(DefaultBackgroundProperty, value);
        }
        public Brush EnterBackground
        {
            get => (Brush)GetValue(EnterBackgroundProperty);
            set => SetValue(EnterBackgroundProperty, value);
        }
        public Brush DownBackground
        {
            get => (Brush)GetValue(DownBackgroundProperty);
            set => SetValue(DownBackgroundProperty, value);
        }

        // 描边动画
        public Brush DefaultBorderBrush
        {
            get => (Brush)GetValue(DefaultBorderBrushProperty);
            set => SetValue(DefaultBorderBrushProperty, value);
        }
        public Brush EnterBorderBrush
        {
            get => (Brush)GetValue(EnterBorderBrushProperty);
            set => SetValue(EnterBorderBrushProperty, value);
        }
        public Brush DownBorderBrush
        {
            get => (Brush)GetValue(DownBorderBrushProperty);
            set => SetValue(DownBorderBrushProperty, value);
        }
        public double DefaultThickness
        {
            get => (double)GetValue(DefaultThicknessProperty);
            set => SetValue(DefaultThicknessProperty, value);
        }
        public double? EnterThickness
        {
            get => (double?)GetValue(EnterThicknessProperty);
            set => SetValue(EnterThicknessProperty, value);
        }
        public double? DownThickness
        {
            get => (double?)GetValue(DownThicknessProperty);
            set => SetValue(DownThicknessProperty, value);
        }

        // 开关判断
        private void OnIsOnChanged(object? sender, EventArgs e)
        {
            if (_isOn)
            {
                MouseDownAnimation();
            }
            else
            {
                MouseLeaveAnimation(DownDuration);
            }
        }
        public bool IsOn
        {
            get => _isOn;
            set
            {
                if (IsEnabled && _isOn != value)
                {
                    _isOn = value;
                    OnIsOnChanged(this, EventArgs.Empty);
                    IsOnChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        // 初始化
        public ButtonControl()
        {
            FocusVisualStyle = null;
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;

            // 装饰
            focusBox = new FocusAdorner(this);

            MouseEnter += ControlMouseEnter;
            MouseLeave += ControlMouseLeave;
            MouseDown += ControlMouseDown;
            MouseUp += ControlMouseUp;
            GotFocus += ControlGotFocus;
            LostFocus += ControlLostFocus;
            PreviewKeyDown += ControlPreviewKeyDown;
            PreviewKeyUp += ControlPreviewKeyUp;
            IsEnabledChanged += ControlEnableChanged;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            SetSize(DefaultSize);
            // 色彩绑定
            ColorUnfreeze();

            if (GetTemplateChild("PART_Image") is Image Image)
            {
                if (Image != null)
                {
                    Image.Source = ImageSource;
                }
            }

            // 触发开关
            if (!IsEnabled) ControlEnableChanged(this, new());
            else if (IsOn) OnIsOnChanged(this, EventArgs.Empty);
        }
        protected virtual void OnClick()
        {
            if (CanAct == false) return;
            if (IsEnabled)
            {
                if (IsSwitch) { _isOn = !_isOn; IsOnChanged?.Invoke(this, EventArgs.Empty); }
                Click?.Invoke(this, EventArgs.Empty);
            }
        }
        private void ColorUnfreeze()
        {
            animatedBackground = new(((SolidColorBrush)Background).Color);
            animatedForeground = new(((SolidColorBrush)Foreground).Color);
            animatedBorderBrush = new(((SolidColorBrush)BorderBrush).Color);
            animatedShadowColor = new(((SolidColorBrush)ShadowColor).Color);
        }
        // 动画程序
        private void MouseEnterAnimation()
        {
            ColorUnfreeze();
            if (EnterBackground != null)
            {
                Background = animatedBackground;
                ColorAnimation backgroundAnimation = new()
                {
                    To = ((SolidColorBrush)EnterBackground).Color,
                    Duration = EnterDuration,
                    EasingFunction = EasingFunction
                };
                animatedBackground.BeginAnimation(SolidColorBrush.ColorProperty, backgroundAnimation);
            }

            if (EnterForeground != null)
            {
                Foreground = animatedForeground;
                ColorAnimation foregroundAnimation = new()
                {
                    To = ((SolidColorBrush)EnterForeground).Color,
                    Duration = EnterDuration,
                    EasingFunction = EasingFunction
                };
                animatedForeground.BeginAnimation(SolidColorBrush.ColorProperty, foregroundAnimation);
            }

            if (EnterBorderBrush != null)
            {
                BorderBrush = animatedBorderBrush;
                ColorAnimation BorderBrushAnimation = new()
                {
                    To = ((SolidColorBrush)EnterBorderBrush).Color,
                    Duration = EnterDuration,
                    EasingFunction = EasingFunction
                };
                animatedBorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, BorderBrushAnimation);
            }

            if (EnterThickness != null)
            {
                ThicknessAnimation thicknessAnimation = new()
                {
                    To = new Thickness((double)EnterThickness),
                    Duration = EnterDuration,
                    EasingFunction = EasingFunction
                };
                BeginAnimation(BorderThicknessProperty, thicknessAnimation);
            }

            if (EnterSize != null)
            {
                DoubleAnimation sizeAnimation = new()
                {
                    To = EnterSize,
                    Duration = EnterDuration,
                    EasingFunction = EasingFunction
                };
                BeginAnimation(SizeProperty, sizeAnimation);
            }

            if (EnterSpaceHeight != null)
            {
                DoubleAnimation spaceHeight = new()
                {
                    To = EnterSpaceHeight,
                    Duration = EnterDuration,
                    EasingFunction = EasingFunction
                };
                BeginAnimation(SpaceHeightProperty, spaceHeight);
            }

            if (EnterShadowColor != null)
            {
                ShadowColor = animatedShadowColor;
                ColorAnimation shadowColor = new()
                {
                    To = ((SolidColorBrush)EnterShadowColor).Color,
                    Duration = EnterDuration,
                    EasingFunction = EasingFunction
                };
                animatedShadowColor.BeginAnimation(SolidColorBrush.ColorProperty, shadowColor);
            }
        }
        private void MouseLeaveAnimation(Duration? duration = null)
        {
            ColorUnfreeze();
            if (EnterBackground != null || DownBackground != null)
            {
                Background = animatedBackground;
                ColorAnimation backgroundAnimation = new()
                {
                    To = ((SolidColorBrush)DefaultBackground).Color,
                    Duration = duration != null ? (Duration)duration : LeaveDuration,
                    EasingFunction = EasingFunction
                };
                animatedBackground.BeginAnimation(SolidColorBrush.ColorProperty, backgroundAnimation);
            }

            if (EnterForeground != null || DownForeground != null)
            {
                Foreground = animatedForeground;
                ColorAnimation foregroundAnimation = new()
                {
                    To = ((SolidColorBrush)DefaultForeground).Color,
                    Duration = duration != null ? (Duration)duration : LeaveDuration,
                    EasingFunction = EasingFunction
                };
                animatedForeground.BeginAnimation(SolidColorBrush.ColorProperty, foregroundAnimation);
            }

            if (EnterBorderBrush != null || DownBorderBrush != null)
            {
                BorderBrush = animatedBorderBrush;
                ColorAnimation BorderBrushAnimation = new()
                {
                    To = ((SolidColorBrush)DefaultBorderBrush).Color,
                    Duration = duration != null ? (Duration)duration : LeaveDuration,
                    EasingFunction = EasingFunction
                };
                animatedBorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, BorderBrushAnimation);
            }

            if (EnterThickness != null || DownThickness != null)
            {
                ThicknessAnimation thicknessAnimation = new()
                {
                    To = new Thickness((double)DefaultThickness),
                    Duration = duration != null ? (Duration)duration : LeaveDuration,
                    EasingFunction = EasingFunction
                };
                BeginAnimation(BorderThicknessProperty, thicknessAnimation);
            }

            if (EnterSize != null || DownSize != null)
            {
                DoubleAnimation sizeAnimation = new()
                {
                    To = DefaultSize,
                    Duration = duration != null ? (Duration)duration : LeaveDuration,
                    EasingFunction = EasingFunction
                };
                BeginAnimation(SizeProperty, sizeAnimation);
            }

            if (EnterSpaceHeight != null || DownSpaceHeight != null)
            {
                DoubleAnimation spaceHeight = new()
                {
                    To = DefaultSpaceHeight,
                    Duration = duration != null ? (Duration)duration : LeaveDuration,
                    EasingFunction = EasingFunction
                };
                BeginAnimation(SpaceHeightProperty, spaceHeight);
            }

            if (EnterShadowColor != null || DownShadowColor != null)
            {
                ShadowColor = animatedShadowColor;
                ColorAnimation shadowColor = new()
                {
                    To = ((SolidColorBrush)DefaultShadowColor).Color,
                    Duration = duration != null ? (Duration)duration : LeaveDuration,
                    EasingFunction = EasingFunction
                };
                animatedShadowColor.BeginAnimation(SolidColorBrush.ColorProperty, shadowColor);
            }
        }
        private void MouseDownAnimation(Duration? duration = null)
        {
            ColorUnfreeze();
            if (DownBackground != null)
            {
                Background = animatedBackground;
                ColorAnimation backgroundAnimation = new()
                {
                    To = ((SolidColorBrush)DownBackground).Color,
                    Duration = duration != null ? (Duration)duration : DownDuration,
                    EasingFunction = EasingFunction
                };
                animatedBackground.BeginAnimation(SolidColorBrush.ColorProperty, backgroundAnimation);
            }

            if (DownForeground != null)
            {
                Foreground = animatedForeground;
                ColorAnimation foregroundAnimation = new()
                {
                    To = ((SolidColorBrush)DownForeground).Color,
                    Duration = duration != null ? (Duration)duration : DownDuration,
                    EasingFunction = EasingFunction
                };
                animatedForeground.BeginAnimation(SolidColorBrush.ColorProperty, foregroundAnimation);
            }

            if (DownBorderBrush != null)
            {
                BorderBrush = animatedBorderBrush;
                ColorAnimation BorderBrushAnimation = new()
                {
                    To = ((SolidColorBrush)DownBorderBrush).Color,
                    Duration = duration != null ? (Duration)duration : DownDuration,
                    EasingFunction = EasingFunction
                };
                animatedBorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, BorderBrushAnimation);
            }

            if (DownThickness != null)
            {
                ThicknessAnimation thicknessAnimation = new()
                {
                    To = new Thickness((double)DownThickness),
                    Duration = duration != null ? (Duration)duration : DownDuration,
                    EasingFunction = EasingFunction
                };
                BeginAnimation(BorderThicknessProperty, thicknessAnimation);
            }

            if (DownSize != null)
            {
                DoubleAnimation sizeAnimation = new()
                {
                    To = DownSize,
                    Duration = duration != null ? (Duration)duration : DownDuration,
                    EasingFunction = EasingFunction
                };
                BeginAnimation(SizeProperty, sizeAnimation);
            }

            if (DownSpaceHeight != null)
            {
                DoubleAnimation spaceHeight = new()
                {
                    To = DownSpaceHeight,
                    Duration = duration != null ? (Duration)duration : DownDuration,
                    EasingFunction = EasingFunction
                };
                BeginAnimation(SpaceHeightProperty, spaceHeight);
            }

            if (DownShadowColor != null)
            {
                ShadowColor = animatedShadowColor;
                ColorAnimation shadowColor = new()
                {
                    To = ((SolidColorBrush)DownShadowColor).Color,
                    Duration = duration != null ? (Duration)duration : DownDuration,
                    EasingFunction = EasingFunction
                };
                animatedShadowColor.BeginAnimation(SolidColorBrush.ColorProperty, shadowColor);
            }
        }
        private void MouseUpAnimation()
        {
            ColorUnfreeze();
            if (DownBackground != null)
            {
                Background = animatedBackground;
                ColorAnimation backgroundAnimation = new()
                {
                    Duration = UpDuration,
                    EasingFunction = EasingFunction
                };
                if (EnterBackground != null)
                {
                    backgroundAnimation.To = ((SolidColorBrush)EnterBackground).Color;
                }
                else
                {
                    backgroundAnimation.To = ((SolidColorBrush)DefaultBackground).Color;
                }
                animatedBackground.BeginAnimation(SolidColorBrush.ColorProperty, backgroundAnimation);
            }

            if (DownForeground != null)
            {
                Foreground = animatedForeground;
                ColorAnimation foregroundAnimation = new()
                {
                    Duration = UpDuration,
                    EasingFunction = EasingFunction
                };
                if (EnterForeground != null)
                {
                    foregroundAnimation.To = ((SolidColorBrush)EnterForeground).Color;

                }
                else
                {
                    foregroundAnimation.To = ((SolidColorBrush)DefaultForeground).Color;
                }
                animatedForeground.BeginAnimation(SolidColorBrush.ColorProperty, foregroundAnimation);
            }

            if (DownBorderBrush != null)
            {
                BorderBrush = animatedBorderBrush;
                ColorAnimation BorderBrushAnimation = new()
                {
                    Duration = UpDuration,
                    EasingFunction = EasingFunction
                };
                if (EnterBorderBrush != null)
                {
                    BorderBrushAnimation.To = ((SolidColorBrush)EnterBorderBrush).Color;
                }
                else
                {
                    BorderBrushAnimation.To = ((SolidColorBrush)DefaultBorderBrush).Color;
                }
                animatedBorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, BorderBrushAnimation);
            }

            if (DownThickness != null)
            {
                ThicknessAnimation thicknessAnimation = new()
                {
                    Duration = UpDuration,
                    EasingFunction = EasingFunction
                };
                if (EnterThickness != null)
                {
                    thicknessAnimation.To = new Thickness((double)EnterThickness);
                    
                }
                else
                {
                    thicknessAnimation.To = new Thickness((double)DefaultThickness);
                }
                BeginAnimation(BorderThicknessProperty, thicknessAnimation);
            }

            if (DownSize != null)
            {
                DoubleAnimation sizeAnimation = new()
                {
                    Duration = UpDuration,
                    EasingFunction = EasingFunction
                };
                if (EnterSize != null)
                {
                    sizeAnimation.To = EnterSize;
                }
                else
                {
                    sizeAnimation.To = DefaultSize;
                }
                BeginAnimation(SizeProperty, sizeAnimation);
            }

            if (DownSpaceHeight != null)
            {
                DoubleAnimation spaceHeight = new()
                {
                    Duration = UpDuration,
                    EasingFunction = EasingFunction
                };
                if (EnterSpaceHeight != null)
                {
                    spaceHeight.To = EnterSpaceHeight;
                }
                else
                {
                    spaceHeight.To = DefaultSpaceHeight;
                }
                BeginAnimation(SpaceHeightProperty, spaceHeight);
            }

            if (DownShadowColor != null)
            {
                ShadowColor = animatedShadowColor;
                ColorAnimation shadowColor = new()
                {
                    Duration = UpDuration,
                    EasingFunction = EasingFunction
                };
                if (EnterShadowColor != null)
                {
                    shadowColor.To = ((SolidColorBrush)EnterShadowColor).Color;
                }
                else
                {
                    shadowColor.To = ((SolidColorBrush)DefaultShadowColor).Color;
                }
                animatedShadowColor.BeginAnimation(SolidColorBrush.ColorProperty, shadowColor);
            }
        }

        #region 界面交互
        // 鼠标事件
        public void ControlMouseEnter(object sender, MouseEventArgs e)
        {
            if (CanAct == false) return;
            if (IsSwitch && IsOn) return;
            if (IsEnabled)
            {
                MouseEnterAnimation();
            }
        }
        public void ControlMouseLeave(object sender, MouseEventArgs e)
        {
            if (CanAct == false) return;
            if (IsSwitch && IsOn) return;
            if (IsEnabled)
            {
                MouseLeaveAnimation();
            }
        }
        public void ControlMouseDown(object sender, MouseEventArgs e)
        {
            if (CanAct == false) return;
            if (IsEnabled)
            {
                MouseDownAnimation();
            }
        }
        public void ControlMouseUp(object sender, MouseEventArgs e)
        {
            if (CanAct == false) return;
            if (IsEnabled)
            {
                if (IsSwitch && !SwitchCanClick) return;
                if ((IsSwitch && _isOn) || !IsSwitch) MouseUpAnimation();
                OnClick();
            }
        }

        // 键盘事件
        private void ControlPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (CanAct == false) return;
            if (IsFocused)
            {
                if (e.Key == Key.Enter)
                {
                    if (IsSwitch && SwitchCanClick) MouseDownAnimation();
                    if (!IsSwitch) MouseDownAnimation();
                }
            }
        }
        private void ControlPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (CanAct == false) return;
            if (IsFocused)
            {
                if (e.Key == Key.Enter)
                {
                    if (IsSwitch && SwitchCanClick)
                    {
                        if (_isOn) MouseLeaveAnimation(UpDuration);
                        OnClick();
                    }
                    if (!IsSwitch) { MouseUpAnimation(); OnClick(); }
                }
            }
        }
        #endregion

        // 焦点事件
        private void ControlGotFocus(object sender, RoutedEventArgs e)
        {
            if (IsSwitch == true)
            {
                var Layer = AdornerLayer.GetAdornerLayer(this);
                Layer.Add(focusBox);
            }
            else
            {
                MouseEnterAnimation();
            }
        }
        private void ControlLostFocus(object sender, RoutedEventArgs e)
        {
            if (IsSwitch == true)
            {
                var Layer = AdornerLayer.GetAdornerLayer(this);
                Layer.Remove(focusBox);
            }
            else
            {
                MouseLeaveAnimation();
            }
        }

        // 可用状态
        private void ControlEnableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ColorUnfreeze();
            if (IsEnabled && IsOn)
            {
                MouseDownAnimation(LeaveDuration);
                return;
            }
            if (DisabledBackground != null)
            {
                Background = animatedBackground;
                ColorAnimation bgA = new()
                {
                    Duration = LeaveDuration,
                    EasingFunction = EasingFunction,
                    To = IsEnabled ? ((SolidColorBrush)DefaultBackground).Color : ((SolidColorBrush)DisabledBackground).Color
                };
                animatedBackground.BeginAnimation(SolidColorBrush.ColorProperty, bgA);
            }
            if (DisabledForeground != null)
            {
                Foreground = animatedForeground;
                ColorAnimation fgA = new()
                {
                    Duration = LeaveDuration,
                    EasingFunction = EasingFunction,
                    To = IsEnabled ? ((SolidColorBrush)DefaultForeground).Color : ((SolidColorBrush)DisabledForeground).Color
                };
                animatedForeground.BeginAnimation(SolidColorBrush.ColorProperty, fgA);
            }
            if (DisabledBorderBrush != null)
            {
                BorderBrush = animatedBorderBrush;
                ColorAnimation bbA = new()
                {
                    Duration = LeaveDuration,
                    EasingFunction = EasingFunction,
                    To = IsEnabled ? ((SolidColorBrush)DefaultBorderBrush).Color : ((SolidColorBrush)DisabledBorderBrush).Color
                };
                animatedBorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, bbA);
            }
            if (DisabledShadowColor != null)
            {
                ShadowColor = animatedShadowColor;
                ColorAnimation scA = new()
                {
                    Duration = LeaveDuration,
                    EasingFunction = EasingFunction,
                    To = IsEnabled ? ((SolidColorBrush)DefaultShadowColor).Color : ((SolidColorBrush)DisabledShadowColor).Color
                };
                animatedShadowColor.BeginAnimation(SolidColorBrush.ColorProperty, scA);
            }
            if (DisabledSpaceHeight != null)
            {
                DoubleAnimation shA = new()
                {
                    Duration = LeaveDuration,
                    EasingFunction = EasingFunction,
                    To = IsEnabled ? DefaultSpaceHeight : DisabledSpaceHeight
                };
                BeginAnimation(SpaceHeightProperty, shA);
            }
            if (DisabledThickness != null)
            {
                ThicknessAnimation tkA = new()
                {
                    Duration = LeaveDuration,
                    EasingFunction = EasingFunction,
                    To = new Thickness((double)(IsEnabled ? DefaultThickness : DisabledThickness))
                };
                BeginAnimation(BorderThicknessProperty, tkA);
            }
            if (EnterSize != null || DownSize != null)
            {
                DoubleAnimation sizeAnimation = new()
                {
                    To = DefaultSize,
                    Duration = LeaveDuration,
                    EasingFunction = EasingFunction
                };
                BeginAnimation(SizeProperty, sizeAnimation);
            }
        }

        // 属性检测
        private static void OnDefaultBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var buttonControl = (ButtonControl)d;
            buttonControl.Background = (Brush)e.NewValue;
        }
        private static void OnDefaultForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var buttonControl = (ButtonControl)d;
            buttonControl.Foreground = (Brush)e.NewValue;
        }
        private static void OnDefaultShadowColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var buttonControl = (ButtonControl)d;
            buttonControl.ShadowColor = (Brush)e.NewValue;
        }
        private static void OnDefaultBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var buttonControl = (ButtonControl)d;
            buttonControl.BorderBrush = (Brush)e.NewValue;
        }
        private static void OnDefaultThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var buttonControl = (ButtonControl)d;
            buttonControl.BorderThickness = new Thickness((double)e.NewValue);
        }
        private static void OnDefaultSpaceHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var buttonControl = (ButtonControl)d;
            buttonControl.SpaceHeight = (double)e.NewValue;
        }

        // 控件渲染
        protected override void OnRender(DrawingContext dc)
        {
            var DefaultRect = new Rect(RenderSize);
            var backgroundRect = DefaultRect;
            DefaultRect.Y += SpaceHeight;
            var borderThickness = BorderThickness.Left;
            var cornerRadius = new CornerRadius(
                CornerRadius.TopLeft > Math.Min(backgroundRect.Height, backgroundRect.Width) / 2 ? Math.Min(backgroundRect.Height, backgroundRect.Width) / 2 : CornerRadius.TopLeft,
                CornerRadius.TopRight > Math.Min(backgroundRect.Height, backgroundRect.Width) / 2 ? Math.Min(backgroundRect.Height, backgroundRect.Width) / 2 : CornerRadius.TopRight,
                CornerRadius.BottomRight > Math.Min(backgroundRect.Height, backgroundRect.Width) / 2 ? Math.Min(backgroundRect.Height, backgroundRect.Width) / 2 : CornerRadius.BottomRight,
                CornerRadius.BottomLeft > Math.Min(backgroundRect.Height, backgroundRect.Width) / 2 ? Math.Min(backgroundRect.Height, backgroundRect.Width) / 2 : CornerRadius.BottomLeft);

            if (ImageSource != null)
            {
                dc.PushClip(GeometryFuction.CreateSmoothRoundedRectangleGeometry(backgroundRect, cornerRadius));

                var ImageWidth = ImageSource.Width;
                var ImageHeight = ImageSource.Height;

                var aspectRatio = ImageWidth / (double)ImageHeight;
                Size ImageSize;

                if (ImageWidth > ImageHeight)
                {
                    ImageSize = new Size(backgroundRect.Height * aspectRatio, backgroundRect.Height);
                }
                else
                {
                    ImageSize = new Size(backgroundRect.Width, backgroundRect.Width / aspectRatio);
                }

                var ImageRect = new Rect((backgroundRect.Right - ImageSize.Width) / 2, (backgroundRect.Bottom - ImageSize.Height) / 2, ImageSize.Width, ImageSize.Height);

                dc.DrawImage(ImageSource, ImageRect);

                dc.Pop();
            }

            if (Background != null)
            {
                var strokeRect = new Rect(
                    backgroundRect.X + borderThickness,
                    backgroundRect.Y + borderThickness,
                    Math.Max(0, backgroundRect.Width - 2 * borderThickness),
                    Math.Max(0, backgroundRect.Height - 2 * borderThickness));
                var strokeRadius = new CornerRadius(
                    Math.Max(0, cornerRadius.TopLeft - borderThickness),
                    Math.Max(0, cornerRadius.TopRight - borderThickness),
                    Math.Max(0, cornerRadius.BottomRight - borderThickness),
                    Math.Max(0, cornerRadius.BottomLeft - borderThickness));

                // 背景
                if (SpaceHeight < 0) dc.PushClip(GeometryFuction.CreateSmoothRoundedRectangleGeometry(DefaultRect, cornerRadius));// 塌陷裁切
                if (BackgroundClipped)
                {
                    var clipRect = new Rect(
                        backgroundRect.X + borderThickness,
                        backgroundRect.Y + borderThickness,
                        Math.Max(0, backgroundRect.Width - borderThickness * 2),
                        Math.Max(0, backgroundRect.Height - borderThickness * 2));
                    var clipRadius = new CornerRadius(
                        Math.Max(0, cornerRadius.TopLeft - borderThickness),
                        Math.Max(0, cornerRadius.TopRight - borderThickness),
                        Math.Max(0, cornerRadius.BottomRight - borderThickness),
                        Math.Max(0, cornerRadius.BottomLeft - borderThickness));
                    var clipShadowRect = new Rect(
                        DefaultRect.X + borderThickness,
                        DefaultRect.Y + borderThickness,
                        Math.Max(0, DefaultRect.Width - borderThickness * 2),
                        Math.Max(0, DefaultRect.Height - borderThickness * 2));
                    dc.DrawGeometry(ShadowColor, null, GeometryFuction.CreateSmoothRoundedRectangleGeometry(clipShadowRect, clipRadius));
                    dc.DrawGeometry(Background, null, GeometryFuction.CreateSmoothRoundedRectangleGeometry(clipRect, clipRadius));
                }
                else if (BorderFixed)
                {
                    var clipRect = new Rect(
                        backgroundRect.X + borderThickness / 2,
                        backgroundRect.Y + borderThickness / 2,
                        Math.Max(0, backgroundRect.Width - borderThickness),
                        Math.Max(0, backgroundRect.Height - borderThickness));
                    var clipRadius = new CornerRadius(
                        Math.Max(0, cornerRadius.TopLeft - borderThickness / 2),
                        Math.Max(0, cornerRadius.TopRight - borderThickness / 2),
                        Math.Max(0, cornerRadius.BottomRight - borderThickness / 2),
                        Math.Max(0, cornerRadius.BottomLeft - borderThickness / 2));
                    dc.DrawGeometry(ShadowColor, null, GeometryFuction.CreateSmoothRoundedRectangleGeometry(DefaultRect, cornerRadius));
                    dc.DrawGeometry(Background, null, GeometryFuction.CreateSmoothRoundedRectangleGeometry(clipRect, clipRadius));
                }
                else
                {
                    dc.DrawGeometry(ShadowColor, null, GeometryFuction.CreateSmoothRoundedRectangleGeometry(DefaultRect, cornerRadius));
                    dc.DrawGeometry(Background, null, GeometryFuction.CreateSmoothRoundedRectangleGeometry(backgroundRect, cornerRadius));
                }
                // 描边
                dc.DrawGeometry(BorderBrush, null,
                    Geometry.Combine(GeometryFuction.CreateSmoothRoundedRectangleGeometry(backgroundRect, cornerRadius),
                    GeometryFuction.CreateSmoothRoundedRectangleGeometry(strokeRect, strokeRadius),
                    GeometryCombineMode.Xor, null));
            }

            // 文本渲染
            if (!string.IsNullOrEmpty(Content))
            {
                FormattedText ft = new(
                    Content,
                    CultureInfo.CurrentUICulture,
                    FlowDirection.LeftToRight,
                    new Typeface(FontFamily.ToString()),
                    FontSize,
                    Foreground,
                    96);
                ft.SetFontStyle(FontStyle);
                ft.SetFontWeight(FontWeight);

                double x;
                double y;
                double textWidth = ft.Width;
                double textHeight = ft.Height;

                switch (ContentAlignment)
                {
                    case AlignmentMode.Left:
                    case AlignmentMode.TopLeft:
                    case AlignmentMode.BottomLeft:
                        x = ContentPadding.Left;
                        break;
                    case AlignmentMode.Right:
                    case AlignmentMode.TopRight:
                    case AlignmentMode.BottomRight:
                        x = RenderSize.Width - textWidth - ContentPadding.Right;
                        break;
                    default:
                        x = (RenderSize.Width - textWidth) / 2;
                        break;
                }

                switch (ContentAlignment)
                {
                    case AlignmentMode.Top:
                    case AlignmentMode.TopLeft:
                    case AlignmentMode.TopRight:
                        y = ContentPadding.Top;
                        break;
                    case AlignmentMode.Bottom:
                    case AlignmentMode.BottomLeft:
                    case AlignmentMode.BottomRight:
                        y = RenderSize.Height - textHeight - ContentPadding.Bottom;
                        break;
                    default:
                        y = (RenderSize.Height - textHeight) / 2;
                        break;
                }

                if (RenderSize.Width - ContentPadding.Left - ContentPadding.Right <= 0 ||
                    RenderSize.Height - ContentPadding.Top - ContentPadding.Bottom <= 0) { return; }
                var textRect = new Rect(ContentPadding.Left, ContentPadding.Top - SpaceHeight, RenderSize.Width - ContentPadding.Left - ContentPadding.Right, RenderSize.Height - ContentPadding.Top - ContentPadding.Bottom);
                var clipRect = new Rect(x, y, textWidth, textHeight);
                clipRect.Intersect(textRect);
                var clipGeom = new RectangleGeometry(clipRect);
                dc.PushClip(clipGeom);

                if (SpaceHeight < 0) dc.PushClip(GeometryFuction.CreateSmoothRoundedRectangleGeometry(DefaultRect, cornerRadius));// 塌陷裁切

                dc.DrawText(ft, new Point(x, y));
                dc.Pop();
            }
        }

        // 刷新渲染
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ButtonControl)?.InvalidateVisual();
        }
        private static void OnSpaceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ButtonControl)d;
            var scale = new ScaleTransform(control.GetSize(), control.GetSize());
            var translate = new TranslateTransform(0, -control.SpaceHeight);
            Point o;
            switch (control.SizeAlignment)
            {
                case ScaleAlignment.Left:
                    o.X = 0;
                    o.Y = 0.5;
                    break;
                case ScaleAlignment.Right:
                    o.X = 1;
                    o.Y = 0.5;
                    break;
                case ScaleAlignment.Top:
                    o.X = 0.5;
                    o.Y = 0;
                    break;
                case ScaleAlignment.Bottom:
                    o.X = 0.5;
                    o.Y = 1;
                    break;
                case ScaleAlignment.TopLeft:
                    o.X = 0;
                    o.Y = 0;
                    break;
                case ScaleAlignment.TopRight:
                    o.X = 1;
                    o.Y = 0;
                    break;
                case ScaleAlignment.BottomLeft:
                    o.X = 0;
                    o.Y = 1;
                    break;
                case ScaleAlignment.BottomRight:
                    o.X = 1;
                    o.Y = 1;
                    break;
                default: // ScaleAlignment.Center
                    o.X = 0.5;
                    o.Y = 0.5;
                    break;
            }
            control.RenderTransformOrigin = o;
            TransformGroup transform = new(); transform.Children.Add(scale); transform.Children.Add(translate);
            control.RenderTransform = transform;
            control.InvalidateVisual();
        }
    }
}
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EyKettlesControlLibrary
{
    public class TextboxControl : ItemsControl
    {
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

        private readonly TextBox textbox;

        // 整体属性
        public static readonly DependencyProperty CanActProperty =
            DependencyProperty.Register("CanAct", typeof(bool), typeof(TextboxControl), new PropertyMetadata(true));

        private static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(double), typeof(TextboxControl), new PropertyMetadata(1d, OnSizeChanged));

        public static readonly DependencyProperty SizeAlignmentProperty =
            DependencyProperty.Register("SizeAlignment", typeof(ScaleAlignment), typeof(TextboxControl), new PropertyMetadata(ScaleAlignment.Center));

        // 缓动属性
        public static readonly DependencyProperty EnterDurationProperty =
            DependencyProperty.Register("EnterDuration", typeof(TimeSpan), typeof(TextboxControl), new PropertyMetadata(TimeSpan.FromSeconds(0.12)));

        public static readonly DependencyProperty LeaveDurationProperty =
            DependencyProperty.Register("LeaveDuration", typeof(TimeSpan), typeof(TextboxControl), new PropertyMetadata(TimeSpan.FromSeconds(0.3)));

        public static readonly DependencyProperty DownDurationProperty =
            DependencyProperty.Register("DownDuration", typeof(TimeSpan), typeof(TextboxControl), new PropertyMetadata(TimeSpan.FromSeconds(0.12)));

        public static readonly DependencyProperty UpDurationProperty =
            DependencyProperty.Register("UpDuration", typeof(TimeSpan), typeof(TextboxControl), new PropertyMetadata(TimeSpan.FromSeconds(0.16)));

        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(TextboxControl), new PropertyMetadata(new CubicEase { EasingMode = EasingMode.EaseOut }));

        // 整体动画
        public static readonly DependencyProperty DefaultSizeProperty =
            DependencyProperty.Register("DefaultSize", typeof(double), typeof(TextboxControl), new PropertyMetadata(1d));

        public static readonly DependencyProperty DisabledSizeProperty =
            DependencyProperty.Register("DisabledSize", typeof(double?), typeof(TextboxControl), new PropertyMetadata(null));

        public static readonly DependencyProperty EnterSizeProperty =
            DependencyProperty.Register("EnterSize", typeof(double?), typeof(TextboxControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DownSizeProperty =
            DependencyProperty.Register("DownSize", typeof(double?), typeof(TextboxControl), new PropertyMetadata(null));

        // 文本属性
        public static readonly DependencyProperty TipProperty =
            DependencyProperty.Register("Tip", typeof(string), typeof(TextboxControl), new PropertyMetadata(null, OnPropertyChanged));

        public static readonly DependencyProperty DisabledTipProperty =
            DependencyProperty.Register("DisabledTip", typeof(string), typeof(TextboxControl), new PropertyMetadata(null, OnPropertyChanged));

        private new static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(Brushes.Black, OnForegroundChanged));

        public static readonly DependencyProperty DisabledForegroundProperty =
            DependencyProperty.Register("DisabledForeground", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(null, OnPropertyChanged));

        // 文本动画
        public static readonly DependencyProperty DefaultForegroundProperty =
            DependencyProperty.Register("DefaultForeground", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(Brushes.Black, OnDefaultForegroundChanged));

        public static readonly DependencyProperty EnterForegroundProperty =
            DependencyProperty.Register("EnterForeground", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(null, OnPropertyChanged));

        public static readonly DependencyProperty DownForegroundProperty =
            DependencyProperty.Register("DownForeground", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(null, OnPropertyChanged));

        // 背景图形
        private new static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(Brushes.Transparent, OnPropertyChanged));

        public static readonly DependencyProperty DisabledBackgroundProperty =
            DependencyProperty.Register("DisabledBackground", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(null, OnPropertyChanged));

        private new static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(Brushes.Transparent, OnPropertyChanged));

        public static readonly DependencyProperty DisabledBorderBrushProperty =
            DependencyProperty.Register("DisabledBorderBrush", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(null, OnPropertyChanged));

        private new static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(TextboxControl), new PropertyMetadata(new Thickness(0), OnPropertyChanged));

        public static readonly DependencyProperty DisabledThicknessProperty =
            DependencyProperty.Register("DisabledThickness", typeof(double?), typeof(TextboxControl), new PropertyMetadata(null, OnPropertyChanged));

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(TextboxControl), new PropertyMetadata(new CornerRadius(), OnPropertyChanged));

        public static readonly DependencyProperty BackgroundClippedProperty =
            DependencyProperty.Register("BackgroundClipped", typeof(bool), typeof(TextboxControl), new PropertyMetadata(false));

        public static readonly DependencyProperty BorderFixedProperty =
            DependencyProperty.Register("BorderFixed", typeof(bool), typeof(TextboxControl), new PropertyMetadata(true));

        // 背景动画
        public static readonly DependencyProperty DefaultBackgroundProperty =
            DependencyProperty.Register("DefaultBackground", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(Brushes.Transparent, OnDefaultBackgroundChanged));

        public static readonly DependencyProperty EnterBackgroundProperty =
            DependencyProperty.Register("EnterBackground", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(null, OnPropertyChanged));

        public static readonly DependencyProperty DownBackgroundProperty =
            DependencyProperty.Register("DownBackground", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(null, OnPropertyChanged));
        
        // 描边动画
        public static readonly DependencyProperty DefaultBorderBrushProperty =
            DependencyProperty.Register("DefaultBorderBrush", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(Brushes.Transparent, OnDefaultBorderBrushChanged));

        public static readonly DependencyProperty EnterBorderBrushProperty =
            DependencyProperty.Register("EnterBorderBrush", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(null, OnPropertyChanged));

        public static readonly DependencyProperty DownBorderBrushProperty =
            DependencyProperty.Register("DownBorderBrush", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(null, OnPropertyChanged));

        public static readonly DependencyProperty DefaultThicknessProperty =
            DependencyProperty.Register("DefaultThickness", typeof(double), typeof(TextboxControl), new PropertyMetadata(0d, OnDefaultThicknessChanged));

        public static readonly DependencyProperty EnterThicknessProperty =
            DependencyProperty.Register("EnterThickness", typeof(double?), typeof(TextboxControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DownThicknessProperty =
            DependencyProperty.Register("DownThickness", typeof(double?), typeof(TextboxControl), new PropertyMetadata(null));

        // Textbox控件属性
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextboxControl), new PropertyMetadata(string.Empty, OnTextChanged));

        public static readonly DependencyProperty CaretBrushProperty =
            DependencyProperty.Register("CaretBrush", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(null, OnCaretBrushChanged));

        public static readonly DependencyProperty SelectionBrushProperty =
            DependencyProperty.Register("SelectionBrush", typeof(Brush), typeof(TextboxControl), new PropertyMetadata(null, OnSelectionBrushChanged));

        public static readonly DependencyProperty BoxMarginProperty =
            DependencyProperty.Register("BoxMargin", typeof(Thickness),typeof(TextboxControl), new PropertyMetadata(new Thickness(0),OnBoxMarginChanged));

        public static readonly DependencyProperty ContentPaddingProperty =
            DependencyProperty.Register("ContentPadding", typeof(Thickness), typeof(TextboxControl), new PropertyMetadata(new Thickness(12, 10, 12, 10), OnContentPaddingChanged));

        new public static readonly DependencyProperty HorizontalContentAlignmentProperty =
            DependencyProperty.Register("HorizontalContentAlignment", typeof(HorizontalAlignment),typeof(TextboxControl), new PropertyMetadata(HorizontalAlignment.Left,OnHorizontalAlignmentChanged));
        
        new public static readonly DependencyProperty VerticalContentAlignmentProperty =
            DependencyProperty.Register("VerticalContentAlignment", typeof(VerticalAlignment),typeof(TextboxControl), new PropertyMetadata(VerticalAlignment.Top, OnVerticalAlignmentChanged));

        public static readonly DependencyProperty TextWrappingProperty =
            DependencyProperty.Register("TextWrapping", typeof(TextWrapping), typeof(TextboxControl), new PropertyMetadata(TextWrapping.NoWrap, OnTextWrappingChanged));

        public static readonly DependencyProperty AcceptsReturnProperty =
            DependencyProperty.Register("AcceptsReturn", typeof(bool), typeof(TextboxControl), new PropertyMetadata(false, OnAcceptsReturnChanged));

        public event EventHandler? Click;
        public event EventHandler<TextChangedEventArgs>? TextChanged;
        public new event KeyEventHandler? PreviewKeyDown;
        public new event RoutedEventHandler? LostFocus;
        public new event RoutedEventHandler? GotFocus;

        // 颜色动画
        SolidColorBrush animatedBackground = Brushes.Transparent;
        SolidColorBrush animatedForeground = Brushes.Transparent;
        SolidColorBrush animatedBorderBrush = Brushes.Transparent;

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
            get { return (ScaleAlignment)GetValue(SizeAlignmentProperty); }
            set { SetValue(SizeAlignmentProperty, value); }
        }

        // 缓动属性
        public TimeSpan EnterDuration
        {
            get { return (TimeSpan)GetValue(EnterDurationProperty); }
            set { SetValue(EnterDurationProperty, value); }
        }
        public TimeSpan LeaveDuration
        {
            get { return (TimeSpan)GetValue(LeaveDurationProperty); }
            set { SetValue(LeaveDurationProperty, value); }
        }
        public TimeSpan DownDuration
        {
            get { return (TimeSpan)GetValue(DownDurationProperty); }
            set { SetValue(DownDurationProperty, value); }
        }
        public TimeSpan UpDuration
        {
            get { return (TimeSpan)GetValue(UpDurationProperty); }
            set { SetValue(UpDurationProperty, value); }
        }
        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        // 整体动画
        public double DefaultSize
        {
            get { return (double)GetValue(DefaultSizeProperty); }
            set
            {
                SetValue(DefaultSizeProperty, value);
                InvalidateVisual();
            }
        }
        public double? DisabledSize
        {
            get { return (double?)GetValue(DisabledSizeProperty); }
            set { SetValue(DisabledSizeProperty, value); }
        }
        public double? EnterSize
        {
            get { return (double?)GetValue(EnterSizeProperty); }
            set { SetValue(EnterSizeProperty, value); }
        }
        public double? DownSize
        {
            get { return (double?)GetValue(DownSizeProperty); }
            set { SetValue(DownSizeProperty, value); }
        }

        // 文本属性
        public string Tip
        {
            get { return (string)GetValue(TipProperty); }
            set { SetValue(TipProperty, value); }
        }
        public string DisabledTip
        {
            get { return (string)GetValue(DisabledTipProperty); }
            set { SetValue(DisabledTipProperty, value); }
        }
        private new Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }
        public Brush DisabledForeground
        {
            get { return (Brush)GetValue(DisabledForegroundProperty); }
            set { SetValue(DisabledForegroundProperty, value); }
        }

        // 文本动画
        public Brush DefaultForeground
        {
            get { return (Brush)GetValue(DefaultForegroundProperty); }
            set { SetValue(DefaultForegroundProperty, value); }
        }
        public Brush EnterForeground
        {
            get { return (Brush)GetValue(EnterForegroundProperty); }
            set { SetValue(EnterForegroundProperty, value); }
        }
        public Brush DownForeground
        {
            get { return (Brush)GetValue(DownForegroundProperty); }
            set { SetValue(DownForegroundProperty, value); }
        }

        //背景图形
        private new Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }
        public Brush DisabledBackground
        {
            get { return (Brush)GetValue(DisabledBackgroundProperty); }
            set { SetValue(DisabledBackgroundProperty, value); }
        }
        private new Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }
        public Brush DisabledBorderBrush
        {
            get { return (Brush)GetValue(DisabledBorderBrushProperty); }
            set { SetValue(DisabledBorderBrushProperty, value); }
        }
        private new Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }
        public double? DisabledThickness
        {
            get { return (double?)GetValue(DisabledThicknessProperty); }
            set { SetValue(DisabledThicknessProperty, value); }
        }
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
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
            get { return (Brush)GetValue(DefaultBackgroundProperty); }
            set { SetValue(DefaultBackgroundProperty, value); }
        }
        public Brush EnterBackground
        {
            get { return (Brush)GetValue(EnterBackgroundProperty); }
            set { SetValue(EnterBackgroundProperty, value); }
        }
        public Brush DownBackground
        {
            get { return (Brush)GetValue(DownBackgroundProperty); }
            set { SetValue(DownBackgroundProperty, value); }
        }

        // 描边动画
        public Brush DefaultBorderBrush
        {
            get { return (Brush)GetValue(DefaultBorderBrushProperty); }
            set { SetValue(DefaultBorderBrushProperty, value); }
        }
        public Brush EnterBorderBrush
        {
            get { return (Brush)GetValue(EnterBorderBrushProperty); }
            set { SetValue(EnterBorderBrushProperty, value); }
        }
        public Brush DownBorderBrush
        {
            get { return (Brush)GetValue(DownBorderBrushProperty); }
            set { SetValue(DownBorderBrushProperty, value); }
        }
        public double DefaultThickness
        {
            get { return (double)GetValue(DefaultThicknessProperty); }
            set { SetValue(DefaultThicknessProperty, value); }
        }
        public double? EnterThickness
        {
            get { return (double?)GetValue(EnterThicknessProperty); }
            set { SetValue(EnterThicknessProperty, value); }
        }
        public double? DownThickness
        {
            get { return (double?)GetValue(DownThicknessProperty); }
            set { SetValue(DownThicknessProperty, value); }
        }

        // Textbox控件属性
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public Brush CaretBrush
        {
            get { return (Brush)GetValue(CaretBrushProperty); }
            set { SetValue(CaretBrushProperty, value); }
        }
        public Brush SelectionBrush
        {
            get { return (Brush)GetValue(SelectionBrushProperty); }
            set { SetValue(SelectionBrushProperty, value); }
        }
        public Thickness BoxMargin
        {
            get { return (Thickness)GetValue(BoxMarginProperty); }
            set { SetValue(BoxMarginProperty, value); }
        }
        public Thickness ContentPadding
        {
            get { return (Thickness)GetValue(ContentPaddingProperty); }
            set { SetValue(ContentPaddingProperty, value); }
        }
        new public HorizontalAlignment HorizontalContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty); }
            set { SetValue(HorizontalContentAlignmentProperty, value); }
        }
        new public VerticalAlignment VerticalContentAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalContentAlignmentProperty); }
            set { SetValue(VerticalContentAlignmentProperty, value); }
        }
        public TextWrapping TextWrapping
        {
            get { return (TextWrapping)GetValue(TextWrappingProperty); }
            set { SetValue(TextWrappingProperty, value); }
        }
        public bool AcceptsReturn
        {
            get { return (bool)GetValue(AcceptsReturnProperty); }
            set { SetValue(AcceptsReturnProperty, value); }
        }

        // 初始化
        public TextboxControl()
        {
            FocusVisualStyle = null;
            Focusable = false;
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;

            // 配置布局
            ItemsPanelTemplate panelTemplate = new()
            {
                VisualTree = new FrameworkElementFactory(typeof(Grid))
            };
            ItemsPanel = panelTemplate;

            // 嵌入Textbox控件
            textbox = new TextBox
            {
                Padding = new Thickness(12, 10, 12, 10),
                TextWrapping = TextWrapping.Wrap,
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0)
            };
            Items.Add(textbox);

            Cursor = Cursors.IBeam;

            Keyboard.AddKeyDownHandler(textbox, OnKeyDown);

            MouseEnter += ControlMouseEnter;
            MouseLeave += ControlMouseLeave;
            textbox.GotFocus += ControlGotFocus;
            textbox.LostFocus += ControlLostFocus;
            textbox.TextChanged += ControlTextChanged;
            textbox.PreviewKeyDown += ControlPreviewKeyDown;
            IsEnabledChanged += ControlEnableChanged;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate(); 
            SetSize(DefaultSize);

            // 色彩绑定
            ColorUnfreeze();

            // Textbox光标颜色
            CaretBrush ??= DownBorderBrush;
            SelectionBrush ??= DownBorderBrush;
            textbox.CaretBrush = CaretBrush;
            textbox.SelectionBrush = SelectionBrush;

            // 触发开关
            if (IsEnabled == false) ControlEnableChanged(this, new());
        }
        protected virtual void OnClick()
        {
            if (CanAct == false) return;
            if (IsEnabled) Click?.Invoke(this, EventArgs.Empty);
        }
        private void ColorUnfreeze()
        {
            animatedBackground = new(((SolidColorBrush)Background).Color);
            animatedForeground = new(((SolidColorBrush)Foreground).Color);
            animatedBorderBrush = new(((SolidColorBrush)BorderBrush).Color);
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
        }
        private void MouseLeaveAnimation()
        {
            ColorUnfreeze();
            if (EnterBackground != null || DownBackground != null)
            {
                Background = animatedBackground;
                ColorAnimation backgroundAnimation = new()
                {
                    To = ((SolidColorBrush)DefaultBackground).Color,
                    Duration = LeaveDuration,
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
                    Duration = LeaveDuration,
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
                    Duration = LeaveDuration,
                    EasingFunction = EasingFunction
                };
                animatedBorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, BorderBrushAnimation);
            }

            if (EnterSize != null)
            {
                DoubleAnimation sizeAnimation = new()
                {
                    To = DefaultSize,
                    Duration = LeaveDuration,
                    EasingFunction = EasingFunction
                };
                BeginAnimation(SizeProperty, sizeAnimation);
            }

            if (EnterThickness != null)
            {
                ThicknessAnimation thicknessAnimation = new()
                {
                    To = new Thickness((double)DefaultThickness),
                    Duration = LeaveDuration,
                    EasingFunction = EasingFunction
                };
                BeginAnimation(BorderThicknessProperty, thicknessAnimation);
            }
        }
        private void GotFocusAnimation()
        {
            ColorUnfreeze();
            if (DownBackground != null)
            {
                Background = animatedBackground;
                ColorAnimation backgroundAnimation = new()
                {
                    To = ((SolidColorBrush)DownBackground).Color,
                    Duration = DownDuration,
                    EasingFunction = EasingFunction
                };
                animatedBackground.BeginAnimation(SolidColorBrush.ColorProperty, backgroundAnimation);
            }
            else if (EnterBackground != null)
            {
                Background = animatedBackground;
                ColorAnimation backgroundAnimation = new()
                {
                    To = ((SolidColorBrush)EnterBackground).Color,
                    Duration = DownDuration,
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
                    Duration = DownDuration,
                    EasingFunction = EasingFunction
                };
                animatedForeground.BeginAnimation(SolidColorBrush.ColorProperty, foregroundAnimation);
            }
            else if (EnterForeground != null)
            {
                Foreground = animatedForeground;
                ColorAnimation foregroundAnimation = new()
                {
                    To = ((SolidColorBrush)EnterForeground).Color,
                    Duration = DownDuration,
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
                    Duration = DownDuration,
                    EasingFunction = EasingFunction
                };
                animatedBorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, BorderBrushAnimation);
            }
            else if (EnterBorderBrush != null)
            {
                BorderBrush = animatedBorderBrush;
                ColorAnimation BorderBrushAnimation = new()
                {
                    To = ((SolidColorBrush)EnterBorderBrush).Color,
                    Duration = DownDuration,
                    EasingFunction = EasingFunction
                };
                animatedBorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, BorderBrushAnimation);
            }

            if (DownSize != null)
            {
                DoubleAnimation sizeAnimation = new()
                {
                    To = DownSize,
                    Duration = DownDuration,
                    EasingFunction = EasingFunction
                };
                BeginAnimation(SizeProperty, sizeAnimation);
            }

            if (DownThickness != null)
            {
                ThicknessAnimation thicknessAnimation = new()
                {
                    To = new Thickness((double)DownThickness),
                    Duration = LeaveDuration,
                    EasingFunction = EasingFunction
                };
                BeginAnimation(BorderThicknessProperty, thicknessAnimation);
            }
        }
        private void LostFocusAnimation()
        {
            ColorUnfreeze();
            if (EnterBackground != null || DownBackground != null)
            {
                Background = animatedBackground;
                ColorAnimation backgroundAnimation = new()
                {
                    To = ((SolidColorBrush)DefaultBackground).Color,
                    Duration = UpDuration,
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
                    Duration = UpDuration,
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
                    Duration = UpDuration,
                    EasingFunction = EasingFunction
                };
                animatedBorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, BorderBrushAnimation);
            }

            if (EnterSize != null || DownSize != null)
            {
                DoubleAnimation sizeAnimation = new()
                {
                   To = DefaultSize,
                   Duration = UpDuration,
                   EasingFunction = EasingFunction
                };
                BeginAnimation(SizeProperty, sizeAnimation);
            }

            if (EnterThickness != null || DownThickness != null)
            {
                ThicknessAnimation thicknessAnimation = new()
                {
                    To = new Thickness((double)DefaultThickness),
                    Duration = LeaveDuration,
                    EasingFunction = EasingFunction
                };
                BeginAnimation(BorderThicknessProperty, thicknessAnimation);
            }
        }

        #region 界面交互
        // 鼠标事件
        private void ControlMouseEnter(object sender, MouseEventArgs e)
        {
            if (CanAct == false) return;
            if (IsEnabled && !textbox.IsFocused)
            {
                MouseEnterAnimation();
            }
        }
        private void ControlMouseLeave(object sender, MouseEventArgs e)
        {
            if (CanAct == false) return;
            if (IsEnabled && !textbox.IsFocused)
            {
                MouseLeaveAnimation();
            }
        }
        #endregion

        // 焦点事件
        public void ControlGotFocus(object sender, RoutedEventArgs e)
        {
            if (IsEnabled)
            {
                GotFocusAnimation();
                GotFocus?.Invoke(this, e);
            }
        }
        public void ControlLostFocus(object sender, RoutedEventArgs e)
        {
            if (IsEnabled)
            {
                LostFocusAnimation();
                LostFocus?.Invoke(this, e);
            }
        }

        // Textbox事件
        private void ControlTextChanged(object sender, TextChangedEventArgs e)
        {
            Text = textbox.Text;
            TextChanged?.Invoke(this, e);
        }
        private void ControlPreviewKeyDown(object sender, KeyEventArgs e)
        {
            PreviewKeyDown?.Invoke(this, e);
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        // 可用状态
        private void ControlEnableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            textbox.IsEnabled = IsEnabled;
            InvalidateVisual();
            ColorUnfreeze();
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
            if (DisabledSize != null)
            {
                DoubleAnimation sizeAnimation = new()
                {
                   To = IsEnabled ? DefaultSize : DisabledSize,
                   Duration = LeaveDuration,
                   EasingFunction = EasingFunction
                };
                BeginAnimation(SizeProperty, sizeAnimation);
            }
        }

        // 属性检测
        private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var TextboxControl = (TextboxControl)d;
            ((TextBox)TextboxControl.Items[0]).Foreground = (Brush)e.NewValue;
        }
        private static void OnDefaultBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var TextboxControl = (TextboxControl)d;
            TextboxControl.Background = (Brush)e.NewValue;
        }
        private static void OnDefaultForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var TextboxControl = (TextboxControl)d;
            TextboxControl.Foreground = (Brush)e.NewValue;
        }
        private static void OnDefaultBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var TextboxControl = (TextboxControl)d;
            TextboxControl.BorderBrush = (Brush)e.NewValue;
        }
        private static void OnDefaultThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var TextboxControl = (TextboxControl)d;
            TextboxControl.BorderThickness = new Thickness((double)e.NewValue);
        }
        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var TextboxControl = (TextboxControl)d;
            var tb = TextboxControl.Items[0] as TextBox;
            tb!.Text = (string)e.NewValue ?? string.Empty;
        }
        private static void OnCaretBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var TextboxControl = (TextboxControl)d;
            var tb = TextboxControl.Items[0] as TextBox;
            tb!.CaretBrush = (Brush)e.NewValue ?? (Brush)TextboxControl.GetValue(DownBorderBrushProperty);
            TextboxControl.InvalidateVisual();
        }
        private static void OnSelectionBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var TextboxControl = (TextboxControl)d;
            var tb = TextboxControl.Items[0] as TextBox;
            tb!.SelectionBrush = (Brush)e.NewValue ?? (Brush)TextboxControl.GetValue(DownBorderBrushProperty);
            TextboxControl.InvalidateVisual();
        }
        private static void OnBoxMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var TextboxControl = (TextboxControl)d;
            var tb = TextboxControl.Items[0] as TextBox;
            tb!.Margin = (Thickness)e.NewValue;
            TextboxControl.InvalidateVisual();
        }
        private static void OnContentPaddingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var TextboxControl = (TextboxControl)d;
            var tb = TextboxControl.Items[0] as TextBox;
            tb!.Padding = (Thickness)e.NewValue;
            TextboxControl.InvalidateVisual();
        }
        private static void OnHorizontalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var TextboxControl = (TextboxControl)d;
            var tb = TextboxControl.Items[0] as TextBox;
            tb!.HorizontalContentAlignment = (HorizontalAlignment)e.NewValue;
            TextboxControl.InvalidateVisual();
        }
        private static void OnVerticalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var TextboxControl = (TextboxControl)d;
            var tb = TextboxControl.Items[0] as TextBox;
            tb!.VerticalContentAlignment = (VerticalAlignment)e.NewValue;
            TextboxControl.InvalidateVisual();
        }
        private static void OnTextWrappingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var TextboxControl = (TextboxControl)d;
            var tb = TextboxControl.Items[0] as TextBox;
            tb!.TextWrapping = (TextWrapping)e.NewValue;
            TextboxControl.InvalidateVisual();
        }
        private static void OnAcceptsReturnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var TextboxControl = (TextboxControl)d;
            var tb = TextboxControl.Items[0] as TextBox;
            tb!.AcceptsReturn = (bool)e.NewValue;
            TextboxControl.InvalidateVisual();
        }

        // 控件渲染
        protected override void OnRender(DrawingContext dc)
        {
            var backgroundRect = new Rect(RenderSize);
            var borderThickness = BorderThickness.Left;
            var cornerRadius = new CornerRadius(
                CornerRadius.TopLeft > Math.Min(backgroundRect.Height, backgroundRect.Width) / 2 ? Math.Min(backgroundRect.Height, backgroundRect.Width) / 2 : CornerRadius.TopLeft,
                CornerRadius.TopRight > Math.Min(backgroundRect.Height, backgroundRect.Width) / 2 ? Math.Min(backgroundRect.Height, backgroundRect.Width) / 2 : CornerRadius.TopRight,
                CornerRadius.BottomRight > Math.Min(backgroundRect.Height, backgroundRect.Width) / 2 ? Math.Min(backgroundRect.Height, backgroundRect.Width) / 2 : CornerRadius.BottomRight,
                CornerRadius.BottomLeft > Math.Min(backgroundRect.Height, backgroundRect.Width) / 2 ? Math.Min(backgroundRect.Height, backgroundRect.Width) / 2 : CornerRadius.BottomLeft);

            if (Background != null)
            {
                if (BackgroundClipped)
                {
                    var clippedRect = new Rect(
                        backgroundRect.X + borderThickness,
                        backgroundRect.Y + borderThickness,
                        Math.Max(0, backgroundRect.Width - borderThickness * 2),
                        Math.Max(0, backgroundRect.Height - borderThickness * 2));
                    var clippedRadius = new CornerRadius(
                        Math.Max(0, cornerRadius.TopLeft - borderThickness),
                        Math.Max(0, cornerRadius.TopRight - borderThickness),
                        Math.Max(0, cornerRadius.BottomRight - borderThickness),
                        Math.Max(0, cornerRadius.BottomLeft - borderThickness));
                    dc.DrawGeometry(Background, null, GeometryFuction.CreateSmoothRoundedRectangleGeometry(clippedRect, clippedRadius));
                }
                else if (BorderFixed)
                {
                    var clippedRect = new Rect(
                        backgroundRect.X + borderThickness / 2,
                        backgroundRect.Y + borderThickness / 2,
                        Math.Max(0, backgroundRect.Width - borderThickness),
                        Math.Max(0, backgroundRect.Height - borderThickness));
                    var clippedRadius = new CornerRadius(
                        Math.Max(0, cornerRadius.TopLeft - borderThickness / 2),
                        Math.Max(0, cornerRadius.TopRight - borderThickness / 2),
                        Math.Max(0, cornerRadius.BottomRight - borderThickness / 2),
                        Math.Max(0, cornerRadius.BottomLeft - borderThickness / 2));
                    dc.DrawGeometry(Background, null, GeometryFuction.CreateSmoothRoundedRectangleGeometry(clippedRect, clippedRadius));
                }
                else
                    dc.DrawGeometry(Background, null, GeometryFuction.CreateSmoothRoundedRectangleGeometry(backgroundRect, cornerRadius));

                var strokeRect = new Rect(
                    backgroundRect.X + borderThickness,
                    backgroundRect.Y + borderThickness,
                    Math.Max(0, backgroundRect.Width - borderThickness * 2),
                    Math.Max(0, backgroundRect.Height - borderThickness * 2));

                var strokeRadius = new CornerRadius(
                    Math.Max(0, cornerRadius.TopLeft - borderThickness),
                    Math.Max(0, cornerRadius.TopRight - borderThickness),
                    Math.Max(0, cornerRadius.BottomRight - borderThickness),
                    Math.Max(0, cornerRadius.BottomLeft - borderThickness));

                dc.DrawGeometry(BorderBrush, null,
                    Geometry.Combine(GeometryFuction.CreateSmoothRoundedRectangleGeometry(backgroundRect, cornerRadius),
                    GeometryFuction.CreateSmoothRoundedRectangleGeometry(strokeRect, strokeRadius),
                    GeometryCombineMode.Xor, null));
            }

            // 渲染提示
            if (((IsEnabled && Tip != null) || (!IsEnabled && DisabledTip != null)) && string.IsNullOrEmpty(textbox.Text) && !textbox.IsFocused)
            {
                FormattedText ft = new(
                    IsEnabled ? Tip : DisabledTip,
                    CultureInfo.CurrentUICulture,
                    FlowDirection.LeftToRight,
                    new Typeface(FontFamily.ToString()),
                    FontSize,
                    Foreground,
                    96);
                ft.SetFontStyle(FontStyle);
                ft.SetFontWeight(FontWeight);
                Color color = ((SolidColorBrush)Foreground).Color;
                color.A = 180;
                ft.SetForegroundBrush(IsEnabled || DisabledForeground == null ? new SolidColorBrush(color) : DisabledForeground);

                double x;
                double y;
                double textWidth = ft.Width;
                double textHeight = ft.Height;

                switch (HorizontalContentAlignment)
                {
                    case HorizontalAlignment.Left:
                        x = ContentPadding.Left;
                        break;
                    case HorizontalAlignment.Right:
                        x = RenderSize.Width - textWidth - ContentPadding.Right;
                        break;
                    default:
                        x = (RenderSize.Width - textWidth) / 2;
                        break;
                }

                switch (VerticalContentAlignment)
                {
                    case VerticalAlignment.Top:
                        y = ContentPadding.Top;
                        break;
                    case VerticalAlignment.Bottom:
                        y = RenderSize.Height - textHeight - ContentPadding.Bottom;
                        break;
                    default:
                        y = (RenderSize.Height - textHeight) / 2;
                        break;
                }

                if (RenderSize.Width - ContentPadding.Left - ContentPadding.Right <= 0 || RenderSize.Height - ContentPadding.Top - ContentPadding.Bottom <= 0) { return; }
                var textRect = new Rect(ContentPadding.Left, ContentPadding.Top, RenderSize.Width - ContentPadding.Left - ContentPadding.Right, RenderSize.Height - ContentPadding.Top - ContentPadding.Bottom);
                var clipRect = new Rect(x, y, textWidth, textHeight);
                clipRect.Intersect(textRect);
                var clipGeom = new RectangleGeometry(clipRect);
                dc.PushClip(clipGeom);

                dc.DrawText(ft, new Point(x, y));

                dc.Pop();
            }
        }

        // 刷新渲染
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TextboxControl)?.InvalidateVisual();
        }
        private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textboxControl = (TextboxControl)d;
            var o = new Point();
            switch (textboxControl.SizeAlignment)
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
            textboxControl.RenderTransform = new ScaleTransform((double)e.NewValue, (double)e.NewValue);
            textboxControl.RenderTransformOrigin = o;
        }
    }
}
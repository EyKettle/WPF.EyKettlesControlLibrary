# WPF.EyKettlesControlLibrary

*ControlLib like Material Design.*  
*Now, just a custom* Button *and a custom* TextBox.

---

`Fixed some bugs and improved details.`

1. Controls will update the state of Enable on loaded.
2. Controls has been added a BorderFixed property to fix background edge burrs.
3. **ButtonControl** is fixed an issue with over-subtracted values.
4. **ButtonControl** will check IsOn property on loaded.
5. **ButtonControl** as switch is fixed an issue with IsEnabled.
6. **TextboxControl** is fixed an issue with Foreground property (cannot change the foreground of inner TextBox).
7. **TextboxControl** is fixed an issue with Text property (default value is null)
8. **TextboxControl** is fixed an issue with apperance about focus state.
9. **TextboxControl** is fixed an animation logical in lost focus.
10. **TextboxControl** 's Size and Thickness has been changed to "Default, Enter, Down" mode.
11. **TextboxControl** 's ClickDuration is split to DownDuration and UpDuration.
12. **TextboxControl** has been added a BackgroundClipped property as ButtonControl.
13. Move Geometry functions to GeometryFunction static class, and add a smoother Rounded rectangle function.

---

# First

Add namespace:

```xaml
xmlns:XXXX="clr-namespace:EyKettlesControlLibrary;assembly=EyKettlesControlLibrary"
```

# To Use

## Universal Properties

| Property | Type | Function |
| --- | --- | --- |
| CanAct | bool | Whether can control do interface act |
| BorderFixed | bool | (Enabled by default) Cut background off half of border thickness |
| BackgroundClipped | bool | Whether cut background with border outline |
| ———— | ———— | —————— |
| EnterDuration | TimeSpan | How long the animtion last when mouse enter |
| LeaveDuration | TimeSpan | How long the animtion last when mouse leave |
| DownDuration | TimeSpan | How long the animtion last when mouse down |
| UpDuration | TimeSpan | How long the animtion last when mouse up |
| EasingFunction | IEasingFunction | How the animation act |
| ———— | ———— | —————— |
| DefaultSize | double | Set control default size |
| EnterSize | double | Size when mouse hovering |
| DownSize | double | Size when mouse down |
| SizeAlignment | `ScaleAlignment` | Set scale alignment |
| ———— | ———— | —————— |
| CornerRadius | CornerRadius | Set outer border corner radius |
| ———— | ———— | —————— |
| DefaultBackground | Brush | Set control background color |
| EnterBackground | Brush | Background color when mouse hovering |
| DownBackground | Brush | Background color when mouse down |
| ———— | ———— | —————— |
| DefaultForeground | Brush | Set control foreground color |
| EnterForeground | Brush | Foreground color when mouse hovering |
| DownForeground | Brush | Foreground color when mouse down |
| ———— | ———— | —————— |
| DefaultBorderBrush | Brush | Set control border color |
| EnterBorderBrush | Brush | Border color when mouse hovering |
| DownBorderBrush | Brush | Border color when mouse down |
| ———— | ———— | —————— |
| DefaultThickness | double | Set control border thickness |
| EnterThickness | double | BorderThickness when mouse hovering |
| DownThickness | double | BorderThickness when mouse down |
| ———— | ———— | —————— |
| DisableBackground | Brush | Background color when disabled |
| DisableForeground | Brush | Foreground color when disabled |
| DisableBorderBrush | Brush | Border color when disabled |
| DisableThickness | double | Border thickness when disabled |

> - **ScaleAlignment** Center, Left, Right, Top, Bottom, TopLeft, TopRight, BottomLeft, BottomRight.

> *The Setting of DefaultSize only act on loaded, then it will be changed in animation.*

## GeometryFunction

* **(Geometry)** GeometryFunction.CreateRoundedRectangleGeometry(Rect rect, CornerRadius cornerRadius)

* **(Geometry)** GeometryFunction.CreateSmoothRoundedRectangleGeometry(Rect rect, CornerRadius cornerRadius)

## ButtonControl

| Property | Type | Function |
| --- | --- | --- |
| Content | string | Set button content |
| ContentAlignment | AlignmentMode | Set button content alignment |
| ContentPadding | Thickness | Set button content padding |
| ———— | ———— | —————— |
| ImageSource | ImageSource | Set button image |
| ImageScale | `ImageScaleMode` | Set button image scale mode |
| ———— | ———— | —————— |
| IsSwitch | bool | If it is a switch |
| SwitchCanClick | bool | If mouse can click switch |
| IsOn | bool | Switch button or get switch state |
| ———— | ———— | —————— |
| DefaultSpaceHeight | double | Set default space position |
| EnterSpaceHeight | double | How high will it move when mouse hovering |
| DownSpaceHeight | double | How high will it move when mouse down |
| ———— | ———— | —————— |
| DefaultShadowColor | Brush | Set the color behind control |
| EnterShadowColor | Brush | Shadow color when mouse hovering |
| DownShadowColor | Brush | Shadow color when mouse down |

| Events | Function |
| --- | --- |
| Click | Happen when click button (Mouse Up) |
| IsOnChanged | Happen when **IsOn** property changed |

> - **ImageScaleMode** Fill, Fit, Stretch, Center

## TextboxControl

| Property | Type | Function |
| --- | --- | --- |
| Text | string | As **TextBox** |
| Tip | string | What will show in blank box |
| DisableTip | string | What will show when disable |
| ———— | ———— | —————— |
| CaretBrush | Brush | As **TextBox** |
| SelectionBrush | Brush | As **TextBox** |
| ———— | ———— | —————— |
| BoxMargin | Thickness | Distance between inner **TextBox** and outer border |
| ContentPadding | Thickness | The Padding property in inner **TextBox** |
| HorizontalContentAlignment | HorizontalAlignment | As **TextBox** |
| VerticalContentAlignment | VerticalAlignment | As **TextBox** |
| ———— | ———— | —————— |
| TextWrapping | TextWrapping | As **TextBox** |
| AcceptsReturn | bool | As **TextBox** |

| Events | Function |
| --- | --- |
| TextChanged | As **TextBox** |
| PreviewKeyDown | As **TextBox** |
| ———— | —————— |
| GotFocus | As **TextBox** |
| LostFocus | As **TextBox** |

## ScrollControl

Not ready yet.

---
*0.0.25*

Any problem you can ask:

* **QQmail** 486716915@qq.com

* **T

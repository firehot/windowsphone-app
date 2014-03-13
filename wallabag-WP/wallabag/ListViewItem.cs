using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace RssReader
{
  public enum Layout
  {
    Custom,
    Default,
    TextAndDetails,
    TextAndDetailsWithIcon,
    TextWithIcon
  }

  //http://aykutkilic.wordpress.com/tag/windows-phone-7/
  public class ListViewItem : Control
  {
    public Layout Layout
    {
      get { return (Layout)GetValue(LayoutProperty); }
      set { SetValue(LayoutProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Layout.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty LayoutProperty =
        DependencyProperty.Register("Layout", typeof(Layout), typeof(ListViewItem),
            new PropertyMetadata(Layout.Default, UpdateLayout));

    public ControlTemplate CustomTemplate
    {
      get { return (ControlTemplate)GetValue(CustomTemplateProperty); }
      set { SetValue(CustomTemplateProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CustomTemplate.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CustomTemplateProperty =
        DependencyProperty.Register("CustomTemplate", typeof(ControlTemplate), typeof(ListViewItem), new PropertyMetadata(null));

    public ControlTemplate TextAndDetailsTemplate
    {
      get { return (ControlTemplate)GetValue(TextAndDetailsTemplateProperty); }
      set { SetValue(TextAndDetailsTemplateProperty, value); }
    }

    // Using a DependencyProperty as the backing store for TextAndDetailsTemplate.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TextAndDetailsTemplateProperty =
        DependencyProperty.Register("TextAndDetailsTemplate", typeof(ControlTemplate), typeof(ListViewItem), new PropertyMetadata(null));

    public ControlTemplate TextAndDetailsWithIconTemplate
    {
      get { return (ControlTemplate)GetValue(TextAndDetailsWithIconTemplateProperty); }
      set { SetValue(TextAndDetailsWithIconTemplateProperty, value); }
    }

    // Using a DependencyProperty as the backing store for TextAndDetailsWithIconTemplate.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TextAndDetailsWithIconTemplateProperty =
        DependencyProperty.Register("TextAndDetailsWithIconTemplate", typeof(ControlTemplate), typeof(ListViewItem), new PropertyMetadata(null));

    public ControlTemplate TextWithIconTemplate
    {
      get { return (ControlTemplate)GetValue(TextWithIconTemplateProperty); }
      set { SetValue(TextWithIconTemplateProperty, value); }
    }

    // Using a DependencyProperty as the backing store for TextWithIconTemplate.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TextWithIconTemplateProperty =
        DependencyProperty.Register("TextWithIconTemplate", typeof(ControlTemplate), typeof(ListViewItem), new PropertyMetadata(null));

    private static void UpdateLayout(object sender, DependencyPropertyChangedEventArgs a)
    {
      var i = sender as ListViewItem;

      BindingOperations.SetBinding(
          i,
          ListViewItem.TemplateProperty,
          new Binding(a.NewValue.ToString() + "Template")
          {
            Source = i,
            Mode = BindingMode.TwoWay
          });
    }

    public string Text
    {
      get { return (string)GetValue(TextProperty); }
      set { SetValue(TextProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(ListViewItem), new PropertyMetadata(string.Empty));

    public string Details
    {
      get { return (string)GetValue(DetailsProperty); }
      set { SetValue(DetailsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Details.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DetailsProperty =
        DependencyProperty.Register("Details", typeof(string), typeof(ListViewItem), new PropertyMetadata(string.Empty));

    public object SecondaryContent
    {
      get { return (object)GetValue(SecondaryContentProperty); }
      set { SetValue(SecondaryContentProperty, value); }
    }

    // Using a DependencyProperty as the backing store for SecondaryContent.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SecondaryContentProperty =
        DependencyProperty.Register("SecondaryContent", typeof(object), typeof(ListViewItem), new PropertyMetadata(null));

    public ImageSource ImageSource
    {
      get { return (ImageSource)GetValue(ImageSourceProperty); }
      set { SetValue(ImageSourceProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ImageSourceProperty =
        DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ListViewItem), new PropertyMetadata(null));

    public Stretch ImageStretch
    {
      get { return (Stretch)GetValue(ImageStretchProperty); }
      set { SetValue(ImageStretchProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ImageStretch.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ImageStretchProperty =
        DependencyProperty.Register("ImageStretch", typeof(Stretch), typeof(ListViewItem), new PropertyMetadata(Stretch.Uniform));

    public ListViewItem()
    {
    }
  }

}

/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.4.
 ** Copyright (c) 2000-2021 by yWorks GmbH, Vor dem Kreuzberg 28,
 ** 72070 Tuebingen, Germany. All rights reserved.
 ** 
 ** yFiles demo files exhibit yFiles.NET functionalities. Any redistribution
 ** of demo files in source code or binary form, with or without
 ** modification, is not permitted.
 ** 
 ** Owners of a valid software license for a yFiles.NET version that this
 ** demo is shipped with are allowed to use the demo source code as basis
 ** for their own yFiles.NET powered applications. Use of such programs is
 ** governed by the rights and conditions as set out in the yFiles.NET
 ** license agreement.
 ** 
 ** THIS SOFTWARE IS PROVIDED ''AS IS'' AND ANY EXPRESS OR IMPLIED
 ** WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 ** MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN
 ** NO EVENT SHALL yWorks BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 ** SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
 ** TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 ** PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 ** LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 ** NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 ** SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 ** 
 ***************************************************************************/

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Demo.yFiles.Option.Handler;
using System.Reflection;

namespace Demo.yFiles.Option.Editor
{
  [DefaultEvent("ValueChanged")]
  internal class GenericValueEditor : Control, ITypeDescriptorContext
  {
    #region Instance Members

    /// <summary>
    /// Indicates whether the control is in auto size mode.
    /// </summary>
    private bool autoSize;

    /// <summary>
    /// The border style. Note that initialization must be done here.
    /// </summary>
    private BorderStyle borderStyle = BorderStyle.Fixed3D;

    /// <summary>
    /// Current value of the editor.
    /// </summary>
    private object currentValue;

    /// <summary>
    /// The text box for editing text.
    /// </summary>
    private TextBox textBox;

    /// <summary>
    /// A button used to drop UI type editors, if any.
    /// </summary>
    private EditorButton editorButton;

    /// <summary>
    /// A control used to paint the current value.
    /// </summary>
    private PreviewControl previewControl;

    /// <summary>
    /// The <strong>IWindowsFormsEditorService</strong> that 
    /// allows you to drop UI type editors.
    /// </summary>
    private GenericValueEditorService genericValueEditorService;

    /// <summary>
    /// Indicates if we want to hide the textbox and only paint the value.
    /// </summary>
    private bool showPreviewOnly;

    /// <summary>
    /// Default width of the paint value rectangle.
    /// </summary>
    internal const int PAINT_VALUE_WIDTH = 20;

    /// <summary>
    /// UITypeEditor for types with standard values.
    /// </summary>
    private StandardValuesUIEditor standardValuesUIEditor;

    private PropertyDescriptor _descriptor;

    private bool textEdited = false;

    /// <summary>
    /// Event fired when the <see cref="Value"/> property is changed on the control.
    /// </summary>
    [Category("Property Changed")]
    [Description("Event fired when the Value property is changed on the control.")]
    public event EventHandler ValueChanged;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericValueEditor"/> class using
    /// the specified type.
    /// </summary>
    /// <param name="descriptor">The <see cref="PropertyDescriptor"/> of object that can be edited by this control.</param>
    public GenericValueEditor(PropertyDescriptor descriptor) {
      this._descriptor = descriptor;
      SetStyle(ControlStyles.Selectable, true);
      SetStyle(ControlStyles.FixedHeight, true);
      autoSize = true;

      SuspendLayout();

      // Text box Control
      textBox = new TextBox();
      InitTextBox();

      // editor button
      editorButton = new EditorButton();
      editorButton.IsDialog = Editor != null && Editor.GetEditStyle() == UITypeEditorEditStyle.Modal;
      editorButton.Click += new EventHandler(ButtonClicked);

      // Paint value box
      previewControl = new PreviewControl(this);
      previewControl.Click += new EventHandler(PreviewControlClicked);

      // Add the sub-controls

      Controls.AddRange(new Control[] {previewControl, textBox, editorButton});

      genericValueEditorService = new GenericValueEditorService(this);
      ResumeLayout();
    }


    /// <summary>
    /// Initializes the text box .
    /// </summary>
    private void InitTextBox() {
      textBox.AcceptsReturn = false;
      textBox.AcceptsTab = false;
      textBox.AutoSize = false;
      textBox.CausesValidation = false;
      textBox.BorderStyle = BorderStyle.None;
      textBox.KeyDown += new KeyEventHandler(TextBoxKeyDown);
      textBox.KeyUp += new KeyEventHandler(TextBoxKeyUp);
      textBox.KeyPress += new KeyPressEventHandler(TextBoxKeyPress);
      textBox.TextChanged += new EventHandler(TextBoxTextChanged);
      textBox.Validating += new CancelEventHandler(TextBoxValidating);
      textBox.Validated += new EventHandler(TextBoxValidated);
      textBox.GotFocus += new EventHandler(TextBoxGotFocus);
      textBox.LostFocus += new EventHandler(TextBoxLostFocus);
    }

    #endregion

    #region Properties

    /// <summary>
    /// This member overrides <see cref="Control.BackgroundImage">Control.BackgroundImage</see>.
    /// </summary>
    [Browsable(false)]
    public override Image BackgroundImage {
      get { return base.BackgroundImage; }
      set { base.BackgroundImage = value; }
    }


    /// <summary>
    /// Gets or sets the foreground color of the control.
    /// </summary>
    /// <value>A <see cref="System.Drawing.Color"/> that represents the foreground color of the control.
    /// The default value is the value for window text (<see cref="SystemColors.WindowText">SystemColors.WindowText</see>).</value>
    [Description("The foreground color.")]
    [DefaultValue(typeof (Color), "WindowText")]
    public override Color ForeColor {
      get { return textBox.ForeColor; }
      set {
        if (ForeColor != value) {
          textBox.ForeColor = value;
          OnForeColorChanged(EventArgs.Empty);
        }
      }
    }

    internal TextBox TextBox {
      get { return textBox; }
    }

    /// <summary>
    /// Resets the <see cref="ForeColor"/> property to its default value.
    /// </summary>
    private new void ResetForeColor() {
      ForeColor = SystemColors.WindowText;
    }


    /// <summary>
    /// Gets or sets the background color of the control.
    /// </summary>
    /// <value>A <see cref="System.Drawing.Color"/> that represents the background color of the control.
    /// The default value is the value for window text (<see cref="SystemColors.Window">SystemColors.Window</see>).</value>
    [Description("The background color.")]
    [DefaultValue(typeof (Color), "Window")]
    public override Color BackColor {
      get { return textBox.BackColor; }
      set {
        if (BackColor != value) {
          textBox.BackColor = value;
          Invalidate(true);
          OnBackColorChanged(EventArgs.Empty);
        }
      }
    }


    /// <summary>
    /// Resets the <see cref="BackColor"/> property to its default value.
    /// </summary>
    private new void ResetBackColor() {
      BackColor = SystemColors.Window;
    }


    /// <summary>
    /// Gets or sets a value indicating whether the control automatically adjusts its height to the font height.
    /// </summary>
    /// <value><see langword="true"/> if the control adjusts its height to closely fit 
    /// its contents; <see langword="false"/> otherwise. The default value is <see langword="true"/>.</value>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Indicating whether the control automatically adjusts its height to the font height.")]
    public override bool AutoSize {
      get { return autoSize; }
      set {
        if (value != autoSize) {
          autoSize = value;
          AdjustHeight();
          SetStyle(ControlStyles.FixedHeight, value);
          OnAutoSizeChanged(EventArgs.Empty);
        }
      }
    }


    /// <summary>
    /// This member overrides <see cref="Control.CreateParams">Control.CreateParams</see>.
    /// </summary>
    protected override CreateParams CreateParams {
      get {
        int WS_EX_CLIENTEDGE = 0x200;
        int WS_BORDER = 0x800000;

        CreateParams cparams;

        cparams = base.CreateParams;

        switch (borderStyle) {
          case BorderStyle.Fixed3D:
            cparams.ExStyle = cparams.ExStyle | WS_EX_CLIENTEDGE;
            break;
          case BorderStyle.FixedSingle:
            cparams.Style = cparams.Style | WS_BORDER;
            break;
        }
        return cparams;
      }
    }


    /// <summary>
    /// Gets or sets the border style of the control.
    /// </summary>
    /// <value>One of the <see cref="System.Windows.Forms.BorderStyle"/> values. The default value
    /// is <see cref="System.Windows.Forms.BorderStyle.Fixed3D"/>.</value>
    [Category("Appearance")]
    [DefaultValue(BorderStyle.Fixed3D)]
    [Description("The border style of the control.")]
    [Localizable(true)]
    public BorderStyle BorderStyle {
      get { return borderStyle; }
      set {
        if (borderStyle != value) {
          borderStyle = value;
          UpdateStyles();
          AdjustHeight();
          LayoutSubControls();
          Invalidate(true);
          OnBorderStyleChanged(EventArgs.Empty);
        }
      }
    }


    /// <summary>
    /// Event fired when the <see cref="BorderStyle"/> property is changed on the control.
    /// </summary>
    [Category("Property Changed")]
    [Description("Event fired when the BorderStyle property is changed on the control.")]
    public event EventHandler BorderStyleChanged;

    /// <summary>
    /// Invoked when the <see cref="BorderStyle"/> property is changed on the control.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data. </param>
    /// <remarks>Called when the <strong>BorderStyle</strong> property is changed.</remarks>
    protected virtual void OnBorderStyleChanged(EventArgs e) {
      if (BorderStyleChanged != null) {
        BorderStyleChanged(this, e);
      }
    }


    /// <summary>
    /// Gets or sets the way text is aligned in a <see cref="GenericValueEditor"/> control.
    /// </summary>
    /// <value>One of the <see cref="System.Windows.Forms.HorizontalAlignment"/> enumeration values that specifies 
    /// how text is aligned in the control. The default value is <see cref="System.Windows.Forms.HorizontalAlignment.Left"/>.</value>
    [Category("Appearance")]
    [DefaultValue(HorizontalAlignment.Left)]
    [Description("The alignment of text.")]
    [Localizable(true)]
    public HorizontalAlignment TextAlign {
      get { return textBox.TextAlign; }
      set {
        if (TextAlign != value) {
          textBox.TextAlign = value;
          OnTextAlignChanged(EventArgs.Empty);
        }
      }
    }


    /// <summary>
    /// Event fired when the <see cref="TextAlign"/> property is changed on the control.
    /// </summary>
    [Category("Property Changed")]
    [Description("Event fired when the TextAlign property is changed on the control.")]
    public event EventHandler TextAlignChanged;

    /// <summary>
    /// Invoked when the <see cref="TextAlign"/> property is changed on the control.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data. </param>
    /// <remarks>Called when the <strong>TextAlign</strong> property is changed.</remarks>
    protected virtual void OnTextAlignChanged(EventArgs e) {
      if (TextAlignChanged != null) {
        TextAlignChanged(this, e);
      }
    }


    /// <summary>
    /// Gets or sets a value indicating whether text in the text box is read-only.
    /// </summary>
    /// <value><see langword="true"/> if the text box is read-only; <see langword="false"/> otherwise. The default value is 
    /// <see langword="false"/>.</value>
    /// <remarks>When this property is set to <see langword="true"/>, the contents of the control cannot be 
    /// changed by the user at runtime. With this property set to <see langword="true"/>, you can still set 
    /// the value of the <see cref="Text"/> property in code. You can use this feature instead of disabling 
    /// the control with the <see cref="Control.Enabled"/> property to allow the contents to be copied.
    /// </remarks>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Controls whether the text in the control can be changed or not.")]
    public bool ReadOnly {
      get { return textBox.ReadOnly; }
      set {
        if (ReadOnly != value) {
          textBox.ReadOnly = value;
          previewControl.Enabled = !value;
          editorButton.Enabled = !value;
          Invalidate(true);
          OnReadOnlyChanged(EventArgs.Empty);
        }
      }
    }


    /// <summary>
    /// Event fired when the <see cref="ReadOnly"/> property is changed on the control.
    /// </summary>
    [Category("Property Changed")]
    [Description("Event fired when the ReadOnly property is changed on the control.")]
    public event EventHandler ReadOnlyChanged;

    /// <summary>
    /// Invoked when the <see cref="ReadOnly"/> property is changed on the control.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data. </param>
    /// <remarks>Called when the <strong>ReadOnly</strong> property is changed.</remarks>
    protected virtual void OnReadOnlyChanged(EventArgs e) {
      if (ReadOnlyChanged != null) {
        ReadOnlyChanged(this, e);
      }
    }


    /// <summary>
    /// Gets or sets a value indicating whether to show only the rectangle 
    /// that displays a representation of the edited value.
    /// </summary>
    /// <value><see langword="true"/> if the control shows only the rectangle that displays 
    /// a representation of the edited value; <see langword="false"/> otherwise. The textual value is then not visible.</value>
    /// <remarks>
    /// When the editor can paint a representation of the value
    /// this control will show both a textual value and a rectangle that displays a
    /// representation of the value.
    /// Setting this property to <see langword="true"/> will hide the textual value.
    /// Not all editors can paint a representation of the edited value. If the
    /// editor cannot paint the edited value, then the value 
    /// of this property is meaningless.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Appearance")]
    [
      Description(
        "Indicates whether the control only displays the rectangle that previews the value and not the text.")]
    public bool ShowPreviewOnly {
      get { return showPreviewOnly; }
      set {
        showPreviewOnly = value;
        LayoutSubControls();
        Invalidate(true);
      }
    }


    /// <summary>
    /// Gets or sets the value edited by the control.
    /// </summary>
    /// <value>The current value of the editor.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [ObfuscationAttribute(Exclude = true, StripAfterObfuscation = false)]
    public object Value {
      get { return currentValue; }
      set {
        //if (value != null && !EditedType.IsAssignableFrom(value.GetType()))
        //    throw new InvalidCastException("GenericValueEditor.Value : Bad value type.");
        if ((value == null && currentValue != null) ||
            (value != null && currentValue == null)
            || !value.Equals(currentValue)) {
          currentValue = value;
          UpdateTextBoxWithValue();
          //                if (PaintValueSupported())
          //                {

          //                }
          //                textEdited = false;
          OnValueChanged(EventArgs.Empty);
          LayoutSubControls();
          Invalidate(true);
        }
      }
    }

    internal bool PaintValueSupported() {
      return Editor != null && Editor.GetPaintValueSupported(this);
    }

    /// <summary>
    /// Invoked when the <see cref="Value"/> property is changed on the control.
    /// </summary>
    /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
    /// <remarks>Called when the <strong>Value</strong> property is changed.</remarks>
    protected virtual void OnValueChanged(EventArgs e) {
      if (ValueChanged != null) {
        ValueChanged(this, e);
      }
    }


    /// <summary>
    /// Gets or sets the starting point of text selected in the control.
    /// </summary>
    /// <value>The starting position of text selected in the control.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionStart {
      get { return textBox.SelectionStart; }
      set { textBox.SelectionStart = value; }
    }


    /// <summary>
    /// Gets or sets the number of characters selected in the control.
    /// </summary>
    /// <value>The number of characters selected in the control.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionLength {
      get { return textBox.SelectionLength; }
      set { textBox.SelectionLength = value; }
    }


    /// <summary>
    /// This member overrides <see cref="Control.Text">Control.Text</see>.
    /// </summary>
    public override string Text {
      get { return textBox.Text; }
      set {
        if (Text != value) {
          ValidateText(value);
        }
      }
    }

    /// <summary>
    /// Gets or sets the type converter used by the editor.
    /// </summary>
    /// <value>A <see cref="TypeConverter"/> instance that is used to convert the edited value from and to text.</value>
    [Browsable(false)]
    [AmbientValue(null)]
    public TypeConverter Converter {
      get { return _descriptor.Converter; }
    }


    private bool ShouldSerializeConverter() {
      return Converter != null;
    }


    /// <summary>
    /// Gets or sets the type editor for this control.
    /// </summary>
    /// <value>A <see cref="UITypeEditor"/> instance that defines the way this control will edit the value.</value>
    /// <remarks>
    /// <p>When the editor has the style <strong>DropDown</strong>
    /// (see <see cref="UITypeEditorEditStyle"/>), then this control will display a
    /// down-arrow button that drops the custom editor. When the editor has the style
    /// <strong>Modal</strong>, then this control will display a <strong>...</strong>
    /// button that opens the modal dialog.</p>
    /// <p>When no editor is set or the editor is of style <strong>None</strong>, then
    /// the behavior of the control depends on the edited type. If the type is enumerated
    /// then the control acts like a combo box of the enumerated values. If the type is
    /// not an enumerated type, then the control acts like a text box.</p>
    /// <p>If the editor can display a representation of the edited value,
    /// then a small rectangle showing this representation will be displayed in addition
    /// to the textual value.</p>
    /// </remarks>
    [Browsable(false)]
    [AmbientValue(null)]
    public UITypeEditor Editor {
      get { return _descriptor.GetEditor(typeof (UITypeEditor)) as UITypeEditor; }
    }

    private bool ShouldSerializeEditor() {
      return Editor != null;
    }

    internal bool HasStandardValues {
      get {
        return Converter != null &&
               Converter.GetStandardValuesSupported() &&
               Converter.GetStandardValues().Count != 0;
      }
    }

    internal bool HasButton {
      get {
        return (Editor != null &&
                Editor.GetEditStyle() != UITypeEditorEditStyle.None)
               || HasStandardValues;
      }
    }

    #endregion

    #region Protected Methods...

    /// <summary>
    /// This members overrides <see cref="Control.OnSystemColorsChanged">Control.OnSystemColorsChanged</see>.
    /// </summary>
    protected override void OnSystemColorsChanged(EventArgs e) {
      base.OnSystemColorsChanged(e);
      // Must delegate to the editors....
      if (genericValueEditorService != null) {
        genericValueEditorService.SystemColorsChanged();
      }
    }

    /// <summary>
    /// This member overrides <see cref="Control.DefaultSize">Control.DefaultSize</see>.
    /// </summary>
    protected override Size DefaultSize {
      get { return new Size(125, PreferredHeight); }
    }

    /// <summary>
    /// This member overrides <see cref="Control.OnCursorChanged">Control.OnCursorChanged</see>.
    /// </summary>
    /// <param name="args">An <see cref="EventArgs"/> that contains the event data.</param>
    protected override void OnCursorChanged(EventArgs args) {
      base.OnCursorChanged(args);
      textBox.Cursor = Cursor;
    }

    /// <summary>
    /// This member overrides <see cref="Control.SetBoundsCore">Control.SetBoundsCore</see>.
    /// </summary>
    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified) {
      if (autoSize && height != Height) {
        height = PreferredHeight;
      }
      base.SetBoundsCore(x, y, width, height, specified);
      LayoutSubControls();
    }

    /// <summary>
    /// This member overrides <see cref="Control.OnFontChanged">Control.OnFontChanged</see>.
    /// </summary>
    protected override void OnFontChanged(EventArgs e) {
      base.OnFontChanged(e);
      AdjustHeight();
    }

    /// <summary>
    /// This member overrides <see cref="Control.OnHandleCreated">Control.OnHandleCreated</see>.
    /// </summary>
    protected override void OnHandleCreated(EventArgs args) {
      base.OnHandleCreated(args);
      AdjustHeight();
      LayoutSubControls();
    }

    /// <summary>
    /// This member overrides <see cref="Control.OnValidating">Control.OnValidating</see>.
    /// </summary>
    protected override void OnValidating(CancelEventArgs e) {
      genericValueEditorService.HideForm();
      {
        e.Cancel = false;
        if (currentValue == null) {
          if (textEdited && !ValidateText()) {
            e.Cancel = true;
            //                    return;
          } else if (!textEdited) {
            return;
          }
          //else return;
        } else if (currentValue == OptionItem.VALUE_UNDEFINED) {
          if (textEdited && !ValidateText()) {
            e.Cancel = true;
            //                    return;
          } else if (!textEdited) {
            return;
          }
          //else return;
        } else if (textEdited && !ValidateText()) {
          e.Cancel = true;
        }
      }
      LayoutSubControls();
      base.OnValidating(e);
    }

    /// <summary>
    /// This member overrides <see cref="Control.OnLeave">Control.OnLeave</see>.
    /// </summary>
    protected override void OnLeave(EventArgs e) {
      genericValueEditorService.HideForm();
      base.OnLeave(e);
    }

    /// <summary>
    /// This member overrides <see cref="Control.OnGotFocus">Control.OnGotFocus</see>.
    /// </summary>
    protected override void OnGotFocus(EventArgs e) {
      base.OnGotFocus(e);
      textBox.Focus();
      Invalidate(true);
    }

    /// <summary>
    /// This member overrides <see cref="Control.Focused">Control.Focused</see>.
    /// </summary>
    public override bool Focused {
      get { return textBox.Focused; }
    }

    /// <summary>
    /// This member overrides <see cref="Control.OnMouseDown">Control.OnMouseDown</see>.
    /// </summary>
    protected override void OnMouseDown(MouseEventArgs e) {
      base.OnMouseDown(e);
      Focus();
      if (!ReadOnly && !IsTextEditable()) {
        DropEditor();
      }
    }

    /// <summary>
    /// This member overrides <see cref="Control.OnEnabledChanged">Control.OnEnabledChanged</see>.
    /// </summary>
    protected override void OnEnabledChanged(EventArgs args) {
      base.OnEnabledChanged(args);
      textBox.Enabled = Enabled;
    }

    #endregion

    #region Misc

    protected override void OnPaintBackground(PaintEventArgs pevent) {
      Brush brush = new SolidBrush(Enabled ? BackColor : SystemColors.Control);
      pevent.Graphics.FillRectangle(brush, ClientRectangle);
      brush.Dispose();
    }

    private void AdjustHeight() {
      if (autoSize) {
        Height = PreferredHeight;
      }
    }

    private int BorderSize {
      get {
        switch (borderStyle) {
          case BorderStyle.FixedSingle:
            return 1;
          case BorderStyle.Fixed3D:
            return 2;
          default:
            return 0;
        }
      }
    }

    private int PreferredHeight {
      get {
        int preferred = Font.Height;
        if (borderStyle != BorderStyle.None) {
          Size size = SystemInformation.BorderSize;
          preferred += size.Height*4 + 3;
        }
        return preferred;
      }
    }


    /// <summary>
    /// Gets the picture box of the control.
    /// </summary>
    internal PreviewControl PreviewControl {
      get { return previewControl; }
    }


    /// <summary>
    /// Invoked when clicking the picture box.
    /// </summary>
    private void PreviewControlClicked(object sender, EventArgs args) {
      Focus();
      if (!IsTextEditable()) {
        DropEditor();
      }
    }

    /// <summary>
    /// Invoked when clicking the drop button.
    /// </summary>
    private void ButtonClicked(object sender, EventArgs args) {
      DropEditor();
    }

    private void LayoutSubControls() {
      Rectangle cRect = ClientRectangle;
      int buttonWidth = HasButton ? SystemInformation.VerticalScrollBarWidth : 0;

      previewControl.Visible = PaintValueSupported();
      editorButton.Visible = HasButton;

      if (PaintValueSupported()) {
        previewControl.SetBounds(cRect.X + 1, cRect.Y + 1,
                                 ShowPreviewOnly
                                   ? Math.Max(0, cRect.Width - buttonWidth - 2)
                                   :
                                 Math.Min(PAINT_VALUE_WIDTH,
                                          Math.Max(0, cRect.Width - buttonWidth - 2)),
                                 Math.Max(0, cRect.Height - 2));
      }

      if (HasButton) {
        editorButton.SetBounds(cRect.Right - buttonWidth,
                               cRect.Y, buttonWidth, cRect.Height);
      }

      if (!(ShowPreviewOnly && PaintValueSupported())) {
        int leftMargin = PaintValueSupported() ? PAINT_VALUE_WIDTH + 5 : 1;
        int topMargin = 0;
        switch (BorderStyle) {
          case BorderStyle.Fixed3D:
            topMargin = 1;
            break;
          case BorderStyle.FixedSingle:
            topMargin = 2;
            break;
        }
        textBox.SetBounds(cRect.X + leftMargin,
                          cRect.Y + topMargin,
                          Math.Max(0, cRect.Width - buttonWidth - leftMargin),
                          Math.Max(0, cRect.Height));
      } else {
        textBox.Width = 0;
      }
    }


    internal string GetValueAsText(object value) {
      //if (value == null)
      //    return string.Empty;
//            if (value is string)
//                return (string) value;
      try {
        if (Converter != null && Converter.CanConvertTo(typeof (string))) {
          return Converter.ConvertToString(value);
        }
      } catch (Exception e) {
//                Console.WriteLine("Validation exception");
        if (value is string) {
          return (string) value;
        } else {
          MessageBox.Show("Invalid value for option: " + e.Message, "Invalid value for option",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      return value != null
               ? value.ToString()
               : String.Empty;
    }

    /// <summary>
    /// Gets the list of standard values from the converter.
    /// </summary>
    /// <returns></returns>
    internal object[] GetStandardValues() {
      object[] values = null;

      TypeConverter converter = Converter;
      if (converter.GetStandardValuesSupported()) {
        ICollection standard = converter.GetStandardValues(
          //new SimpleTypeDescriptorContext(_descriptor)
          );
        values = new Object[standard.Count];
        standard.CopyTo(values, 0);
      }
      return values;
    }

    /// <summary>
    /// Drops the <see cref="UITypeEditor"/> associated with the edited value.
    /// </summary>
    /// <remarks>The method may also drop a list box if the edited value does not 
    /// have any editor and the type proposes standard values.
    /// </remarks>
    protected virtual void DropEditor() {
      UITypeEditor editor = Editor;

      if ((editor == null ||
           editor.GetEditStyle() == UITypeEditorEditStyle.None) &&
          HasStandardValues) {
        if (standardValuesUIEditor == null) {
          standardValuesUIEditor = new StandardValuesUIEditor(this);
        }
        editor = standardValuesUIEditor;
      }

      if (editor != null) {
        try {
          object result = editor.EditValue(genericValueEditorService, currentValue);
          Value = result;
        } catch (Exception) {}
      }
    }

    internal void SelectTextBox() {
      textBox.SelectAll();
      textBox.SelectionStart = 0;
      textBox.SelectionLength = 0;
    }


    internal bool IsTextEditable() {
      if (showPreviewOnly && PaintValueSupported()) {
        return false;
      }
      TypeConverter converter = Converter;
      if (converter != null) {
        if (converter.GetStandardValuesSupported() &&
            converter.GetStandardValuesExclusive()) {
          return false;
        } else {
          return true;
        }
      }
      return false;
    }

    private void TextBoxValidating(object sender, CancelEventArgs e) {
      OnValidating(e);
    }

    private void TextBoxValidated(object sender, EventArgs e) {
      OnValidated(e);
    }

    private void TextBoxTextChanged(object sender, EventArgs e) {
      OnTextChanged(e);
    }

    private void TextBoxKeyPress(object sender, KeyPressEventArgs ke) {
      OnKeyPress(ke);
    }

    private void TextBoxKeyDown(object sender, KeyEventArgs ke) {
      OnKeyDown(ke);
    }

    private void TextBoxKeyUp(object sender, KeyEventArgs ke) {
      OnKeyUp(ke);
    }

    private void TextBoxLostFocus(object sender, EventArgs e) {
      Invalidate(true);
    }

    private void TextBoxGotFocus(object sender, EventArgs e) {
      Invalidate(true);
    }

    ///<summary>
    ///Raises the <see cref="E:System.Windows.Forms.Control.TextChanged"></see> event.
    ///</summary>
    ///
    ///<param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data. </param>
    protected override void OnTextChanged(EventArgs e) {
//            if(!IsTextEditable())
//            {
//                textBox.Undo();
//                textEdited = false;
//                return;
//            }
      textEdited = true;
      base.OnTextChanged(e);
    }

    /// <summary>
    /// This member overrides <see cref="Control.OnKeyPress">Control.OnKeyPress</see>.
    /// </summary>
    protected override void OnKeyPress(KeyPressEventArgs ke) {
      if (!(IsTextEditable())) {
        ke.Handled = true;
      } else if (ke.KeyChar == (char) 13 || ke.KeyChar == (char) 27) {
        ke.Handled = true; // avoid beep done by TextBox when
        // multiline is not allowed
      }
      base.OnKeyPress(ke);
    }

    private void SelectStandardValue(bool next) {
      if (!HasStandardValues) {
        return;
      }
      object[] values = GetStandardValues();
      int validation = next
                         ? validation = values.Length - 1
                         : 0;
      for (int i = 0; i < values.Length; i++) {
        if (values[i].Equals(currentValue)) {
          if (next) {
            if (i == 0) {
              return;
            }
            validation = i - 1;
          } else {
            if (i == values.Length - 1) {
              return;
            }
            validation = i + 1;
          }
          break;
        }
      }
      ValidateValue(values[validation]);
      SelectTextBox();
    }

    /// <summary>
    /// This member overrides <see cref="Control.OnMouseWheel">Control.OnMouseWheel</see>.
    /// </summary>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the data.</param>
    /// <remarks>The default implementation iterates on the standard values proposed by
    /// the edited type, if any.</remarks>
    protected override void OnMouseWheel(MouseEventArgs e) {
      if (textBox.Focused && !ReadOnly) {
        if (HasStandardValues) {
          SelectStandardValue(e.Delta > 0);
        }
      }
      base.OnMouseWheel(e);
    }

    /// <summary>
    /// This member overrides <see cref="Control.OnKeyDown">Control.OnKeyDown</see>.
    /// </summary>
    protected override void OnKeyDown(KeyEventArgs ke) {
      if (!ReadOnly) {
        bool alt = ke.Alt;
        if (!(alt) && ke.KeyCode == Keys.Down || ke.KeyCode == Keys.Up) {
          if (HasStandardValues) {
            SelectStandardValue(ke.KeyCode == Keys.Down);
            SelectTextBox();
          }
        }
        if (alt && ke.KeyCode == Keys.Down && HasButton) {
          ke.Handled = true;
          DropEditor();
        } else if (ke.KeyCode == Keys.Enter) {
          ke.Handled = true;
          ValidateText();
        } else if (ke.KeyCode == Keys.Escape) {
          // ??
        }
      }
      if (!IsTextEditable()) {
        ke.Handled = true;
      }
      base.OnKeyDown(ke);
    }

    private bool ValidateValue(object value) {
      try {
        genericValueEditorService.CloseDropDown();
        Value = value;
      } catch (Exception) {
        return false;
      }
      return true;
    }

    private void UpdateTextBoxWithValue() {
      //if(textEdited)
      textBox.Text = GetValueAsText(currentValue);
      //we don't want to get the events again
      textEdited = false;
    }


    /// <summary>
    /// Is called to validate the text that is currently edited by the control.
    /// </summary>
    /// <returns><see langword="true"/> if the string has been successfully converted into 
    /// the type defined by the property ; <see langword="false"/> otherwise.
    /// </returns>
    protected virtual bool ValidateText() {
      if (!textEdited) {
        return true;
      }
      if (!ValidateText(textBox.Text)) {
        UpdateTextBoxWithValue();
        return false;
      } else {
        return true;
      }
    }

    private bool ValidateText(string text) {
      object value = null;
      try {
        if (Converter != null && Converter.CanConvertFrom(typeof (string))) {
          value = Converter.ConvertFromString(
            null, CultureInfo.CurrentCulture, text);
        }
        return ValidateValue(value);
      } catch (Exception e) {
//                Console.WriteLine("Validation Exception");
        MessageBox.Show("Invalid value for option: " + e.Message, "Invalid value for option",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        if (currentValue == null) {
          textEdited = false;
          return false;
          //return ValidateValue(null);
        } else if (currentValue == OptionItem.VALUE_UNDEFINED) {
//                    textBox.Undo();
          textEdited = false;
          return false;
//                    return ValidateValue(OptionItem.VALUE_UNDEFINED);
        }
      }

      return false;
    }

    #endregion

    public bool OnComponentChanging() {
      throw new NotImplementedException();
    }

    public void OnComponentChanged() {
      throw new NotImplementedException();
    }

    public object Instance {
      get { throw new NotImplementedException(); }
    }


    public PropertyDescriptor PropertyDescriptor {
      get { return _descriptor; }
    }

    object IServiceProvider.GetService(Type serviceType) {
      return null;
    }
  }

  #region Internal classes...

  /// <summary>
  /// The small rectangle that paints the current edited value.
  /// </summary>
  internal class PreviewControl : Button
  {
    private GenericValueEditor editor;

    public PreviewControl(GenericValueEditor editor) {
      this.editor = editor;
      Cursor = Cursors.Default;
    }

    protected override void OnPaint(PaintEventArgs pe) {
      Rectangle rect = ClientRectangle;
      Brush b = new SolidBrush(editor.BackColor);
      pe.Graphics.FillRectangle(b, rect);
      b.Dispose();
      editor.Editor.PaintValue(editor.Value, pe.Graphics, rect);
      pe.Graphics.DrawRectangle(SystemPens.WindowText, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
    }
  }

  /// <summary>
  /// The button that opens <see cref="UITypeEditor"/> controls.
  /// </summary>
  internal class EditorButton : Button
  {
    /// <summary>
    /// Indicates whether the button should be displayed as a 
    /// drop-down arrow or as a dialog button.
    /// </summary>
    private bool dialog;

    private bool pushed;

    /// <summary>
    /// Creates a <strong>EditorButton</strong>.
    /// </summary>
    public EditorButton() {
      SetStyle(ControlStyles.Selectable, true);
      BackColor = SystemColors.Control;
      ForeColor = SystemColors.ControlText;
      TabStop = false;
      IsDefault = false;
      dialog = false;
      Cursor = Cursors.Default;
    }

    /// <summary>
    /// Gets or sets a value indicating if the button should be 
    /// drawn as a drop dialog button or as a drop button.
    /// </summary>
    /// <value><see langword="true"/> if the button should be 
    /// drawn as a drop dialog button; <see langword="false"/> otherwise.</value>
    public bool IsDialog {
      get { return dialog; }
      set {
        dialog = value;
        Invalidate();
      }
    }

    protected override void OnMouseDown(MouseEventArgs arg) {
      base.OnMouseDown(arg);
      if (arg.Button == MouseButtons.Left) {
        pushed = true;
        Invalidate();
      }
    }

    protected override void OnMouseUp(MouseEventArgs arg) {
      base.OnMouseUp(arg);
      if (arg.Button == MouseButtons.Left) {
        pushed = false;
        Invalidate();
      }
    }

    /// <summary>
    /// This member overrides <see cref="Control.OnPaint">Control.OnPaint</see>.
    /// </summary>
    protected override void OnPaint(PaintEventArgs pe) {
      Graphics g = pe.Graphics;
      Rectangle r = ClientRectangle;

      if (dialog) {
        base.OnPaint(pe);
        // draws dot dot dot.
        int x = r.X + r.Width/2 - 5;
        int y = r.Bottom - 5;
        Brush brush = new SolidBrush(Enabled ? SystemColors.ControlText : SystemColors.GrayText);
        g.FillRectangle(brush, x, y, 2, 2);
        g.FillRectangle(brush, x + 4, y, 2, 2);
        g.FillRectangle(brush, x + 8, y, 2, 2);
        brush.Dispose();
      } else {
        ControlPaint.DrawComboButton(g, ClientRectangle,
                                     !Enabled
                                       ? ButtonState.Inactive
                                       : (pushed ? ButtonState.Pushed : ButtonState.Normal));
      }
    }
  }


  internal class StandardValuesUIEditor : UITypeEditor
  {
    private GenericValueEditor editor;
    private StandardValuesListBox listbox;
    private IWindowsFormsEditorService edSvc;

    public StandardValuesUIEditor(GenericValueEditor editor) {
      this.editor = editor;
    }

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
      return UITypeEditorEditStyle.DropDown;
    }

    public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
      // Uses the IWindowsFormsEditorService to display a  drop-down UI
      if (edSvc == null) {
        edSvc = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
      }
      if (edSvc != null) {
        if (listbox == null) {
          listbox = new StandardValuesListBox(editor);
          listbox.SelectedIndexChanged += new EventHandler(OnListBoxChanged);
        }
        object[] values = editor.GetStandardValues();
        listbox.Items.Clear();

        int width = 0;
        Font font = listbox.Font;

        // Add the standard values in the list box and
        // measure the text at the same time.

        using (Graphics g = listbox.CreateGraphics()) {
          foreach (object item in values) {
            if (item == null || !listbox.Items.Contains(item)) {
              string valueString = editor.GetValueAsText(item);
              if (!editor.ShowPreviewOnly) {
                width = (int) Math.Max(width, g.MeasureString(valueString, font).Width);
              }
              if (item == null) {
                listbox.Items.Add(valueString);
              } else {
                listbox.Items.Add(item);
              }
            }
          }
        }

        if (editor.PaintValueSupported()) {
          width += GenericValueEditor.PAINT_VALUE_WIDTH + 4;
        }

        Rectangle bounds = editor.Bounds;
        listbox.SelectedItem = value;
        listbox.Height =
          Math.Max(font.Height + 2, Math.Min(200, listbox.PreferredHeight));
        listbox.Width = Math.Max(width, bounds.Width);

        edSvc.DropDownControl(listbox);

        if (listbox.SelectedItem != null) {
          return listbox.SelectedItem;
        } else {
          return value;
        }
      }
      return value;
    }

    private void OnListBoxChanged(object sender, EventArgs e) {
      edSvc.CloseDropDown();
    }
  }

  /// <summary>
  /// <strong>ListBox</strong> which is dropped when the type contains standard values.
  /// </summary>
  /// 
  internal class StandardValuesListBox : ListBox
  {
    private GenericValueEditor editor;

    /// <summary>
    /// Creates a <strong>DropListBox</strong>.
    /// </summary>
    public StandardValuesListBox(GenericValueEditor control) {
      editor = control;
      BorderStyle = BorderStyle.None;
      IntegralHeight = false;
      DrawMode = DrawMode.OwnerDrawVariable;
    }

    /// <summary>
    /// This member overrides <see cref="ListBox.OnDrawItem">ListBox.OnDrawItem</see>.
    /// </summary>
    protected override void OnDrawItem(DrawItemEventArgs e) {
      e.DrawBackground();

      if (e.Index < 0 || e.Index >= Items.Count) {
        return;
      }

      object value = Items[e.Index];
      Rectangle bounds = e.Bounds;

      if (editor.PaintValueSupported()) {
        Pen pen = new Pen(ForeColor);
        try {
          Rectangle r = e.Bounds;
          r.Height -= 1;
          if (editor.ShowPreviewOnly) {
            r.X += 2;
            r.Width -= 5;
          } else {
            r.Width = GenericValueEditor.PAINT_VALUE_WIDTH;
            r.X += 2;
            bounds.X += GenericValueEditor.PAINT_VALUE_WIDTH + 2;
            bounds.Width -= GenericValueEditor.PAINT_VALUE_WIDTH + 2;
          }
          editor.Editor.PaintValue(value, e.Graphics, r);
          e.Graphics.DrawRectangle(pen, r);
        } finally {
          pen.Dispose();
        }
      }
      if (!editor.ShowPreviewOnly || !editor.PaintValueSupported()) {
        Brush brush = new SolidBrush(e.ForeColor);
        StringFormat format = new StringFormat();

        try {
          e.Graphics.DrawString(editor.GetValueAsText(value), Font, brush, bounds, format);
        } finally {
          brush.Dispose();
          format.Dispose();
        }
      }
    }

    /// <summary>
    /// This member overrides <see cref="ListBox.OnMeasureItem">ListBox.OnMeasureItem</see>.
    /// </summary>
    protected override void OnMeasureItem(MeasureItemEventArgs e) {
      e.ItemHeight += 1;
    }
  }

  internal class SimpleTypeDescriptorContext : ITypeDescriptorContext
  {
    private PropertyDescriptor descriptor;

    public SimpleTypeDescriptorContext(PropertyDescriptor descriptor) {
      this.descriptor = descriptor;
    }

    #region ITypeDescriptorContext Members

    public IContainer Container {
      get { return null; }
    }

    public object Instance {
      get { throw new Exception("The method or operation is not implemented."); }
    }

    public void OnComponentChanged() {
      throw new Exception("The method or operation is not implemented.");
    }

    public bool OnComponentChanging() {
      throw new Exception("The method or operation is not implemented.");
    }

    public PropertyDescriptor PropertyDescriptor {
      get { return descriptor; }
    }

    #endregion

    #region IServiceProvider Members

    public object GetService(Type serviceType) {
      return null;
    }

    #endregion
  }

  #endregion
}
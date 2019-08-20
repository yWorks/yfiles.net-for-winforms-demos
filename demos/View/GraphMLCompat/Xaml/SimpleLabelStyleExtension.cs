/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.2.
 ** Copyright (c) 2000-2019 by yWorks GmbH, Vor dem Kreuzberg 28,
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
using System.Drawing;
using System.Reflection;
using System.Windows.Markup;
using yWorks.Graph.Styles;

namespace Demo.yFiles.IO.GraphML.Compat.Xaml
{
  [Obfuscation(StripAfterObfuscation = false, Exclude = true, ApplyToMembers = true)]
  public class SimpleLabelStyleExtension : MarkupExtension
  {
    public SimpleLabelStyleExtension() {
      BackgroundBrush = null;
      StringFormat = (StringFormat) StringFormat.GenericDefault.Clone();
      ClipText = true;
      BackgroundPen = null;
      Font = SystemFonts.DefaultFont;
      AutoFlip = true;
      TextBrush = Brushes.Black;
      NormalizeBrushes = true;
    }


    public Brush BackgroundBrush { get; set; }
    public bool ClipText { get; set; }
    public Pen BackgroundPen { get; set; }
    public bool AutoFlip { get; set; }
    public bool NormalizeBrushes { get; set; }
    public Brush TextBrush { get; set; }
    public Font Font { get; set; }
    public StringFormat StringFormat { get; set; }

    #region Overrides of MarkupExtension

    public override object ProvideValue(IServiceProvider serviceProvider) {
      return new DefaultLabelStyle
      {
        BackgroundBrush = BackgroundBrush,
        StringFormat = StringFormat,
        ClipText=ClipText,
        BackgroundPen = BackgroundPen,
        AutoFlip = AutoFlip,
        TextBrush = TextBrush,
        Font = Font,
        NormalizeBrushes = NormalizeBrushes
      };
    }

    #endregion
  }

}
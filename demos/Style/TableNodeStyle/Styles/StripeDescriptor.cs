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

using System.ComponentModel;
using System.Drawing;

namespace Demo.yFiles.Graph.TableNodeStyle.Style
{
  /// <summary>
  /// Helper class that can be used as StyleTag to bundle common visualization parameters for stripes
  /// </summary>
  public class StripeDescriptor
  {
    private Brush backgroundBrush = Brushes.Transparent;

    /// <summary>
    /// The background brush for a stripe
    /// </summary>
    [DefaultValue(typeof(Brush), "Transparent")]
    public Brush BackgroundBrush {
      get { return backgroundBrush; }
      set { backgroundBrush = value; }
    }

    private Brush insetBrush = Brushes.Transparent;

    /// <summary>
    /// The inset brush for a stripe
    /// </summary>
    [DefaultValue(typeof(Brush), "Transparent")]
    public Brush InsetBrush {
      get { return insetBrush; }
      set { insetBrush = value; }
    }

    private Brush borderBrush = Brushes.Black;

    /// <summary>
    /// The border brush for a stripe
    /// </summary>
    [DefaultValue(typeof(Brush), "Black")]
    public Brush BorderBrush {
      get { return borderBrush; }
      set { borderBrush = value; }
    }

    private float borderThickness = 1;

    /// <summary>
    /// The border thickness for a stripe
    /// </summary>
    [DefaultValue(1f)]
    public float BorderThickness {
      get { return borderThickness; }
      set { borderThickness = value; }
    }
  }
}
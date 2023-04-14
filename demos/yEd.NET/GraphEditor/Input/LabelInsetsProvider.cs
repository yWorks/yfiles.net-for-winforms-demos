/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.5.
 ** Copyright (c) 2000-2023 by yWorks GmbH, Vor dem Kreuzberg 28,
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

using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;

namespace Demo.yFiles.GraphEditor.Input
{
  /// <summary>
  /// Provides insets for node instances via the insets suggested by the labels.
  /// </summary>
  public class LabelInsetsProvider : INodeInsetsProvider
  {
    private readonly INodeInsetsProvider innnerProvider;

    public LabelInsetsProvider(INodeInsetsProvider innnerProvider) {
      this.innnerProvider = innnerProvider;
    }

    public InsetsD GetInsets(INode item) {
      InsetsD result = InsetsD.Empty;
      if (innnerProvider != null) {
        result = innnerProvider.GetInsets(item);
      } 
      foreach (ILabel label in item.Labels) {
        var provider =
          label.LayoutParameter.Model.Lookup<INodeInsetsProvider>();
        if (provider != null) {
          InsetsD insets = provider.GetInsets(item);
          result = result.CreateUnion(insets);
        }
      }
      return result;
    }
  }
}

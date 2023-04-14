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

namespace Demo.yFiles.Layout.LayerConstraints
{
  /// <summary>
  /// A business object that represents the weight (through property "Value") of the node and whether or not
  /// its weight should be taken into account as a layer constraint.
  /// </summary>
  public class LayerConstraintsInfo
  {
    /// <summary>
    /// The weight of the object. An object with a lower number will be layered in a higher layer.
    /// </summary>
    /// <remarks>
    /// The number 0 means the node should be the in the first, 7 means it should be the last layer.
    /// </remarks>
    private int value;

    /// <summary>
    /// Determines whether this instances value can be increased.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instances value can be increased; otherwise, <c>false</c>.
    /// </returns>
    public bool CanIncreaseValue() {
      return Constraints && 0 < value;
    }

    /// <summary>
    /// Determines whether this instances value can be decreased.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance value can be decreased; otherwise, <c>false</c>.
    /// </returns>
    public bool CanDecreaseValue() {
      return Constraints && value < 7;
    }

    /// <summary>
    /// Increases the value.
    /// </summary>
    public void IncreaseValue() {
      if(CanIncreaseValue()) {
        Value--;
      }
    }

    /// <summary>
    /// Decreases the value.
    /// </summary>
    public void DecreaseValue() {
      if(CanDecreaseValue()) {
        Value++;
      }
    }

    /// <summary>
    /// Toggles the state.
    /// </summary>
    public void ToggleState() {
      Constraints = !Constraints;
    }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String"/> that represents this instance.
    /// </returns>
    public override string ToString() {
      switch (value) {
        case 0:
          return "First";
        case 7:
          return "Last";
        default:
          return value.ToString();
      }
    }

    /// <summary>
    /// The weight of the object. An object with a lower number will be layered in a higher layer.
    /// </summary>
    /// <remarks>
    /// The number 0 means the node should be the in the first, 7 means it should be the last layer.
    /// </remarks>
    public int Value {
      get { return value; }
      set {
        int oldVal = this.value;
        this.value = value;
        if (oldVal != value) {
          LayerConstraintsForm.InvalidateControlCommand.Execute(null, LayerConstraintsForm.GetActiveGraphControl());
        }
      }
    }

    private bool constraints;

    /// <summary>
    /// Describes whether or not the constraint is active. If <see langword="true"/>, the constraint will be
    /// taken into account by the layout algorithm.
    /// </summary>
    public bool Constraints {
      get { return constraints; }
      set {
        if (constraints != value) {
          constraints = value;
          LayerConstraintsForm.InvalidateControlCommand.Execute(null, LayerConstraintsForm.GetActiveGraphControl());
        }
      }
    }
  }
}
/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.3.
 ** Copyright (c) 2000-2020 by yWorks GmbH, Vor dem Kreuzberg 28,
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

using System.Collections.Generic;

namespace Demo.yFiles.Graph.OrgChart
{
  public class Employee
  {
    public Employee() {
      this.SubOrdinates = new List<Employee>();
    }

    public string Name { get; set; }

    public string Position { get; set; }

    public string Fax { get; set; }

    public string BusinessUnit { get; set; }

    public List<Employee> SubOrdinates{
      get;
      private set;}

    private EmployeeStatus status = EmployeeStatus.Present;
    public EmployeeStatus Status {
      get { return status; }
      set { status = value; }
    }

    public string Icon { get; set; }

    private EmployeeLayout layout = EmployeeLayout.None;

    public EmployeeLayout Layout {
      get { return layout; }
      set { layout = value; }
    }

    public bool Assistant { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }
  }

  public enum EmployeeStatus
  {
    Present,
    Unavailable,
    Travel
  }
  
  public enum EmployeeLayout
  {
    None,
    LeftHanging,
    RightHanging,
    BothHanging
  }
}

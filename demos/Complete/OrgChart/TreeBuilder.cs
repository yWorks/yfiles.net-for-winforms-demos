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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using yWorks.Graph;

namespace Demo.yFiles.Graph.OrgChart
{
  /// <summary>
  /// A simple helper class that builds the Organization Chart from the XML data.
  /// </summary>
  /// <remarks>
  /// This class is fairly focused on that one single data file. For a more generic approach that works with
  /// different kinds of data, consider using
  /// <see cref="SimpleGraphBuilder{TNode,TEdge,TGroup}"/> or <see cref="TreeBuilder{TNode,TGroup}"/>.
  /// </remarks>
  internal static class TreeBuilder
  {
    public static IEnumerable<Employee> BuildEmployeesFromXml() {
      TextReader reader = new StreamReader("Resources/orgchartmodel.xml");

      XmlReader xr = XmlReader.Create(reader);
      XDocument xd = XDocument.Load(xr);
      
      var roots = xd.Root.Elements("employee").Select(element => CreateSubtree(CreateEmployee(element), element)).ToList();

      return roots;
    }

    private static Employee CreateEmployee(XElement element) {
      var employee = new Employee
                       {
                         Name = (string) element.Attribute("name"),
                         Position = (string) element.Attribute("position"),
                         Fax = (string) element.Attribute("fax"),
                         Phone = (string) element.Attribute("phone"),
                         Email = (string) element.Attribute("email"),
                         BusinessUnit = (string) element.Attribute("businessUnit"),
                         Icon = (string) element.Attribute("icon")
                       };
      var statusAttr = element.Attribute("status");
      
      if(statusAttr != null) {
        employee.Status = (EmployeeStatus) Enum.Parse(typeof (EmployeeStatus), (string) statusAttr, true);
      }
      
      var layoutAttr = element.Attribute("layout");
      if (layoutAttr != null) {
        employee.Layout = (EmployeeLayout)Enum.Parse(typeof(EmployeeLayout), (string)layoutAttr, true);
      }

      var assistantAttr = element.Attribute("assistant");
      if (assistantAttr != null) {
        employee.Assistant = (bool)assistantAttr;
      }
      return employee;
    }

    private static Employee CreateSubtree(Employee parent, XElement element) {
      foreach (XElement child in element.Elements("employee")) {
        var subtreeRoot = CreateEmployee(child);

        // Add new employee to list of children
        Employee childEmployee = subtreeRoot;
        Employee parentEmployee = parent;
        if (parentEmployee != null) {
          parentEmployee.SubOrdinates.Add(childEmployee);
        }

        CreateSubtree(subtreeRoot, child);
      }
      return parent;
    }
  }
}

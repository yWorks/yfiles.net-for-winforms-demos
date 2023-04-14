
# 02 Custom Styles
This tutorial is a step-by-step introduction to customizing the visual representation of graph elements. It is intended for developers that want to learn how to work with styles and how to customize them. <br /> The tutorial shows you how to create custom styles for nodes, edges, labels, and ports. It also presents how to create a custom arrowhead rendering, how to customize edge selection, and how the visual representation of graph elements can be changed depending on application state. Furthermore, the tutorial discusses several optimization strategies. 

If you are new to styles, respectively their customization, it is recommended to start by going through the projects in this tutorial one by one. To make full use of the tutorial, it is also recommended to review and possibly modify the source code for each sample project. 



You will find the following programming samples in this package: 


| Name | Description |
|:---|:---|
|**01 Custom Node Style** | Shows how to create a custom node style. |
|**02 Node Color** | Shows how to create properties so that the style can be changed dynamically. |
|**03 Dropshadow** | This step shows how to add a drop shadow to the custom node style. |
|**04 IsInside** | Shows how to override IsInside() and GetIntersection() in SimpleAbstractNodeStyle&lt;TVisual&gt;. |
|**05 Hit Test** | Shows how to override IsHit() and IsInBox() in SimpleAbstractNodeStyle&lt;TVisual&gt;. |
|**06 GetBounds** | Shows how to override the SimpleAbstractNodeStyle&lt;TVisual&gt;.GetBounds() method. |
|**07 Rendering Performance** | Shows how to pre-render the drop shadow of nodes in order to improve the rendering performance in comparison to the built-in effect. |
|**08 Edge from Node to Label** | Shows how to visually connect a node to its label(s) by means of edges. |
|**09 Visibility Test** | This step shows how to override the IsVisible() method of SimpleAbstractNodeStyle. |
|**10 Custom Label Style** | Shows how to create a custom label style. |
|**11 Label Preferred Size** | Shows how to override the GetPreferredSize() method to  set the size of the label dependent on the size of its text. |
|**12 Label Edit Button** | Shows how to implement a button within a label to open the label editor. |
|**13 Button Visibility** | This step shows how to hide the button dependent on the zoom level. |
|**14 Using Data in Label Tag** | Shows how to use data from a business object, which is stored in the label's tag, for rendering. |
|**15 Custom Edge Style** | Shows how to create a custom edge style which allows to specify the edge thickness by setting a property on the style. |
|**16 Edge Hit Test** | This step shows how to take the thickness of the edge into account when checking if the edge was clicked. |
|**17 Edge Cropping** | Shows how to crop an edge at the node bounds. |
|**18 Custom Arrow** | Shows how to create a custom arrow. |
|**19 Arrow Thickness** | Shows how to render the arrow dependent on a property of the edge it belongs to. |
|**20 Custom Ports** | Shows how to implement a custom port style. |
|**21 Style Decorator** | Shows how to create a style that decorates an existing style. |
|**22 Bridge Support** | Shows how to enable bridges for a custom edge style. |

## Running the Demos

### With Visual Studio

* To load all samples into Visual Studio you can simply open the solution file yFiles Tutorials.sln. 
* To load a single sample into Visual Studio you can open the project file (.csproj) in the sample's directory. 




#### See also
[Product Page](https://www.yworks.com/products/yfiles.net)  
[API Documentation](https://docs.yworks.com/yfilesdotnet)    
[Help and Support](https://www.yworks.com/products/yfiles/support)


#### Contact
yWorks GmbH  
Vor dem Kreuzberg 28  
72070 Tuebingen  
Germany  
Phone: +49 7071 979050
Email: contact@yworks.com

COPYRIGHT &#x00A9; 2021 yWorks   



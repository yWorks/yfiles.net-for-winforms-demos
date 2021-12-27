# 14 Automatic Graph Layout

This demo shows how to use the layout algorithms in yFiles.NET to automatically
  place the graph elements.
  

To use the layout algorithms you have to reference the following
  assemblies in addition to `yFilesViewer.dll`:
  
- `yFilesAlgorithms.dll`: This assembly contains the actual layout
  and analysis algorithms.
- `yFilesAdapter.dll`: This class is necessary to use the layout and
  analysis algorithms with the object model in the
  yFiles.NET viewer part.


  Note that this sample loads a sample graph from a file instead of creating it
  programmatically, since the graphs from the previous examples
  are not really suited for automatic layout.

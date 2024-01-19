/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.6.
 ** Copyright (c) 2000-2024 by yWorks GmbH, Vor dem Kreuzberg 28,
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
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Demo.yFiles.ImageExport
{
  /// <summary>
  /// Taken from http://support.microsoft.com/kb/323530/en-us/
  /// </summary>
  public class ClipboardMetafileHelper
  {
    [DllImport("user32.dll")]
    static extern bool OpenClipboard(IntPtr hWndNewOwner);
    [DllImport("user32.dll")]
    static extern bool EmptyClipboard();
    [DllImport("user32.dll")]
    static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);
    [DllImport("user32.dll")]
    static extern bool CloseClipboard();
    [DllImport("gdi32.dll")]
    static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, IntPtr hNULL);
    [DllImport("gdi32.dll")]
    static extern bool DeleteEnhMetaFile(IntPtr hemf);

    // Metafile mf is set to a state that is not valid inside this function.
    public static bool PutEnhMetafileOnClipboard(IntPtr hWnd, Metafile mf) {
      bool bResult = false;
      var hEMF = mf.GetHenhmetafile();
      if (!hEMF.Equals(new IntPtr(0))) {
        var hEMF2 = CopyEnhMetaFile(hEMF, new IntPtr(0));
        if (!hEMF2.Equals(new IntPtr(0))) {
          if (OpenClipboard(hWnd)) {
            if (EmptyClipboard()) {
              IntPtr hRes = SetClipboardData(14 /*CF_ENHMETAFILE*/, hEMF2);
              bResult = hRes.Equals(hEMF2);
              CloseClipboard();
            }
          }
        }
        DeleteEnhMetaFile(hEMF);
      }
      return bResult;
    }
  }
}
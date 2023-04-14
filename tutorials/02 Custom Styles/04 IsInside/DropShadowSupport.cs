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
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security;

namespace Tutorial.CustomStyles
{
  /// <summary>
  /// Provides static helper methods to calculate and blur a drop shadow.
  /// </summary>
  internal class DropShadowSupport
  {
    
      #region Low Level Calculations

      /// <summary>
      /// Creates a Drop Shadow from a given bitmap.
      /// </summary>
      /// <param name="bitmap">The bitmap to create a drop shadow for.</param>
      /// <param name="color">The color of the shadow.</param>
      /// <param name="kernel">The kernel to use</param>
      public static void DropShadow(Bitmap bitmap, Color color, int[] kernel) {
        BitmapData data =
            bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

        try {
          int count1 = data.Width*data.Height;
          int count = data.Width*data.Height*4;
          byte[] src = new byte[count];
          byte[] src2 = new byte[count1];
          byte[] target = new byte[count1];
          Marshal.Copy(data.Scan0, src, 0, count);

          int c = 0;
          for (int i = 3; i < src.Length; i += 4, c++) {
            src2[c] = src[i];
          }
          ApplyKernelHorizontallyA(data, kernel, src2, target);
          ApplyKernelVerticallyA(data, kernel, target, src2);
          c = 0;
          byte b = color.B;
          byte r = color.R;
          byte g = color.G;
          byte a = color.A;
          for (int i = 0; i < src.Length - 4;) {
            src[i++] = b;
            src[i++] = g;
            src[i++] = r;
            src[i++] = (byte) ((src2[c++]*a)/255);
          }

          Marshal.Copy(src, 0, data.Scan0, count);
          return;
        } catch (SecurityException) {
          // ok - we cannot do that - lets just use the slow version of the code...
        } catch (MethodAccessException) {
          // ok - we cannot do that - lets just use the slow version of the code...
        } finally {
          bitmap.UnlockBits(data);
        }
        // slow version but no securitypermission required.
        {
          int count1 = bitmap.Width*bitmap.Height;
          byte[] src2 = new byte[count1];
          byte[] target = new byte[count1];

          for (int y = 0; y < bitmap.Height; y++) {
            int offset = y*bitmap.Width;
            for (int x = 0; x < bitmap.Width; x++) {
              Color pixel = bitmap.GetPixel(x, y);
              src2[x + offset] = pixel.A;
            }
          }

          ApplyKernelHorizontallyA(data, kernel, src2, target);
          ApplyKernelVerticallyA(data, kernel, target, src2);

          for (int y = 0; y < bitmap.Height; y++) {
            int offset = y*bitmap.Width;
            for (int x = 0; x < bitmap.Width; x++) {
              Color newPixel = Color.FromArgb((byte) ((src2[x + offset]*color.A)/255), color.R, color.G, color.B);
              bitmap.SetPixel(x, y, newPixel);
            }
          }
        }
      }

      private static void ApplyKernelHorizontallyA(BitmapData data, int[] kernel, byte[] src, byte[] target) {
        int k2 = (kernel.Length + 1)/2;
        int kend = kernel.Length - k2;
        int yend = data.Height;
        int xend = data.Width - kend;
        for (int y = 0; y < yend; y++) {
          int offset = y*data.Width;
          offset += k2;
          for (int x = k2; x < xend; x++) {
            int fa = 0;
            int k = 0;
            int p = offset - k2;
            for (int i = -k2; i < kend; i++) {
              fa += src[p++]*kernel[k++];
            }
            target[offset++] = (byte) Math.Min(255, Math.Max(fa/256, 0));
          }
        }
      }

      private static void ApplyKernelVerticallyA(BitmapData data, int[] kernel, byte[] src, byte[] target) {
        int k2 = (kernel.Length + 1)/2;
        int kend = kernel.Length - k2;
        int yend = data.Height - kend;
        int xend = data.Width;
        int deltaY = data.Width;
        int d1 = k2*deltaY;
        for (int x = 0; x < xend; x++) {
          int offset = d1 + x;
          for (int y = k2; y < yend; y++, offset += deltaY) {
            int srcOffset = offset;
            int fa = 0;
            int k = 0;
            int l = srcOffset - d1;
            for (int i = -k2; i < kend; i++, l += deltaY) {
              fa += src[l]*kernel[k++];
            }
            target[srcOffset] = (byte) Math.Min(255, Math.Max(fa/256, 0));
          }
        }
      }

      /// <summary>
      /// Creates a 1 dimensional gaussian kernel normalized to integer values between 0 and 255.
      /// </summary>
      public static int[] Gaussian1DScaled(double theta, int size) {
        int[] kernel = new int[size];
        double[] ktemp = new double[size];
        double sum = 0;
        for (int i = 0; i < size; ++i) {
          var val = GaussianDiscrete1D(theta, i - (size*.5d));
          ktemp[i] = val;
          sum += val;
        }
        for (int i = 0; i < kernel.Length; ++i) {
          kernel[i] = (int) Math.Round(256*(ktemp[i]/sum));
        }
        return kernel;
      }

      private static double GaussianDiscrete1D(double theta, double x) {
        double tmp = 1.0/(Math.Sqrt(2.0*Math.PI)*theta);
        double g = 0;
        int i = 0;
        for (double xSubPixel = x - 0.5; i < 11; i++, xSubPixel += 0.1) {
          g += tmp*Math.Pow(Math.E, -xSubPixel*xSubPixel/(2*theta*theta));
        }
        return g/11.0d;
      }

      #endregion

  }
}

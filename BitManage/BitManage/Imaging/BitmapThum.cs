using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitManage.Imaging
{
    public static class BitmapThum
    {
        public static unsafe void GetThum(Bitmap src, Bitmap dst)
        {
            BitmapData srcdata = null;
            BitmapData dstdata = null;
            try
            {
                int dw = dst.Width;
                int dh = dst.Height;
                int sw = src.Width;
                int sh = src.Height;
                srcdata = src.LockBits(new Rectangle(0, 0, sw, sh), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                dstdata = dst.LockBits(new Rectangle(0, 0, dw, dh), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                float zx = (float)src.Width / dst.Width;
                float zy = (float)src.Height / dst.Height;
                int* srcInt = (int*)srcdata.Scan0;
                int* dstInt = (int*)dstdata.Scan0;
                Parallel.For(0, dh, (j) =>
                  {
                      int dd = j * dw;
                      int sy = Convert.ToInt32(j * zy);
                      if (sy >= sh) return;
                      int ds = sy * sw;
                      for (int i = 0; i < dw; ++i)
                      {
                          int sx = Convert.ToInt32(i * zx);
                          if (sx < sw)
                          {
                              dstInt[dd] = srcInt[ds + sx];
                          }
                          ++dd;
                      }
                  });
                src.UnlockBits(srcdata); srcdata = null;
                dst.UnlockBits(dstdata); dstdata = null;
            }
            catch(Exception ex)
            {
                if (srcdata != null)
                {
                    src.UnlockBits(srcdata); srcdata = null;
                }
                if (dstdata != null)
                {
                    dst.UnlockBits(dstdata); dstdata = null;
                }
            }
        }
    }
}

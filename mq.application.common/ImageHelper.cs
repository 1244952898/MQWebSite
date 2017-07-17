using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.application.common
{
    public class ImageHelper
    {

        /// <summary>
        /// 同一比例压缩图片的尺寸，同时一定程度上压缩图片的字节数
        /// </summary>
        /// <param name="Width">缩略图的最大宽度</param>
        /// <param name="Height">缩略图的最大高度</param>
        /// <returns>压缩后的Image对象</returns>
        public static Image UniformScaleDeflate(Image resourceImage, int maxWidth, int maxHeight)
        {
            try
            {
                double percent = 1;
                if (resourceImage == null)
                {
                    return null;
                }
                int imageWidth = resourceImage.Width;
                int imageHeight = resourceImage.Height;

                int newWidth;
                int newHeight;

                newWidth = imageWidth;
                newHeight = imageHeight;

                if (imageWidth > imageHeight)//如果图片的宽大于高
                {
                    if (imageWidth > maxWidth)
                    {
                        newWidth = maxWidth;
                        percent = maxWidth * 1.0 / imageWidth;
                        newHeight = (int)Math.Floor(imageHeight * percent);
                    }
                }
                else
                {
                    if (imageHeight > maxHeight)
                    {
                        newHeight = maxHeight;
                        percent = maxHeight * 1.0 / newHeight;
                        newHeight = (int)Math.Floor(imageWidth * percent);
                    }
                }

                //用指定的大小和格式初始化Bitmap类的新实例
                Bitmap bitmap = new Bitmap(newWidth, newHeight, PixelFormat.Format32bppArgb);
                //从指定的Image对象创建新Graphics对象
                Graphics graphics = Graphics.FromImage(bitmap);
                //清除整个绘图面并以透明背景色填充
                graphics.Clear(Color.Transparent);
                //呈现质量
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //在指定位置并且按指定大小绘制原图片对象
                graphics.DrawImage(resourceImage, new Rectangle(0, 0, maxWidth, maxHeight));
                return bitmap;
            }
            catch (Exception e)
            {
                //ErrMessage = e.Message;
                return null;
            }
        }


        #region   截取图象

        ///   <summary> 
        ///   从图片中截取部分生成新图 
        ///   </summary> 
        ///   <param   name= "sFromFilePath "> 原始图片 </param> 
        ///   <param   name= "saveFilePath "> 生成新图 </param> 
        ///   <param   name= "width "> 截取图片宽度 </param> 
        ///   <param   name= "height "> 截取图片高度 </param> 
        ///   <param   name= "spaceX "> 截图图片X坐标 </param> 
        ///   <param   name= "spaceY "> 截取图片Y坐标 </param> 
        public static void CaptureImage(string sFromFilePath, string saveFilePath, int width, int height, int spaceX, int spaceY)
        {
            if (!File.Exists(sFromFilePath))
                return;
            //载入底图 
            Image fromImage = Image.FromFile(sFromFilePath);
            int x = 0;   //截取X坐标 
            int y = 0;   //截取Y坐标 
            //原图宽与生成图片宽   之差     
            //当小于0(即原图宽小于要生成的图)时，新图宽度为较小者   即原图宽度   X坐标则为0   
            //当大于0(即原图宽大于要生成的图)时，新图宽度为设置值   即width         X坐标则为   sX与spaceX之间较小者 
            //Y方向同理 
            int sX = fromImage.Width - width;
            int sY = fromImage.Height - height;
            if (sX > 0)
            {
                x = sX > spaceX ? spaceX : sX;
            }
            else
            {
                width = fromImage.Width;
            }
            if (sY > 0)
            {
                y = sY > spaceY ? spaceY : sY;
            }
            else
            {
                height = fromImage.Height;
            }

            //创建新图位图 
            Bitmap bitmap = new Bitmap(width, height);
            //创建作图区域 
            Graphics graphic = Graphics.FromImage(bitmap);
            //截取原图相应区域写入作图区 
            graphic.DrawImage(fromImage, 0, 0, new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
            //从作图区生成新图 
            Image saveImage = Image.FromHbitmap(bitmap.GetHbitmap());


            //保存图象 
            saveImage.Save(saveFilePath, ImageFormat.Jpeg);

            //释放资源 
            fromImage.Dispose();
            saveImage.Dispose();
            bitmap.Dispose();
            graphic.Dispose();
        }
        #endregion

        #region   截取图象

        ///   <summary> 
        ///   从图片中截取部分生成新图 
        ///   </summary> 
        ///   <param   name= "sFromFilePath "> 原始图片 </param> 
        ///   <param   name= "saveFilePath "> 生成新图 </param> 
        ///   <param   name= "width "> 截取图片宽度 </param> 
        ///   <param   name= "height "> 截取图片高度 </param> 
        ///   <param   name= "spaceX "> 截图图片X坐标 </param> 
        ///   <param   name= "spaceY "> 截取图片Y坐标 </param> 
        public static void CaptureImageSmall(string sFromFilePath, string saveFilePath, int width, int height, int spaceX, int spaceY, int newWidth, int newHeight)
        {
            if (!File.Exists(sFromFilePath))
                return;
            //载入底图 
            Image fromImage = Image.FromFile(sFromFilePath);
            int x = 0;   //截取X坐标 
            int y = 0;   //截取Y坐标 
            //原图宽与生成图片宽   之差     
            //当小于0(即原图宽小于要生成的图)时，新图宽度为较小者   即原图宽度   X坐标则为0   
            //当大于0(即原图宽大于要生成的图)时，新图宽度为设置值   即width         X坐标则为   sX与spaceX之间较小者 
            //Y方向同理 
            int sX = fromImage.Width - width;
            int sY = fromImage.Height - height;
            if (sX > 0)
            {
                x = sX > spaceX ? spaceX : sX;
            }
            else
            {
                width = fromImage.Width;
            }
            if (sY > 0)
            {
                y = sY > spaceY ? spaceY : sY;
            }
            else
            {
                height = fromImage.Height;
            }


            Size newSize = NewSize(width, height, newWidth, newHeight);

            //创建新图位图 
            Bitmap bitmap = new Bitmap(width, height);
            //创建作图区域 
            Graphics graphic = Graphics.FromImage(bitmap);

            //设置 System.Drawing.Graphics对象的SmoothingMode属性为HighQuality
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //下面这个也设成高质量
            graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //下面这个设成High
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //截取原图相应区域写入作图区 
            graphic.DrawImage(fromImage, 0, 0, new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
            //从作图区生成新图 
            Image saveImage = Image.FromHbitmap(bitmap.GetHbitmap());




            //保存图象 
            saveImage.Save(saveFilePath, ImageFormat.Jpeg);
            //保存缩略图





            //释放资源 
            fromImage.Dispose();
            saveImage.Dispose();
            bitmap.Dispose();
            graphic.Dispose();

        }
        #endregion
        /// <summary>
        /// 获取图片中的各帧
        /// </summary>
        /// <param name="pPath">图片路径</param>
        /// <param name="pSavePath">保存路径</param>
        public void GetFrames(string pPath, string pSavedPath)
        {
            Image gif = Image.FromFile(pPath);
            FrameDimension fd = new FrameDimension(gif.FrameDimensionsList[0]);

            //获取帧数(gif图片可能包含多帧，其它格式图片一般仅一帧)
            int count = gif.GetFrameCount(fd);

            //以Jpeg格式保存各帧
            for (int i = 0; i < count; i++)
            {
                gif.SelectActiveFrame(fd, i);
                gif.Save(pSavedPath + "\\frame_" + i + ".jpg", ImageFormat.Jpeg);
            }
        }

        /**/
        /// <summary>
        /// 获取图片缩略图
        /// </summary>
        /// <param name="pPath">图片路径</param>
        /// <param name="pSavePath">保存路径</param>
        /// <param name="pWidth">缩略图宽度</param>
        /// <param name="pHeight">缩略图高度</param>
        /// <param name="pFormat">保存格式，通常可以是jpeg</param>
        public void GetSmaller(string pPath, string pSavedPath, int pWidth, int pHeight)
        {
            string fileSaveUrl = pSavedPath + "\\smaller.jpg";

            using (FileStream fs = new FileStream(pPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                MakeSmallImg(fs, fileSaveUrl, pWidth, pHeight);
            }

        }


        //按模版比例生成缩略图（以流的方式获取源文件）  
        //生成缩略图函数  
        //顺序参数：源图文件流、缩略图存放地址、模版宽、模版高  
        //注：缩略图大小控制在模版区域内  
        public static void MakeImgOutNewSize(System.IO.Stream fromFileStream, string fileSaveUrl, int templateWidth, int templateHeight, out int newWidth, out int newHeight, out int oldWidth, out int oldHeight)
        {
            //从文件取得图片对象，并使用流中嵌入的颜色管理信息  
            System.Drawing.Image myImage = System.Drawing.Image.FromStream(fromFileStream, true);

            Size newSize = NewSize(templateWidth, templateHeight, myImage.Width, myImage.Height);

            //原始宽、高
            oldWidth = myImage.Width;
            oldHeight = myImage.Height;
            //缩略图宽、高  
            newWidth = newSize.Width;
            newHeight = newSize.Height;
            if (File.Exists(fileSaveUrl))
            {
                File.SetAttributes(fileSaveUrl, FileAttributes.Normal);
                File.Delete(fileSaveUrl);
            }

            myImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
            myImage.Dispose();

        }


        private static Size NewSize(int maxWidth, int maxHeight, int width, int height)
        {
            double w = 0.0;
            double h = 0.0;
            double sw = Convert.ToDouble(width);
            double sh = Convert.ToDouble(height);
            double mw = Convert.ToDouble(maxWidth);
            double mh = Convert.ToDouble(maxHeight);
            if (sw < mw && sh < mh)
            {
                w = sw;
                h = sh;
            }
            else if ((sw / sh) > (mw / mh))
            {
                w = maxWidth;
                h = (w * sh) / sw;
            }
            else
            {
                h = maxHeight;
                w = (h * sw) / sh;
            }
            return new Size(Convert.ToInt32(w), Convert.ToInt32(h));
        }

        //按模版比例生成缩略图（以流的方式获取源文件）  
        //生成缩略图函数  
        //顺序参数：源图文件流、缩略图存放地址、模版宽、模版高  
        //注：缩略图大小控制在模版区域内  
        public static void MakeSmallImg(System.IO.Stream fromFileStream, string fileSaveUrl, System.Double templateWidth, System.Double templateHeight)
        {
            //从文件取得图片对象，并使用流中嵌入的颜色管理信息  
            System.Drawing.Image myImage = System.Drawing.Image.FromStream(fromFileStream, true);

            //缩略图宽、高  
            System.Double newWidth = myImage.Width, newHeight = myImage.Height;
            //System.Double newWidth = templateWidth, newHeight = templateHeight;
            //宽大于模版的横图  
            if (myImage.Width > myImage.Height || myImage.Width == myImage.Height)
            {
                if (myImage.Width > templateWidth)
                {
                    //宽按模版，高按比例缩放  
                    newWidth = templateWidth;
                    newHeight = myImage.Height * (newWidth / myImage.Width);
                }
            }
            //高大于模版的竖图  
            else
            {
                if (myImage.Height > templateHeight)
                {
                    //高按模版，宽按比例缩放  
                    newHeight = templateHeight;
                    newWidth = myImage.Width * (newHeight / myImage.Height);
                }
            }

            //取得图片大小  
            System.Drawing.Size mySize = new Size((int)newWidth, (int)newHeight);
            //新建一个bmp图片  
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(mySize.Width, mySize.Height);
            //新建一个画板  
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法  
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度  
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空一下画布  
            g.Clear(Color.White);
            //在指定位置画图  
            g.DrawImage(myImage, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
            new System.Drawing.Rectangle(0, 0, myImage.Width, myImage.Height),
            System.Drawing.GraphicsUnit.Pixel);

            ///文字水印  
            //System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(bitmap);
            //System.Drawing.Font f = new Font("Lucida Grande", 6);
            //System.Drawing.Brush b = new SolidBrush(Color.Gray);
            //G.DrawString("Ftodo.com", f, b, 0, 0);
            //G.Dispose();

            ///图片水印  
            //System.Drawing.Image   copyImage   =   System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("pic/1.gif"));  
            //Graphics   a   =   Graphics.FromImage(bitmap);  
            //a.DrawImage(copyImage,   new   Rectangle(bitmap.Width-copyImage.Width,bitmap.Height-copyImage.Height,copyImage.Width,   copyImage.Height),0,0,   copyImage.Width,   copyImage.Height,   GraphicsUnit.Pixel);  

            //copyImage.Dispose();  
            //a.Dispose();  
            //copyImage.Dispose();  

            //保存缩略图  
            if (File.Exists(fileSaveUrl))
            {
                File.SetAttributes(fileSaveUrl, FileAttributes.Normal);
                File.Delete(fileSaveUrl);
            }

            bitmap.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);

            g.Dispose();
            myImage.Dispose();
            bitmap.Dispose();
        }




        /**/
        /// <summary>
        /// 获取图片指定部分
        /// </summary>
        /// <param name="pPath">图片路径</param>
        /// <param name="pSavePath">保存路径</param>
        /// <param name="pPartStartPointX">目标图片开始绘制处的坐标X值(通常为)</param>
        /// <param name="pPartStartPointY">目标图片开始绘制处的坐标Y值(通常为)</param>
        /// <param name="pPartWidth">目标图片的宽度</param>
        /// <param name="pPartHeight">目标图片的高度</param>
        /// <param name="pOrigStartPointX">原始图片开始截取处的坐标X值</param>
        /// <param name="pOrigStartPointY">原始图片开始截取处的坐标Y值</param>
        /// <param name="pFormat">保存格式，通常可以是jpeg</param>
        public void GetPart(string pPath, string pSavedPath, int pPartStartPointX, int pPartStartPointY, int pPartWidth, int pPartHeight, int pOrigStartPointX, int pOrigStartPointY)
        {
            string normalJpgPath = pSavedPath;

            using (Image originalImg = Image.FromFile(pPath))
            {
                Bitmap partImg = new Bitmap(pPartWidth, pPartHeight);
                Graphics graphics = Graphics.FromImage(partImg);
                Rectangle destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(pPartWidth, pPartHeight));//目标位置
                Rectangle origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY), new Size(pPartWidth, pPartHeight));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）


                ///文字水印  
                //System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(partImg);
                //System.Drawing.Font f = new Font("Lucida Grande", 6);
                //System.Drawing.Brush b = new SolidBrush(Color.Gray);
                //G.Clear(Color.White);
                //graphics.DrawImage(originalImg, destRect, origRect, GraphicsUnit.Pixel);
                //G.DrawString("Ftodo.com", f, b, 0, 0);
                //G.Dispose();

                originalImg.Dispose();
                if (File.Exists(normalJpgPath))
                {
                    File.SetAttributes(normalJpgPath, FileAttributes.Normal);
                    File.Delete(normalJpgPath);
                }
                partImg.Save(normalJpgPath);
            }
        }
        /**/
        /// <summary>
        /// 获取按比例缩放的图片指定部分
        /// </summary>
        /// <param name="pPath">图片路径</param>
        /// <param name="pSavePath">保存路径</param>
        /// <param name="pPartStartPointX">目标图片开始绘制处的坐标X值(通常为)</param>
        /// <param name="pPartStartPointY">目标图片开始绘制处的坐标Y值(通常为)</param>
        /// <param name="pPartWidth">目标图片的宽度</param>
        /// <param name="pPartHeight">目标图片的高度</param>
        /// <param name="pOrigStartPointX">原始图片开始截取处的坐标X值</param>
        /// <param name="pOrigStartPointY">原始图片开始截取处的坐标Y值</param>
        /// <param name="imageWidth">缩放后的宽度</param>
        /// <param name="imageHeight">缩放后的高度</param>
        public void GetPart(string pPath, string pSavedPath, int pPartStartPointX, int pPartStartPointY, int pPartWidth, int pPartHeight, int pOrigStartPointX, int pOrigStartPointY, int imageWidth, int imageHeight)
        {
            string normalJpgPath = pSavedPath;
            using (Image originalImg = Image.FromFile(pPath))
            {
                if (originalImg.Width == imageWidth && originalImg.Height == imageHeight)
                {
                    GetPart(pPath, pSavedPath, pPartStartPointX, pPartStartPointY, pPartWidth, pPartHeight, pOrigStartPointX, pOrigStartPointY);
                    return;
                }

                Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Image zoomImg = originalImg.GetThumbnailImage(imageWidth, imageHeight, callback, IntPtr.Zero);//缩放
                Bitmap partImg = new Bitmap(pPartWidth, pPartHeight);

                Graphics graphics = Graphics.FromImage(partImg);
                Rectangle destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(pPartWidth, pPartHeight));//目标位置
                Rectangle origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY), new Size(pPartWidth, pPartHeight));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）

                ///文字水印  
                System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(partImg);
                System.Drawing.Font f = new Font("Lucida Grande", 6);
                System.Drawing.Brush b = new SolidBrush(Color.Gray);
                G.Clear(Color.White);

                graphics.DrawImage(zoomImg, destRect, origRect, GraphicsUnit.Pixel);
                G.DrawString("", f, b, 0, 0);
                G.Dispose();

                originalImg.Dispose();
                if (File.Exists(normalJpgPath))
                {
                    File.SetAttributes(normalJpgPath, FileAttributes.Normal);
                    File.Delete(normalJpgPath);
                }
                partImg.Save(normalJpgPath, ImageFormat.Jpeg);
            }
        }

        /// <summary>
        /// 获得图像高宽信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ImageInformation GetImageInfo(string path)
        {
            using (Image image = Image.FromFile(path))
            {
                ImageInformation imginfo = new ImageInformation { Width = image.Width, Height = image.Height };
                image.Dispose();
                return imginfo;
            }
        }
        public bool ThumbnailCallback()
        {
            return false;
        }

        public bool GetThumb(string filepath, string savepath, int width, int height)
        {
            bool issuccess = true;
            System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);

            ImageHelper imageHelp = new ImageHelper();
            ImageInformation imageInfo = (ImageInformation)imageHelp.GetImageInfo(filepath);

            //缩略图宽、高  
            System.Double newWidth = imageInfo.Width, newHeight = imageInfo.Height;
            //宽大于模版的横图  
            if (imageInfo.Width > imageInfo.Height || imageInfo.Width == imageInfo.Height)
            {
                if (imageInfo.Width > width)
                {
                    //宽按模版，高按比例缩放  
                    newWidth = width;
                    newHeight = imageInfo.Height * (newWidth / imageInfo.Width);
                }
            }
            //高大于模版的竖图  
            else
            {
                if (imageInfo.Height > height)
                {
                    //高按模版，宽按比例缩放  
                    newHeight = height;
                    newWidth = imageInfo.Width * (newHeight / imageInfo.Height);
                }
            }

            try
            {
                string tempath = savepath.Substring(0, savepath.LastIndexOf("\\"));
                if (!File.Exists(filepath) || !Directory.Exists(tempath))
                {
                    issuccess = false;
                }
                else
                {
                    Bitmap myBitmap = new Bitmap(filepath);
                    System.Drawing.Image myThumbnail = myBitmap.GetThumbnailImage((int)newWidth, (int)newHeight, myCallback, IntPtr.Zero);

                    //保存图片
                    myThumbnail.Save(savepath);
                    myThumbnail.Dispose();
                    myBitmap.Dispose();
                }
            }
            catch
            {
                issuccess = false;
            }

            return issuccess;

        }

        public static string CropImage(string originamImgPath, string imgPath, int width, int height, int x, int y, string userid)
        {
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + "id" + userid + ".jpg";
            byte[] CropImage = Crop(originamImgPath, width, height, x, y);
            if (CropImage == null)
            {
                return "";
            }
            using (MemoryStream ms = new MemoryStream(CropImage, 0, CropImage.Length))
            {
                ms.Write(CropImage, 0, CropImage.Length);
                using (System.Drawing.Image CroppedImage = System.Drawing.Image.FromStream(ms, true))
                {
                    string SaveTo = imgPath + filename;
                    CroppedImage.Save(SaveTo, CroppedImage.RawFormat);
                    CroppedImage.Dispose();
                }
                ms.Close();
            }
            return filename;
        }

        public static byte[] Crop(string Img, int Width, int Height, int X, int Y)
        {
            try
            {
                using (Image OriginalImage = Image.FromFile(Img))
                {
                    Bitmap bmp = null;
                    if (IsPixelFormatIndexed(OriginalImage.PixelFormat))
                    {
                        bmp = new Bitmap(OriginalImage.Width, OriginalImage.Height, PixelFormat.Format32bppArgb);
                    }
                    else
                    {
                        bmp = new Bitmap(Width, Height, OriginalImage.PixelFormat);
                    }

                    bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                    using (Graphics Graphic = Graphics.FromImage(bmp))
                    {
                        Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                        Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Graphic.DrawImage(OriginalImage, new Rectangle(0, 0, Width, Height), X, Y, Width, Height, GraphicsUnit.Pixel);


                        MemoryStream ms = new MemoryStream();
                        bmp.Save(ms, OriginalImage.RawFormat);
                        byte[] newimg = ms.GetBuffer();
                        ms.Close();
                        Graphic.Dispose();
                        bmp.Dispose();
                        OriginalImage.Dispose();
                        return newimg;
                    }
                }
            }
            catch
            {
                return null;//throw (Ex);
            }
        }

        /// <summary>
        /// 裁剪一个Image对象，返回图像的字节流
        /// </summary>
        /// <param name="originImg"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static byte[] CropImage(Image originImg, int width, int height, int x, int y)
        {
            try
            {
                if (originImg == null)
                    return new byte[0];

                using (Image OriginalImage = originImg)
                {
                    Bitmap bmp = null;
                    if (IsPixelFormatIndexed(OriginalImage.PixelFormat))
                    {
                        bmp = new Bitmap(OriginalImage.Width, OriginalImage.Height, PixelFormat.Format32bppArgb);
                    }
                    else
                    {
                        bmp = new Bitmap(width, height, OriginalImage.PixelFormat);
                    }

                    bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                    using (Graphics Graphic = Graphics.FromImage(bmp))
                    {
                        Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                        Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Graphic.DrawImage(OriginalImage, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);


                        MemoryStream ms = new MemoryStream();
                        bmp.Save(ms, OriginalImage.RawFormat);
                        byte[] newimg = ms.GetBuffer();
                        ms.Close();
                        Graphic.Dispose();
                        bmp.Dispose();
                        OriginalImage.Dispose();
                        return newimg;
                    }
                }
            }
            catch
            {
                return new byte[0];//throw (Ex);
            }
        }

        public static string NewCropImage(string originamImgPath, string imgPath, int width, int height, int x, int y, string userid)
        {
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + "id" + userid + ".jpg";

            try
            {
                using (Image OriginalImage = Image.FromFile(originamImgPath))
                {
                    Bitmap bmp = null;
                    if (IsPixelFormatIndexed(OriginalImage.PixelFormat))
                    {
                        bmp = new Bitmap(OriginalImage.Width, OriginalImage.Height, PixelFormat.Format32bppArgb);
                    }
                    else
                    {
                        bmp = new Bitmap(width, height, OriginalImage.PixelFormat);
                    }

                    bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                    using (Graphics Graphic = Graphics.FromImage(bmp))
                    {
                        Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                        Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Graphic.DrawImage(OriginalImage, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
                        string SaveTo = System.IO.Path.Combine(imgPath.TrimEnd('\\'), filename);
                        bmp.Save(SaveTo, OriginalImage.RawFormat);
                        Graphic.Dispose();
                        bmp.Dispose();
                        OriginalImage.Dispose();
                        return filename;
                    }
                }
            }
            catch
            {
                return "";//throw (Ex);
            }
        }


        public static string CropImage2(string originamImgPath, string imgPath, string serverIp, int width, int height, int x, int y, List<ImageInformation> thumbList, out string strMsg)
        {
            strMsg = string.Empty;
            List<string> filenameList = CropImageList2(originamImgPath, imgPath, serverIp, width, height, x, y, thumbList, out strMsg);
            if (filenameList == null || filenameList.Count <= 0)
                return string.Empty;
            else if (filenameList.Count == 1)
                return filenameList[0];
            else
                return filenameList[1];
        }

        public static List<string> CropImageList2(string originamImgPath, string imgPath, string serverIp, int width, int height, int x, int y, List<ImageInformation> thumbList, out string strMsg)
        {
            strMsg = string.Empty;
            List<string> filenameList = new List<string>();
            try
            {
                string fileName = System.IO.Path.GetFileName(originamImgPath);
                string fileExtension = System.IO.Path.GetExtension(originamImgPath);
                string strNewImgFileName = fileName.Replace(fileExtension, string.Empty) + "_small" + "_" + serverIp + fileExtension;
                string strNewImgSavePath = System.IO.Path.Combine(imgPath, strNewImgFileName);

                string normalJpgPath = strNewImgSavePath;
                using (Image originalImg = Image.FromFile(originamImgPath))
                {
                    int origWidth = originalImg.Width;
                    int origHeight = originalImg.Height;
                    int imageWidth = width;
                    int imageHeight = height;
                    int pPartStartPointX = x;
                    int pPartStartPointY = y;
                    ImageHelper imageHelp = new ImageHelper();
                    if (origWidth >= width && origHeight >= height)
                    {
                        originalImg.Dispose();
                        //if (File.Exists(normalJpgPath))
                        //{
                        //    File.SetAttributes(normalJpgPath, FileAttributes.Normal);
                        //    File.Delete(normalJpgPath);
                        //}
                        File.Copy(originamImgPath, normalJpgPath, true);
                        filenameList.Add(strNewImgFileName);
                    }
                    //如果视图中的图片尺寸与原图上尺寸一致
                    //if (origWidth != viewWidth || origHeight != viewHeight)
                    //{
                    //    imageWidth = width * origWidth / viewWidth; //
                    //    imageHeight = height * origHeight / viewHeight; //
                    //    pPartStartPointX = x * origWidth / viewWidth;
                    //    pPartStartPointY = y * origHeight / viewHeight;
                    //}
                    else
                    {
                        Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(imageHelp.ThumbnailCallback);
                        //Image zoomImg = originalImg.GetThumbnailImage(imageWidth, imageHeight, callback, IntPtr.Zero);//缩放
                        Bitmap partImg = new Bitmap(imageWidth, imageHeight);

                        Graphics graphics = Graphics.FromImage(partImg);
                        Rectangle destRect = new Rectangle(new Point(0, 0), new Size(imageWidth, imageHeight));//目标位置
                        Rectangle origRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(imageWidth, imageHeight));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）

                        ///文字水印  
                        System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(partImg);
                        //System.Drawing.Font f = new Font("Lucida Grande", 6);
                        //System.Drawing.Brush b = new SolidBrush(Color.Gray);
                        G.Clear(Color.White);

                        graphics.DrawImage(originalImg, destRect, origRect, GraphicsUnit.Pixel);
                        //G.DrawString("", f, b, 0, 0);
                        G.Dispose();

                        originalImg.Dispose();
                        if (File.Exists(normalJpgPath))
                        {
                            File.SetAttributes(normalJpgPath, FileAttributes.Normal);
                            File.Delete(normalJpgPath);
                        }
                        partImg.Save(normalJpgPath, ImageFormat.Jpeg);

                        filenameList.Add(strNewImgFileName);
                    }
                }
            }
            catch (Exception Ex)
            {
                strMsg = Ex.Message;
            }
            return filenameList;
        }

        /// <summary>
        /// 会产生graphics异常的PixelFormat
        /// </summary>
        private static PixelFormat[] indexedPixelFormats = { PixelFormat.Undefined, PixelFormat.DontCare,
            PixelFormat.Format16bppArgb1555, PixelFormat.Format1bppIndexed, PixelFormat.Format4bppIndexed,
            PixelFormat.Format8bppIndexed
        };

        /// <summary>
        /// 判断图片的PixelFormat 是否在 引发异常的 PixelFormat 之中
        /// </summary>
        /// <param name="imgPixelFormat">原图片的PixelFormat</param>
        /// <returns></returns>
        private static bool IsPixelFormatIndexed(PixelFormat imgPixelFormat)
        {
            foreach (PixelFormat pf in indexedPixelFormats)
            {
                if (pf.Equals(imgPixelFormat)) return true;
            }

            return false;
        }

    }
    public struct ImageInformation
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

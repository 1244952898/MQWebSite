using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using mq.application.enumlib;

namespace mq.application.common
{
    public class FileValidation
    {
        public static bool IsAllowedExtension(HttpPostedFileBase file, FileExtension[] fileEx)
        {
            int fileLen = file.ContentLength;
            byte[] imgArray = new byte[fileLen];
            file.InputStream.Read(imgArray, 0, fileLen);
            MemoryStream ms = new MemoryStream(imgArray);
            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = br.ReadByte();
                fileclass = buffer.ToString();
                buffer = br.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
            }
            br.Close();
            ms.Close();
            //注意将文件流指针还原
            file.InputStream.Position = 0;
            foreach (FileExtension fe in fileEx)
            {
                if (Int32.Parse(fileclass) == (int)fe)
                    return true;
            }
            return false;
        }

        public static bool IsAllowedExtension(Stream fileStream, FileExtension[] fileEx)
        {
            int fileLen = (Int32)fileStream.Length;
            byte[] imgArray = new byte[fileLen];
            imgArray = ImageToByteHelper.StreamToBytes(fileStream);
            MemoryStream ms = new MemoryStream(imgArray);
            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
            string fileclass = "";
            byte buffer;
            try
            {
                if (br.PeekChar() > -1)//避免长度为0的文件，再次抛出异常
                {
                    buffer = br.ReadByte();
                    fileclass = buffer.ToString();
                    buffer = br.ReadByte();
                    fileclass += buffer.ToString();
                }
                else
                {
                    return false;
                }
            }
            catch (EndOfStreamException ex)
            {
                return false;//EndOfStreamException 无法在流的结尾之外进行读取。可以通过判断if(br.PeekChar()>-1)来判断，是否可以继续读取文件流
            }
            catch (Exception ex2)
            {
                return false;
            }
            finally
            {
                br.Close();
                ms.Close();
            }

            //注意将文件流指针还原
            fileStream.Seek(0, SeekOrigin.Begin);
            foreach (FileExtension fe in fileEx)
            {
                if (Int32.Parse(fileclass) == (int)fe)
                    return true;
            }
            return false;
        }

        public static string FileExtension(HttpPostedFile file, FileExtension[] fileEx)
        {
            int fileLen = file.ContentLength;
            byte[] imgArray = new byte[fileLen];
            file.InputStream.Read(imgArray, 0, fileLen);
            MemoryStream ms = new MemoryStream(imgArray);
            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = br.ReadByte();
                fileclass = buffer.ToString();
                buffer = br.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
            }
            br.Close();
            ms.Close();
            file.InputStream.Position = 0;
            string result = "";
            foreach (FileExtension fe in fileEx)
            {
                int flag = (int)fe;
                if (Int32.Parse(fileclass) == (int)fe)
                {
                    switch (flag)
                    {
                        case 3780:
                            result = "pdf";
                            break;
                        case 208207:
                            result = "doc";
                            break;
                        case 8075:
                            result = "docx";
                            break;
                        case 255216:
                            result = "jpg";
                            break;
                        case 7173:
                            result = "gif";
                            break;
                        case 6677:
                            result = "bmp";
                            break;
                        case 13780:
                            result = "png";
                            break;
                    }
                    break;
                }
            }
            return result;
        }

        public static string FileExtensionPdfRarZip(HttpPostedFile file, FileExtension[] fileEx)
        {
            int fileLen = file.ContentLength;
            byte[] imgArray = new byte[fileLen];
            file.InputStream.Read(imgArray, 0, fileLen);
            MemoryStream ms = new MemoryStream(imgArray);
            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = br.ReadByte();
                fileclass = buffer.ToString();
                buffer = br.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
            }
            br.Close();
            ms.Close();
            file.InputStream.Position = 0;
            string result = "";
            foreach (FileExtension fe in fileEx)
            {
                int flag = (int)fe;
                if (Int32.Parse(fileclass) == (int)fe)
                {
                    switch (flag)
                    {
                        case 3780:
                            result = "pdf";
                            break;
                        case 8297:
                            result = "rar";
                            break;
                        case 8075:
                            result = "ZIP";
                            break;
                    }
                    break;
                }
            }
            return result;
        }
    }
}

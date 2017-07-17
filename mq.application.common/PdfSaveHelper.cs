using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.application.common
{
    public class PdfSaveHelper
    {
        /// <summary>
        /// 获取指定PDF文件的总页码数
        /// </summary>
        /// <param name="strSourceFile">PDF文件路径</param>
        /// <returns></returns>
        public static int GetPdfFilePages(string strSourceFile, out string errorMsg)
        {
            errorMsg = string.Empty;
            int iPages = -1;
            if (System.IO.File.Exists(strSourceFile))
            {
                iTextSharp.text.pdf.PdfReader reader = null;
                try
                {
                    reader = new iTextSharp.text.pdf.PdfReader(strSourceFile);
                    iPages = reader.NumberOfPages;
                }
                catch (iTextSharp.text.exceptions.IllegalPdfSyntaxException)
                {
                    iPages = -10;
                    errorMsg = "iTextSharp IllegalPdfSyntaxException";
                }
                catch (iTextSharp.text.exceptions.InvalidImageException)
                {
                    iPages = -10;
                    errorMsg = "iTextSharp InvalidImageException";
                }
                catch (iTextSharp.text.exceptions.InvalidPdfException)
                {
                    iPages = -10;
                    errorMsg = "iTextSharp InvalidPdfException";
                }
                catch (iTextSharp.text.exceptions.BadPasswordException)
                {
                    iPages = -10;
                    errorMsg = "iTextSharp BadPasswordException";
                }
                catch (StackOverflowException es)
                {
                    iPages = -1;
                    errorMsg = string.Format("iTextSharp Reader StackOverflowException Error:{0}", es.Message);
                }
                catch (Exception ex)
                {
                    iPages = -1;
                    errorMsg = ex.Message;
                }
                finally
                {
                    if (reader != null)
                        reader.Dispose();
                }
            }
            else
                iPages = -1;
            if (errorMsg.Length > 0)
            {
                IOHelp.WriteLog(string.Format("pdffile={0}, error={1}", strSourceFile, errorMsg), 5, "checkpdfError");
            }
            return iPages;
        }
    }
}

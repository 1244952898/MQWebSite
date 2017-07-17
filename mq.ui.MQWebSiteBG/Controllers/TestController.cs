using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mq.application.common;

namespace mq.ui.EmployeeWebSite.Controllers
{
    public class TestController : Controller
    {

        public ActionResult Img()
        {
            return View();
        }

        public ActionResult GetImg()
        {
            return View();
        }

        public ActionResult LoginRSA()
        {
            return View();
        }

        public JsonResult Validate()
        {
            #region 公钥 私钥
            //-----BEGIN PUBLIC KEY-----
            //MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC4mZ8DLLwSMETsoQTKdu4efwhd
            //fTY/rxhsGA5EittII0/SeDQlRC4IzpxzMTKQSMWvnRlvD7V4Z2u1KBGhE++i/T5K
            //RdNCBXdo/G0pUdP2Nf4DpMP30HF54GwP8iZoXWVLy/tWtdZx9DUvajCn6GmgnjqK
            //NPTiCQsARLvYAou18wIDAQAB
            //-----END PUBLIC KEY-----

            //-----BEGIN RSA PRIVATE KEY-----
            //MIICXAIBAAKBgQC4mZ8DLLwSMETsoQTKdu4efwhdfTY/rxhsGA5EittII0/SeDQl
            //RC4IzpxzMTKQSMWvnRlvD7V4Z2u1KBGhE++i/T5KRdNCBXdo/G0pUdP2Nf4DpMP3
            //0HF54GwP8iZoXWVLy/tWtdZx9DUvajCn6GmgnjqKNPTiCQsARLvYAou18wIDAQAB
            //AoGAHjwbDTwQebIqN8+Pp1GfYqNPzXAqqHeXOm0eOR+9Fq2h70j80XW/THX6retK
            //6tLgfgjvF3+tK1FVFNDBQm6veuzRQjUO5JE0uRs4LSlRECa0ENxPgxFtELtTzRdJ
            //u/KXY3DB+ke+RLG0azylcwGeCtNbijYEneCqpCK5Fpjh8yECQQC/YWl4VCbqAUKE
            //GBipUdg9C8l/Aq/0H1vnS9jepvi5lYmSMNY60/r9h89qNbTB5FOn6LZC0n/TDmFL
            //YSr1MTiZAkEA9u4e5i2lJUG4w5IQGRKDrAzmto9a0ooQatPKoW4XiEHmwnZWFTUD
            //3eUc0IDlh+WYGi41Bg1+dzn9h0blq+Q+awJAR5Lo3QWr4AxEkh5o6rofQwVrgELD
            //B2vK9T/ahbqwfse8QZ5eIHYzAiqOmcwoI/N+jedscqVDBO312Tkn1bdo0QJBAOQC
            //HpAGh96uIBCeN7UfDmx5ATSDjKaqC9zIset7/8i2qYDYykYMzQRBAelZjBh/HYLX
            //Nejf3u3yozMdeQfO2v8CQD/uSS5BeJerr4pnF4suTzaqpIW66D2HVI3JxssBJWwt
            //fdVFi1a6EbZc0mHTzY/zLVX/ApTXbQZSFF/rwG0P8Dk=
            //-----END RSA PRIVATE KEY-----
            #endregion

            string publicKey= @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC4mZ8DLLwSMETsoQTKdu4efwhdfTY/rxhsGA5EittII0/SeDQlRC4IzpxzMTKQSMWvnRlvD7V4Z2u1KBGhE++i/T5KRdNCBXdo/G0pUdP2Nf4DpMP30HF54GwP8iZoXWVLy/tWtdZx9DUvajCn6GmgnjqKNPTiCQsARLvYAou18wIDAQAB";
            string privateKey = @"MIICXAIBAAKBgQC4mZ8DLLwSMETsoQTKdu4efwhdfTY/rxhsGA5EittII0/SeDQlRC4IzpxzMTKQSMWvnRlvD7V4Z2u1KBGhE++i/T5KRdNCBXdo/G0pUdP2Nf4DpMP30HF54GwP8iZoXWVLy/tWtdZx9DUvajCn6GmgnjqKNPTiCQsARLvYAou18wIDAQABAoGAHjwbDTwQebIqN8+Pp1GfYqNPzXAqqHeXOm0eOR+9Fq2h70j80XW/THX6retK6tLgfgjvF3+tK1FVFNDBQm6veuzRQjUO5JE0uRs4LSlRECa0ENxPgxFtELtTzRdJu/KXY3DB+ke+RLG0azylcwGeCtNbijYEneCqpCK5Fpjh8yECQQC/YWl4VCbqAUKEGBipUdg9C8l/Aq/0H1vnS9jepvi5lYmSMNY60/r9h89qNbTB5FOn6LZC0n/TDmFLYSr1MTiZAkEA9u4e5i2lJUG4w5IQGRKDrAzmto9a0ooQatPKoW4XiEHmwnZWFTUD3eUc0IDlh+WYGi41Bg1+dzn9h0blq+Q+awJAR5Lo3QWr4AxEkh5o6rofQwVrgELDB2vK9T/ahbqwfse8QZ5eIHYzAiqOmcwoI/N+jedscqVDBO312Tkn1bdo0QJBAOQCHpAGh96uIBCeN7UfDmx5ATSDjKaqC9zIset7/8i2qYDYykYMzQRBAelZjBh/HYLXNejf3u3yozMdeQfO2v8CQD/uSS5BeJerr4pnF4suTzaqpIW66D2HVI3JxssBJWwtfdVFi1a6EbZc0mHTzY/zLVX/ApTXbQZSFF/rwG0P8Dk=";

            RSACrypto rsaCrypto = new RSACrypto(privateKey, publicKey);
            //获取参数
            string usernameEncode = Request["username"];
            string pwdEncode = Request["passwd"];

            string name1 = CommonHelper.GetPostValue("username", 100, true, true, true);
            string pwd2 = CommonHelper.GetPostValue("passwd", 100, true, true, true);

            //解密 RSA
            string username = rsaCrypto.Decrypt(usernameEncode);
            string pwd = rsaCrypto.Decrypt(pwdEncode);

            string username3 = rsaCrypto.Decrypt(name1);
            string pwd3 = rsaCrypto.Decrypt(pwd2);
            return null;
        }

        public ActionResult A()
        {
            return View();
        }

        public ActionResult Weupload()
        {
            return View();
        }
    }
}
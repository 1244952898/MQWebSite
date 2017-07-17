using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.application.common
{
    public class ProductConfigHelper
    {

        public static string ThinkTankJsEncryptPublickKeyForCSharp
        {
            get
            {
                return ThinkTankJsEncryptPublickKey.Replace("-----BEGIN PUBLIC KEY-----", string.Empty).Replace("-----END PUBLIC KEY-----", string.Empty);
            }
        }

        public static string ThinkTankJsEncryptPrivateKeyForCSharp
        {
            get
            {
                return ThinkTankJsEncryptPrivateKey.Replace("-----BEGIN RSA PRIVATE KEY-----", string.Empty).Replace("-----END RSA PRIVATE KEY-----", string.Empty);
            }
        }

        private static readonly string ThinkTankJsEncryptPublickKey = "-----BEGIN PUBLIC KEY-----" +
         "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC4mZ8DLLwSMETsoQTKdu4efwhdfTY/rxhsGA5EittII0/SeDQlRC4IzpxzMTKQSMWvnRlvD7V4Z2u1KBGhE++i/T5KRdNCBXdo/G0pUdP2Nf4DpMP30HF54GwP8iZoXWVLy/tWtdZx9DUvajCn6GmgnjqKNPTiCQsARLvYAou18wIDAQAB" +
         "-----END PUBLIC KEY-----";
        private static readonly string ThinkTankJsEncryptPrivateKey = "-----BEGIN RSA PRIVATE KEY-----" +
            "MIICXAIBAAKBgQC4mZ8DLLwSMETsoQTKdu4efwhdfTY/rxhsGA5EittII0/SeDQlRC4IzpxzMTKQSMWvnRlvD7V4Z2u1KBGhE++i/T5KRdNCBXdo/G0pUdP2Nf4DpMP30HF54GwP8iZoXWVLy/tWtdZx9DUvajCn6GmgnjqKNPTiCQsARLvYAou18wIDAQABAoGAHjwbDTwQebIqN8+Pp1GfYqNPzXAqqHeXOm0eOR+9Fq2h70j80XW/THX6retK6tLgfgjvF3+tK1FVFNDBQm6veuzRQjUO5JE0uRs4LSlRECa0ENxPgxFtELtTzRdJu/KXY3DB+ke+RLG0azylcwGeCtNbijYEneCqpCK5Fpjh8yECQQC/YWl4VCbqAUKEGBipUdg9C8l/Aq/0H1vnS9jepvi5lYmSMNY60/r9h89qNbTB5FOn6LZC0n/TDmFLYSr1MTiZAkEA9u4e5i2lJUG4w5IQGRKDrAzmto9a0ooQatPKoW4XiEHmwnZWFTUD3eUc0IDlh+WYGi41Bg1+dzn9h0blq+Q+awJAR5Lo3QWr4AxEkh5o6rofQwVrgELDB2vK9T/ahbqwfse8QZ5eIHYzAiqOmcwoI/N+jedscqVDBO312Tkn1bdo0QJBAOQCHpAGh96uIBCeN7UfDmx5ATSDjKaqC9zIset7/8i2qYDYykYMzQRBAelZjBh/HYLXNejf3u3yozMdeQfO2v8CQD/uSS5BeJerr4pnF4suTzaqpIW66D2HVI3JxssBJWwtfdVFi1a6EbZc0mHTzY/zLVX/ApTXbQZSFF/rwG0P8Dk=" +
            "-----END RSA PRIVATE KEY-----";
    }
}

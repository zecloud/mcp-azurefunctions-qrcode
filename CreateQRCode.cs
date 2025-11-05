using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Extensions.Logging;
using QRCoder;
using System.Drawing; 

namespace QRCodeFunction
{
    public class CreateQRCode
    {
        private readonly ILogger<CreateQRCode> _logger;

        public CreateQRCode(ILogger<CreateQRCode> logger)
        {
            _logger = logger;
        }

        [Function("createqrcodeasciiart")]
        public string RunAsciiArt(
            [McpToolTrigger("createqrcodeasciiart", "Generates a QR code in ASCII art format from the provided text.")]
            ToolInvocationContext context,
            [McpToolProperty("text", "The text which should be encoded.", isRequired: true)]
            string text)
        {
            _logger.LogInformation("MCP tool function processed a request to generate QR code.");

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                AsciiQRCode qrCode = new AsciiQRCode(qrCodeData);
                string qrCodeAsAsciiArt = qrCode.GetGraphic(1);
                return qrCodeAsAsciiArt;
            }
        }

        [Function("createqrcodehtml")]
        public string RunQrCodeHtml(
            [McpToolTrigger("createqrcodehtml", "Generates a QR code in HTML format from the provided text.")]
            ToolInvocationContext context,
            [McpToolProperty("text", "The text which should be encoded.", isRequired: true)]
            string text)
        {
            _logger.LogInformation("MCP tool function processed a request to generate QR code.");

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                using (Base64QRCode qrCode = new Base64QRCode(qrCodeData))
                {
                    var imgType = Base64QRCode.ImageType.Jpeg;
                    string qrCodeImageAsBase64 = qrCode.GetGraphic(20, Color.Black, Color.White, true, imgType);
                    var htmlPictureTag =  $"<img alt=\"Embedded QR Code\" src=\"data:image/{imgType.ToString().ToLower()};base64,{qrCodeImageAsBase64}\" />";
                    return htmlPictureTag;
                }
            }
        }
    }
}

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Extensions.Logging;
using QRCoder;

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
            [McpToolTrigger("createqrcodehtml", "Generates a QR code in base64 png format from the provided text.")]
            ToolInvocationContext context,
            [McpToolProperty("text", "The text which should be encoded.", isRequired: true)]
            string text)
        {
            _logger.LogInformation("MCP tool function processed a request to generate QR code.");

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                // Génère des bytes PNG (compatible Linux / cross-platform, pas de System.Drawing)
                var pngQrCode = new PngByteQRCode(qrCodeData);
                byte[] pngBytes = pngQrCode.GetGraphic(20); // 20 = taille des pixels

                string base64 = Convert.ToBase64String(pngBytes);
                var htmlPictureTag = $"<img alt=\"Embedded QR Code\" src=\"data:image/png;base64,{base64}\" />";
                return htmlPictureTag;
            }
        }
    }
}

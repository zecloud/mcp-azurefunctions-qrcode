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

        [Function("createqrcode")]
        public string Run(
            [McpToolTrigger("createqrcode", "Generates a QR code in ASCII art format from the provided text.")]
            ToolInvocationContext context,
            [McpToolProperty("text", "The text which should be encoded.", isRequired: true)]
            string text)
        {
            _logger.LogInformation("MCP tool function processed a request to generate QR code.");

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            AsciiQRCode qrCode = new AsciiQRCode(qrCodeData);
            string qrCodeAsAsciiArt = qrCode.GetGraphic(1);

            return qrCodeAsAsciiArt;
        }
    }
}

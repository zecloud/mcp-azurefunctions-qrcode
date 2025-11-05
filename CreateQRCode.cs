using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
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
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
            AsciiQRCode qrCode = new AsciiQRCode(qrCodeData);
            string qrCodeAsAsciiArt = qrCode.GetGraphic(1);

            return new OkObjectResult(qrCodeAsAsciiArt);
        }
    }
}

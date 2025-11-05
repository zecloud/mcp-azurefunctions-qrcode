# mcp-azurefunctions-qrcode
A mcp server that generates qrcode running on azure functions

## Overview
This is an Azure Functions project built with the isolated worker model (.NET 8.0) that provides QR code generation functionality.

## Features
- **createqrcode**: HTTP POST endpoint that generates QR codes in ASCII art format
- Built with Azure Functions v4 isolated worker model
- Uses QRCoder library for QR code generation

## Function Details

### createqrcode
- **Method**: POST
- **Authorization Level**: Function
- **Returns**: QR code as ASCII art string

## Build and Run

### Prerequisites
- .NET 8.0 SDK
- Azure Functions Core Tools (optional, for local testing)

### Build
```bash
dotnet build
```

### Run Locally
```bash
dotnet run
```

## Dependencies
- Microsoft.Azure.Functions.Worker 2.1.0
- QRCoder 1.7.0

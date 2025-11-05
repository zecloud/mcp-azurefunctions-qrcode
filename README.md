# mcp-azurefunctions-qrcode
A mcp server that generates qrcode running on azure functions

## Overview
This is an Azure Functions project built with the isolated worker model (.NET 8.0) that provides QR code generation functionality.

## Features
- **createqrcode**: MCP Tool that generates QR codes in ASCII art format
- Built with Azure Functions v4 isolated worker model
- Uses QRCoder library for QR code generation
- Model Context Protocol (MCP) integration

## Function Details

### createqrcode
- **Type**: MCP Tool
- **Description**: Generates a QR code in ASCII art format from the provided text
- **Parameters**:
  - `text` (required): The text which should be encoded
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
- Microsoft.Azure.Functions.Worker.Extensions.Mcp 1.0.0
- QRCoder 1.7.0

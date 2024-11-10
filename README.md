# WhatsAppAPI

## Overview
WhatsAppAPI is a simple ASP.NET Core Web API for sending WhatsApp messages. It provides an endpoint to send messages to specified phone numbers using a WhatsApp service.

## Features
- Send WhatsApp messages to specified phone numbers.
- Handle errors and return appropriate HTTP status codes.

## Technologies
- ASP.NET Core
- C#
- Moq (for unit testing)
- xUnit (for unit testing)

## Getting Started

### Prerequisites
- .NET 6.0 SDK or later

### Installation
1. Clone the repository:
    ```sh
    git clone https://github.com/AhmedFathyDev/WhatsAppAPI.git
    ```
2. Navigate to the project directory:
    ```sh
    cd WhatsAppAPI
    ```

### Running the Application
1. Build the project:
    ```sh
    dotnet build
    ```
2. Run the project:
    ```sh
    dotnet run
    ```

### Running Tests
1. Navigate to the test project directory:
    ```sh
    cd WhatsAppAPI.Test
    ```
2. Run the tests:
    ```sh
    dotnet test
    ```

## API Endpoints

### Send Message
- **URL:** `/api/message`
- **Method:** `POST`
- **Query Parameters:**
    - `phone` (string): The phone number to send the message to.
    - `body` (string): The body of the message to be sent.
- **Responses:**
    - `200 OK`: Message sent successfully.
    - `400 Bad Request`: The phone number is null or empty, or an error occurred.

## License
This project is licensed under the GNU General Public License (GPL) Version 2.

# AskGemini - Interactive AI Console Chatbot 🤖

A lightweight, robust, and modern C# console application that brings the power of Google's Gemini AI directly to your terminal. Communicate seamlessly with the latest `gemini-3.6-flash` model without any heavy third-party dependencies!

## 🎥 Showcase

https://github.com/user-attachments/assets/showcase.mp4

## 📖 What is AskGemini?

**AskGemini** is a command-line chatbot interface built in C# (.NET 10). It utilizes the official Google Generative Language API to provide a continuous, conversational AI experience. It maintains your chat history, so the AI remembers the context of your conversation just like a real chat interface!

## 🎯 Key Features

1. **Continuous Conversation Memory:** Maintains a `history` of your chat session, allowing Gemini to remember what you talked about earlier.
2. **Latest Model Integration:** Updated to use Google's cutting-edge `gemini-3.6-flash` model for incredibly fast and accurate responses.
3. **Secure API Key Management:** Reads the API key safely from Environment Variables. If not found, it prompts for secure, masked input (password style) directly in the console—your key is never exposed on the screen or in the source code!
4. **Zero Third-Party Dependencies:** Built purely using the native .NET `System.Net.Http` and `System.Text.Json`. No external SDKs or NuGet packages are required, keeping the project ultra-fast and lightweight.
5. **Robust Error Handling:** Safely parses API errors and connection issues, providing you with clean, readable error messages instead of application crashes.

## 🚀 How to Run

Follow these steps to compile and run the application on your local machine:

### Prerequisites
- [.NET SDK 10.0](https://dotnet.microsoft.com/download) or higher must be installed on your system.
- A free **Google Gemini API Key**. You can get one from [Google AI Studio](https://aistudio.google.com/).

### Steps
1. **Clone the repository**:
   ```bash
   git clone https://github.com/Kaaner4x/ask-gemini.git
   ```
2. **Navigate to the project directory**:
   ```bash
   cd ask-gemini
   ```
3. **Set your API Key (Optional but Recommended)**:
   Set the API key as an environment variable to skip typing it every time.
   - On Windows (PowerShell):
     ```powershell
     $env:GEMINI_API_KEY="YOUR_API_KEY"
     ```
   - On Mac/Linux:
     ```bash
     export GEMINI_API_KEY="YOUR_API_KEY"
     ```
4. **Build and Run the application**:
   ```bash
   dotnet run
   ```
5. **Usage**:
   - If the environment variable isn't set, the app will securely prompt you to type or paste your API key (the characters will be masked as `*`).
   - Start chatting! Type your message and hit `Enter`.
   - Type `exit` or `quit` to end the session.

## 📄 License

This project is licensed under the [MIT License](LICENSE.txt). See the `LICENSE.txt` file for details.

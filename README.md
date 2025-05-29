# Syncer

💾 A lightweight cross-platform file sync tool. Sync a folder between devices via your own cloud server.

## 🌐 Features

- Push/pull files between devices
- Auto-versioned on server per device & timestamp
- CLI interface for client
- Built-in FastAPI server backend

## 🛠️ Tech Stack

- Server: Python 3.11 + FastAPI
- Client: C# (.NET 6 or above)
- File versioning, hash comparison, async upload/download

## 🚀 Getting Started

```bash
# Server side
cd server
pip install -r requirements.txt
uvicorn main:app --reload

# Client side
# Open with Visual Studio / Rider or compile via dotnet CLI
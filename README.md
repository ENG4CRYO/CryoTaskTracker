# ❄️ CryoTaskTracker

**CryoTaskTracker** is a clean and simple console-based task tracking application built with C# and .NET. It allows users to add, view, update, filter, and delete tasks — all from the terminal — with clear separation of concerns using Clean Architecture principles.

---

## 🧩 Features

- ✅ Add a new task with a description
- 📝 Update task description
- 🔄 Change task state (Todo / InProgress / Done)
- 🔍 Filter tasks by state
- ❌ Delete tasks
- 💾 Persistent JSON storage

---

## 🗂 Project Structure

```bash
CryoTaskTracker/
├── ConsoleApp/          # User Interface (console entry point)
├── Domain/              # Interfaces and Models
│   ├── Interfaces/
│   └── Models/
├── Application/         # Business logic (TaskService)
├── Infrastructure/      # Storage logic (JSON-based)
└── tasks.json           # Data file
```

---

## 🚀 Getting Started

### 🔧 Prerequisites

- [.NET 6 SDK or higher](https://dotnet.microsoft.com/download)
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json)

### 📥 Clone and Run

```bash
git clone https://github.com/ENG4CRYO/CryoTaskTracker.git
cd CryoTaskTracker
dotnet restore
dotnet run --project ConsoleApp
```

---

## 💬 Command Usage

```bash
add       # Add a new task
view      # Show all tasks
update    # Update task description
status    # Change task state (todo, inprogress, done)
filter    # Show tasks by a specific state
delete    # Delete task by ID
exit      # Exit the app
```

---

## 📦 Technologies

- C# (.NET 6+)
- Newtonsoft.Json
- Clean Architecture principles
- Console Application

---

## 🤝 Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you'd like to change or enhance.

---

# Student Application Management System 

A simple and intuitive desktop application (Windows Forms) developed in C#, designed to manage student applications. The application enables data recording in a local database and automatic generation of print-ready Word documents based on a template.

##  Key Features

*   **Data Management (CRUD):** Add, view, update, and delete applications.
*   **Local Database:** Uses SQLite, which automatically creates and configures itself on the first run (file: komis.db).
*   **Document generation (.docx):** Automatic creation of Word documents. The program replaces specific placeholders (e.g., <<Imie>>, <<Album>>) in a template file with data entered into the form.
*   **Data validation:** Protection against generating empty or incomplete documents.
*   **Auto-refresh:** The application list updates in real-time after every save, edit, or delete action.

## Technologies and Tools

*   **Lenguage:** C#
*   **Interface:** Windows Forms
*   **Databae:** SQLite (`System.Data.SQLite`)
*   **Word File Handling:** DocX (legacy, free version `1.7.1` by Xceed)

## Installation and Setup

### 1. Prerequisites
*   Visual Studio (2019 or newer recommended) with .NET desktop development (Windows Forms) support.
*   Internet connection to download NuGet packages.

### 2. Cloning and NuGet Packages
1. Download the project and open the `.sln` file in Visual Studio.
2. Right-click the project name in w *Solution Explorer* and select **Manage NuGet Packages for Solution...**.
3. Ensure the following packages are installed:
   *   `System.Data.SQLite`
   *   `DocX` (Note: Ensure you are using version **1.7.1** to avoid paid license issues).

### 3. Word Template Configuration
For the document generation feature to work correctly:
1. A file named **`wniosek.docx`** must be located in the project's main `bin/Debug` folder (containing the placeholders for replacement, e.g. `<<Imie>>`, `<<Nazwisko>>`).

##  How to use

1. **Launch the App:** The `komis.db` database will be created automatically (if it doesn't exist) in the `bin/Debug` folder.
2. **Enter Data:** Fill in the form fields on the left side of the screen.
3. **Save:** Click the `Zapisz`(Save) button. The application will immediately appear in the list on the right.
4. **View and edit:** Click any entry in the list. The form will automatically populate with that record's data. You can correct errors and click `Aktualizuj` (Update).
5. **Generate Document:** Once all the fields are filled, click `Generuj Word`. The program will create a new Word file based on the template and open it automatically.
6. **Clean Form:** `Wyczyść` (Clear) button resets the fields to prepare for a new entry.

##  Project Structure

*   `Form1.cs` - UI Layer, event handling, data entry logic, and Word document generation.
*   `DatabaseManager.cs` - Database access layer (SQL queries, SQLite connection).
*   `wniosek.docx` - University template with tags for data replacement.

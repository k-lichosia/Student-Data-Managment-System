# System Ewidencji Wniosków Studenckich 

Prosta i intuicyjna aplikacja okienkowa (Windows Forms) napisana w języku C#, służąca do zarządzania wnioskami studenckimi (np. o powołanie komisji dydaktycznej). Aplikacja pozwala na ewidencję danych w lokalnej bazie oraz automatyczne generowanie gotowych do druku dokumentów Word na podstawie szablonu.

##  Główne funkcje

*   **Zarządzanie danymi (CRUD):** Dodawanie, przeglądanie, aktualizowanie i usuwanie wniosków.
*   **Lokalna baza danych:** Wykorzystanie bazy SQLite, która tworzy się i konfiguruje automatycznie przy pierwszym uruchomieniu programu (plik `komis.db`).
*   **Generowanie dokumentów (.docx):** Automatyczne tworzenie wniosków w programie Word. Program podmienia odpowiednie znaczniki (np. `<<Imie>>`, `<<Album>>`) w pliku szablonu na dane wpisane w formularzu.
*   **Walidacja danych:** Zabezpieczenie przed wygenerowaniem pustego lub niepełnego dokumentu.
*   **Automatyczne odświeżanie:** Lista wniosków aktualizuje się na bieżąco po każdej akcji zapisu, edycji lub usunięcia.

## Technologie i narzędzia

*   **Język:** C#
*   **Interfejs:** Windows Forms
*   **Baza danych:** SQLite (`System.Data.SQLite`)
*   **Obsługa plików Word:** DocX (starsza, darmowa wersja `1.7.1` od Xceed)

## Instrukcja instalacji i uruchomienia

### 1. Wymagania wstępne
*   Zainstalowane środowisko **Visual Studio** (najlepiej 2019 lub nowsze) z obsługą środowiska .NET dla aplikacji desktopowych (Windows Forms).
*   Połączenie z internetem w celu pobrania pakietów NuGet.

### 2. Klonowanie i pakiety NuGet
1. Pobierz projekt i otwórz plik `.sln` w Visual Studio.
2. Kliknij prawym przyciskiem myszy na nazwę projektu w *Solution Explorer* i wybierz **Manage NuGet Packages for Solution...**.
3. Upewnij się, że masz zainstalowane pakiety:
   *   `System.Data.SQLite`
   *   `DocX` (Ważne: upewnij się, że jest to wersja **1.7.1**, aby uniknąć problemów z płatną licencją).

### 3. Konfiguracja szablonu Word
Aby funkcja generowania dokumentów działała poprawnie:
1. W głównym folderze projektu `bin/Debug` musi znajdować się plik o nazwie **`wniosek.docx`** (zawierający odpowiednie znaczniki do podmiany, np. `<<Imie>>`, `<<Nazwisko>>`).

##  Jak korzystać z programu?

1. **Uruchom aplikację:** Baza danych `komis.db` zostanie utworzona automatycznie (jeśli nie istnieje) w folderze `bin/Debug`.
2. **Wprowadź dane:** Wypełnij pola formularza po lewej stronie ekranu.
3. **Zapisz:** Kliknij przycisk `Zapisz`. Zapisany wniosek natychmiast pojawi się na liście po prawej stronie.
4. **Przeglądaj i edytuj:** Kliknij dowolny wniosek na liście. Formularz zostanie automatycznie uzupełniony jego danymi. Możesz poprawić błędy i kliknąć `Aktualizuj`.
5. **Generuj wniosek:** Po upewnieniu się, że pola Imię, Nazwisko i Numer albumu są wypełnione, kliknij `Generuj Word`. Program stworzy nowy plik Word na podstawie szablonu i otworzy go automatycznie.
6. **Czyszczenie formularza:** Przycisk `Wyczyść` przygotowuje puste pola do wprowadzenia nowego studenta.

##  Struktura projektu

*   `Form1.cs` - Warstwa wizualna (UI), obsługa zdarzeń i logika wprowadzania danych, generowanie pliku Word.
*   `DatabaseManager.cs` - Warstwa obsługi bazy danych (zapytania SQL, połączenie z SQLite).
*   `wniosek.docx` - Szablon uczelniany z tagami do podmiany.

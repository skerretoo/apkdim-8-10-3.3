# Katalog — zasady projektu

- Interfejs po polsku, nazwy plików, klas i metod po angielsku.
- Jeden ekran to jeden plik `.axaml` i jeden plik `.axaml.cs`: `ListScreen`, `DetailScreen`, `AddScreen` (UserControl). Logika spoza ekranu mieszka w `DataFile.cs` (dane) i `EntryValidator.cs` (walidacja).
- Nie dodawaj pakietów ani plików `.csproj`; w `katalog.csproj` wolno tylko wpis kopiujący `dane/` do folderu wyjściowego.
- Dane wczytujemy wyłącznie z `dane/pozycje.txt`, format `tytuł;rok;ocena` (średnik, dokładnie 3 części). Zapis tylko przez dopisanie linii w tym samym formacie.
- Rok to liczba 1800–2100, ocena to liczba 1–10. Zły wpis nie trafia do pliku.
- Wszystkie pliki w UTF-8 bez BOM.
- Zmieniaj tylko pliki potrzebne do zadania: ekran, `DataFile`, `EntryValidator`, `MainWindow.axaml(.cs)`. Nie ruszaj `Program.cs`, `App.axaml(.cs)`.
- Metoda nie dłuższa niż 20 linii, jedna metoda to jedna czynność. Bez bindingu, `DataContext`, MVVM — wszystko w code-behind.
- Przełączanie ekranów jednym oknem: `MainWindow` trzyma `ContentControl` o nazwie `ScreenHost` i podmienia `Content` na instancję ekranu.

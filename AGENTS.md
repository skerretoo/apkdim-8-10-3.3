# Katalog — zasady projektu

- Interfejs po polsku, nazwy plików, klas i metod po angielsku.
- Jeden ekran to jeden plik `.axaml` i jeden plik `.axaml.cs`; ekrany to `ListScreen`, `DetailScreen`, `AddScreen` (UserControl).
- Nie dodawaj do projektu niczego, czego nie było w szablonie: żadnych nowych `PackageReference`, żadnych nowych plików `.csproj`.
- Dane wczytujemy wyłącznie z pliku `dane/pozycje.txt`, format `tytuł;rok;ocena` (średnik, dokładnie 3 części). Zapis tylko przez dopisanie linii w tym samym formacie.
- Wszystkie pliki w UTF-8 bez BOM.
- Zmieniaj tylko pliki ekranu, o który proszono, oraz `MainWindow.axaml(.cs)` gdy trzeba podłączyć nawigację. Nie ruszaj `Program.cs`, `App.axaml(.cs)`, `katalog.csproj`.
- Metoda nie dłuższa niż 20 linii, jedna metoda to jedna czynność. Bez automatycznego łączenia danych z kontrolkami (bez bindingu, `DataContext`, MVVM): wszystko wprost w kodzie ekranu (`code-behind`).
- Przełączanie ekranów jednym oknem: `MainWindow` trzyma `ContentControl` o nazwie `ScreenHost` i podmienia `Content` na instancję ekranu.

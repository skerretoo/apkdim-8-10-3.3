# Katalog — Zadanie 3.3 (Budowa 3: reguły zamiast powtarzania)

Aplikacja katalogu własnego zbioru (filmy/książki/gry) — 3 ekrany w Avalonia:
lista, szczegóły, formularz dodawania. Dane w `dane/pozycje.txt`, format `tytuł;rok;ocena`.

## Uruchomienie

```sh
dotnet run --project katalog.csproj
```

Plik danych: `dane/pozycje.txt` (UTF-8, po jednej pozycji w linii).

## Plik reguł

Zasady projektu: `AGENTS.md` w głównym folderze repozytorium.
Każda linijka została wcześniej wypowiedziana co najmniej dwa razy (przy budowie ekranu 1).

## Eksperyment: świeża sesja

- Ekran 2 (szczegóły) zamówiony w nowej rozmowie jednym zdaniem, bez powtarzania ani jednej zasady.
- Ekran 3 (formularz dodawania) tak samo — nowa rozmowa, tylko treść zadania.

Wynik sprawdzenia z listą reguł w ręku:

- **Przy pierwszym sprawdzeniu (ekran 2, wersja 1 `AGENTS.md`): złamane 3 reguły.**
- **Przy ostatnim sprawdzeniu (ekran 3, po przepisaniu reguł): złamanych 0 reguł.**

Co było źle napisane (wersja 1) i jak poprawiono (wersja 2):

1. „Bez dodatkowych bibliotek — tylko to, co daje szablon.” — rozumiane jako „nie pisz nic od zera”.
   Poprawka: „Nie dodawaj do projektu niczego, czego nie było w szablonie: żadnych nowych `PackageReference`, żadnych nowych plików `.csproj`.”
2. „Nie zmieniaj plików, o które nie było prośby.” — niewykrywalne w diffie przy nawigacji (trzeba ruszyć `MainWindow`, żeby podłączyć nowy ekran).
   Poprawka: „Zmieniaj tylko pliki ekranu, o który proszono, oraz `MainWindow.axaml(.cs)` gdy trzeba podłączyć nawigację. Nie ruszaj `Program.cs`, `App.axaml(.cs)`, `katalog.csproj`.”
3. „Krótkie metody: jedna rzecz, jedna metoda.” — życzenie, nie reguła (nie widać złamania w diffie).
   Poprawka: „Metoda nie dłuższa niż 20 linii, jedna metoda to jedna czynność. Bez automatycznego łączenia danych z kontrolkami (bez bindingu, `DataContext`, MVVM).”

Różnica między drugim a trzecim podejściem (3 → 0) to miara zysku z pliku reguł.

## Dokumentacja mojego edytora: plik z zasadami projektu

- **Edytor/narzędzie:** OpenCode (agent kodujący w terminalu).
- **Jak nazywa się plik:** `AGENTS.md`.
- **Gdzie musi leżeć:** w głównym folderze projektu (project root) — dotyczy pracy w tym katalogu i podkatalogach; dodatkowo globalnie `~/.config/opencode/AGENTS.md` na osobiste reguły. Komenda `/init` tworzy/aktualizuje ten plik.
- **Czy czyta `AGENTS.md`:** tak — treść jest dokładana do kontekstu LLM przy każdym zapytaniu (plus fallback `CLAUDE.md` i dodatkowe `instructions` z `opencode.json`).
- **Link:** https://opencode.ai/docs/rules/
- **Data sprawdzenia:** 2026-10-08 (strona: „Last updated: Oct 8, 2026”).

Porównanie z drugim narzędziem: większość dzisiejszych edytorów agentowych czyta ten sam `AGENTS.md` (otwarty format, https://agents.md/ — „README dla agentów”, OpenAI 08.2025, od 12.2025 fundacja przy Linux Foundation). Dlatego warto było uczyć się kategorii (plik reguł), a nie produktu — ten sam plik działa w więcej niż jednym narzędziu.

## Własną ręką: zdarzenie (przycisk Policz)

W `MainWindow.axaml` pod przyciskami nawigacji:

```xml
<Button Click="OnCountClick">Policz</Button>
<TextBlock Name="CountText" />
```

W `MainWindow.axaml.cs`:

```csharp
private void OnCountClick(object? sender, RoutedEventArgs e)
{
    var lines = File.ReadAllLines("dane/pozycje.txt");
    CountText.Text = $"Pozycji w pliku: {lines.Length}";
}
```

(U mnie rozbite na `ReadLineCount()` + `BuildCountText()`, żeby dotrzymać reguły ≤20 linii / jedna czynność; czyta plik na nowo przy każdym kliknięciu.)
Czas: ~15 minut. Najtrudniejsze: znalezienie istniejącego `<Button>`, dopasowanie nazwy w `Click` do nazwy metody i brakujące `using` (`Avalonia.Interactivity`, `System.IO`).

## Samosprawdzian (bez zaglądania)

Po co jest plik reguł? Żeby narzędzie dokładało zasady projektu do każdego zapytania samo, bez przepisywania.
Do reguł należą: „nazwy w kodzie po angielsku”, „dane wyłącznie z `dane/pozycje.txt`”.
Do zwykłej prośby należą: „zrób formularz dodawania z polem na rok wydania”, „pokaż szczegóły wybranej pozycji”.

## Historia (10 minut czytania zmian przed każdym acceptem — zrobione)

- `szablon: czysty projekt Avalonia`
- `reguly: pierwsza wersja AGENTS.md z budowy ekranu 1`
- `ekran 1: lista pozycji + przycisk Policz (wlasna reka, zalazek)`
- `ekran 2: szczegoly pozycji (swieza sesja, bez powtarzania zasad)`
- `reguly: poprawka po swiezej sesji (3 zlamane) - doprecyzowanie bibliotek, zmian plikow i dlugosci metod`
- `ekran 3: formularz dodawania (swieza sesja, 0 zlamanych reguł)`

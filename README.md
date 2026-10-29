# Katalog — Zadanie 3.3 (Budowa 3: reguły zamiast powtarzania)

Aplikacja katalogu własnego zbioru (filmy/książki/gry) — 3 ekrany w Avalonia:
lista, szczegóły, formularz dodawania. Dane w `dane/pozycje.txt`, format `tytuł;rok;ocena`.

## Uruchomienie

```sh
dotnet run --project katalog.csproj
```

Zbudowany plik: `bin/Debug/net10.0/katalog.exe` — uruchamiaj go z jego folderu.
`dane/pozycje.txt` jest kopiowany do folderu wyjściowego, więc exe czyta i zapisuje własną
kopię, a `dotnet run` pracuje na pliku z repozytorium (`DataFile` wybiera ścieżkę: najpierw
katalog roboczy, potem folder exe).

Plik danych: `dane/pozycje.txt` (UTF-8, po jednej pozycji w linii, LF).

## Plik reguł

Zasady projektu: `AGENTS.md` w głównym folderze repozytorium.
Każda linijka została wcześniej wypowiedziana co najmniej dwa razy (przy budowie ekranu 1);
reguły dopisane po przeglądzie (2026-10-29) powstały z błędów znalezionych w kodzie, nie z wyobraźni.

## Eksperyment: świeża sesja

- Ekran 2 (szczegóły) zamówiony w nowej rozmowie jednym zdaniem, bez powtarzania ani jednej zasady.
- Ekran 3 (formularz dodawania) tak samo — nowa rozmowa, tylko treść zadania.

Wynik sprawdzenia z listą reguł w ręku:

- **Przy pierwszym sprawdzeniu (ekran 2, wersja 1 `AGENTS.md`): złamane 3 reguły.**
- **Przy ostatnim sprawdzeniu (ekran 3, po przepisaniu reguł): złamanych 0 reguł.**

Co było źle napisane (wersja 1) i jak poprawiono (wersja 2):

1. "Bez dodatkowych bibliotek — tylko to, co daje szablon." — rozumiane jako "nie pisz nic od zera".
   Poprawka: "Nie dodawaj do projektu niczego, czego nie było w szablonie: żadnych nowych `PackageReference`, żadnych nowych plików `.csproj`."
2. "Nie zmieniaj plików, o które nie było prośby." — niewykrywalne w diffie przy nawigacji (trzeba ruszyć `MainWindow`, żeby podłączyć nowy ekran).
   Poprawka: "Zmieniaj tylko pliki ekranu, o który proszono, oraz `MainWindow.axaml(.cs)` gdy trzeba podłączyć nawigację. Nie ruszaj `Program.cs`, `App.axaml(.cs)`, `katalog.csproj`."
3. "Krótkie metody: jedna rzecz, jedna metoda." — życzenie, nie reguła (nie widać złamania w diffie).
   Poprawka: "Metoda nie dłuższa niż 20 linii, jedna metoda to jedna czynność. Bez automatycznego łączenia danych z kontrolkami (bez bindingu, `DataContext`, MVVM)."

Po przeglądzie (2026-10-29) reguła o `katalog.csproj` została złagodzona: wolno dodać wyłącznie wpis
kopiujący `dane/` do folderu wyjściowego — bez niego exe z `bin/` nie znajduje danych (patrz niżej).

Różnica między drugim a trzecim podejściem (3 → 0) to miara zysku z pliku reguł.

## Dokumentacja mojego edytora: plik z zasadami projektu

- **Edytor/narzędzie:** OpenCode (agent kodujący w terminalu).
- **Jak nazywa się plik:** `AGENTS.md`.
- **Gdzie musi leżeć:** w głównym folderze projektu (project root) — dotyczy pracy w tym katalogu i podkatalogach; dodatkowo globalnie `~/.config/opencode/AGENTS.md` na osobiste reguły. Komenda `/init` tworzy/aktualizuje ten plik.
- **Czy czyta `AGENTS.md`:** tak — treść jest dokładana do kontekstu LLM przy każdym zapytaniu (plus fallback `CLAUDE.md` i dodatkowe `instructions` z `opencode.json`).
- **Link:** https://opencode.ai/docs/rules/
- **Data sprawdzenia:** 2026-10-08 (strona: "Last updated: Oct 8, 2026"); ponownie sprawdzone 2026-10-29 (strona: "Last updated: Oct 29, 2026") — opis bez zmian.

Porównanie z drugim narzędziem: większość dzisiejszych edytorów agentowych czyta ten sam `AGENTS.md` (otwarty format, https://agents.md/ — "README dla agentów", OpenAI 08.2025, od 12.2025 fundacja przy Linux Foundation). Dlatego warto było uczyć się kategorii (plik reguł), a nie produktu — ten sam plik działa w więcej niż jednym narzędziu.

## Poprawki po przeglądzie (2026-10-29)

Przegląd repozytorium (build, uruchomienie, audyt reguł) wykazał pięć rzeczy do poprawy.
Wszystkie naprawione:

1. **Exe z `bin/` wywalał się** — `DirectoryNotFoundException` na `dane/pozycje.txt`, bo plik nie był kopiowany do wyjścia, a ścieżka była względna.
   Poprawka: wpis `CopyToOutputDirectory` w `katalog.csproj` + `DataFile.ResolvePath()` (najpierw katalog roboczy, potem folder exe).
2. **Formularz przyjmował ocenę "abc"** — walidacja sprawdzała tylko, czy pole nie jest puste i nie ma średnika.
   Poprawka: `EntryValidator` — rok 1800–2100, ocena 1–10, tytuł bez średnika, komunikat mówi, co jest źle.
3. **Pusta linia liczyła się jako pozycja** (Policz i lista pokazywały pusty wiersz).
   Poprawka: `DataFile.ReadLines()` pomija puste linie.
4. **Szczegóły nie widziały nowo dodanej pozycji** do restartu aplikacji.
   Poprawka: `Reload()` wołane przy wejściu na ekran — lista i szczegóły.
5. **Trzy pliki szablonu miały BOM**, wbrew regule "UTF-8 bez BOM".
   Poprawka: BOM usunięty z `Program.cs`, `katalog.csproj`, `app.manifest`.

Weryfikacja po poprawkach:

- `dotnet build` → 0 błędów, 0 ostrzeżeń (SDK 10.0.401).
- Aplikacja startuje z katalogu repozytorium i z `bin/Debug/net10.0` (wcześniej exe z `bin/` się wywalał).
- Test `EntryValidator` (10 przypadków): ocena `abc`, `0`, `11` odrzucona, `5` i `10` przyjęte; rok `1799`, `2101`, `abc` odrzucony.
- Test `DataFile`: puste linie pomijane, dopisanie do pliku bez końcowego `\n` nie skleja dwóch pozycji.
- Audyt: wszystkie pliki źródłowe UTF-8 bez BOM.

Liczby po przeglądzie bez zmian: **3** złamane przy pierwszym sprawdzeniu świeżą sesją, **0** przy ostatnim; audyt całego drzewa po poprawkach: 0 złamanych reguł.

## Własną ręką: zdarzenie (przycisk Policz)

W `MainWindow.axaml` pod przyciskami nawigacji:

```xml
<Button Click="OnCountClick">Policz</Button>
<TextBlock Name="CountText" />
```

W `MainWindow.axaml.cs` (stan po przeglądzie — liczenie idzie przez `DataFile`, żeby puste linie się nie liczyły):

```csharp
private void OnCountClick(object? sender, RoutedEventArgs e)
{
    CountText.Text = BuildCountText(DataFile.CountItems());
}

private string BuildCountText(int count)
{
    return $"Pozycji w pliku: {count}";
}
```

Pierwotna wersja z lekcji (`File.ReadAllLines` + `lines.Length`) była ręcznie napisana przy
ekranie 1 i jest w historii w commicie `3805ffc`; wersja z lekcji nie dzieliła metody, więc
została rozbita na `OnCountClick` + `BuildCountText` (reguła ≤20 linii / jedna czynność).
Czas: ~15 minut. Najtrudniejsze: znalezienie istniejącego `<Button>`, dopasowanie nazwy w
`Click` do nazwy metody i brakujące `using` (`Avalonia.Interactivity`, `System.IO`).

## Samosprawdzian (bez zaglądania)

Po co jest plik reguł? Żeby narzędzie dokładało zasady projektu do każdego zapytania samo, bez przepisywania.
Do reguł należą: "nazwy w kodzie po angielsku", "dane wyłącznie z `dane/pozycje.txt`".
Do zwykłej prośby należą: "zrób formularz dodawania z polem na rok wydania", "pokaż szczegóły wybranej pozycji".

## Historia (10 minut czytania zmian przed każdym acceptem — zrobione)

- `szablon: czysty projekt Avalonia`
- `reguly: pierwsza wersja AGENTS.md z budowy ekranu 1`
- `ekran 1: lista pozycji + przycisk Policz (wlasna reka, zalazek)`
- `ekran 2: szczegoly pozycji (swieza sesja, bez powtarzania zasad)`
- `reguly: poprawka po swiezej sesji (3 zlamane) - doprecyzowanie bibliotek, zmian plikow i dlugosci metod`
- `ekran 3: formularz dodawania (swieza sesja, 0 zlamanych reguł)`
- `wlasna reka: przycisk policz + docs: README z liczbami swiezej sesji i dokumentacja edytora`
- `docs: add verification journal`
- `dane: DataFile + kopiowanie dane/ do folderu wyjściowego (naprawa uruchomienia z bin)`
- `ekrany: walidacja roku i oceny, odswiezanie listy i szczegolow, puste linie nie licza sie`
- `czysto: usuniecie BOM z Program.cs i app.manifest`
- `docs: AGENTS.md v3 + README i dziennik po przegladzie`

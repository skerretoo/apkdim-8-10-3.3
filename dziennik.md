# Dziennik weryfikacji

Wpisy od najnowszego. Zapisuję wynik sprawdzenia, nie wrażenie.

## 2026-10-29 — przegląd zewnętrzny: pięć poprawek

- `dotnet build` → **0 błędów, 0 ostrzeżeń** (SDK 10.0.401). Wcześniejszy wpis o `NETSDK1045` dotyczył innego środowiska (tylko SDK 9); tutaj SDK 10 jest zainstalowany i projekt się buduje.
- Odtworzony błąd z 2026-10-22: `bin/Debug/net10.0/katalog.exe` → **`DirectoryNotFoundException`** na `dane/pozycje.txt`. Po poprawce (kopiowanie do wyjścia + `DataFile.ResolvePath()`) exe startuje z `bin/`.
- Potwierdzony błąd walidacji: `IsRatingValid("abc")` → **true**. Po poprawce `EntryValidator`: `abc`, `0`, `11` odrzucone, `1`–`10` przyjęte; rok `1799`, `2101`, `abc` odrzucony (10 przypadków, wszystkie zgodne z oczekiwaniem).
- Potwierdzone liczenie pustej linii jako pozycji; po poprawce `DataFile.ReadLines()` ją pomija (test: 3 linie, w tym 1 pusta → 2 pozycje).
- Potwierdzone sklejanie dwóch pozycji przy dopisaniu do pliku bez końcowego `\n` („...2021;7” + „C;2022;8” → jedna linia). Po poprawce `EnsureTrailingNewline()` dopisuje brakujący znak nowej linii.
- BOM usunięty z `Program.cs`, `katalog.csproj`, `app.manifest`; drzewo plików jest teraz w UTF-8 bez BOM.
- Nadal niesprawdzone w działaniu: kliknięcia w GUI (brak automatyzacji) — sprawdzone jest uruchomienie okna oraz cała logika danych i walidacji poza UI.
- Następnym razem: testować zachowanie formularza i przycisku Policz po każdej zmianie ekranu, nie tylko czytać diff.

## 2026-10-22 — wpis wsteczny: katalog

- Przyjęte bez sprawdzenia (2026-10-08, commity `3805ffc`–`c4b328b`): formularz dodawania i przycisk „Policz”. Czytałem diffy, ale nie wpisałem do formularza oceny, która nie jest liczbą, i nie sprawdziłem, skąd aplikacja czyta `dane/pozycje.txt` po uruchomieniu z `bin/`.
- Sprawdzone dzisiaj: `dotnet build` → **1 błąd**, `NETSDK1045`. Zainstalowany jest tylko SDK 9.0.311, a projekt celuje w `net10.0`; w tym środowisku nie zbuduję projektu ani nie uruchomię okna.
- Sprawdzone dzisiaj na kopii walidacji z `AddScreen.axaml.cs`: `IsRatingValid("abc")` → **true** i `IsRatingValid("99")` → **true**, bo metoda sprawdza tylko, czy pole nie jest puste i nie zawiera średnika. Wpis „Tytuł;2020;abc” przechodzi walidację i trafia do pliku jako ocena.
- Sprawdzone dzisiaj na kopii odczytu: `File.ReadAllLines` zwraca też puste linie, a `ReadLineCount()` bierze `lines.Length` — pusta linia liczy się jako pozycja i pokaże się jako pusty wiersz na liście.
- Zauważone w kodzie, nie sprawdzone w działaniu: `"dane/pozycje.txt"` to ścieżka względna do katalogu roboczego, a plik nie jest kopiowany do `bin/`; uruchomienie `.exe` z `bin/Debug/net10.0` nie znajdzie danych.
- Następnym razem: przed commitem wpisać do każdego pola wartość spoza formatu (ocena „abc”, rok „0”), uruchomić exe z `bin/` i sprawdzić plik po zapisie.

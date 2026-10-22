# Dziennik weryfikacji

Wpisy od najnowszego. Zapisuję wynik sprawdzenia, nie wrażenie.

## 2026-10-22 — wpis wsteczny: katalog

- Przyjęte bez sprawdzenia (2026-10-08, commity `3805ffc`–`c4b328b`): formularz dodawania i przycisk „Policz”. Czytałem diffy, ale nie wpisałem do formularza oceny, która nie jest liczbą, i nie sprawdziłem, skąd aplikacja czyta `dane/pozycje.txt` po uruchomieniu z `bin/`.
- Sprawdzone dzisiaj: `dotnet build` → **1 błąd**, `NETSDK1045`. Zainstalowany jest tylko SDK 9.0.311, a projekt celuje w `net10.0`; w tym środowisku nie zbuduję projektu ani nie uruchomię okna.
- Sprawdzone dzisiaj na kopii walidacji z `AddScreen.axaml.cs`: `IsRatingValid("abc")` → **true** i `IsRatingValid("99")` → **true**, bo metoda sprawdza tylko, czy pole nie jest puste i nie zawiera średnika. Wpis „Tytuł;2020;abc” przechodzi walidację i trafia do pliku jako ocena.
- Sprawdzone dzisiaj na kopii odczytu: `File.ReadAllLines` zwraca też puste linie, a `ReadLineCount()` bierze `lines.Length` — pusta linia liczy się jako pozycja i pokaże się jako pusty wiersz na liście.
- Zauważone w kodzie, nie sprawdzone w działaniu: `"dane/pozycje.txt"` to ścieżka względna do katalogu roboczego, a plik nie jest kopiowany do `bin/`; uruchomienie `.exe` z `bin/Debug/net10.0` nie znajdzie danych.
- Następnym razem: przed commitem wpisać do każdego pola wartość spoza formatu (ocena „abc”, rok „0”), uruchomić exe z `bin/` i sprawdzić plik po zapisie.

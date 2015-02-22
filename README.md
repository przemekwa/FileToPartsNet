# FileToPartsNet
Program konsolowy do dzielenia plików tekstowych na mniejsze części.

###Szybki start:

Program działa z lini komend. Program dzieli pliki tekstowe na części, jeśli znajdzie plik, który jest większy niż podany rozmiar. 

Wywołuje się go poprzez komendę FileToParts.exe podając 3 parametry:

* Katalog, gdzie znajdują się pliki.
* Maksymalny rozmiar pliku w bajtach.
* Wyrażenie regularne, które filtruje pliki z katalogu.

Naprzykład:

Komenda: FileToParts.exe c:\mojePliki 30000 *.txt

Ta komenda w katalogu c:\mojePliki podzieli pliki txt, które są większe niż 30000 bajtów  na części nie większe niż 30000 bajtów.

Program przepisuje również nagłówki plików. Więc każdy podzielony plik, będzie zawierał nagłówek z pliku źródłowego.




/// <reference path="../jquery-2.1.1.min.js" />
/// <reference path="../jquery-2.1.0-vsdoc.js" />
function rokPrzestepny(rok) {
    return ((rok % 4 == 0) && ((rok % 100 != 0) || (rok % 400 == 0)));
  }

function daneMiesiaca(miesiac, wartosc) {
  switch(miesiac){
      case 1 : nazwa = "Styczeń";
               dlugosc = 31;
               break;
      case 2 : nazwa = "Luty";
               dlugosc = rokPrzestepny(rok)?29:28;
               break;
      case 3 : nazwa = "Marzec";
               dlugosc = 31;
               break;
      case 4 : nazwa = "Kwiecień";
               dlugosc = 30;
               break;
      case 5 : nazwa = "Maj";
               dlugosc = 31;
               break;
      case 6 : nazwa = "Czerwiec";
               dlugosc = 30;
               break;
      case 7 : nazwa = "Lipiec";
               dlugosc = 31;
               break;
      case 8 : nazwa = "Sierpień";
               dlugosc = 31;
               break;
      case 9 : nazwa = "Wrzesień";
               dlugosc = 30;
               break;
      case 10 : nazwa = "Październik";
               dlugosc = 31;
               break;
      case 11 : nazwa = "Listopad";
               dlugosc = 30;
               break;
      case 12 : nazwa = "Grudzień";
               dlugosc = 31;
               break;
  }
  if(wartosc == true)
    return nazwa;
  else if(wartosc == false)
    return dlugosc;
}
  
function printCalendar() {
  data = new Date();

  miesiac = data.getMonth() + 1 - al;
  rok = data.getYear();

  if(miesiac < 0) {
    miesiac = miesiac + 13;
    rok = rok - 1;
  } else if (miesiac > 12) {
    miesiac = miesiac - 12;
    rok = rok + 1;
  }

  if (rok < 1000) rok = 2000 + rok - 100;

  dzienTygodnia = data.getDay();
  var dzienMiesiaca = data.getDate();

  var tempDate = new Date(rok, miesiac - 1, 1);
  var pierwszyDzienMiesiaca = tempDate.getDay();

  

  if(dzienTygodnia == 0) dzienTygodnia = 7;
  if(pierwszyDzienMiesiaca == 0) pierwszyDzienMiesiaca = 7;

  switch(dzienTygodnia){
    case 1 : nazwaDniaTygodnia = "poniedziałek";
             break;
    case 2 : nazwaDniaTygodnia = "wtorek";
             break;
    case 3 : nazwaDniaTygodnia = "środa";
             break;
    case 4 : nazwaDniaTygodnia = "czwartek";
             break;
    case 5 : nazwaDniaTygodnia = "piątek";
             break;
    case 6 : nazwaDniaTygodnia = "sobota";
             break;
    case 7 : nazwaDniaTygodnia = "niedziela";
             break;
  }

  var nazwaMiesiaca = daneMiesiaca(miesiac, true);
  var dniWMiesiacu = daneMiesiaca(miesiac, false);
  var dniWPoprzednimMiesiacu = daneMiesiaca(miesiac - 1, false);

  var text = "<span class='dzien-miesiaca'>" + dzienMiesiaca + "</span>";
  text += "<span class='dzien-tygodnia'>" + nazwaDniaTygodnia + "</span>";
  text += "<div id='miesiac'><span id='wstecz' onclick='wstecz()'></span>";
  text += nazwaMiesiaca + " " + rok;
  text += "<span id='dalej' onclick='dalej()'></span></div><table><thead><tr>";

  text += "<tr>";
  text += "<th>PON</th>";
  text += "<th>WTO</th>";
  text += "<th>ŚR</th>";
  text += "<th>CZW</th>";
  text += "<th>PT</th>";
  text += "<th>SO</th>";
  text += "<th>NDZ</th>";
  text += "</tr></thead>";

  var j = dniWMiesiacu + pierwszyDzienMiesiaca - 1;

  for(var i = 0; i < j; i++){
    if(i < pierwszyDzienMiesiaca - 1){
      dzienZPoprzedniegoMiesiaca = dniWPoprzednimMiesiacu - (pierwszyDzienMiesiaca - 2 - i);
      text += "<td class='poprzedni-miesiac'>" + dzienZPoprzedniegoMiesiaca + "</td>";
      continue;
    }
    if((i % 7) == 0){
      text += "</tr><tr>";
    }
    if((i - pierwszyDzienMiesiaca + 2) == dzienMiesiaca){
      if(al == 0) dzien = "dzis";
    }
    else{
      dzien = "";
    }
    text += "<td class='" + dzien + "' onclick=ustawDzien('";
    text += rok;
    text += "/" + miesiac;
    text += "/" + (i - pierwszyDzienMiesiaca + 2);
    text += "')>";
    text += i - pierwszyDzienMiesiaca + 2;
    text += "</td>";
  }
  text += "</tr></table>";
  var a = document.getElementById('dodaj');
    /* var dodaj = "<a href='" + a.href + "' id='dodaj'>Dodaj zadanie</a>";*/
  var dodaj = "<input type='hidden' id='dzien' name='dzien'><a id='dodaj' class='transition' data-ajax='true' data-ajax-method='Get' data-ajax-mode='replace' data-ajax-update='#data' href='/Dashboard/AddTask?Length=9'>Dodaj zadanie</a>";
    //var dodaj = "<input type='hidden' id='dzien' name='dzien'><input type='submit' id='dodaj' value='Dodaj zadanie' class='transition'>";
if (kalendarz != null)
    kalendarz.innerHTML = text + dodaj;
}

function wstecz() {
  al++;
  printCalendar();
}

function dalej() {
  al--;
  printCalendar();
}

function ustawDzien(dzien) {
    document.getElementById('dzien').value = dzien;
    $("#dodaj").attr("href", "/Dashboard/AddTask?data=" + dzien);
    $("#dodaj").click();
}
'use strict'

/** GLOBALS */

var endpoint = createEndpoint("http://87.253.157.240");

/** UTILITIES */

function getRandomInt(min, max) {
  return Math.floor(Math.random() * (max - min)) + min;
}

/** TABLE UPDATE LOGIC*/

function changeFullTable(content){
  $("#dataTable").html(content);
}
function changeTableHead(content){
  $("#tableHead").html(content);
}
function changeTableBody(content){
  $("#tableBody").html(content);
}

function createSpinnerRow(){
  return "<tr>"+
    "<td>"+
      '<div class="preloader-wrapper small active">'+
        '<div class="spinner-layer spinner-green-only">'+
          '<div class="circle-clipper left">'+
          '  <div class="circle"></div>'+
          '</div><div class="gap-patch">'+
            '<div class="circle"></div>'+
          '</div><div class="circle-clipper right">'+
            '<div class="circle"></div>'+
          '</div>'+
        '</div>'+
      '</div>'+
    '</td>'+
  '</tr>'
}


/**EVENT DISPLAY LOGIC*/

function createEventHeader(){
  return "<tr>"+
    "<th>Afkorting</th>"+
    "<th>Naam</th>"+
    "<th>Organisator</th>"+
    "<th>Startdatum<th>"+
  "<tr>"
}

function createEventActions(event){
  $("#details").click(function(){
    updateModalBody(createViewEventModalBody(event))
    updateModalFooter(createSaveButton())
  });
    return '<a class="waves-effect waves-light btn" id="details" href="event.html?id='+event.id+'">view</a>'
}

function createEventRow(event){
    return "<tr> "+
      "<td>"+event.afkorting+"</td>"+
      "<td>"+event.naam+"</td>"+
      "<td>"+event.organisator.naam+"</td>"+
      "<td>"+event.startdatum+"</td>"+
      "<td>"+ createEventActions(event)+ "</td>"
      "</tr>"
}

function showEventList(){
  var events = endpoint.fetchEvents();
  var eventsHtml = events.map(createEventRow);
  changeTableHead(createEventHeader());
  changeTableBody(eventsHtml);
  $('.modal-trigger').leanModal();
}

/** ENDPOINT CREATION */

function createEndpoint(baseUrl){
  var post = function(uri, data, handler){
    $.post(baseUrl+uri, data , handler);
  }
  var get = function(uri, handler){
    $.get(baseUrl+uri, {}, handler);
  }
  return {
    fetchEvents: function(){
      return [
        {
          id:1,
          afkorting: "HEAD",
          naam: "Head of the river Amstel",
          organisator: {
            afkorting: "WIL",
            naam: "Willem III",
            ploegen:[]
          },
          startdatum: "1-1-2020",
          einddatum: "2-2-2020",
          afstand: 8000,
          plaats: "Amstel, Amsterdam",
          blokken: [{},{},{}]
        },
        {
          id:1
          afkorting: "HEAD",
          naam: "Head of the river Amstel",
          organisator: {
            afkorting: "WIL",
            naam: "Willem III",
            ploegen:[]
          },
          startdatum: "1-1-2020",
          einddatum: "2-2-2020",
          afstand: 8000,
          plaats: "Amstel, Amsterdam",
          blokken: [{},{},{}]
        }
      ]
    },
    updateEvent: function(id, newEvent){

    }
  }
}

/** STARTUP FUNCTION*/

$(document).ready(function(){
  setTimeout(showEventList, getRandomInt(300,2000));
})

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
    "<th>Startdatum</th>"+
    "<th><a class='waves-effect waves-light btn blue' id='details' href='event.html?type=NEW&id="+event.Id+"'>new</a></th>"+
  "<tr>"
}

function createEventActions(event){
  return '<a class="waves-effect waves-light btn" id="details" href="event.html?type=VIEW&id='+event.Id+'">view</a>'+
  '<a class="waves-effect waves-light btn red" id="details" href="event.html?type=DELETE&id='+event.Id+'">delete</a>'
}

function createEventRow(event){
    return "<tr> "+
      "<td>"+event.Afkorting+"</td>"+
      "<td>"+event.Naam+"</td>"+
      "<td>"+event.Organisator.Naam+"</td>"+
      "<td>"+event.StartDatum.split('T')[0]+"</td>"+
      "<td>"+ createEventActions(event)+ "</td>"
      "</tr>"
}

function showEventList(events){
  events = [].concat(events)
  console.dir(events);
  var eventsHtml = events.map(createEventRow);
  changeTableHead(createEventHeader());
  changeTableBody(eventsHtml);
}

/** ENDPOINT CREATION */

var toPromise = function ($promise) {
  return new Promise(function (resolve, reject) {
    $promise.then(resolve, reject);
  });
}

function createEndpoint(baseUrl){
  var get = function(uri, handler){
    return toPromise(
      $.get(baseUrl+uri, {}, function(data){
        handler(JSON.parse(data.Data));
      }, 'json')
    )
  }
  return {
    fetchEvents: function(token, handler){
      var token = "4004d869-f629-4acd-8360-2475d7270fce"
      return get("/event/all/user/"+token, handler);
    },
    fetchClub: function(id, token, handler){
      var token = "4004d869-f629-4acd-8360-2475d7270fce";
      return get("/vereniging/"+id+"/"+token, handler);
    }
  }
}

/** STARTUP FUNCTION*/

$(document).ready(function(){
  endpoint.fetchEvents("token", function(events){
    var promises = [];
    events.forEach(function(event){
      promises.push(endpoint.fetchClub(event.OrganisatorID, "token", function(res){return res}))
    });
    Promise.all(promises).then(function(){
      var parsed = arguments[0].map(function(response){
        return JSON.parse(response.Data);
      });
      showEventList(
        events.map(function(event){
          event.Organisator = parsed.find(function(club){
            return club.Id === event.OrganisatorID;
          });
          return event;
        })
      );
    });
  });
})

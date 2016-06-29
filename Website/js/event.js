'use strict'

/** GLOBALS */

var endpoint = createEndpoint("http://87.253.157.240");

/** UTILITIES */

function getRandomInt(min, max) {
  return Math.floor(Math.random() * (max - min)) + min;
}

/** TABLE UPDATE LOGIC*/

function changeBody(content){
  $("#event").html(content);
}

function createSpinner(){
  return '<div class="preloader-wrapper small active">'+
        '<div class="spinner-layer spinner-green-only">'+
          '<div class="circle-clipper left">'+
          '  <div class="circle"></div>'+
          '</div><div class="gap-patch">'+
            '<div class="circle"></div>'+
          '</div><div class="circle-clipper right">'+
            '<div class="circle"></div>'+
          '</div>'+
        '</div>'+
      '</div>'
}


/**EVENT LIST DISPLAY LOGIC*/

function createEventHeader(event){
  return "<h1>"+event.Naam+"</h1>"
}

function createEventActions(event){
  return '<a class="waves-effect waves-light btn" id="details" href="event.html?type=EDIT&id='+event.Id+'">edit</a>'+
  '<a class="waves-effect waves-light btn red" id="details" href="event.html?type=DELETE&id='+event.Id+'">delete</a>'
}

function createEventHTML(event){
    return "<div> <b>Afkorting:</b> "+event.Afkorting+"</div>"+
      "<div> <b>Organisator:</b> "+event.OrganisatorID+"</div>"+
      "<div> <b>Startdatum:</b> "+event.StartDatum.split('T')[0]+"</div>"+
      "<div>"+createEventActions(event)+"</div>"
}

function showEvent(event){
  changeBody(createEventHeader(event)+
  createEventHTML(event));
}

/** ENDPOINT CREATION */

function createEndpoint(baseUrl){
  var get = function(uri, handler){
    $.get(baseUrl+uri, {}, function(data){
      handler(JSON.parse(data.Data));
    }, 'json');
  }
  var post = function(uri, data, handler){
    $.post(baseUrl+uri, data, function(data){
      handler(JSON.parse(data.Data));
    }, 'json');
  }
  var remove = function(uri, data, handler){
    $.ajax({
      url: baseUrl+uri,
      type: 'DELETE',
      dataType: 'json',
      success: function(data){
        handler(JSON.parse(data.Data))
      }
    });
  }
  return {
    fetchEvent: function(id, token, handler){
      var token = "4004d869-f629-4acd-8360-2475d7270fce"
      return get("/event/"+id+"/"+token, handler);
    },
    fetchClub: function(id, token, handler){
      var token = "4004d869-f629-4acd-8360-2475d7270fce";
      return get("/vereniging/"+id+"/"+token, handler);
    },
    deleteEvent: function(id, token, handler){
      var token = "4004d869-f629-4acd-8360-2475d7270fce";
      return remove("/event/"+id+"/"+token, handler);
    }
  }
}

/** STARTUP FUNCTION*/

$(document).ready(function(){
  var params = {}
  location.search.substr(1).split("&").forEach(function(item) {params[item.split("=")[0]] = item.split("=")[1]})
  console.dir(params);
  if(params.type==="VIEW"){
    endpoint.fetchEvent(1, "token", showEvent);
  }else if(params.type==="DELETE"){
    endpoint.deleteEvent(1, "token", showEvent);
  }else if(params.type==="CREATE"){
    endpoint.fetchEvent(1, "token", showEvent);
  }else if(params.type==="EDIT"){
    endpoint.fetchEvent(1, "token", showEvent);
  }
})

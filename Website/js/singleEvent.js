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

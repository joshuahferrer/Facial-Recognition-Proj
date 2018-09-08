navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia ||
navigator.mozGetUserMedia || navigator.msGetUserMedia || navigator.oGetUserMedia;

if (navigator.getUserMedia){
   navigator.getUserMedia({video: true}, handleVideo, videoError);
}
function handleVideo(stream)
{
  document.querySelector('#vidDisplay').src = window.URL.createObjectURL(stream);
}

function videoError(e)
{
  alert("some problem is going on");
}

document.write("hello");

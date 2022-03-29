function scrollIntoView(elementId) {
  var elem = document.getElementById(elementId);
  if (elem) {
    elem.scrollIntoView(
    {
        block: 'start',
        // behavior: 'smooth',
    });
    // window.location.hash = elementId;
  }
}
function scrollToEnd() {
window.scrollTo(0, document.body.scrollHeight);
}

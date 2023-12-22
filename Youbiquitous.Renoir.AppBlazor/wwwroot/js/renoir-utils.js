

/////////////////////////////////////////////////////////////////////
// 
// Post the login form
// 
function postForm(formId) {
    var form = document.querySelector(formId);
    var url = form.getAttribute("action");
    var method = form.getAttribute("method");
    var formData = new FormData(form);

    var request = new XMLHttpRequest();
    request.open(method, url, false);  // False makes the request synchronous
    try {
        request.send(formData);
        return request.responseText;
    }
    catch(e) {
        return { success: false, message: 'Network error' };
    }
}
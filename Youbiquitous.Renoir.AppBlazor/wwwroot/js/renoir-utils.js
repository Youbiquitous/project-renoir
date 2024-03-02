

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

/////////////////////////////////////////////////////////////////////
// 
// Copy value of DOM element to the clipboard 
// 
window.clipboardCopy = {
    copyText: function (sourceElementId, feedbackElementId, msg) {
        var element = document.getElementById(sourceElementId);
        var content = element.value;
        navigator.clipboard.writeText(content)
            .then(function() {
                var feedback = document.getElementById(feedbackElementId);
                feedback.innerHTML = msg;
                setTimeout(() => {
                        feedback.innerHTML = "";
                    },
                    2500);
            })
            .catch(function(error) {
                alert(error);
            });
    }
}
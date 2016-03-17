function hideHeader() {
    var elem = document.getElementById('headerLinks');
    elem.style.visibility = 'hidden';

    var elem1 = document.getElementById('homePageLink');
    elem1.removeAttribute('href');

    var elem2 = document.getElementById('menu-toggle-2');
    elem2.style.visibility = 'hidden';
}

function showRegisterForm() {

    var elem = document.getElementById('showRegistrationForm');
    elem.style.display = 'block';

    var elemUserName = document.getElementById('Username');
    elemUserName.disabled = true;

    var elemPassword = document.getElementById('Password');
    elemPassword.disabled = true;

    var elemLoginButton = document.getElementById('loginButton');
    elemLoginButton.disabled = true;

}

   

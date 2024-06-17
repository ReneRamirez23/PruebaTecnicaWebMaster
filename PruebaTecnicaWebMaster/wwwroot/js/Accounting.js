function showTab(tabId) {
    var contents = document.querySelectorAll('.tab-content');
    contents.forEach(function (content) {
        content.classList.remove('active');
    });

    var tabs = document.querySelectorAll('.tab');
    tabs.forEach(function (tab) {
        tab.classList.remove('active');
    });

    document.getElementById(tabId).classList.add('active');

    event.currentTarget.classList.add('active');
}
